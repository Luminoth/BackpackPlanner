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
using System.IO;
using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner.Core.Logging;

namespace EnergonSoftware.BackpackPlanner.Core.PlayServices
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// http://android-developers.blogspot.com/2015/09/google-play-services-81-and-android-60.html
    /// </remarks>
    public abstract class PlayServicesManager
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(PlayServicesManager));

#region Events
        public event EventHandler<PlayServicesConnectedEventArgs> PlayServicesConnectedEvent;
#endregion

        public abstract bool IsConnected { get; }

        public abstract Task InitAsync();

        public virtual async Task DestroyAsync()
        {
            await DisconnectAsync().ConfigureAwait(false);
        }

        public abstract Task ConnectAsync();

        public abstract Task DisconnectAsync();

#region appfolder Management
        public abstract Task<bool> SaveFileToDriveAppFolderAsync(string title, string contentType, Stream contentStream);

        public abstract Task<bool> UpdateFileInDriveAppFolderAsync(string fileId, string title, string contentType, Stream contentStream);

        public abstract Task<Stream> DownloadFileFromDriveAppFolderAsync(string fileId);

        public abstract Task DeleteFileFromDriveAppFolderAsync(string fileId);
#endregion

        public void SyncDatabaseInBackground()
        {
            Logger.Info("Starting database sync task...");
            Task.Run(() => {
// TODO
            });

        }

        protected void OnConnected(PlayServicesConnectedEventArgs args)
        {
            PlayServicesConnectedEvent?.Invoke(this, args);
        }
    }
}
