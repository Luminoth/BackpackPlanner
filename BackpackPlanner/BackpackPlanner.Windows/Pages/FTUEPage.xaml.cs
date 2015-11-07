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

using Windows.UI.Popups;
using Windows.UI.Xaml;

using EnergonSoftware.BackpackPlanner.Core.Logging;

namespace EnergonSoftware.BackpackPlanner.Windows.Pages
{
    // ReSharper disable once InconsistentNaming
    public sealed partial class FTUEPage
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(FTUEPage));

        public FTUEPage()
        {
            InitializeComponent();
        }

        private void SkipButton_Click(object sender, RoutedEventArgs e)
        {
            FinishFTUE();
        }

        // ReSharper disable once InconsistentNaming
        private async void FinishFTUE()
        {
            MessageDialog dialog = new MessageDialog("test", "test");
            dialog.Commands.Add(new UICommand { Label = "Yes", Id = 0 });
            dialog.Commands.Add(new UICommand { Label = "No", Id = 1 });

            IUICommand result = await dialog.ShowAsync().AsTask();
            switch((int)result.Id)
            {
            case 0: // Yes
                Logger.Debug("User accepted Google Play Services");
                App.CurrentApp.BackpackPlannerState.Settings.ConnectGooglePlayServices = true;

                // try to connect now to get all of the confirmations out of the way
                // TODO: show progress bar
                App.CurrentApp.BackpackPlannerState.PlatformPlayServicesManager.PlayServicesConnectedEvent += (s, a) => {
                    // TODO: don't need this event under windows, so really should add an "IsConnected" to the play services manager
                    // and then log the result after Connect() finishes
                    Logger.Debug($"Google Play Services connected (success: {a.IsSuccess}), finishing activity...");
                };
                await App.CurrentApp.BackpackPlannerState.PlatformPlayServicesManager.ConnectAsync();
                // TODO: hide progress bar
                break;
            case 1: // No
                Logger.Debug("User declined Google Play Services");
                App.CurrentApp.BackpackPlannerState.Settings.ConnectGooglePlayServices = false;
                break;
            }

            Frame.Navigate(typeof(MainPage));
        }
    }
}
