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
using Android.Views;
using Android.Widget;

using EnergonSoftware.BackpackPlanner.Core.Logging;

namespace EnergonSoftware.BackpackPlanner.Droid.Util
{
    /// <summary>
    /// Helper to manage all of the NavigationDrawer controls
    /// </summary>
    public sealed class NavigationDrawerManager
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(NavigationDrawerManager));

        private const string StateSelectedResId = "navigation_selected_resid";

        public Android.Support.V4.Widget.DrawerLayout Layout { get; private set; }

        public DrawerToggle Toggle { get; private set; }

        public Android.Support.Design.Widget.NavigationView NavView { get; private set; }

        public TextView HeaderText { get; private set; }

        private Android.Support.V7.App.AppCompatActivity _activity;

        private int _selectedMenuItemResId = -1;

#region Events
        public event EventHandler<Android.Support.Design.Widget.NavigationView.NavigationItemSelectedEventArgs> NavigationItemSelected;
#endregion

        public void Create(Android.Support.V7.App.AppCompatActivity activity, Android.Support.V7.Widget.Toolbar toolbar, Bundle savedInstanceState)
        {
            _activity = activity;

            InitNavigation();
            InitDrawer(toolbar);

            if(null != savedInstanceState) {
                _selectedMenuItemResId = savedInstanceState.GetInt(StateSelectedResId, -1);
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
            outState.PutInt(StateSelectedResId, _selectedMenuItemResId);
        }

        public void SelectInitialItem(int defaultMenuItemResId)
        {
            Logger.Debug($"Selecting initial navigation item with defaultId={defaultMenuItemResId} and selectedId={_selectedMenuItemResId}");
            SelectItemByResId(_selectedMenuItemResId < 0 ? defaultMenuItemResId : _selectedMenuItemResId);
        }

        public void SelectItemByResId(int resId)
        {
            Logger.Debug($"Selecting navigation item with id={resId}");
            NavView.Menu.PerformIdentifierAction(resId, 0);
        }

        public void RemoveItemByResId(int resId)
        {
            NavView.Menu.RemoveItem(resId);
        }

        public void SetGroupCheckable(int group, bool checkable, bool exclusive)
        {
            NavView.Menu.SetGroupCheckable(group, checkable, exclusive);
        }

        public void LockDrawer(bool open)
        {
            Layout.SetDrawerLockMode(open ? Android.Support.V4.Widget.DrawerLayout.LockModeLockedOpen : Android.Support.V4.Widget.DrawerLayout.LockModeLockedClosed);
        }

        public void UnlockDrawer()
        {
            Layout.SetDrawerLockMode(Android.Support.V4.Widget.DrawerLayout.LockModeUnlocked);
        }

        private void InitNavigation()
        {
            NavView = _activity.FindViewById<Android.Support.Design.Widget.NavigationView>(Resource.Id.navigation);
            NavView.NavigationItemSelected += (sender, args) => {
                _selectedMenuItemResId = args.MenuItem.ItemId;

                Layout.CloseDrawers();
                NavigationItemSelected?.Invoke(sender, args);
            };

            HeaderText = _activity.FindViewById<TextView>(Resource.Id.navigation_header_text);
        }

        private void InitDrawer(Android.Support.V7.Widget.Toolbar toolbar)
        {
            Layout = _activity.FindViewById<Android.Support.V4.Widget.DrawerLayout>(Resource.Id.drawer_layout);

            Toggle = new DrawerToggle(_activity, Layout, toolbar, Resource.String.drawer_open, Resource.String.drawer_close);
            Toggle.SyncState();
            Layout.AddDrawerListener(Toggle);
        }
    }
}
