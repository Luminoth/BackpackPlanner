﻿using System;
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

		protected async override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

            InitHockeyApp();

            await BackpackPlannerState.Instance.InitDatabaseAsync(new SQLitePlatformAndroid(),
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), BackpackPlannerState.DatabaseName);

            InitToolBar();
            InitNavigation();
            InitDrawer();
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
            SupportActionBar.Title = GetString(Resource.String.app_name);
        }

        private void InitDrawer()
        {
            _drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            _drawerToggle = new DrawerToggle(this, _drawerLayout, Resource.String.drawer_open, Resource.String.drawer_close);
            _drawerLayout.SetDrawerListener(_drawerToggle);

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
        }

        private void InitNavigation()
        {
            _navigation = FindViewById<NavigationView>(Resource.Id.navigation);
            _navigation.NavigationItemSelected += (sender, args) => {
                SelectDrawerItem(args.MenuItem);
            };

            TextView navigationHeaderText = FindViewById<TextView>(Resource.Id.navigation_header_text);
            navigationHeaderText.Text = BackpackPlannerState.Instance.PersonalInformation.FullName;
        }

        private void SelectDrawerItem(IMenuItem menuItem)
        {
            Fragment fragment = null;
            switch(menuItem.ItemId)
            {
            }

            menuItem.SetChecked(true);
            //SetTitle(menuItem.Title);
            _drawerLayout.CloseDrawers();
        }
	}
}
