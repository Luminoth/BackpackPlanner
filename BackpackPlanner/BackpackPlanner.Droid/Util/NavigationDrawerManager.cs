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
using Android.Util;
using Android.Views;
using Android.Widget;

namespace EnergonSoftware.BackpackPlanner.Droid.Util
{
    /// <summary>
    /// Helper to manage all of the NavigationDrawer controls
    /// </summary>
    public class NavigationDrawerManager
    {
        private const string StateSelectedResId = "navigation_selected_resid";

        public int DefaultSelectedResId { get; set; }

        public Android.Support.V4.Widget.DrawerLayout Layout { get; private set; }

        public DrawerToggle Toggle { get; private set; }

        public Android.Support.Design.Widget.NavigationView NavView { get; private set; }

        public TextView HeaderText { get; private set; }

        private Android.Support.V7.App.AppCompatActivity _activity;

        private int _selectedResId;

#region Events
        public event EventHandler<Android.Support.Design.Widget.NavigationView.NavigationItemSelectedEventArgs> NavigationItemSelected;
#endregion

        public void Create(Android.Support.V7.App.AppCompatActivity activity, Android.Support.V7.Widget.Toolbar toolbar, Bundle savedInstanceState)
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
                // TODO: something about this callback is causing
                // java.lang.IllegalStateException: Can not perform this action after onSaveInstanceState
                // when the app is killed. need to find out where we're
                // trying to commit after saving state with this
                // http://www.androiddesignpatterns.com/2013/08/fragment-transaction-commit-state-loss.html

                _selectedResId = args.MenuItem.ItemId;

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
            Layout.SetDrawerListener(Toggle);
        }
    }
}
