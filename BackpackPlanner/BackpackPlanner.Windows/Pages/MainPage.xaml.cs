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

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using EnergonSoftware.BackpackPlanner.Windows.Pages.Gear.Collections;
using EnergonSoftware.BackpackPlanner.Windows.Pages.Gear.Items;
using EnergonSoftware.BackpackPlanner.Windows.Pages.Gear.Systems;
using EnergonSoftware.BackpackPlanner.Windows.Pages.Meals;
using EnergonSoftware.BackpackPlanner.Windows.Pages.Trips.Itineraries;
using EnergonSoftware.BackpackPlanner.Windows.Pages.Trips.Plans;

namespace EnergonSoftware.BackpackPlanner.Windows.Pages
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

#if !DEBUG
            NavMenu.Items?.Remove(TripItinerariesItem);
#endif

#if !DEBUG_FRAGMENT
            NavMenu.Items?.Remove(DebugItem);
#endif
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if(App.CurrentApp.BackpackPlannerState.PlatformPlayServicesManager.IsEnabled && App.CurrentApp.BackpackPlannerState.Settings.ConnectGooglePlayServices) {
                await App.CurrentApp.BackpackPlannerState.PlatformPlayServicesManager.ConnectAsync();
            }

            ContentFrame.Navigate(typeof(GearItemsPage));
        }

        private void NavMenuItem_Click(object sender, ItemClickEventArgs e)
        {
            Panel item = e.ClickedItem as Panel;
            if(null == item) {
                return;
            }

            if(item.Name == MenuItem.Name) {
                ContentView.IsPaneOpen = !ContentView.IsPaneOpen;
                return;
            }

            if(item.Name == GearItemsMenuItem.Name) {
                ContentFrame.Navigate(typeof(GearItemsPage));
            } else if(item.Name == GearSystemsMenuItem.Name) {
                ContentFrame.Navigate(typeof(GearSystemsPage));
            } else if(item.Name == GearCollectionsMenuItem.Name) {
                ContentFrame.Navigate(typeof(GearCollectionsPage));
            } else if(item.Name == MealsMenuItem.Name) {
                ContentFrame.Navigate(typeof(MealsPage));
            } else if(item.Name == TripItinerariesMenuItem.Name) {
                ContentFrame.Navigate(typeof(TripItinerariesPage));
            } else if(item.Name == TripPlansMenuItem.Name) {
                ContentFrame.Navigate(typeof(TripPlansPage));
            } else if(item.Name == SettingsMenuItem.Name) {
                ContentFrame.Navigate(typeof(SettingsPage));
            } else if(item.Name == HelpMenuItem.Name) {
                ContentFrame.Navigate(typeof(HelpPage));
            } else if(item.Name == DebugMenuItem.Name) {
                ContentFrame.Navigate(typeof(DebugPage));
            }

            ContentView.IsPaneOpen = false;
        }
    }
}
