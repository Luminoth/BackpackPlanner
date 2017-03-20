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
using System.Threading;
using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Core.PlayServices;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;

namespace EnergonSoftware.BackpackPlanner.Windows
{
    internal sealed class WindowsPlayServicesManager : PlayServicesManager
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(WindowsPlayServicesManager));

        public override bool IsConnected => null != _driveService;

        private UserCredential _userCredential;

        private DriveService _driveService;

        private string _appFolderId;

        public override void Init()
        {
        }

        public override void Destroy()
        {
            base.Destroy();

            Cleanup();
        }

        public override async Task ConnectAsync()
        {
            if(IsConnected) {
                Logger.Info("Google API already connected!");
                OnConnected(new PlayServicesConnectedEventArgs { IsSuccess= true });
                return;
            }

            Logger.Info("Connecting Google Play Services client...");

            try {
                _userCredential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    new Uri("ms-appx:///Assets/google_play_client_secrets.json"),
                    new[] {
                        DriveService.Scope.DriveFile,
                        "https://www.googleapis.com/auth/drive.appfolder"
                    },
                    App.CurrentApp.BackpackPlannerState.Settings.GooglePlayServicesUser,
                    CancellationToken.None).ConfigureAwait(false);
                if(null == _userCredential) {
                    OnConnected(new PlayServicesConnectedEventArgs { IsSuccess = false });
                    return;
                }

                App.CurrentApp.BackpackPlannerState.Settings.GooglePlayServicesUser = _userCredential.UserId;

                _driveService = new DriveService(
                    new BaseClientService.Initializer
                    {
                        HttpClientInitializer = _userCredential,
                        ApplicationName = "Backpacking Planner"
                    }
                );

                OnConnected(new PlayServicesConnectedEventArgs { IsSuccess = true });
            } catch(Exception ex) {
                Logger.Error($"Error connecting to Google Services: {ex.Message}", ex);

                Cleanup();

                OnConnected(new PlayServicesConnectedEventArgs { IsSuccess = false });
            }
        }

        public override async Task DisconnectAsync()
        {
            await Task.Delay(0).ConfigureAwait(false);

            if(!IsConnected) {
                return;
            }

            Logger.Info("Disonnecting Google Play Services client...");

            Cleanup();
        }

#region appfolder Management
        public override Task<bool> HasFileInDriveAppFolderAsync(string title)
        {
throw new NotImplementedException();
        }

        public override Task<bool> SaveFileToDriveAppFolderAsync(string title, string contentType, Stream contentStream)
        {
throw new NotImplementedException();
        }

        public override Task<bool> UpdateFileInDriveAppFolderAsync(string title, string contentType, Stream contentStream)
        {
throw new NotImplementedException();
        }

        public override Task<Stream> DownloadFileFromDriveAppFolderAsync(string title)
        {
throw new NotImplementedException();
        }

        public override Task DeleteFileFromDriveAppFolderAsync(string title)
        {
throw new NotImplementedException();
        }

        private async Task InitAppFolderIdAsync()
        {
            if(!string.IsNullOrEmpty(_appFolderId)) {
                return;
            }

            Logger.Debug("Initializing appfolder Id...");

            Google.Apis.Drive.v3.Data.File file = await _driveService.Files.Get("appfolder").ExecuteAsync().ConfigureAwait(false);
            if(null == file) {
                Logger.Error("Unable to get appfolder Id!");
                return;
            }

            _appFolderId = file.Id;
            Logger.Debug($"appfolder id: {_appFolderId}");
        }
#endregion

        private void Cleanup()
        {
            _userCredential = null;
            _driveService = null;
            _appFolderId = null;
        }
    }
}
