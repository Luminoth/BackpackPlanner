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

using Android.Content.Res;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace EnergonSoftware.BackpackPlanner.Droid.Util
{
    public class NavigationDrawerManager
    {
        private const string StateSelectedResId = "navigation_selected_resid";

        public int DefaultSelectedResId { get; set; }

        public DrawerLayout Layout { get; private set; }

        public DrawerToggle Toggle { get; private set; }

        public NavigationView NavView { get; private set; }

        public TextView HeaderText { get; private set; }

        private AppCompatActivity _activity;

        private int _selectedResId;

#region Events
        public event EventHandler<NavigationView.NavigationItemSelectedEventArgs> NavigationItemSelected;
#endregion

        public void Create(AppCompatActivity activity, Android.Support.V7.Widget.Toolbar toolbar, Bundle savedInstanceState)
        {
            _activity = activity;

            InitNavigation();
            InitDrawer(toolbar);

            if(null != savedInstanceState) {
                int selectedResId = savedInstanceState.GetInt(StateSelectedResId);
                Log.Debug(MainActivity.LogTag, "Setting selected item from savedInstanceState: " + selectedResId);
                SelectItemByResId(selectedResId);
            } else if(DefaultSelectedResId > 1) {
                Log.Debug(MainActivity.LogTag, "Setting selected item from DefaultSelectedResId: " + DefaultSelectedResId);
                SelectItemByResId(DefaultSelectedResId);
            }
        }

        public void OnConfigurationChanged(Configuration newConfig)
        {
            Toggle.OnConfigurationChanged(newConfig);
        }

        public bool OnOptionsItemSelected(IMenuItem item)
        {
            return Toggle.OnOptionsItemSelected(item);
        }

        public void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt(StateSelectedResId, _selectedResId);
        }

        public void SelectItemByResId(int resId)
        {
            NavView.Menu.PerformIdentifierAction(resId, 0);
        }

        public void SetGroupCheckable(int group, bool checkable, bool exclusive)
        {
            NavView.Menu.SetGroupCheckable(group, checkable, exclusive);
        }

        private void InitNavigation()
        {
            NavView = _activity.FindViewById<NavigationView>(Resource.Id.navigation);
            NavView.NavigationItemSelected += (sender, args) => {
                _selectedResId = args.MenuItem.ItemId;

                Layout.CloseDrawers();
                NavigationItemSelected?.Invoke(sender, args);
            };

            HeaderText = _activity.FindViewById<TextView>(Resource.Id.navigation_header_text);
        }

        private void InitDrawer(Android.Support.V7.Widget.Toolbar toolbar)
        {
            Layout = _activity.FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            Toggle = new DrawerToggle(_activity, Layout, toolbar, Resource.String.drawer_open, Resource.String.drawer_close);
            Toggle.SyncState();
            Layout.SetDrawerListener(Toggle);
        }
    }
}