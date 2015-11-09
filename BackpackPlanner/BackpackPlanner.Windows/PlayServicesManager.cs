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
using System.Threading;
using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Core.PlayServices;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using Google.Apis.Services;

namespace EnergonSoftware.BackpackPlanner.Windows
{
    internal sealed class PlayServicesManager : IPlayServicesManager
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(PlayServicesManager));

#region Events
        public event EventHandler<PlayServicesConnectedEventArgs> PlayServicesConnectedEvent;
#endregion

// https://developers.google.com/drive/web/appdata

        private UserCredential _userCredential;

        private DriveService _driveService;

        public async Task InitAsync()
        {
            await Task.Delay(0).ConfigureAwait(false);
        }

        public async Task DestroyAsync()
        {
            await DisconnectAsync().ConfigureAwait(false);

            _userCredential = null;
            _driveService = null;
        }

        public async Task ConnectAsync()
        {
            try {
                _userCredential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    new Uri("ms-appx:///Assets/google_play_client_secrets.json"),
                    new[] {
                        DriveService.Scope.DriveFile,
                        "https://www.googleapis.com/auth/drive.appfolder" },
                    App.CurrentApp.BackpackPlannerState.Settings.GooglePlayServicesUser,
                    CancellationToken.None).ConfigureAwait(false);
                if(null == _userCredential) {
                    PlayServicesConnectedEvent?.Invoke(this, new PlayServicesConnectedEventArgs { IsSuccess = false });
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

                PlayServicesConnectedEvent?.Invoke(this, new PlayServicesConnectedEventArgs { IsSuccess = true });

File appFolder = await _driveService.Files.Get("appfolder").ExecuteAsync();
            } catch(Exception ex) {
                Logger.Error($"Error connecting to Google Services: {ex.Message}", ex);
                _userCredential = null;
                _driveService = null;
                PlayServicesConnectedEvent?.Invoke(this, new PlayServicesConnectedEventArgs { IsSuccess = false });
            }
        }

        public async Task DisconnectAsync()
        {
            await Task.Delay(0).ConfigureAwait(false);
        }
    }
}
