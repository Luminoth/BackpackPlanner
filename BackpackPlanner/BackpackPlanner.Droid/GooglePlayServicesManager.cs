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
    public sealed class GooglePlayServicesManager : Java.Lang.Object, IPlayServices,
        IGoogleApiClientConnectionCallbacks, IGoogleApiClientOnConnectionFailedListener
    {
        // error resolution
        public const int RequestCodeResolution = 9001;

        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(GooglePlayServicesManager));

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

                Logger.Debug("Got a Google Drive contents result!");
	        }
        }

        public event EventHandler<EventArgs> PlayServicesConnectedEvent;

        // TODO: this should go in the base class to time the lifecycle
        private readonly Stopwatch _connectStopwatch = new Stopwatch();

        private Activity _activity;

        private IGoogleApiClient _googleClientApi;

        public bool Init(Activity activity)
        {
            _activity = activity;
            return Init();
        }

        public bool Init()
        {
            if(null == _activity) {
                return false;
            }

            if(null == _googleClientApi) {
                Logger.Debug("Building Google API Client...");
                _googleClientApi = new GoogleApiClientBuilder(_activity)
                    .AddApi(DriveClass.API)
                    .AddScope(DriveClass.ScopeFile)
                    .AddScope(DriveClass.ScopeAppfolder)
                    .AddConnectionCallbacks(this)
                    .AddOnConnectionFailedListener(this)
                    .Build();
            }

            return true;
        }

        public void Destroy()
        {
            if(null != _googleClientApi) {
                _googleClientApi.Disconnect();
                _googleClientApi = null;
            }
        }

        public void Connect()
        {
            if(_googleClientApi.IsConnected) {
                Logger.Info("Google API already connected!");
            } else {
                Logger.Info("Connecting Google Play Services client...");
                _connectStopwatch.Start();
                _googleClientApi.Connect();
            }
        }

        public void Disconnect()
        {
            Logger.Info("Disonnecting Google Play Services client...");
            _googleClientApi.Disconnect();
        }

	    public void OnConnected(Bundle connectionHint)
	    {
            _connectStopwatch.Stop();
	        Logger.Info($"Google Play Services connected in {_connectStopwatch.ElapsedMilliseconds}ms!");

            DriveClass.DriveApi.NewDriveContents(_googleClientApi).SetResultCallback(new DriveContentsResultCallback());

            PlayServicesConnectedEvent?.Invoke(this, new EventArgs());
	    }

	    public void OnConnectionSuspended(int cause)
	    {
	        Logger.Info($"Google Play Services suspended: {cause}");
	    }

	    public void OnConnectionFailed(ConnectionResult result)
	    {
            _connectStopwatch.Stop();
            Logger.Warn($"Google Play Services connection failed after {_connectStopwatch.ElapsedMilliseconds}ms: {result}");

            if(!result.HasResolution) {
                Logger.Debug("Google Play Services connection failure has no resolution, showing error dialog");
                GoogleApiAvailability.Instance.GetErrorDialog(_activity, result.ErrorCode, 0).Show();

                PlayServicesConnectedEvent?.Invoke(this, new EventArgs());
                return;
            }

            try {
                Logger.Debug("Starting Google Play Services connection failure resolution activity...");
                result.StartResolutionForResult(_activity, RequestCodeResolution);
            } catch(IntentSender.SendIntentException ex) {
                Logger.Error("Exception while starting resolution activity!", ex);
                PlayServicesConnectedEvent?.Invoke(this, new EventArgs());
            }
	    }
    }
}
