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

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Util;
using Android.Views;

using EnergonSoftware.BackpackPlanner.Droid.Fragments;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Collections;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Items;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Systems;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Meals;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Trips.Itineraries;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Trips.Plans;
using EnergonSoftware.BackpackPlanner.Droid.Logging;
using EnergonSoftware.BackpackPlanner.Droid.Util;
using EnergonSoftware.BackpackPlanner.Models.Personal;
using EnergonSoftware.BackpackPlanner.Units;

using SQLite.Net.Platform.XamarinAndroid;

namespace EnergonSoftware.BackpackPlanner.Droid
{
	[Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon")]
    [IntentFilter(new[] { "android.intent.action.SEARCH" })]
    [MetaData("com.google.android.gms.version", Value = "@integer/google_play_services_version")]
    [MetaData("android.app.searchable", Resource = "@xml/searchable")]
	public class MainActivity : Android.Support.V7.App.AppCompatActivity, View.IOnClickListener
	{
        public const string LogTag = "BackpackPlanner.Droid";

        private const string HockeyAppAppId = "32a2c37622529305ec763b7e2c224deb";

#region Controls
        private Android.Support.V7.Widget.Toolbar _toolbar;
        private readonly NavigationDrawerManager _navigationDrawerManager = new NavigationDrawerManager();
#endregion

		protected async override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.activity_main);

            InitHockeyApp();

            BackpackPlannerState.Instance.Logger = new DroidLogger();

            await BackpackPlannerState.Instance.InitDatabaseAsync(new SQLitePlatformAndroid(),
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), BackpackPlannerState.DatabaseName);

            InitToolbar();

            // setup the navigation drawer manager
            _navigationDrawerManager.DefaultSelectedResId = Resource.Id.nav_gear_items_fragment;
            _navigationDrawerManager.NavigationItemSelected += (sender, args) => {
                SelectDrawerItem(args.MenuItem);
            };

            // create the navigation drawer
            _navigationDrawerManager.Create(this, _toolbar, savedInstanceState);
            _navigationDrawerManager.Toggle.ToolbarNavigationClickListener = this;
            _navigationDrawerManager.HeaderText.Text = !string.IsNullOrWhiteSpace(BackpackPlannerState.Instance.PersonalInformation.Name)
                    ? BackpackPlannerState.Instance.PersonalInformation.Name
                    : "Backpacking Planner";

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

            LoadPreferences();

