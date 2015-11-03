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

using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

using EnergonSoftware.BackpackPlanner.Commands;
using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Models.Gear.Items;

namespace EnergonSoftware.BackpackPlanner.Windows.Pages.Gear.Items
{
    public sealed partial class GearItemsPage
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(GearItemsPage));

        public GearItemsPage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Progress.Visibility = Visibility.Visible;

            var command = new GetItemsCommand<GearItem>();
            await command.DoActionAsync(App.CurrentApp.BackpackPlannerState.DatabaseState, App.CurrentApp.BackpackPlannerState.Settings);

            Logger.Debug($"Read {command.Items.Count} items...");
            if(command.Items.Count < 1) {
                NoItems.Visibility = Visibility.Visible;
                SortBy.Visibility = Visibility.Collapsed;
                ItemsListView.Visibility = Visibility.Collapsed;
                return;
            }

            NoItems.Visibility = Visibility.Collapsed;
            SortBy.Visibility = Visibility.Visible;
            ItemsListView.Visibility = Visibility.Visible;

            foreach(GearItem item in command.Items) {
                ItemsListView?.Items?.Add(item);
            }

            Progress.Visibility = Visibility.Collapsed;
        }
    }
}
