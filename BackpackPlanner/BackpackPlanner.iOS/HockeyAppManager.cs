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

using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner.Core.HockeyApp;
using EnergonSoftware.BackpackPlanner.Core.Logging;

using HockeyApp.iOS;

namespace EnergonSoftware.BackpackPlanner.iOS
{
    public class HockeyAppManager : IHockeyAppManager
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(HockeyAppManager));

        public string AppId => "TODO";

        public bool IsInitialized { get; private set; }

        public async Task InitAsync()
        {
            if(IsInitialized) {
                Logger.Debug("HockeyApp already initialized!");
                return;
            }

            Logger.Info("Initializing HockeyApp...");

            //Get the shared instance
            BITHockeyManager manager = BITHockeyManager.SharedHockeyManager;

// TODO: how do we turn always send on?

            //Configure it to use our APP_ID
            manager.Configure(AppId);

            manager.CrashManager.CrashManagerStatus = BITCrashManagerStatus.AutoSend;

            //Start the manager
            manager.StartManager();

            //Authenticate (there are other authentication options)
            manager.Authenticator.AuthenticateInstallation();

            await Task.Delay(0).ConfigureAwait(false);

            IsInitialized = true;
        }

        public void Destroy()
        {
        }

        public void ShowFeedback()
        {
        }
    }
}
