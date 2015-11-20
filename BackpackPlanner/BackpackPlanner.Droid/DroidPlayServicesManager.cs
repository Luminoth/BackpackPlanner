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
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Drive;
using Android.OS;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Core.PlayServices;
using EnergonSoftware.BackpackPlanner.Models;

namespace EnergonSoftware.BackpackPlanner.Droid
{
    internal sealed class DroidPlayServicesManager : PlayServicesManager
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(PlayServicesManager));

        // error resolution
        public const int RequestCodeResolveError = 9001;

        private const string StateResolvingError = "play_services_manager_resolving_error";

        // this is necessary because the listener needs to implement
        // Java.Lang.Object while the manager needs to implement PlayServicesManager
        private sealed class PlayServicesConnectionListener : Java.Lang.Object,
            GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener
        {
            private readonly DroidPlayServicesManager _playServicesManager;

            public PlayServicesConnectionListener(DroidPlayServicesManager playServicesManager)
            {
                _playServicesManager = playServicesManager;
            }

	        public void OnConnected(Bundle connectionHint)
            {
                _playServicesManager.OnConnected(connectionHint);
            }

	        public void OnConnectionSuspended(int cause)
            {
                _playServicesManager.OnConnectionSuspended(cause);
            }

	        public void OnConnectionFailed(ConnectionResult result)
            {
                _playServicesManager.OnConnectionFailed(result);
            }
        }

// https://developers.google.com/drive/android/java-client
// https://www.youtube.com/watch?v=UiTHG_yl-jA
// https://github.com/athingunique/Drive-Database-Sync
// https://github.com/googledrive/android-demos/blob/master/app/src/main/java/com/google/android/gms/drive/sample/demo/CreateFileInAppFolderActivity.java
// https://developers.google.com/drive/android/appfolder

        public override bool IsConnected => null != _googleClientApi && _googleClientApi.IsConnected;

        public bool IsResolvingError { get; private set; }

        private readonly Stopwatch _connectStopwatch = new Stopwatch();

        private readonly Activity _activity;

        private GoogleApiClient _googleClientApi;

        private readonly PlayServicesConnectionListener _connectionListener;

        public DroidPlayServicesManager(Activity activity)
        {
            if(null == activity) {
                throw new ArgumentNullException(nameof(activity));
            }

            _activity = activity;
            _connectionListener = new PlayServicesConnectionListener(this);
        }

        public override async Task InitAsync()
        {
            if(null == _googleClientApi) {
                Logger.Debug("Building Google API Client...");
                _googleClientApi = new GoogleApiClient.Builder(_activity)
                    .AddApi(DriveClass.API)
                    .AddScope(DriveClass.ScopeFile)
                    .AddScope(DriveClass.ScopeAppfolder)
                    .AddConnectionCallbacks(_connectionListener)
                    .AddOnConnectionFailedListener(_connectionListener)
                    .Build();
            }

// TODO: is this the bit that prompts for the user's login? if so, can we save it here?

            await Task.Delay(0).ConfigureAwait(false);
        }

        public override async Task DestroyAsync()
        {
            await base.DestroyAsync().ConfigureAwait(false);

            _googleClientApi = null;
        }

        public override async Task ConnectAsync()
        {
            await Task.Delay(0).ConfigureAwait(false);

            if(null == _googleClientApi) {
                Logger.Warn("Google Play Services client is null on connect!");
                return;
            }

            if(IsConnected) {
                Logger.Info("Google API already connected!");
                return;
            }

            Logger.Info("Connecting Google Play Services client...");
            _connectStopwatch.Start();
            _googleClientApi.Connect();
        }

        public override async Task DisconnectAsync()
        {
            await Task.Delay(0).ConfigureAwait(false);

            if(!IsConnected) {
                return;
            }

            Logger.Info("Disonnecting Google Play Services client...");
            _googleClientApi.Disconnect();
        }

