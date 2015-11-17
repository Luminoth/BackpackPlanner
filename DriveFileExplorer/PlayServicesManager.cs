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
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

using log4net;

namespace EnergonSoftware.BackpackPlanner.DriveFileExplorer
{
    public class PlayServicesManager : INotifyPropertyChanged
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(PlayServicesManager));

        public bool IsConnected => null != _driveService;

        public bool IsNotConnected => !IsConnected;

        private UserCredential _userCredential;

        private DriveService _driveService;

        private string _appFolderId;

        public async Task ConnectAsync()
        {
            if(IsConnected) {
                await DisconnectAsync().ConfigureAwait(false);
            }

            Logger.Info("Connecting to Google Services...");

            try {
                ClientSecrets secrets;
                using(Stream stream = new FileStream("C:/EnergonSoftware/google_play_client_secrets.json", FileMode.Open, FileAccess.Read)) {
                    secrets = GoogleClientSecrets.Load(stream).Secrets;
                }

                if(null == secrets) {
                    Logger.Error("Failed to read client secrets!");
                    return;
                }

                _userCredential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    secrets,
                    new[] {
                        DriveService.Scope.DriveFile,
                        "https://www.googleapis.com/auth/drive.appfolder"
                    },
                    Properties.Settings.Default.GooglePlayServicesUser,
                    CancellationToken.None,
                    new FileDataStore("EnergonSoftware.DriveFileExplorer.Auth.Store")).ConfigureAwait(false);
                if(null == _userCredential) {
                    Logger.Error("Failed to create user credentials!");
                    return;
                }

                Properties.Settings.Default.GooglePlayServicesUser = _userCredential.UserId;

                _driveService = new DriveService(
                    new BaseClientService.Initializer
                    {
                        HttpClientInitializer = _userCredential,
                        ApplicationName = "Backpacking Planner"
                    }
                );

                Logger.Info("Connected!");
                NotifyPropertyChanged("IsConnected");
                NotifyPropertyChanged("IsNotConnected");
            } catch(Exception ex) {
                Logger.Error($"Error connecting to Google Services: {ex.Message}", ex);

                Cleanup();

                NotifyPropertyChanged("IsConnected");
                NotifyPropertyChanged("IsNotConnected");
            }
        }

        public async Task DisconnectAsync()
        {
            if(!IsConnected) {
                return;
            }

            Logger.Info("Disconnecting from Google Services...");

            Cleanup();

            NotifyPropertyChanged("IsConnected");
            NotifyPropertyChanged("IsNotConnected");

            await Task.Delay(0).ConfigureAwait(false);
        }

#region appfolder Management
        // https://developers.google.com/drive/web/appdata

        public async Task<ICollection<Google.Apis.Drive.v2.Data.File>>  ListDriveAppFolderFilesAsync()
        {
            if(!IsConnected) {
                return new List<Google.Apis.Drive.v2.Data.File>();
            }

            await InitAppFolderIdAsync().ConfigureAwait(false);

            Logger.Info("Getting appfolder file list...");
            FilesResource.ListRequest request = _driveService.Files.List();
            request.Q = $"'{_appFolderId}' in parents";

            FileList fileList = await request.ExecuteAsync().ConfigureAwait(false);
            return fileList.Items;
        }

        public async Task<Google.Apis.Drive.v2.Data.File> SaveFileToDriveAppFolderAsync(string filePath, string contentType)
        {
            await InitAppFolderIdAsync().ConfigureAwait(false);

            Logger.Info($"Saving file to appfolder: {filePath}...");

            Google.Apis.Drive.v2.Data.File body = new Google.Apis.Drive.v2.Data.File
            {
                Title = Path.GetFileName(filePath),
                Parents = new List<ParentReference> { new ParentReference { Id = _appFolderId } },
                MimeType = contentType
            };

            using(Stream stream = new FileStream(filePath, FileMode.Open)) {
                FilesResource.InsertMediaUpload request = _driveService.Files.Insert(body, stream, contentType);
                await request.UploadAsync().ConfigureAwait(false);
                return request.ResponseBody;
            }
        }

        public async Task<Google.Apis.Drive.v2.Data.File> UpdateFileInDriveAppFolderAsync(Google.Apis.Drive.v2.Data.File file, string filePath, string contentType)
        {
            await InitAppFolderIdAsync().ConfigureAwait(false);

            Logger.Info($"Updating file {file.Id} ({file.Title}) in appfolder: {filePath}...");

            file.Title = Path.GetFileName(filePath);
            file.MimeType = contentType;

            using(Stream stream = new FileStream(filePath, FileMode.Open)) {
                FilesResource.UpdateMediaUpload request = _driveService.Files.Update(file, file.Id, stream, contentType);
                request.NewRevision = true;
                await request.UploadAsync().ConfigureAwait(false);
                return request.ResponseBody;
            }
        }

        public async Task<Stream> DownloadFileFromDriveAppFolderAsync(Google.Apis.Drive.v2.Data.File file)
        {
            await InitAppFolderIdAsync().ConfigureAwait(false);

            Logger.Info($"Downloading file {file.Id} ({file.Title})...");

            if(string.IsNullOrEmpty(file.DownloadUrl)) {
                Logger.Error("File download URL is empty!");
                return null;
            }

            Logger.Info($"Downloading file {file.Id} ({file.Title}) from {file.DownloadUrl}...");
            return await _driveService.HttpClient.GetStreamAsync(new Uri(file.DownloadUrl));
        }

        public async Task DeleteFileFromDriveAppFolderAsync(Google.Apis.Drive.v2.Data.File file)
        {
            await InitAppFolderIdAsync().ConfigureAwait(false);

            Logger.Info($"Deleting file {file.Id} ({file.Title})...");
            await _driveService.Files.Delete(file.Id).ExecuteAsync().ConfigureAwait(false);
        }

        private async Task InitAppFolderIdAsync()
        {
            if(!string.IsNullOrEmpty(_appFolderId)) {
                return;
            }

            Logger.Debug("Initializing appfolder Id...");

            Google.Apis.Drive.v2.Data.File file = await _driveService.Files.Get("appfolder").ExecuteAsync().ConfigureAwait(false);
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

#region Property Notifier
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string property=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
#endregion
    }
}
