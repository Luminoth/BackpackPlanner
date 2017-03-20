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
using System.Linq;
using System.Threading.Tasks;

using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Drive;
using Android.Gms.Drive.Query;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Core.PlayServices;
using EnergonSoftware.BackpackPlanner.Droid.Activities;

namespace EnergonSoftware.BackpackPlanner.Droid
{
    internal sealed class DroidPlayServicesManager : PlayServicesManager
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(DroidPlayServicesManager));

        public const int GetAccountsRequestCode = 1000;

        // this is necessary because the listener interfaces require implementing
        // Java.Lang.Object while the manager needs to implement PlayServicesManager
        // TODO: this could be made more general-use by adding versions of the interfaces
        // that don't require implementing Java.Lang.Object and hooking into those
        private sealed class PlayServicesConnectionListener : Java.Lang.Object, GoogleApiClient.IOnConnectionFailedListener
        {
            private readonly DroidPlayServicesManager _playServicesManager;

            public PlayServicesConnectionListener(DroidPlayServicesManager playServicesManager)
            {
                _playServicesManager = playServicesManager;
            }

            public void OnConnectionFailed(ConnectionResult result)
            {
                _playServicesManager.OnConnectionFailed(result);
            }
        }

        public override bool IsConnected => null != _googleClientApi && _googleClientApi.IsConnected;

        private GoogleApiClient _googleClientApi;

        private readonly PlayServicesConnectionListener _connectionListener;

        private BaseActivity _activity;

        public DroidPlayServicesManager()
        {
            _connectionListener = new PlayServicesConnectionListener(this);
        }

        public override void Init()
        {
        }

        public void OnCreate(BaseActivity activity)
        {
            if(IsEnabled && null == _googleClientApi) {
                _activity = activity;

                Logger.Debug($"{_activity.GetType()} Building Google API Client...");
                _googleClientApi = new GoogleApiClient.Builder(activity)
                    .EnableAutoManage(activity, _connectionListener)
                    .AddApi(DriveClass.API)
                    .AddScope(DriveClass.ScopeFile)
                    .AddScope(DriveClass.ScopeAppfolder)
                    .Build();
            }

// TODO: is this the bit that prompts for the user's login? if so, can we save it here?
        }

        public override async Task ConnectAsync()
        {
            await Task.Delay(0).ConfigureAwait(false);

            /*Logger.Info("Connecting Google Play Services...");
            if(null != _googleClientApi) {
                _googleClientApi.Connect();
            }*/

            // using auto-managed connection, which should have connected in the base OnStart()
            OnConnected(new PlayServicesConnectedEventArgs { IsSuccess = IsConnected });
        }

        public override async Task DisconnectAsync()
        {
            await Task.Delay(0).ConfigureAwait(false);

            if(!IsConnected) {
                return;
            }

            /*Logger.Info($"Disconnecting Google Play Services client...");
            if(null != _googleClientApi) {
                _googleClientApi.Disconnect();
            }*/
        }

        public void OnConnectionFailed(ConnectionResult result)
        {
            // auto-managed connection failures are always unresolvable
            Logger.Debug("Google Play Services connection failure has no resolution, showing error dialog...");
            GoogleApiAvailability.Instance.GetErrorDialog(_activity, result.ErrorCode, 0).Show();

            OnConnected(new PlayServicesConnectedEventArgs { IsSuccess = false });
        }

