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

using System.Diagnostics;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Views;

using EnergonSoftware.BackpackPlanner.Core.Database;
using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Droid.Fragments;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Collections;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Items;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Systems;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Meals;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Trips.Itineraries;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Trips.Plans;
using EnergonSoftware.BackpackPlanner.Droid.Util;

namespace EnergonSoftware.BackpackPlanner.Droid.Activities
{
    [Activity(Label = "@string/app_name")]
    public sealed class BackpackPlannerActivity : BaseActivity, View.IOnClickListener
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(BackpackPlannerActivity));

#region Controls
        private Android.Support.V7.Widget.Toolbar _toolbar;
        private readonly NavigationDrawerManager _navigationDrawerManager = new NavigationDrawerManager();
#endregion

        private readonly GooglePlayServicesManager _googlePlayServicesManager;

        public BackpackPlannerActivity()
        {
            _googlePlayServicesManager = new GooglePlayServicesManager(this);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_backpack_planner);

            _googlePlayServicesManager.OnCreate();

            InitToolbar();

            // setup the navigation drawer manager
            _navigationDrawerManager.NavigationItemSelected += (sender, args) => {
                SelectDrawerItem(args.MenuItem);
            };

            // create the navigation drawer
            _navigationDrawerManager.Create(this, _toolbar, savedInstanceState);
            _navigationDrawerManager.Toggle.ToolbarNavigationClickListener = this;
            _navigationDrawerManager.HeaderText.Text = !string.IsNullOrWhiteSpace(BackpackPlannerState.Instance.PersonalInformation.Name)
                    ? BackpackPlannerState.Instance.PersonalInformation.Name
                    : Resources.GetString(Resource.String.app_name);

#if !DEBUG
            _navigationDrawerManager.RemoveItemByResId(Resource.Id.nav_debug_fragment);
#endif

            // setup the fragment drawer indicator state
            SupportFragmentManager.BackStackChanged += (sender, args) => {
                if(SupportFragmentManager.BackStackEntryCount > 0) {
                    _navigationDrawerManager.Toggle.DrawerIndicatorEnabled = false;
                    _navigationDrawerManager.LockDrawer(false);

                    SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                } else {
                    _navigationDrawerManager.Toggle.DrawerIndicatorEnabled = true;
                    _navigationDrawerManager.UnlockDrawer();

                    SupportActionBar.SetDisplayHomeAsUpEnabled(false);
                }

                _navigationDrawerManager.Toggle.SyncState();
            };

            // do this here instead of in OnResume() so that
            // we don't open the selected fragment twice
            _navigationDrawerManager.SelectInitialItem(Resource.Id.nav_gear_items_fragment);
        }

	    public override void OnPostCreate(Bundle savedInstanceState, PersistableBundle persistentState)
	    {
	        base.OnPostCreate(savedInstanceState, persistentState);

            _navigationDrawerManager.Toggle.SyncState();
	    }

	    protected override void OnStart()
	    {
	        base.OnStart();

            _googlePlayServicesManager.OnStart();
	    }

	    protected override void OnStop()
	    {
	        base.OnStop();

            _googlePlayServicesManager.OnStop();
	    }

	    protected override void OnResume()
	    {
	        base.OnResume();

            _googlePlayServicesManager.OnResume();

            BackpackPlannerState.Instance.DatabaseState.ConnectAsync(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), DatabaseState.DatabaseName).Wait();

            Task.Run(async () => {
                Stopwatch stopwatch = Stopwatch.StartNew();
                await BackpackPlannerState.Instance.DatabaseState.InitDatabaseAsync().ConfigureAwait(false);
                stopwatch.Stop();
                Logger.Debug($"Database init took {stopwatch.ElapsedMilliseconds}ms");

                FragmentTransitionUtil.ReloadCurrentFragment(SupportFragmentManager);
            });
	    }

	    protected override void OnPause()
	    {
	        base.OnPause();

            _googlePlayServicesManager.OnPause();

            BackpackPlannerState.Instance.DatabaseState.Connection.CloseAsync().Wait();
	    }

	    public override void OnConfigurationChanged(Configuration newConfig)
	    {
	        base.OnConfigurationChanged(newConfig);

            _navigationDrawerManager.OnConfigurationChanged(newConfig);
	    }

	    public override bool OnOptionsItemSelected(IMenuItem item)
	    {
	        if(_navigationDrawerManager.OnOptionsItemSelected(item)) {
                return true;
            }

            return base.OnOptionsItemSelected(item);
	    }

	    protected override void OnSaveInstanceState(Bundle outState)
	    {
	        base.OnSaveInstanceState(outState);

            _navigationDrawerManager.OnSaveInstanceState(outState);
	    }

	    public void OnClick(View view)
        {
            // this handles the toolbar button press on stacked fragments
            OnBackPressed();
        }

	    protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
	    {
	        base.OnActivityResult(requestCode, resultCode, data);

            _googlePlayServicesManager.OnActivityResult(requestCode, resultCode, data);
	    }

        private void InitToolbar()
        {
            _toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(_toolbar);
        }

        private void SelectDrawerItem(IMenuItem menuItem)
        {
            // this solves the problem of checking more than one item
            // in the list across groups even when checkableBehavior is single on each group
            // TODO: the problem with this solution is that it requires an update
            // any time a new group is added to the menu, and that's maybe not so good
            _navigationDrawerManager.SetGroupCheckable(Resource.Id.group_gear, (menuItem.GroupId == Resource.Id.group_gear), true);
            _navigationDrawerManager.SetGroupCheckable(Resource.Id.group_meals, (menuItem.GroupId == Resource.Id.group_meals), true);
            _navigationDrawerManager.SetGroupCheckable(Resource.Id.group_trips, (menuItem.GroupId == Resource.Id.group_trips), true);
            _navigationDrawerManager.SetGroupCheckable(Resource.Id.group_settings, (menuItem.GroupId == Resource.Id.group_settings), true);

            Android.Support.V4.App.Fragment fragment = null;
            switch(menuItem.ItemId)
            {
            case Resource.Id.nav_gear_items_fragment:
                fragment = new GearItemsFragment();
                break;
            case Resource.Id.nav_gear_systems_fragment:
                fragment = new GearSystemsFragment();
                break;
            case Resource.Id.nav_gear_collections_fragment:
                fragment = new GearCollectionsFragment();
                break;
            case Resource.Id.nav_meals_fragment:
                fragment = new MealsFragment();
                break;
            case Resource.Id.nav_trip_itineraries_fragment:
                fragment = new TripItinerariesFragment();
                break;
            case Resource.Id.nav_trip_plans_fragment:
                fragment = new TripPlansFragment();
                break;
            case Resource.Id.nav_settings_fragment:
                fragment = new SettingsFragment();
                break;
            case Resource.Id.nav_help_fragment:
                fragment = new HelpFragment();
                break;
            case Resource.Id.nav_debug_fragment:
                fragment = new DebugFragment();
                break;
            }

            if(null != fragment) {
                FragmentTransitionUtil.Transition(this, SupportFragmentManager.BeginTransaction(), Resource.Id.frame_content, fragment);
            }

            menuItem.SetChecked(true);
        }
    }
}
