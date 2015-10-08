/*
   Copyright 2015 Shane Lillie

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using System;
using System.Globalization;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Droid.Logging;
using EnergonSoftware.BackpackPlanner.Models.Personal;
using EnergonSoftware.BackpackPlanner.Settings;
using EnergonSoftware.BackpackPlanner.Units.Currency;
using EnergonSoftware.BackpackPlanner.Units.Units;

using SQLite.Net.Platform.XamarinAndroid;

namespace EnergonSoftware.BackpackPlanner.Droid.Activities
{
	[Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon")]
    [MetaData("com.google.android.gms.version", Value = "@integer/google_play_services_version")]
	public sealed class MainActivity : BaseActivity
	{
        public const string LogTag = "BackpackPlanner.Droid";

        private const string HockeyAppAppId = "32a2c37622529305ec763b7e2c224deb";

        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(MainActivity));

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

            // NOTE: this is happening *before* we init HockeyApp
            // so any exceptions here will go un-uploaded
            BackpackPlannerState.Instance.InitPlatform(new DroidLogger(), new SQLitePlatformAndroid(),
                (sender, args) => {
                    Logger.Debug($"Setting changed: {args.PreferenceKey}");

                    ISharedPreferencesEditor sharedPreferencesEditor = PreferenceManager.GetDefaultSharedPreferences(this).Edit();
                    switch(args.PreferenceKey)
                    {
                    case PersonalInformation.NamePreferenceKey:
                        sharedPreferencesEditor.PutString(args.PreferenceKey, BackpackPlannerState.Instance.PersonalInformation.Name);
                        break;
                    case PersonalInformation.DateOfBirthPreferenceKey:
                        sharedPreferencesEditor.PutString(args.PreferenceKey, BackpackPlannerState.Instance.PersonalInformation.DateOfBirth?.ToString() ?? string.Empty);
                        break;
                    case PersonalInformation.UserSexPreferenceKey:
                        sharedPreferencesEditor.PutString(args.PreferenceKey, BackpackPlannerState.Instance.PersonalInformation.Sex.ToString());
                        break;
                    case PersonalInformation.HeightPreferenceKey:
                        sharedPreferencesEditor.PutFloat(args.PreferenceKey, (float)BackpackPlannerState.Instance.PersonalInformation.HeightInUnits);
                        break;
                    case PersonalInformation.WeightPreferenceKey:
                        sharedPreferencesEditor.PutFloat(args.PreferenceKey, (float)BackpackPlannerState.Instance.PersonalInformation.WeightInUnits);
                        break;
                    case BackpackPlannerSettings.FirstRunPreferenceKey:
                        sharedPreferencesEditor.PutString(args.PreferenceKey, BackpackPlannerState.Instance.Settings.FirstRun.ToString());
                        break;
                    case BackpackPlannerSettings.UnitSystemPreferenceKey:
                        sharedPreferencesEditor.PutString(args.PreferenceKey, BackpackPlannerState.Instance.Settings.Units.ToString());
                        break;
                    case BackpackPlannerSettings.CurrencyPreferenceKey:
                        sharedPreferencesEditor.PutString(args.PreferenceKey, BackpackPlannerState.Instance.Settings.Currency.ToString());
                        break;
                    default:
                        Logger.Warn("Unhandled preference key: " + args.PreferenceKey);
                        break;
                    }
                    sharedPreferencesEditor.Commit();
                }
            );

            InitHockeyApp();

			SetContentView(Resource.Layout.activity_main);
		}

	    protected override void OnResume()
	    {
	        base.OnResume();

            LoadPreferences();

            if(BackpackPlannerState.Instance.Settings.FirstRun) {
                Logger.Debug("First run, starting FTUE...");
                StartActivity(typeof(FTUEActivity));
            } else {
                Logger.Debug("Not first run, starting main activity...");
                StartActivity(typeof(BackpackPlannerActivity));
            }
            BackpackPlannerState.Instance.Settings.FirstRun = false;

            // this ensures that we never come back to this activity
            Finish();
	    }

	    private void InitHockeyApp()
        {
            Logger.Info("Initializing HockeyApp...");

            // Register the crash manager before Initializing the trace writer
            HockeyApp.CrashManager.Register(this, HockeyAppAppId); 

            // Register to with the Update Manager
            HockeyApp.UpdateManager.Register(this, HockeyAppAppId);

            // Register the Feedback Manager
            HockeyApp.FeedbackManager.Register(this, HockeyAppAppId);

            // Initialize the Trace Writer
            HockeyApp.TraceWriter.Initialize();

            // Wire up Unhandled Expcetion handler from Android
            AndroidEnvironment.UnhandledExceptionRaiser += (sender, args) => {
                // Use the trace writer to log exceptions so HockeyApp finds them
                HockeyApp.TraceWriter.WriteTrace(args.Exception);
                args.Handled = true;
            };

            // Wire up the .NET Unhandled Exception handler
            AppDomain.CurrentDomain.UnhandledException += (sender, args) => HockeyApp.TraceWriter.WriteTrace(args.ExceptionObject);

            // Wire up the unobserved task exception handler
            TaskScheduler.UnobservedTaskException += (sender, args) => HockeyApp.TraceWriter.WriteTrace(args.Exception);
        }

        private void LoadPreferences()
        {
            Logger.Debug("Setting default preferences...");
            PreferenceManager.SetDefaultValues(this, Resource.Xml.settings, false);

            Logger.Debug("Loading preferences...");
            ISharedPreferences sharedPreferences = PreferenceManager.GetDefaultSharedPreferences(this);

            BackpackPlannerState.Instance.Settings.FirstRun = Convert.ToBoolean(sharedPreferences.GetString(
                BackpackPlannerSettings.FirstRunPreferenceKey,
                BackpackPlannerState.Instance.Settings.FirstRun.ToString()));

            // NOTE: have to read these settings first so we know how to interpret everything else

            string unitSystemPreference = sharedPreferences.GetString(
                BackpackPlannerSettings.UnitSystemPreferenceKey,
                BackpackPlannerState.Instance.Settings.Units.ToString());

            UnitSystem unitSystem;
            if(Enum.TryParse(unitSystemPreference, out unitSystem)) {
                BackpackPlannerState.Instance.Settings.Units = unitSystem;
            } else {
                Logger.Error("Error parsing unit system preference!");
            }

            string currencyPreference = sharedPreferences.GetString(
                BackpackPlannerSettings.CurrencyPreferenceKey,
                BackpackPlannerState.Instance.Settings.Currency.ToString());

            Currency currency;
            if(Enum.TryParse(currencyPreference, out currency)) {
                BackpackPlannerState.Instance.Settings.Currency = currency;
            } else {
                Logger.Error("Error parsing currency preference!");
            }

            BackpackPlannerState.Instance.PersonalInformation.Name = sharedPreferences.GetString(
                PersonalInformation.NamePreferenceKey,
                BackpackPlannerState.Instance.PersonalInformation.Name);

            try {
                string birthDatePreference = sharedPreferences.GetString(
                    PersonalInformation.DateOfBirthPreferenceKey,
                    BackpackPlannerState.Instance.PersonalInformation.DateOfBirth?.ToString() ?? string.Empty);
                if(!string.IsNullOrWhiteSpace(birthDatePreference)) {
                    BackpackPlannerState.Instance.PersonalInformation.DateOfBirth = Convert.ToDateTime(birthDatePreference);
                }
            } catch(FormatException) {
                Logger.Error("Error parsing date of birth preference!");
            }

            string userSexPreference = sharedPreferences.GetString(
                PersonalInformation.UserSexPreferenceKey,
                BackpackPlannerState.Instance.PersonalInformation.Sex.ToString());

            UserSex userSex;
            if(Enum.TryParse(userSexPreference, out userSex)) {
                BackpackPlannerState.Instance.PersonalInformation.Sex = userSex;
            } else {
                Logger.Error("Error parsing user sex preference!");
            }

            BackpackPlannerState.Instance.PersonalInformation.HeightInUnits = Convert.ToSingle(sharedPreferences.GetString(
                PersonalInformation.HeightPreferenceKey,
                BackpackPlannerState.Instance.PersonalInformation.HeightInUnits.ToString(CultureInfo.InvariantCulture)));

            BackpackPlannerState.Instance.PersonalInformation.WeightInUnits = Convert.ToSingle(sharedPreferences.GetString(
                PersonalInformation.WeightPreferenceKey,
                BackpackPlannerState.Instance.PersonalInformation.WeightInUnits.ToString(CultureInfo.InvariantCulture)));
        }
	}
}
