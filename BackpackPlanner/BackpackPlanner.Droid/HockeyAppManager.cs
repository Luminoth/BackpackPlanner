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

        public async Task InitAsync()
        {
            if(IsInitialized) {
                Logger.Debug("HockeyApp already initialized!");
                return;
            }

            Logger.Info("Initializing HockeyApp...");

            TraceWriter.Initialize();

            AndroidEnvironment.UnhandledExceptionRaiser += OnUnhandledExceptionRaised;
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;

            await Task.Delay(0).ConfigureAwait(false);

            IsInitialized = true;
        }

        public void OnCreate(Activity activity)
        {
            UpdateManager.Register(activity, AppId);
        }

        public void OnDestroy()
        {
            UpdateManager.Unregister();
        }

        public void OnResume(Activity activity)
        {
            Tracking.StartUsage(activity);
            CrashManager.Register(activity, AppId);
        }

        public void OnPause(Activity activity)
        {
            UpdateManager.Unregister();
            Tracking.StopUsage(activity);
        }

        public bool HasNewCrashes(Activity activity)
        {
            return 1 == CrashManager.HasStackTraces(new Java.Lang.Ref.WeakReference(activity));
        }

        public async Task DestroyAsync()
        {
            TaskScheduler.UnobservedTaskException -= OnUnobservedTaskException;
            AppDomain.CurrentDomain.UnhandledException -= OnUnhandledException;
            AndroidEnvironment.UnhandledExceptionRaiser -= OnUnhandledExceptionRaised;

            await Task.Delay(0).ConfigureAwait(false);
        }

        public void ShowFeedback()
        {
            throw new NotImplementedException();
        }

        public void ShowFeedback(Activity activity)
        {
            FeedbackManager.Register(activity, AppId);
            /*Logger.Debug("Showing feedback activity");
            FeedbackManager.ShowFeedbackActivity(activity);*/
        }

        private void OnUnhandledExceptionRaised(object sender, RaiseThrowableEventArgs args)
        {
            TraceWriter.WriteTrace(args.Exception);
            args.Handled = true;
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            TraceWriter.WriteTrace(args.ExceptionObject);
        }

        private void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs args)
        {
        }
    }
}
