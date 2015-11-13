using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2;
using Google.Apis.Services;
using Google.Apis.Util.Store;

using log4net;

namespace EnergonSoftware.BackpackPlanner.DriveFileExplorer
{
    public class PlayServicesManager
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(PlayServicesManager));

        public bool IsConnected { get; private set; }

        public bool IsNotConnect => !IsConnected;

        private UserCredential _userCredential;

        private DriveService _driveService;

// https://github.com/LindaLawton/Google-Dotnet-Samples/blob/master/Google-Drive/Google-Drive-Api-dotnet/Google-Drive-Api-dotnet/Authentication.cs
// http://stackoverflow.com/questions/24386905/how-to-download-file-from-google-drive-in-wpf-installed-application

        public async Task ConnectAsync()
        {
            Logger.Info("Connecting to Google Services...");

            try {
                using(Stream stream = new FileStream("C:/EnergonSoftware/google_play_client_secrets.json", FileMode.Open, FileAccess.Read)) {
                    _userCredential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        new[] {
                            DriveService.Scope.DriveFile,
                            "https://www.googleapis.com/auth/drive.appfolder"
                        },
                        Properties.Settings.Default.GooglePlayServicesUser,
                        CancellationToken.None,
                        new FileDataStore("EnergonSoftware.DriveFileExplorer.Auth.Store")).ConfigureAwait(false);
                }

                if(null == _userCredential) {
                    Logger.Error("Failed to create user credentials!");

                    IsConnected = false;
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
                IsConnected = true;
            } catch(Exception ex) {
                Logger.Error($"Error connecting to Google Services: {ex.Message}", ex);

                _userCredential = null;
                _driveService = null;

                IsConnected = false;
            }
        }

        public async Task DisconnectAsync()
        {
            Logger.Info("Disconnecting from Google Services...");

            _userCredential = null;
            _driveService = null;

await Task.Delay(0).ConfigureAwait(false);

            IsConnected = false;
        }
    }
}
