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
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Drive;
using Android.OS;
using Android.Runtime;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Core.PlayServices;

namespace EnergonSoftware.BackpackPlanner.Droid
{
    internal sealed class PlayServicesManager : Java.Lang.Object, IPlayServicesManager,
        GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener
    {
        // error resolution
        public const int RequestCodeResolveError = 9001;

        private const string StateResolvingError = "play_services_manager_resolving_error";

        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(PlayServicesManager));

// https://developers.google.com/drive/android/java-client
// https://www.youtube.com/watch?v=UiTHG_yl-jA
// https://github.com/athingunique/Drive-Database-Sync

        // TODO: continue with this
        private class DriveContentsResultCallback : Java.Lang.Object, IResultCallback
        {
	        public void OnResult(Java.Lang.Object result)
	        {
// TODO: https://github.com/googledrive/android-demos/blob/master/app/src/main/java/com/google/android/gms/drive/sample/demo/CreateFileInAppFolderActivity.java
                IDriveApiDriveContentsResult contentsResult = result.JavaCast<IDriveApiDriveContentsResult>();
                if(null == contentsResult) {
                    // TODO: error
                    Logger.Error("Null Google Drive content result?");
                    return;
                }

// https://developers.google.com/drive/android/appfolder

                Logger.Debug($"Got a Google Drive contents result: {contentsResult.Status.IsSuccess}");
	        }
        }

#region Events
        public event EventHandler<PlayServicesConnectedEventArgs> PlayServicesConnectedEvent;
#endregion

        public bool IsResolvingError { get; private set; }

        // TODO: this should go in the base class to time the lifecycle
        private readonly Stopwatch _connectStopwatch = new Stopwatch();

        private readonly Activity _activity;

        private GoogleApiClient _googleClientApi;

        public PlayServicesManager(Activity activity)
        {
            if(null == activity) {
                throw new ArgumentNullException(nameof(activity));
            }

            _activity = activity;
        }

        public async Task InitAsync()
        {
            if(null == _googleClientApi) {
                Logger.Debug("Building Google API Client...");
                _googleClientApi = new GoogleApiClient.Builder(_activity)
                    .AddApi(DriveClass.API)
                    .AddScope(DriveClass.ScopeFile)
                    .AddScope(DriveClass.ScopeAppfolder)
                    .AddConnectionCallbacks(this)
                    .AddOnConnectionFailedListener(this)
                    .Build();
            }

// TODO: is this the bit that prompts for the user's login? if so, can we save it here?

            await Task.Delay(0).ConfigureAwait(false);
        }

        public async Task DestroyAsync()
        {
            await DisconnectAsync().ConfigureAwait(false);

            _googleClientApi = null;
        }

        public async Task ConnectAsync()
        {
            await Task.Delay(0).ConfigureAwait(false);

            if(null == _googleClientApi) {
                Logger.Warn("Google Play Services client is null on connect!");
                return;
            }

            if(_googleClientApi.IsConnected) {
                Logger.Info("Google API already connected!");
            } else {
                Logger.Info("Connecting Google Play Services client...");
                _connectStopwatch.Start();
                _googleClientApi.Connect();
            }
        }

        public async Task DisconnectAsync()
        {
            await Task.Delay(0).ConfigureAwait(false);

            if(null != _googleClientApi && _googleClientApi.IsConnected) {
                Logger.Info("Disonnecting Google Play Services client...");
                _googleClientApi.Disconnect();
            }
        }

	    public void OnConnected(Bundle connectionHint)
	    {
            _connectStopwatch.Stop();
	        Logger.Info($"Google Play Services connected in {_connectStopwatch.ElapsedMilliseconds}ms!");

// TODO: ican we save the user's login here?

            PlayServicesConnectedEvent?.Invoke(this, new PlayServicesConnectedEventArgs { IsSuccess= true });

DriveClass.DriveApi.NewDriveContents(_googleClientApi).SetResultCallback(new DriveContentsResultCallback());
	    }

	    public void OnConnectionSuspended(int cause)
	    {
	        Logger.Info($"Google Play Services suspended: {cause}");
	    }

	    public void OnConnectionFailed(ConnectionResult result)
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

                PlayServicesConnectedEvent?.Invoke(this, new PlayServicesConnectedEventArgs { IsSuccess = false });
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
    }
}
