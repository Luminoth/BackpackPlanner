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
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using EnergonSoftware.BackpackPlanner.Commands;
using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Models;

namespace EnergonSoftware.BackpackPlanner.Windows.Pages
{
    public abstract class ListItemsPage<T> : BasePage where T: DatabaseItem, new()
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(ListItemsPage<>));

        protected abstract ProgressRing LoadProgressRing { get; }

        protected abstract TextBlock NoItemsTextBlock { get; }

        protected abstract TextBlock SortByTextBlock { get; }

        protected abstract ListView ItemsListView { get; }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            LoadProgressRing.Visibility = Visibility.Visible;

            var command = new GetItemsCommand<T>();
            await command.DoActionAsync(App.CurrentApp.BackpackPlannerState);

            Logger.Debug($"Read {command.Items.Count} items...");
            if(command.Items.Count < 1) {
                NoItemsTextBlock.Visibility = Visibility.Visible;
                SortByTextBlock.Visibility = Visibility.Collapsed;
                ItemsListView.Visibility = Visibility.Collapsed;
                return;
            }

            NoItemsTextBlock.Visibility = Visibility.Collapsed;
            SortByTextBlock.Visibility = Visibility.Visible;
            ItemsListView.Visibility = Visibility.Visible;

            // add the items to the list view
            foreach(T item in command.Items) {
                ItemsListView.Items?.Add(item);
            }

            // necessary to update the layout before we can update values
            ItemsListView.UpdateLayout();

            UpdateValues();

            LoadProgressRing.Visibility = Visibility.Collapsed;
        }

        protected abstract void UpdateValues();
    }
}
