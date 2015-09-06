using System;

using Android.Content.Res;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;

// nav drawer checked state bug: https://code.google.com/p/android/issues/detail?id=175224
// and http://stackoverflow.com/questions/30592080/save-state-on-navigationview

namespace EnergonSoftware.BackpackPlanner.Droid.Util
{
    public class NavigationDrawerManager
    {
        private const string StateSelectedResId = "navigation_selected_resid";

        public int DefaultSelectedResId { get; set; }

        private AppCompatActivity _owner;

        private DrawerLayout _drawerLayout;
        private DrawerToggle _drawerToggle;
        private NavigationView _navigation;
        private TextView _navigationHeaderText;

        private int _selectedResId;

#region Events
        public event EventHandler<NavigationView.NavigationItemSelectedEventArgs> NavigationItemSelected;
#endregion

        public void Create(AppCompatActivity owner, Android.Support.V7.Widget.Toolbar toolbar, Bundle savedInstanceState)
        {
            _owner = owner;

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

        public void SyncState()
        {
            _drawerToggle.SyncState();
        }

        public void OnConfigurationChanged(Configuration newConfig)
        {
            _drawerToggle.OnConfigurationChanged(newConfig);
        }

        public bool OnOptionsItemSelected(IMenuItem item)
        {
            return _drawerToggle.OnOptionsItemSelected(item);
        }

        public void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt(StateSelectedResId, _selectedResId);
        }

        public void UpdateNavigationHeaderText(string text)
        {
            _navigationHeaderText.Text = text;
        }

        public void SelectItemByResId(int resId)
        {
            _navigation.Menu.PerformIdentifierAction(resId, 0);
        }

        public void SetGroupCheckable(int group, bool checkable, bool exclusive)
        {
            _navigation.Menu.SetGroupCheckable(group, checkable, exclusive);
        }

        private void InitNavigation()
        {
            _navigation = _owner.FindViewById<NavigationView>(Resource.Id.navigation);
            _navigation.NavigationItemSelected += (sender, args) => {
                _selectedResId = args.MenuItem.ItemId;

                _drawerLayout.CloseDrawers();
                NavigationItemSelected?.Invoke(sender, args);
            };

            _navigationHeaderText = _owner.FindViewById<TextView>(Resource.Id.navigation_header_text);
        }

        private void InitDrawer(Android.Support.V7.Widget.Toolbar toolbar)
        {
            _drawerLayout = _owner.FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            _drawerToggle = new DrawerToggle(_owner, _drawerLayout, toolbar, Resource.String.drawer_open, Resource.String.drawer_close);
            _drawerToggle.SyncState();
            _drawerLayout.SetDrawerListener(_drawerToggle);
        }
    }
}
