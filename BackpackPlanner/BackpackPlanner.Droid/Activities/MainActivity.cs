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
using EnergonSoftware.BackpackPlanner.Units.Currency;
using EnergonSoftware.BackpackPlanner.Units.Units;

namespace EnergonSoftware.BackpackPlanner.Droid.Activities
{
	[Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon")]
    [MetaData("com.google.android.gms.version", Value = "@integer/google_play_services_version")]
	public sealed class MainActivity : Android.Support.V7.App.AppCompatActivity
	{
        public const string LogTag = "BackpackPlanner.Droid";

        private const string HockeyAppAppId = "32a2c37622529305ec763b7e2c224deb";

        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(MainActivity));

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

            BackpackPlannerState.Instance.SystemLogger = new DroidLogger();

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

            // TODO: this needs a platform callback so we can save-on-set settings
            ISharedPreferencesEditor sharedPreferencesEditor = PreferenceManager.GetDefaultSharedPreferences(this).Edit();
            BackpackPlannerState.Instance.Settings.FirstRun = false;
            sharedPreferencesEditor.PutString(BackpackPlannerSettings.FirstRunPreferenceKey, BackpackPlannerState.Instance.Settings.FirstRun.ToString());
            sharedPreferencesEditor.Commit();

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

            try {
                string scratch = sharedPreferences.GetString(BackpackPlannerSettings.FirstRunPreferenceKey,
                    BackpackPlannerState.Instance.Settings.FirstRun.ToString());
                BackpackPlannerState.Instance.Settings.FirstRun = Convert.ToBoolean(scratch);
            } catch(FormatException) {
                // it's k, we'll live
            }

            // NOTE: have to read these settings first so we know how to interpret everything else
            try {
                string scratch = sharedPreferences.GetString(BackpackPlannerSettings.UnitSystemPreferenceKey,
                    BackpackPlannerState.Instance.Settings.Units.ToString());
                BackpackPlannerState.Instance.Settings.Units = (UnitSystem)Convert.ToInt32(scratch);
            } catch(FormatException) {
                // it's k, we'll live
            }

            try {
                string scratch = sharedPreferences.GetString(BackpackPlannerSettings.CurrencyPreferenceKey,
                    BackpackPlannerState.Instance.Settings.Currency.ToString());
                BackpackPlannerState.Instance.Settings.Currency = (Currency)Convert.ToInt32(scratch);
            } catch(FormatException) {
                // it's k, we'll live
            }

            BackpackPlannerState.Instance.PersonalInformation.Name = sharedPreferences.GetString(PersonalInformation.NamePreferenceKey,
                BackpackPlannerState.Instance.PersonalInformation.Name);

            try {
                string scratch = sharedPreferences.GetString(PersonalInformation.DateOfBirthPreferenceKey,
                    BackpackPlannerState.Instance.PersonalInformation.DateOfBirth?.ToString() ?? string.Empty);
                if(!string.IsNullOrWhiteSpace(scratch)) {
                    BackpackPlannerState.Instance.PersonalInformation.DateOfBirth = Convert.ToDateTime(scratch);
                }
            } catch(FormatException) {
                // it's k, we'll live
            }

            try {
                string scratch = sharedPreferences.GetString(PersonalInformation.UserSexPreferenceKey,
                    BackpackPlannerState.Instance.PersonalInformation.Sex.ToString());
                BackpackPlannerState.Instance.PersonalInformation.Sex = (UserSex)Convert.ToInt32(scratch);
            } catch(FormatException) {
                // it's k, we'll live
            }

            try {
                string scratch = sharedPreferences.GetString(PersonalInformation.HeightPreferenceKey,
                    BackpackPlannerState.Instance.PersonalInformation.HeightInUnits.ToString(CultureInfo.InvariantCulture));
                BackpackPlannerState.Instance.PersonalInformation.HeightInUnits = Convert.ToInt32(scratch);
            } catch(FormatException) {
                // it's k, we'll live
            }

            try {
                string scratch = sharedPreferences.GetString(PersonalInformation.WeightPreferenceKey,
                    BackpackPlannerState.Instance.PersonalInformation.WeightInUnits.ToString(CultureInfo.InvariantCulture));
                BackpackPlannerState.Instance.PersonalInformation.WeightInUnits = Convert.ToInt32(scratch);
            } catch(FormatException) {
                // it's k, we'll live
            }
        }
	}
}
