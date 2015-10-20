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

using System.Diagnostics;

using Android.App;
using Android.Content;
using Android.OS;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Droid.Logging;

using SQLite.Net.Platform.XamarinAndroid;

namespace EnergonSoftware.BackpackPlanner.Droid.Activities
{
    public class BaseActivity : Android.Support.V7.App.AppCompatActivity
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(BaseActivity));

        public BackpackPlannerState BackpackPlannerState { get; private set; }

        private readonly Stopwatch _startupStopwatch = new Stopwatch();

#region Controls
        public Android.Support.V7.Widget.Toolbar Toolbar { get; private set; }
#endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _startupStopwatch.Start();

            CustomLogger.PlatformLogger = new DroidLogger();

            BackpackPlannerState = new BackpackPlannerState(
                new HockeyAppManager(this),
                new DroidSettingsManager(Android.Support.V7.Preferences.PreferenceManager.GetDefaultSharedPreferences(this)),
                new PlayServicesManager(this),
                new SQLitePlatformAndroid()
            );
            BackpackPlannerState.InitAsync().Wait();

            ((PlayServicesManager)BackpackPlannerState.PlatformPlayServicesManager).OnCreate(savedInstanceState);
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);

            ((PlayServicesManager)BackpackPlannerState.PlatformPlayServicesManager).OnSaveInstanceState(outState);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            BackpackPlannerState.DestroyAsync().Wait();
        }

        protected override void OnStart()
        {
            base.OnStart();

            Logger.Debug($"OnStart - {GetType()}");

            if(_startupStopwatch.IsRunning) {
                Logger.Debug($"Time to Activity.Start(): {_startupStopwatch.ElapsedMilliseconds}ms");
            }
        }

        protected override void OnStop()
        {
            base.OnStop();

            Logger.Debug($"OnStop - {GetType()}");

            BackpackPlannerState.PlatformPlayServicesManager.DisconnectAsync().Wait();
        }

        protected override void OnResume()
        {
            base.OnResume();

            Logger.Debug($"OnResume - {GetType()}");

            LoadPreferences();

            if(_startupStopwatch.IsRunning) {
                Logger.Debug($"Time to Activity.OnResume() finish: {_startupStopwatch.ElapsedMilliseconds}ms");
            }
            _startupStopwatch.Stop();
        }

        protected override void OnPause()
        {
            base.OnPause();

            Logger.Debug($"OnPause - {GetType()}");

            BackpackPlannerState.DatabaseState.DisconnectAsync().Wait();
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
	    {
            base.OnActivityResult(requestCode, resultCode, data);

            ((PlayServicesManager)BackpackPlannerState.PlatformPlayServicesManager).OnActivityResult(requestCode, resultCode, data);
	    }

        protected void InitToolbar()
        {
            Toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(Toolbar);
        }

        private void LoadPreferences()
        {
            Logger.Debug("Setting default preferences...");
            Android.Support.V7.Preferences.PreferenceManager.SetDefaultValues(this, Resource.Xml.settings, false);

            Logger.Debug("Loading preferences...");
            BackpackPlannerState.PlatformSettingsManager.Load(BackpackPlannerState.Settings, BackpackPlannerState.PersonalInformation);
        }
    }
}
