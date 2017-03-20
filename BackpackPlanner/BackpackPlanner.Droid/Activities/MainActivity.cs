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
using Android.OS;

using EnergonSoftware.BackpackPlanner.Core.Logging;

namespace EnergonSoftware.BackpackPlanner.Droid.Activities
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon")]
    [MetaData("com.google.android.gms.version", Value = "@integer/google_play_services_version")]
    public sealed class MainActivity : BaseActivity
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(MainActivity));

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

            InitToolbar();

            Title = Resources.GetString(Resource.String.app_name);
        }
#if DEBUG
        private readonly Stopwatch _initStopwatch = new Stopwatch();
#endif

        protected override void OnDestroy()
        {
            if(((HockeyAppManager)DroidState.Instance.BackpackPlannerState.PlatformHockeyAppManager).HasNewCrashes(this)) {
                Logger.Warn("Hockey app has new crashes, probably leaking the dialog!");
            }

            base.OnDestroy();
        }

        protected override void OnResume()
        {
            base.OnResume();

            Task.Run(async () => await Init().ConfigureAwait(false));
        }

        public override void OnBackPressed()
        {
            // prevent back button from doing anything while loading
        }

        private async Task Init()
        {
            Logger.Debug("Initializing...");

#if DEBUG
            _initStopwatch.Start();
#endif

            await DroidState.Instance.InitDatabase().ConfigureAwait(false);

            InitFinished();
        }

        private void InitFinished()
        {
#if DEBUG
            if(_initStopwatch.IsRunning) {
                Logger.Debug($"Initialization finished in: {_initStopwatch.ElapsedMilliseconds}ms");
            }
            _initStopwatch.Stop();
#else
            Logger.Debug("Initialization finished!");
#endif

            RunOnUiThread(() =>
            {
                if(DroidState.Instance.BackpackPlannerState.Settings.MetaSettings.FirstRun) {
                    Logger.Debug("Starting FTUE...");
                    StartActivity(typeof(FTUEActivity));
                } else {
                    Logger.Debug("Starting main activity...");
                    StartActivity(typeof(BackpackPlannerActivity));
                }
                DroidState.Instance.BackpackPlannerState.Settings.MetaSettings.FirstRun = false;

                Finish();
            });
        }
    }
}
