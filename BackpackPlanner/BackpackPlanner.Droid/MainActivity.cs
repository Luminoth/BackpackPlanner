using System;
using System.Threading.Tasks;

using Android.App;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;

using EnergonSoftware.BackpackPlanner.Droid.Fragments;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Items;
using EnergonSoftware.BackpackPlanner.Droid.Util;

using SQLite.Net.Platform.XamarinAndroid;

namespace EnergonSoftware.BackpackPlanner.Droid
{
	[Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : AppCompatActivity
	{
        public const string LogTag = "BackpackPlanner.Droid";

        private const string HockeyAppAppId = "32a2c37622529305ec763b7e2c224deb";

        private Android.Support.V7.Widget.Toolbar _toolBar;
        private DrawerLayout _drawerLayout;
        private DrawerToggle _drawerToggle;
        private NavigationView _navigation;
        private TextView _navigationHeaderText;

        public void UpdateNavigationHeaderText()
        {
            _navigationHeaderText.Text = !string.IsNullOrWhiteSpace(BackpackPlannerState.Instance.PersonalInformation.FirstName)
                ? BackpackPlannerState.Instance.PersonalInformation.FullName
                : "Backpacking Planner";
        }

	    public override void OnPostCreate(Bundle savedInstanceState, PersistableBundle persistentState)
	    {
	        base.OnPostCreate(savedInstanceState, persistentState);
            _drawerToggle.SyncState();
	    }

	    public override void OnConfigurationChanged(Configuration newConfig)
	    {
	        base.OnConfigurationChanged(newConfig);
            _drawerToggle.OnConfigurationChanged(newConfig);
	    }

	    public override bool OnOptionsItemSelected(IMenuItem item)
	    {
	        if(_drawerToggle.OnOptionsItemSelected(item)) {
                return true;
            }

            return base.OnOptionsItemSelected(item);
	    }

		protected async override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_main);

            InitHockeyApp();

            await BackpackPlannerState.Instance.InitDatabaseAsync(new SQLitePlatformAndroid(),
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), BackpackPlannerState.DatabaseName);

            InitToolBar();
            InitNavigation();
            InitDrawer();
		}

	    private void InitHockeyApp()
        {
            Log.Debug(LogTag, "Initializing HockeyApp...");

            // Register the crash manager before Initializing the trace writer
            HockeyApp.CrashManager.Register(this, HockeyAppAppId); 

            // Register to with the Update Manager
            HockeyApp.UpdateManager.Register(this, HockeyAppAppId);

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
            _toolBar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(_toolBar);
        }

        private void InitDrawer()
        {
            _drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            _drawerToggle = new DrawerToggle(this, _drawerLayout, _toolBar, Resource.String.drawer_open, Resource.String.drawer_close);
            _drawerToggle.SyncState();
            _drawerLayout.SetDrawerListener(_drawerToggle);
        }

        private void InitNavigation()
        {
            _navigation = FindViewById<NavigationView>(Resource.Id.navigation);
            _navigation.NavigationItemSelected += (sender, args) => {
                // this solves the problem of checking more than one item
                // in the list across groups even when checkableBehavior is single on each group
                // TODO: the problem with this solution is that it requires an update
                // any time a new group is added to the menu, and that's maybe not so good
                _navigation.Menu.SetGroupCheckable(Resource.Id.group_personal_information, (args.MenuItem.GroupId == Resource.Id.group_personal_information), true);
                _navigation.Menu.SetGroupCheckable(Resource.Id.group_gear, (args.MenuItem.GroupId == Resource.Id.group_gear), true);
                _navigation.Menu.SetGroupCheckable(Resource.Id.group_meals, (args.MenuItem.GroupId == Resource.Id.group_meals), true);
                _navigation.Menu.SetGroupCheckable(Resource.Id.group_trips, (args.MenuItem.GroupId == Resource.Id.group_trips), true);
                _navigation.Menu.SetGroupCheckable(Resource.Id.group_settings, (args.MenuItem.GroupId == Resource.Id.group_settings), true);

                SelectDrawerItem(args.MenuItem);
            };

            _navigationHeaderText = FindViewById<TextView>(Resource.Id.navigation_header_text);
            UpdateNavigationHeaderText();
        }

        private void SelectDrawerItem(IMenuItem menuItem)
        {
            Android.Support.V4.App.Fragment fragment;
            switch(menuItem.ItemId)
            {
            case Resource.Id.nav_personal_information_fragment:
                fragment = new PersonalInformationFragment();
                break;
            case Resource.Id.nav_gear_items_fragment:
                fragment = new GearItemsFragment();
                break;
            default:
                fragment = null;
                break;
            }

            if(null != fragment) {
                SupportFragmentManager.BeginTransaction().Replace(Resource.Id.frame_content, fragment).Commit();
            }

            menuItem.SetChecked(true);
            Title = menuItem.TitleFormatted.ToString();
            _drawerLayout.CloseDrawers();
        }
	}
}