            HandleIntent(Intent);
		}

	    public override void OnPostCreate(Bundle savedInstanceState, PersistableBundle persistentState)
	    {
	        base.OnPostCreate(savedInstanceState, persistentState);

            _navigationDrawerManager.Toggle.SyncState();
	    }

	    public override bool OnCreateOptionsMenu(IMenu menu)
	    {
            return InitOptionsMenu(menu);
	    }

	    public override bool OnPrepareOptionsMenu(IMenu menu)
	    {
            return InitOptionsMenu(menu);
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

	    private void InitHockeyApp()
        {
            Log.Info(LogTag, "Initializing HockeyApp...");

            // Register the crash manager before Initializing the trace writer
            HockeyApp.CrashManager.Register(this, HockeyAppAppId); 

            // Register to with the Update Manager
            HockeyApp.UpdateManager.Register(this, HockeyAppAppId);

            // Register the Feedback Manager
            HockeyApp.FeedbackManager.Register(this, HockeyAppAppId);

            // Initialize the Trace Writer
            HockeyApp.TraceWriter.Initialize();

            // Wire up Unhandled Expcetion handler from Android
            AndroidEnvironment.UnhandledExceptionRaiser += (sender, args) => {
                // Use the trace writer to log exceptions so HockeyApp finds them
                HockeyApp.TraceWriter.WriteTrace(args.Exception);
                args.Handled = true;
            };

            // Wire up the .NET Unhandled Exception handler
            AppDomain.CurrentDomain.UnhandledException += (sender, args) => HockeyApp.TraceWriter.WriteTrace(args.ExceptionObject);

            // Wire up the unobserved task exception handler
            TaskScheduler.UnobservedTaskException += (sender, args) => HockeyApp.TraceWriter.WriteTrace(args.Exception);
        }

        private void InitToolbar()
        {
            _toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(_toolbar);
        }

        private bool InitSearchView(IMenuItem searchItem)
        {
            if(null == searchItem) {
                return true;
            }

            View actionView = Android.Support.V4.View.MenuItemCompat.GetActionView(searchItem);
            Android.Support.V7.Widget.SearchView searchView = actionView.JavaCast<Android.Support.V7.Widget.SearchView>();

            SearchManager searchManager = (SearchManager)GetSystemService(SearchService);
	        searchView.SetSearchableInfo(searchManager.GetSearchableInfo(ComponentName));

            return true;
        }

        public bool InitOptionsMenu(IMenu menu)
        {
            menu.Clear();

            if(FragmentManager.BackStackEntryCount < 1) {
                // TODO: this should depend on the currently selected drawer item
                MenuInflater.Inflate(Resource.Menu.options_menu, menu);
                return InitSearchView(menu.FindItem(Resource.Id.action_search));
            } 

            // TODO: this should depend on the top fragment's tags
            MenuInflater.Inflate(Resource.Menu.options_menu_nosearch, menu);
            return true;
        }

        private void LoadPreferences()
        {
            PreferenceManager.SetDefaultValues(this, Resource.Xml.settings, false);

            ISharedPreferences sharedPreferences = PreferenceManager.GetDefaultSharedPreferences(this);

            string scratch;

            // NOTE: have to read these settings first so we know how to interpret everything else
            try {
                scratch = sharedPreferences.GetString(BackpackPlannerSettings.UnitSystemPreferenceKey, "0");
                BackpackPlannerState.Instance.Settings.Units = (UnitSystem)Convert.ToInt32(scratch);
            } catch(FormatException) {
                // it's k, we'll live
            }

            try {
                scratch = sharedPreferences.GetString(BackpackPlannerSettings.CurrencyPreferenceKey, "0");
                BackpackPlannerState.Instance.Settings.Currency = (Currency)Convert.ToInt32(scratch);
            } catch(FormatException) {
                // it's k, we'll live
            }

            BackpackPlannerState.Instance.PersonalInformation.Name = sharedPreferences.GetString(PersonalInformation.NamePreferenceKey, "");

            try {
                scratch = sharedPreferences.GetString(PersonalInformation.DateOfBirthPreferenceKey, "");
                if(!string.IsNullOrWhiteSpace(scratch)) {
                    BackpackPlannerState.Instance.PersonalInformation.DateOfBirth = Convert.ToDateTime(scratch);
                }
            } catch(FormatException) {
                // it's k, we'll live
            }

            try {
                scratch = sharedPreferences.GetString(PersonalInformation.UserSexPreferenceKey, "0");
                BackpackPlannerState.Instance.PersonalInformation.Sex = (UserSex)Convert.ToInt32(scratch);
            } catch(FormatException) {
                // it's k, we'll live
            }

            try {
                scratch = sharedPreferences.GetString(PersonalInformation.HeightPreferenceKey, "0");
                BackpackPlannerState.Instance.PersonalInformation.HeightInUnits = Convert.ToInt32(scratch);
            } catch(FormatException) {
                // it's k, we'll live
            }

            try {
                scratch = sharedPreferences.GetString(PersonalInformation.WeightPreferenceKey, "0");
                BackpackPlannerState.Instance.PersonalInformation.WeightInUnits = Convert.ToInt32(scratch);
            } catch(FormatException) {
                // it's k, we'll live
            }
        }

        private void HandleIntent(Intent intent)
        {
            if(Intent.ActionSearch.Equals(intent.Action)) {
                //string query = intent.GetStringExtra(SearchManager.Query);
                // TODO: use the query somehow
// https://developer.android.com/training/search/setup.html
// https://developer.android.com/training/search/search.html
            }
        }

        private void SelectDrawerItem(IMenuItem menuItem)
        {
            Log.Debug(LogTag, "DrawerItem selected, ItemId=" + menuItem.ItemId + ", GroupId=" + menuItem.GroupId);

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
                Log.Info(LogTag, "Gear Items selected");
                fragment = new GearItemsFragment();
                break;
            case Resource.Id.nav_gear_systems_fragment:
                Log.Info(LogTag, "Gear Systems");
                fragment = new GearSystemsFragment();
                break;
            case Resource.Id.nav_gear_collections_fragment:
                Log.Info(LogTag, "Gear Collections selected");
                fragment = new GearCollectionsFragment();
                break;
            case Resource.Id.nav_meals_fragment:
                Log.Info(LogTag, "Meals selected");
                fragment = new MealsFragment();
                break;
            case Resource.Id.nav_trip_itineraries_fragment:
                Log.Info(LogTag, "Trip Itineraries selected");
                fragment = new TripItinerariesFragment();
                break;
            case Resource.Id.nav_trip_plans_fragment:
                Log.Info(LogTag, "Trip Plans selected");
                fragment = new TripPlansFragment();
                break;
            case Resource.Id.nav_settings_fragment:
// TODO: when the Xamarin Support Library Preference v7 is out
// replace this activity with the PreferenceFragment
                Log.Info(LogTag, "Settings selected");
                StartActivity(typeof(SettingsActivity));
                return;
            case Resource.Id.nav_help_fragment:
                Log.Info(LogTag, "Help selected");
                fragment = new HelpFragment();
                break;
            }

            if(null != fragment) {
                FragmentTransitionUtil.Transition(this, SupportFragmentManager.BeginTransaction(), Resource.Id.frame_content, fragment);
            }

            menuItem.SetChecked(true);
        }
	}
}