#region appfolder Management
// https://www.youtube.com/watch?v=UiTHG_yl-jA
// https://github.com/athingunique/Drive-Database-Sync
// https://developers.google.com/drive/android/appfolder

        // https://developers.google.com/drive/android/queries
        private async Task<Metadata> QueryFileInDriveAppFolderAsync(string title)
        {
            if(!IsConnected) {
                return null;
            }

            QueryClass query = new QueryClass.Builder()
                .AddFilter(Filters.Eq(SearchableField.Title, title))
                .Build();

            IDriveApiMetadataBufferResult metadataBufferResult = await DriveClass.DriveApi
                .GetAppFolder(_googleClientApi)
                .QueryChildrenAsync(_googleClientApi, query).ConfigureAwait(false);
            if(!metadataBufferResult.Status.IsSuccess) {
                Logger.Error($"{_activity.GetType()} Failed to query drive file: {metadataBufferResult.Status.StatusMessage}");
                return null;
            }

            return metadataBufferResult.MetadataBuffer.Count < 1 ? null : metadataBufferResult.MetadataBuffer.ElementAt(0);
        }

        public override async Task<bool> HasFileInDriveAppFolderAsync(string title)
        {
            return null != await QueryFileInDriveAppFolderAsync(title).ConfigureAwait(false);
        }

        public override async Task<bool> SaveFileToDriveAppFolderAsync(string title, string contentType, Stream contentStream)
        {
            if(!IsConnected) {
                return false;
            }

            IDriveApiDriveContentsResult driveContentsResult = await DriveClass.DriveApi
                .NewDriveContentsAsync(_googleClientApi).ConfigureAwait(false);
            if(!driveContentsResult.Status.IsSuccess) {
                Logger.Error($"{_activity.GetType()} Failed to get drive contents for new appfolder file: {driveContentsResult.Status.StatusMessage}");
                return false;
            }
            IDriveContents driveContents = driveContentsResult.DriveContents;

            await contentStream.CopyToAsync(driveContents.OutputStream);

            MetadataChangeSet changeSet = new MetadataChangeSet.Builder()
                .SetTitle(title)
                .SetMimeType(contentType)
                .Build();

            IDriveFolderDriveFileResult driveFileResult = await DriveClass.DriveApi
                .GetAppFolder(_googleClientApi)
                .CreateFileAsync(_googleClientApi, changeSet, driveContents).ConfigureAwait(false);
            if(!driveFileResult.Status.IsSuccess) {
                Logger.Error($"{_activity.GetType()} Failed to create new appfolder file: {driveFileResult.Status.StatusMessage}");
                return false;
            }

            return true;
        }

        public override async Task<bool> UpdateFileInDriveAppFolderAsync(string title, string contentType, Stream contentStream)
        {
            if(!IsConnected) {
                return false;
            }

            Metadata metadata = await QueryFileInDriveAppFolderAsync(title).ConfigureAwait(false);
            if(null == metadata) {
                Logger.Error($"{_activity.GetType()} No such file to update in appfolder: {title}!");
                return false;
            }
            IDriveFile driveFile = metadata.DriveId.AsDriveFile();

            IDriveApiDriveContentsResult driveContentsResult = await driveFile.OpenAsync(_googleClientApi, DriveFile.ModeWriteOnly, null).ConfigureAwait(false);
            if(!driveContentsResult.Status.IsSuccess) {
                Logger.Error($"{_activity.GetType()} Failed to get drive contents for appfolder file {title}: {driveContentsResult.Status.StatusMessage}");
                return false;
            }
            IDriveContents driveContents = driveContentsResult.DriveContents;

            await contentStream.CopyToAsync(driveContents.OutputStream);

            await driveContents.CommitAsync(_googleClientApi, null).ConfigureAwait(false);
            return true;
        }

        public override async Task<Stream> DownloadFileFromDriveAppFolderAsync(string title)
        {
            if(!IsConnected) {
                return null;
            }

await Task.Delay(0).ConfigureAwait(false);

// http://developer.android.com/guide/topics/data/data-storage.html
// http://stackoverflow.com/questions/3425906/creating-temporary-files-in-android
throw new NotImplementedException();
        }

        public override async Task DeleteFileFromDriveAppFolderAsync(string title)
        {
            if(!IsConnected) {
                return;
            }

await Task.Delay(0).ConfigureAwait(false);

throw new NotImplementedException();
        }
#endregion
    }
}