#region Connect Callbacks
	    private void OnConnected(Bundle connectionHint)
	    {
            _connectStopwatch.Stop();
	        Logger.Info($"Google Play Services connected in {_connectStopwatch.ElapsedMilliseconds}ms!");

// TODO: can we save the user's login here?

            OnConnected(new PlayServicesConnectedEventArgs { IsSuccess= true });
	    }

	    private void OnConnectionSuspended(int cause)
	    {
	        Logger.Info($"Google Play Services suspended: {cause}");
	    }

	    private void OnConnectionFailed(ConnectionResult result)
	    {
            _connectStopwatch.Stop();
            Logger.Warn($"Google Play Services connection failed after {_connectStopwatch.ElapsedMilliseconds}ms: {result}");

            if(IsResolvingError) {
                return;
            }

            if(!result.HasResolution) {
                Logger.Debug("Google Play Services connection failure has no resolution, showing error dialog...");
// TODO: if the error here is service missing, bad things happen (as per the emulator behavior)
                IsResolvingError = true;
                GoogleApiAvailability.Instance.GetErrorDialog(_activity, result.ErrorCode, 0).Show();
                IsResolvingError = false;

                OnConnected(new PlayServicesConnectedEventArgs { IsSuccess = false });
                return;
            }

            try {
                IsResolvingError = true;

                Logger.Debug("Starting Google Play Services connection failure resolution activity...");
                result.StartResolutionForResult(_activity, RequestCodeResolveError);
            } catch(IntentSender.SendIntentException) {
                Logger.Error("Exception while starting resolution activity, retrying connection...");
                ConnectAsync().Wait();
            }
	    }
#endregion

#region appfolder Management
        public override async Task<bool> SaveFileToDriveAppFolderAsync(string title, string contentType, Stream contentStream)
        {
            IDriveApiDriveContentsResult driveContentsResult = await DriveClass.DriveApi.NewDriveContentsAsync(_googleClientApi).ConfigureAwait(false);
            if(!driveContentsResult.Status.IsSuccess) {
                Logger.Error($"Failed to get drive contents: {driveContentsResult.Status.StatusMessage}");
                return false;
            }

            IDriveContents driveContents = driveContentsResult.DriveContents;

            MetadataChangeSet changeSet = new MetadataChangeSet.Builder()
                .SetTitle(title)
                .SetMimeType(contentType)
                .Build();

            IDriveFolderDriveFileResult driveFileResult = await DriveClass.DriveApi
                .GetAppFolder(_googleClientApi)
                .CreateFileAsync(_googleClientApi, changeSet, driveContents).ConfigureAwait(false);
            if(!driveFileResult.Status.IsSuccess) {
                Logger.Error($"Failed to get drive contents: {driveFileResult.Status.StatusMessage}");
                return false;
            }

            await contentStream.CopyToAsync(driveContents.OutputStream);

            await driveContents.CommitAsync(_googleClientApi, null).ConfigureAwait(false);
            return true;
        }

        public override Task<bool> UpdateFileInDriveAppFolderAsync(string fileId, string title, string contentType, Stream contentStream)
        {
            //await DriveClass.DriveApi.GetAppFolder(_googleClientApi).QueryChildrenAsync(_googleClientApi, new QueryClass());
throw new NotImplementedException();
        }

        public override Task<Stream> DownloadFileFromDriveAppFolderAsync(string fileId)
        {
throw new NotImplementedException();
        }

        public override Task DeleteFileFromDriveAppFolderAsync(string fileId)
        {
throw new NotImplementedException();
        }
#endregion

#region Activity Lifecycle Hooks
        public void OnCreate(Bundle savedInstanceState)
        {
            if(null != savedInstanceState) {
                IsResolvingError = savedInstanceState.GetBoolean(StateResolvingError, false);
            }
        }

        public void OnSaveInstanceState(Bundle outState)
        {
            outState.PutBoolean(StateResolvingError, IsResolvingError);
        }

        public void OnActivityResult(int requestCode, Result resultCode, Intent data)
	    {
            switch(requestCode)
            {
            case RequestCodeResolveError:
                IsResolvingError = false;
                if(Result.Ok == resultCode) {
                    Logger.Info("Got Google Play Services Ok result code, retrying connection...");
                    ConnectAsync().Wait();
                }
                break;
            }
	    }
#endregion
    }
}
