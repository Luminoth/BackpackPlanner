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
using System.Threading.Tasks;

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
            await ConnectGooglePlayServices().ConfigureAwait(false);

            Frame.Navigate(typeof(MainPage));
        }

        private async Task ConnectGooglePlayServices()
        {
            if(!App.CurrentApp.BackpackPlannerState.PlatformPlayServicesManager.IsEnabled) {
                return;
            }

            // TODO: these strings (including the command labels) should come from the resource file
            MessageDialog dialog = new MessageDialog("Connect to Google Play Services?", "Backpacking Planner can take advantage of Google Play Services to sync your data (some day). Would you like to allow this?");
            dialog.Commands.Add(new UICommand { Label = "Yes", Id = 0 });
            dialog.Commands.Add(new UICommand { Label = "No", Id = 1 });

            IUICommand result = await dialog.ShowAsync().AsTask().ConfigureAwait(false);
            switch((int)result.Id)
            {
            case 0: // Yes
                Logger.Debug("User accepted Google Play Services");
                App.CurrentApp.BackpackPlannerState.Settings.ConnectGooglePlayServices = true;

                // try to connect now to get all of the confirmations out of the way
                // TODO: show progress bar
                await App.CurrentApp.BackpackPlannerState.PlatformPlayServicesManager.ConnectAsync().ConfigureAwait(false);
                // TODO: validate connect success
                Logger.Debug($"Google Play Services connected, finishing activity...");
                // TODO: hide progress bar
                break;
            case 1: // No
                Logger.Debug("User declined Google Play Services");
                App.CurrentApp.BackpackPlannerState.Settings.ConnectGooglePlayServices = false;
                break;
            }
        }
    }
}
