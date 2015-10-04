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

using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Drive;
using Android.OS;
using Android.Runtime;

using EnergonSoftware.BackpackPlanner.Core.Logging;

namespace EnergonSoftware.BackpackPlanner.Droid
{
    public sealed class GooglePlayServicesManager : Java.Lang.Object, IGoogleApiClientConnectionCallbacks, IGoogleApiClientOnConnectionFailedListener
    {
        // error resolution
        private const int RequestCodeResolution = 9001;

        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(GooglePlayServicesManager));

        private class DriveContentsResultCallback : Java.Lang.Object, IResultCallback
        {
	        public void OnResult(Java.Lang.Object result)
	        {
// TODO: https://github.com/googledrive/android-demos/blob/master/app/src/main/java/com/google/android/gms/drive/sample/demo/CreateFileInAppFolderActivity.java
                IDriveApiDriveContentsResult contentsResult = result.JavaCast<IDriveApiDriveContentsResult>();
                if(null == contentsResult) {
                    // TODO: error
                    Logger.Error("Null content result?");
                    return;
                }

// https://developers.google.com/drive/android/appfolder

                Logger.Debug("Got a contents result!");
	        }
        }

        private readonly Activity _activity;

        private IGoogleApiClient _googleClientApi;

        public GooglePlayServicesManager(Activity activity)
        {
            if(null == activity) {
                throw new ArgumentNullException(nameof(activity));
            }

            _activity = activity;
        }

        public void OnCreate()
        {
            Logger.Debug("Building Google API Client...");
            _googleClientApi = new GoogleApiClientBuilder(_activity)
                .AddApi(DriveClass.API)
                .AddScope(DriveClass.ScopeFile)
                .AddScope(DriveClass.ScopeAppfolder)
                .AddConnectionCallbacks(this)
                .AddOnConnectionFailedListener(this)
                .Build();
        }

        public void OnStart()
        {
            Logger.Info("Connecting Google Play Services client...");
            _googleClientApi.Connect();
        }

        public void OnStop()
        {
            Logger.Info("Disonnecting Google Play Services client...");
            _googleClientApi.Disconnect();
        }

        public void OnResume()
        {
        }

        public void OnPause()
        {
        }

	    public void OnActivityResult(int requestCode, Result resultCode, Intent data)
	    {
            switch(requestCode)
            {
            case RequestCodeResolution:
                if(Result.Ok == resultCode) {
                    Logger.Info("Connecting Google Play Services client...");
                    _googleClientApi.Connect();
                }
                break;
            }
	    }

	    public void OnConnected(Bundle connectionHint)
	    {
	        Logger.Info("Google Play Services connected!");
            DriveClass.DriveApi.NewDriveContents(_googleClientApi).SetResultCallback(new DriveContentsResultCallback());
	    }

	    public void OnConnectionSuspended(int cause)
	    {
	        Logger.Info($"Google Play Services suspended: {cause}");
	    }

	    public void OnConnectionFailed(ConnectionResult result)
	    {
            Logger.Info($"Google Play Services connection failed: {result}");
            if(!result.HasResolution) {
                GoogleApiAvailability.Instance.GetErrorDialog(_activity, result.ErrorCode, 0).Show();
                return;
            }

            try {
                result.StartResolutionForResult(_activity, RequestCodeResolution);
            } catch (IntentSender.SendIntentException ex) {
                Logger.Error("Exception while starting resolution activity", ex);
            }
	    }
    }
}
