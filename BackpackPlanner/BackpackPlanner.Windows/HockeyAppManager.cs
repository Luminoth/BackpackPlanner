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

using HockeyApp;

namespace EnergonSoftware.BackpackPlanner.Windows
{
    public class HockeyAppManager : IHockeyAppManager
    {
        public string AppId => "555fd9c1dfd5151312f39bf6c704827b";

        public bool IsInitialized { get; private set; }

        public async Task InitAsync()
        {
            if(IsInitialized) {
                return;
            }

            HockeyClient.Current.Configure(AppId);

            await HockeyClient.Current.SendCrashesAsync();

#if WINDOWS_PHONE_APP
            await HockeyClient.Current.CheckForAppUpdateAsync();
#endif

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
