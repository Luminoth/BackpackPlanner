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
using System.Threading.Tasks;

using Android.App;
using Android.OS;
using Android.Preferences;
using Android.Runtime;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Droid.Logging;
using EnergonSoftware.BackpackPlanner.Droid.Util;

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

// TODO: still crashing somewhere before InitHockyApp() is called
// but it goes away when a debugger is attached

            // NOTE: this is happening *before* we init HockeyApp
            // so any exceptions here will go un-uploaded
            BackpackPlannerState.Instance.InitPlatform(
                new DroidLogger(),
                new GooglePlayServicesManager(),
                new SQLitePlatformAndroid(),
                (sender, args) => {
                    SettingsUtil.SaveToSharedPreferences(PreferenceManager.GetDefaultSharedPreferences(this), args.PreferenceKey);
                }
            );

            InitHockeyApp();

			SetContentView(Resource.Layout.activity_main);

            InitToolbar();

            Title = Resources.GetString(Resource.String.app_name);
		}

	    protected override void OnResume()
	    {
	        base.OnResume();

            LoadPreferences();

            if(BackpackPlannerState.Instance.Settings.MetaSettings.FirstRun) {
                Logger.Debug("Starting FTUE...");
                StartActivity(typeof(FTUEActivity));
            } else {
                Logger.Debug("Starting main activity...");
                StartActivity(typeof(BackpackPlannerActivity));
            }
            BackpackPlannerState.Instance.Settings.MetaSettings.FirstRun = false;

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
            SettingsUtil.UpdateFromSharedPreferences(PreferenceManager.GetDefaultSharedPreferences(this), null);
        }
	}
}
