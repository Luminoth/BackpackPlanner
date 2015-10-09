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

using Android.OS;

using EnergonSoftware.BackpackPlanner.Core.Logging;

namespace EnergonSoftware.BackpackPlanner.Droid.Activities
{
    public class BaseActivity : Android.Support.V7.App.AppCompatActivity
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(BaseActivity));

        private readonly Stopwatch _startupStopwatch = new Stopwatch();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _startupStopwatch.Start();
        }

        protected override void OnStart()
        {
            base.OnStart();

            if(_startupStopwatch.IsRunning) {
                Logger.Debug($"Time to Activity.Start(): {_startupStopwatch.ElapsedMilliseconds}ms");
            }
        }

        protected override void OnResume()
        {
            base.OnResume();

            if(_startupStopwatch.IsRunning) {
                Logger.Debug($"Time to Activity.OnResume(): {_startupStopwatch.ElapsedMilliseconds}ms");
            }
            _startupStopwatch.Stop();
        }
    }
}