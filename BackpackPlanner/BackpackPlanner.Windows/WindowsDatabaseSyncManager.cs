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

using EnergonSoftware.BackpackPlanner.Core;
using EnergonSoftware.BackpackPlanner.Core.PlayServices;

namespace EnergonSoftware.BackpackPlanner.Windows
{
    public sealed class WindowsDatabaseSyncManager : DatabaseSyncManager
    {
        protected override async Task<PlayServicesManifest> ReadLocalManifest()
        {
await Task.Delay(0).ConfigureAwait(false);
return null;
        }

        protected override async Task WriteLocalManifset(PlayServicesManifest localManifest)
        {
await Task.Delay(0).ConfigureAwait(false);
        }

        protected override async Task<SyncConflictAction> GetConflictResolutionAction()
        {
await Task.Delay(0).ConfigureAwait(false);
return SyncConflictAction.Nothing;
        }
    }
}
