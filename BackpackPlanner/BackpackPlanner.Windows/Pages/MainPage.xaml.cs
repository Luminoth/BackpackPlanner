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

using EnergonSoftware.BackpackPlanner.Windows.Frames.Gear;

namespace EnergonSoftware.BackpackPlanner.Windows.Pages
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

#if DEBUG
            NavMenu?.Items?.Remove(TripItinerariesItem);
            NavMenu?.Items?.Remove(DebugItem);
#endif
        }

        private void NavMenuItem_Click(object sender, ItemClickEventArgs e)
        {
            Panel item = e.ClickedItem as Panel;
            if(null == item) {
                return;
            }

            if(item.Name == MenuItem.Name) {
                ContentView.IsPaneOpen = !ContentView.IsPaneOpen;
            } else if(item.Name == GearItemsMenuItem.Name) {
// TODO: this crashes!
                ContentFrame.Navigate(typeof(GearItemsFrame));
            } else if(item.Name == GearSystemsMenuItem.Name) {
// TODO
            } else if(item.Name == GearCollectionsMenuItem.Name) {
// TODO
            } else if(item.Name == MealsMenuItem.Name) {
// TODO
            } else if(item.Name == TripItinerariesMenuItem.Name) {
// TODO
            } else if(item.Name == TripPlansMenuItem.Name) {
// TODO
            } else if(item.Name == SettingsMenuItem.Name) {
// TODO
            } else if(item.Name == HelpMenuItem.Name) {
// TODO
            } else if(item.Name == DebugMenuItem.Name) {
// TODO
            }
        }
    }
}
