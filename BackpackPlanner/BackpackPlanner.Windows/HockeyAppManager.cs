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

using Microsoft.HockeyApp;

namespace EnergonSoftware.BackpackPlanner.Windows
{
    public sealed class HockeyAppManager : IHockeyAppManager
    {
        public string AppId => "4a1fe69d1c8a49f08ed3de584e44f0fe";

        public bool IsInitialized { get; private set; }

        public async Task InitAsync()
        {
            if(IsInitialized) {
                return;
            }

            HockeyClient.Current.Configure(AppId);
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
