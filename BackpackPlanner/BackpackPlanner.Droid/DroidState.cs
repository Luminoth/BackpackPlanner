/*
   Copyright 2016 Shane Lillie

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

using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Droid.Activities;
using EnergonSoftware.BackpackPlanner.Droid.Logging;
using EnergonSoftware.BackpackPlanner.Droid.Permissions;

using SQLite.Net.Platform.XamarinAndroid;

namespace EnergonSoftware.BackpackPlanner.Droid
{
    /// <summary>
    /// Singleton to hold global application state
    /// </summary>
    public sealed class DroidState
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(DroidState));

        public static readonly DroidState Instance = new DroidState();

        public BackpackPlannerState BackpackPlannerState { get; private set; }

// TODO: hold easy access references to more of the platform specific modules here
// that way all of the weird casting everywhere can go away

        public DroidPermissionRequestFactory PermissionRequestFactory { get; private set; }

        private bool _preferencesLoaded;

        /// <summary>
        /// Called whenever an Activity receives OnCreate().
        /// </summary>
        /// <param name="activity">The activity.</param>
        /// <returns>Can safely be called multiple times over the life of the application.</returns>
        public async Task OnCreate(BaseActivity activity)
        {
            if(null == CustomLogger.PlatformLogger) {
                CustomLogger.PlatformLogger = new DroidLogger();
            }

            if(null == PermissionRequestFactory) {
                PermissionRequestFactory = new DroidPermissionRequestFactory(activity);
            }

            if(null == BackpackPlannerState) {
                BackpackPlannerState = new BackpackPlannerState(
                    new HockeyAppManager(),
                    new DroidSettingsManager(Android.Support.V7.Preferences.PreferenceManager.GetDefaultSharedPreferences(activity)),
                    new DroidPlayServicesManager(),
                    new DroidDatabaseSyncManager(),
                    //new SQLitePlatformAndroid()
                    new SQLitePlatformAndroidN(),
                    PermissionRequestFactory
                );

                await BackpackPlannerState.InitAsync().ConfigureAwait(false);
            }

            LoadPreferences(activity);

            ((HockeyAppManager)BackpackPlannerState.PlatformHockeyAppManager).OnCreate(activity);
        }

        public void OnResume(BaseActivity activity)
        {
            ((HockeyAppManager)BackpackPlannerState.PlatformHockeyAppManager).OnResume(activity);
        }

        public void OnPause(BaseActivity activity)
        {
            ((HockeyAppManager)BackpackPlannerState.PlatformHockeyAppManager).OnPause(activity);
        }

        public async Task InitDatabase()
        {
            if(BackpackPlannerState.DatabaseState.IsInitialized) {
                return;
            }

            await BackpackPlannerState.DatabaseState.ConnectAsync(
                BackpackPlannerState,
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
                DatabaseState.DatabaseName).ConfigureAwait(false);

            await BackpackPlannerState.DatabaseState.InitDatabaseAsync(BackpackPlannerState).ConfigureAwait(false);
        }

        private void LoadPreferences(BaseActivity activity)
        {
            if(_preferencesLoaded) {
                return;
            }

            Logger.Debug("Setting default preferences...");
            Android.Support.V7.Preferences.PreferenceManager.SetDefaultValues(activity, Resource.Xml.settings, false);

            Logger.Debug("Loading preferences...");
            BackpackPlannerState.PlatformSettingsManager.Load(BackpackPlannerState.Settings, BackpackPlannerState.PersonalInformation);

            _preferencesLoaded = true;
        }
    }
}