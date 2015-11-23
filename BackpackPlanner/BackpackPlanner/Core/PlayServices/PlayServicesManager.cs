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
using System.Text;
using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner.Core.Logging;

namespace EnergonSoftware.BackpackPlanner.Core.PlayServices
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// http://android-developers.blogspot.com/2015/09/google-play-services-81-and-android-60.html
    /// https://github.com/googledrive/android-demos/tree/master/app/src/main/java/com/google/android/gms/drive/sample/demo
    /// </remarks>
    public abstract class PlayServicesManager
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(PlayServicesManager));

        private const string ManifestFileTitle = "BackpackPlanner.manifest";
        private const string ManifestFileContentType = "text/plain";

        private static async Task WriteManifestFile(Stream stream)
        {
            string content = $@"Backpacking Planner
{DateTime.Now}";

            var contentBytes = Encoding.UTF8.GetBytes(content);
            Logger.Debug($"Manifest file is {contentBytes.Length} bytes");
            await stream.WriteAsync(contentBytes, 0, contentBytes.Length).ConfigureAwait(false);
        }

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
        public abstract Task<bool> HasFileInDriveAppFolderAsync(string title);

        public abstract Task<bool> SaveFileToDriveAppFolderAsync(string title, string contentType, Stream contentStream);

        public abstract Task<bool> UpdateFileInDriveAppFolderAsync(string title, string contentType, Stream contentStream);

        public abstract Task<Stream> DownloadFileFromDriveAppFolderAsync(string title);

        public abstract Task DeleteFileFromDriveAppFolderAsync(string title);
#endregion

        public void SyncDatabaseInBackground()
        {
            if(!IsConnected) {
                return;
            }

            Logger.Info("Starting database sync task...");
            Task.Run(async () => {
                if(!await HasFileInDriveAppFolderAsync(ManifestFileTitle)) {
Logger.Debug($"no such manifest file {ManifestFileTitle}, creating...");
                    using(Stream stream = new MemoryStream()) {
                        await WriteManifestFile(stream);

                        await stream.FlushAsync().ConfigureAwait(false);
                        stream.Position = 0;
                        await SaveFileToDriveAppFolderAsync(ManifestFileTitle, ManifestFileContentType, stream).ConfigureAwait(false);
                    }
                } else {
Logger.Debug($"manifest file {ManifestFileTitle} exists!");
                }

                Logger.Info("Database sync task complete!");
            });
        }

        protected void OnConnected(PlayServicesConnectedEventArgs args)
        {
            PlayServicesConnectedEvent?.Invoke(this, args);
        }
    }
}
