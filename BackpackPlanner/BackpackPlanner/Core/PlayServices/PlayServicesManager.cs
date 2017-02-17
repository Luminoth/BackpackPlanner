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

namespace EnergonSoftware.BackpackPlanner.Core.PlayServices
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// http://android-developers.blogspot.com/2015/09/google-play-services-81-and-android-60.html
    /// https://github.com/googledrive/android-demos/tree/master/app/src/main/java/com/google/android/gms/drive/sample/demo
    /// </remarks>
    public abstract class PlayServicesManager : IDisposable
    {
#region Events
        /// <summary>
        /// Occurs when Google Play Services connects or disconnects.
        /// </summary>
        public event EventHandler<PlayServicesConnectedEventArgs> PlayServicesConnectedEvent;
        #endregion

        /// <summary>
        /// Gets a value indicating whether play services are enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if play services are enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsEnabled { get; private set; } = false;

        /// <summary>
        /// Gets a value indicating whether Google Play Services are connected or not.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is connected; otherwise, <c>false</c>.
        /// </value>
        public abstract bool IsConnected { get; }

#region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if(disposing) {
                DestroyAsync().Wait();
            }
        }
#endregion

        ~PlayServicesManager()
        {
            Dispose(false);
        }

        /// <summary>
        /// Initializes Google Play Services.
        /// </summary>
        public abstract Task InitAsync();

        /// <summary>
        /// Destroys Google Play Services.
        /// </summary>
        public virtual async Task DestroyAsync()
        {
            await DisconnectAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Connects Google Play Services.
        /// </summary>
        public abstract Task ConnectAsync();

        /// <summary>
        /// Disconnects Google Play Services.
        /// </summary>
        public abstract Task DisconnectAsync();

#region appfolder Management
        /// <summary>
        /// Determines whether the given file exists in the Google Drive appfolder.
        /// </summary>
        /// <param name="title">The file title.</param>
        /// <returns>True if the file exists in the Google Drive appfolder, otherwise false.</returns>
        public abstract Task<bool> HasFileInDriveAppFolderAsync(string title);

        /// <summary>
        /// Saves the given content to the given file in the Google Drive appfolder
        /// </summary>
        /// <param name="title">The file title.</param>
        /// <param name="contentType">The file content type.</param>
        /// <param name="contentStream">The file content.</param>
        /// <returns>True if the file was successfully saved, otherwise false.</returns>
        public abstract Task<bool> SaveFileToDriveAppFolderAsync(string title, string contentType, Stream contentStream);

        /// <summary>
        /// Updates the given Google Drive appfolder file with the given content.
        /// </summary>
        /// <param name="title">The file title.</param>
        /// <param name="contentType">The new file content type.</param>
        /// <param name="contentStream">The new file content.</param>
        /// <returns>True if the file was successfully updated, otherwise false.</returns>
        public abstract Task<bool> UpdateFileInDriveAppFolderAsync(string title, string contentType, Stream contentStream);

        /// <summary>
        /// Downloads the given Google Drive appfolder file.
        /// </summary>
        /// <param name="title">The file title.</param>
        /// <returns>The stream associated with the given file, if it exists.</returns>
        public abstract Task<Stream> DownloadFileFromDriveAppFolderAsync(string title);

        /// <summary>
        /// Deletes the given Google Drive appfolder file.
        /// </summary>
        /// <param name="title">The file title.</param>
        public abstract Task DeleteFileFromDriveAppFolderAsync(string title);
#endregion

        /// <summary>
        /// Raises the <see cref="E:PlayServicesConnectedEvent" /> event.
        /// </summary>
        /// <param name="args">The <see cref="PlayServicesConnectedEventArgs"/> instance containing the event data.</param>
        protected void OnConnected(PlayServicesConnectedEventArgs args)
        {
            PlayServicesConnectedEvent?.Invoke(this, args);
        }
    }
}
