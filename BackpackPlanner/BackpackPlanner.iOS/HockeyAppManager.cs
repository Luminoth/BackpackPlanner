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

using EnergonSoftware.BackpackPlanner.Core.HockeyApp;

using HockeyApp;

namespace EnergonSoftware.BackpackPlanner.iOS
{
    public class HockeyAppManager : IHockeyAppManager
    {
        public string AppId => "TODO";

        public bool IsInitialized { get; private set; }

        public async Task InitAsync()
        {
            if(IsInitialized) {
                return;
            }

            //We MUST wrap our setup in this block to wire up
            // Mono's SIGSEGV and SIGBUS signals
            Setup.EnableCustomCrashReporting(() => {

                //Get the shared instance
                BITHockeyManager manager = BITHockeyManager.SharedHockeyManager;

                //Configure it to use our APP_ID
                manager.Configure(AppId);

                //Start the manager
                manager.StartManager();

                //Authenticate (there are other authentication options)
                manager.Authenticator.AuthenticateInstallation();

                //Rethrow any unhandled .NET exceptions as native iOS 
                // exceptions so the stack traces appear nicely in HockeyApp
                AppDomain.CurrentDomain.UnhandledException += (sender, args) => Setup.ThrowExceptionAsNative(args.ExceptionObject);

                TaskScheduler.UnobservedTaskException += (sender, args) => Setup.ThrowExceptionAsNative(args.Exception);
            });

            await Task.Delay(0).ConfigureAwait(false);

            IsInitialized = true;
        }

        public async Task DestroyAsync()
        {
            await Task.Delay(0).ConfigureAwait(false);
        }

        public void ShowFeedback()
        {
        }
    }
}
