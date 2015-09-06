using System;
using System.Threading.Tasks;

using Android.App;
using Android.Content.Res;
using Android.OS;
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
using EnergonSoftware.BackpackPlanner.Droid.Util;

using SQLite.Net.Platform.XamarinAndroid;

namespace EnergonSoftware.BackpackPlanner.Droid
{
	[Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon")]
    [MetaData("com.google.android.gms.version", Value = "@integer/google_play_services_version")]
	public class MainActivity : Android.Support.V7.App.AppCompatActivity
	{
        public const string LogTag = "BackpackPlanner.Droid";

        private const string HockeyAppAppId = "32a2c37622529305ec763b7e2c224deb";
        private const string StateBackEnabled = "navigation_back_enabled";

        private Android.Support.V7.Widget.Toolbar _toolbar;
        private readonly NavigationDrawerManager _navigationDrawerManager = new NavigationDrawerManager();

        private bool _backEnabled;

		protected async override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_main);

            InitHockeyApp();

            Log.Info(LogTag, "Initializing database...");
            await BackpackPlannerState.Instance.InitDatabaseAsync(new SQLitePlatformAndroid(),
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), BackpackPlannerState.DatabaseName);

            InitToolBar();

            _navigationDrawerManager.DefaultSelectedResId = Resource.Id.nav_gear_items_fragment;
            _navigationDrawerManager.NavigationItemSelected += (sender, args) => {
                SelectDrawerItem(args.MenuItem);
            };

            _navigationDrawerManager.Create(this, _toolbar, savedInstanceState);
            _navigationDrawerManager.UpdateNavigationHeaderText(
                !string.IsNullOrWhiteSpace(BackpackPlannerState.Instance.PersonalInformation.Name)
                    ? BackpackPlannerState.Instance.PersonalInformation.Name
                    : "Backpacking Planner");

            if(null != savedInstanceState) {
                _backEnabled = savedInstanceState.GetBoolean(StateBackEnabled);
            }

            SupportFragmentManager.BackStackChanged += (sender, args) => {
                if(SupportFragmentManager.BackStackEntryCount > 0) {
                    _navigationDrawerManager.DrawerIndicatorEnabled = false;
                    SupportActionBar.SetDisplayHomeAsUpEnabled(true);

                    /*if(!_backEnabled) {
                        _toolbar.NavigationClick += NavigationClickBackEventHandler;
                    }
                    _backEnabled = true;*/
                } else {
                    SupportActionBar.SetDisplayHomeAsUpEnabled(false);
                    _navigationDrawerManager.DrawerIndicatorEnabled = true;

                    /*_toolbar.NavigationClick -= NavigationClickBackEventHandler;
                    _backEnabled = false;*/
                }
            };
		}

	    public override void OnPostCreate(Bundle savedInstanceState, PersistableBundle persistentState)
	    {
	        base.OnPostCreate(savedInstanceState, persistentState);

            _navigationDrawerManager.SyncState();
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

             outState.PutBoolean(StateBackEnabled, _backEnabled);

            _navigationDrawerManager.OnSaveInstanceState(outState);
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

        private void InitToolBar()
        {
            _toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(_toolbar);
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
                Log.Info(LogTag, "Settings selected");
                StartActivity(typeof(SettingsActivity));
                return;
            case Resource.Id.nav_help_fragment:
                Log.Info(LogTag, "Help selected");
                fragment = new HelpFragment();
                break;
            }

            if(null != fragment) {
                Android.Support.V4.App.FragmentTransaction fragmentTransaction = SupportFragmentManager.BeginTransaction();
                fragmentTransaction.Replace(Resource.Id.frame_content, fragment);
                fragmentTransaction.Commit();
            }

            menuItem.SetChecked(true);
        }

        private void NavigationClickBackEventHandler(object sender, Android.Support.V7.Widget.Toolbar.NavigationClickEventArgs args)
        {
            OnBackPressed();
        }
	}
}
