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

using System.IO;
using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Core.PlayServices;

namespace EnergonSoftware.BackpackPlanner.Core
{
    public abstract class DatabaseSyncManager
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(DatabaseSyncManager));

        protected enum SyncConflictAction
        {
            Nothing,
            Push,
            Pull
        }

        private static bool ShouldPull(PlayServicesManifest localManifest)
        {
            return localManifest.LastUpdateTimestampSeconds < localManifest.LastSyncTimestampSeconds;
        }

        private static bool ShouldPush(PlayServicesManifest localManifest, PlayServicesManifest remoteManifest)
        {
            return !ShouldPull(localManifest) && remoteManifest.LastSyncTimestampSeconds < localManifest.LastSyncTimestampSeconds;
        }

        protected abstract Task<PlayServicesManifest> ReadLocalManifest();

        protected abstract Task WriteLocalManifset(PlayServicesManifest localManifest);

        protected abstract Task<SyncConflictAction> GetConflictResolutionAction();

        public void SyncDatabaseInBackground(PlayServicesManager playServicesManager)
        {
            if(null == playServicesManager || !playServicesManager.IsConnected) {
                return;
            }

            Logger.Info("Starting database sync task...");
            Task.Run(async () => {
                PlayServicesManifest localManifest = await ReadLocalManifest().ConfigureAwait(false);
                if(null == localManifest) {
                    localManifest = new PlayServicesManifest();
                    await WriteLocalManifset(localManifest).ConfigureAwait(false);
                }
    
                PlayServicesManifest remoteManifest = await ReadRemoteManifest().ConfigureAwait(false);
                if(null == remoteManifest) {
                    remoteManifest = new PlayServicesManifest();
                    await WriteRemoteManifestFile(playServicesManager).ConfigureAwait(false);
                }

                if(ShouldPull(localManifest)) {
                    await PullDatabaseFile(playServicesManager).ConfigureAwait(false);
                } else if(ShouldPush(localManifest, remoteManifest)) {
                    await PushDatabaseFile(playServicesManager).ConfigureAwait(false);
                } else {
                    SyncConflictAction conflictResolutionAction = await GetConflictResolutionAction().ConfigureAwait(false);
                    switch(conflictResolutionAction)
                    {
                    case SyncConflictAction.Push:
                        await PushDatabaseFile(playServicesManager).ConfigureAwait(false);
                        break;
                    case SyncConflictAction.Pull:
                        await PullDatabaseFile(playServicesManager).ConfigureAwait(false);
                        break;
                    }
                }

                Logger.Info("Database sync task complete!");
            });
        }

        private async Task<PlayServicesManifest> ReadRemoteManifest()
        {
await Task.Delay(0).ConfigureAwait(false);
return null;
        }

        private async Task WriteRemoteManifestFile(PlayServicesManager playServicesManager)
        {
            using(Stream stream = new MemoryStream()) {
                PlayServicesManifest manifest = new PlayServicesManifest();
                await manifest.Write(stream).ConfigureAwait(false);
                await stream.FlushAsync().ConfigureAwait(false);

                stream.Position = 0;
                await playServicesManager.SaveFileToDriveAppFolderAsync(PlayServicesManifest.FileTitle, PlayServicesManifest.ContentType, stream).ConfigureAwait(false);
            }
        }

        private async Task PushDatabaseFile(PlayServicesManager playServicesManager)
        {
await Task.Delay(0).ConfigureAwait(false);
        }

        private async Task PullDatabaseFile(PlayServicesManager playServicesManager)
        {
await Task.Delay(0).ConfigureAwait(false);
        }
    }
}
