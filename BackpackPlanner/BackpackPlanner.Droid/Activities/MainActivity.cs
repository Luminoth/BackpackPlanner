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
using System.Threading.Tasks;

using Android.App;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Droid.Util;

using JetBrains.Annotations;

namespace EnergonSoftware.BackpackPlanner.Droid.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.Splash", MainLauncher = true, NoHistory = true, Exported = true)]
    [MetaData("com.google.android.gms.version", Value = "@integer/google_play_services_version")]
    public sealed class MainActivity : BaseActivity
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(MainActivity));

#if DEBUG
        private readonly Stopwatch _initStopwatch = new Stopwatch();
#endif

#region Activity Lifecycle
        protected override void OnDestroy()
        {
            if(((HockeyAppManager)BackpackPlannerState.PlatformHockeyAppManager).HasNewCrashes(this)) {
                Logger.Warn("Hockey app has new crashes, probably leaking the dialog!");
            }

            base.OnDestroy();
        }

        protected override void OnResume()
        {
            base.OnResume();

#if DEBUG
            ProgressDialog progressDialog = DialogUtil.ShowProgressDialog(this, Resource.String.message_initializing, false, true);
#else
            ProgressDialog progressDialog = null;
#endif

            Task.Run(async () => await Init(progressDialog).ConfigureAwait(false));
        }
#endregion

        public override void OnBackPressed()
        {
            // prevent back button from doing anything while loading
        }

        private async Task Init([CanBeNull] ProgressDialog progressDialog)
        {
            Logger.Debug("Initializing...");

#if DEBUG
            _initStopwatch.Start();
#endif

            bool success = await InitDatabase().ConfigureAwait(false);

            InitFinished(progressDialog, success);
        }

        private async Task<bool> InitDatabase()
        {
            return await BackpackPlannerState.DatabaseState.InitAsync(
                BackpackPlannerState,
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
                DatabaseState.DatabaseName).ConfigureAwait(false);
        }

        private void InitFinished([CanBeNull] ProgressDialog progressDialog, bool initSuccess)
        {
#if DEBUG
            if(_initStopwatch.IsRunning) {
                Logger.Debug($"Initialization finished in {_initStopwatch.ElapsedMilliseconds}ms, success: {initSuccess}");
            }
            _initStopwatch.Stop();
#else
            Logger.Debug($"Initialization finished, success: ${initSuccess}");
#endif

            RunOnUiThread(() =>
            {
                progressDialog?.Dismiss();

                if(!initSuccess) {
                    DialogUtil.ShowAlert(this, Resource.String.message_error_initialization, Resource.String.title_error_initialization);
                    return;
                }

                if(BackpackPlannerState.Settings.MetaSettings.FirstRun) {
                    Logger.Debug("Starting FTUE...");
                    StartActivity(typeof(FTUEActivity));
                } else {
                    Logger.Debug("Starting main activity...");
                    StartActivity(typeof(BackpackPlannerActivity));
                }
                BackpackPlannerState.Settings.MetaSettings.FirstRun = false;

                Finish();
            });
        }
    }
}
