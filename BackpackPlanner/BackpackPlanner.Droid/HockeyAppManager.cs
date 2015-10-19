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
using Android.Runtime;

using EnergonSoftware.BackpackPlanner.Core.HockeyApp;
using EnergonSoftware.BackpackPlanner.Core.Logging;

using HockeyApp;

namespace EnergonSoftware.BackpackPlanner.Droid
{
    public sealed class HockeyAppManager : IHockeyAppManager
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(HockeyAppManager));

        // static so that separate activities don't
        // initialize the handlers more than once
        private static bool _isInitialized;

        public string AppId => "32a2c37622529305ec763b7e2c224deb";

        public bool IsInitialized { get { return _isInitialized; } private set { _isInitialized = value; } }

        private readonly Activity _activity;

        public HockeyAppManager(Activity activity)
        {
            if(null == activity) {
                throw new ArgumentNullException(nameof(activity));
            }

            _activity = activity;
        }

        public async Task InitAsync()
        {
            if(IsInitialized) {
                Logger.Debug("HockeyApp already initialized!");
                return;
            }

            Logger.Info("Initializing HockeyApp...");

            // Register the crash manager before Initializing the trace writer
            CrashManager.Register(_activity, AppId); 

            // Register to with the Update Manager
            UpdateManager.Register(_activity, AppId);

            // Register the Feedback Manager
            FeedbackManager.Register(_activity, AppId);

            // Initialize the Trace Writer
            TraceWriter.Initialize();

            // Wire up Unhandled Expcetion handler from Android
            AndroidEnvironment.UnhandledExceptionRaiser += (sender, args) => {
                // Use the trace writer to log exceptions so HockeyApp finds them
                TraceWriter.WriteTrace(args.Exception);
                args.Handled = true;
            };

            // Wire up the .NET Unhandled Exception handler
            AppDomain.CurrentDomain.UnhandledException += (sender, args) => TraceWriter.WriteTrace(args.ExceptionObject);

            // Wire up the unobserved task exception handler
            TaskScheduler.UnobservedTaskException += (sender, args) => TraceWriter.WriteTrace(args.Exception);

            await Task.Delay(0).ConfigureAwait(false);

            IsInitialized = true;
        }

        public async Task DestroyAsync()
        {
            await Task.Delay(0).ConfigureAwait(false);
        }

        public void ShowFeedback()
        {
            /*Logger.Debug("Showing feedback activity");
            FeedbackManager.ShowFeedbackActivity(_activity);*/
        }
    }
}
