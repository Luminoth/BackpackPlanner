﻿/*
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
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Drive;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;

using EnergonSoftware.BackpackPlanner.Core.Logging;
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
using EnergonSoftware.BackpackPlanner.Units.Currency;
using EnergonSoftware.BackpackPlanner.Units.Units;

using SQLite.Net.Platform.XamarinAndroid;

namespace EnergonSoftware.BackpackPlanner.Droid
{
	[Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon")]
    [MetaData("com.google.android.gms.version", Value = "@integer/google_play_services_version")]
	public sealed class MainActivity : Android.Support.V7.App.AppCompatActivity, View.IOnClickListener,
        IGoogleApiClientConnectionCallbacks, IGoogleApiClientOnConnectionFailedListener
	{
        public const string LogTag = "BackpackPlanner.Droid";

        private const string HockeyAppAppId = "32a2c37622529305ec763b7e2c224deb";

        // Google Play Services error resolution
        private const int RequestCodeResolution = 9001;

        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(MainActivity));

        private class DriveContentsResultCallback : Java.Lang.Object, IResultCallback
        {
	        public void OnResult(Java.Lang.Object result)
	        {
// TODO: https://github.com/googledrive/android-demos/blob/master/app/src/main/java/com/google/android/gms/drive/sample/demo/CreateFileInAppFolderActivity.java
                var contentsResult = result.JavaCast<IDriveApiDriveContentsResult>();
                if(null == contentsResult) {
                    // TODO: error
                    return;
                }

// https://developers.google.com/drive/android/appfolder

                Logger.Debug("Got a contents result!");
	        }
        }

#region Controls
        private Android.Support.V7.Widget.Toolbar _toolbar;
        private readonly NavigationDrawerManager _navigationDrawerManager = new NavigationDrawerManager();
#endregion

        private IGoogleApiClient _googleClientApi;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.activity_main);

            BackpackPlannerState.Instance.SystemLogger = new DroidLogger();

            InitHockeyApp();

            _googleClientApi = new GoogleApiClientBuilder(this)
                .AddApi(DriveClass.API)
                .AddScope(DriveClass.ScopeFile)
                .AddScope(DriveClass.ScopeAppfolder)
                .AddConnectionCallbacks(this)
                .AddOnConnectionFailedListener(this)
                .Build();

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

            HandleIntent(Intent);

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

            Logger.Info("Connecting Google Play Services client...");
            _googleClientApi.Connect();
	    }

	    protected override void OnStop()
	    {
	        base.OnStop();

            Logger.Info("Disonnecting Google Play Services client...");
            _googleClientApi.Disconnect();
	    }

	    protected override void OnResume()
	    {
	        base.OnResume();

            LoadPreferences();

            BackpackPlannerState.Instance.InitDatabaseAsync(new SQLitePlatformAndroid(),
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), BackpackPlannerState.DatabaseName).Wait();
	    }

	    protected override void OnPause()
	    {
	        base.OnPause();

            BackpackPlannerState.Instance.DatabaseConnection.CloseAsync().Wait();
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

	    public void OnConnected(Bundle connectionHint)
	    {
	        Logger.Info("Google Play Services connected!");
            DriveClass.DriveApi.NewDriveContents(_googleClientApi).SetResultCallback(new DriveContentsResultCallback());
	    }

	    public void OnConnectionSuspended(int cause)
	    {
	        Logger.Info($"Google Play Services suspended: {cause}");
	    }

	    public void OnConnectionFailed(ConnectionResult result)
	    {
            Logger.Info($"Google Play Services connection failed: {result}");
            if(!result.HasResolution) {
                GoogleApiAvailability.Instance.GetErrorDialog(this, result.ErrorCode, 0).Show();
                return;
            }

            try {
                result.StartResolutionForResult(this, RequestCodeResolution);
            } catch (IntentSender.SendIntentException ex) {
                Logger.Error("Exception while starting resolution activity", ex);
            }
	    }

	    protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
	    {
	        base.OnActivityResult(requestCode, resultCode, data);

            switch(requestCode)
            {
            case RequestCodeResolution:
                if(Result.Ok == resultCode) {
                    Logger.Info("Connecting Google Play Services client...");
                    _googleClientApi.Connect();
                }
                break;
            }
	    }

	    private void InitHockeyApp()
        {
            Logger.Info("Initializing HockeyApp...");

            // Register the crash manager before Initializing the trace writer
            HockeyApp.CrashManager.Register(this, HockeyAppAppId); 

            // Register to with the Update Manager
            // TODO: this call causes a crash on the lollipop
            // emulators, but I have no idea why!
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

        private void LoadPreferences()
        {
            Logger.Debug("Setting default preferences...");
            PreferenceManager.SetDefaultValues(this, Resource.Xml.settings, false);

            Logger.Debug("Loading preferences...");
            ISharedPreferences sharedPreferences = PreferenceManager.GetDefaultSharedPreferences(this);

            // NOTE: have to read these settings first so we know how to interpret everything else
            try {
                string scratch = sharedPreferences.GetString(BackpackPlannerSettings.UnitSystemPreferenceKey, "0");
                BackpackPlannerState.Instance.Settings.Units = (UnitSystem)Convert.ToInt32(scratch);
            } catch(FormatException) {
                // it's k, we'll live
            }

            try {
                string scratch = sharedPreferences.GetString(BackpackPlannerSettings.CurrencyPreferenceKey, "0");
                BackpackPlannerState.Instance.Settings.Currency = (Currency)Convert.ToInt32(scratch);
            } catch(FormatException) {
                // it's k, we'll live
            }

            BackpackPlannerState.Instance.PersonalInformation.Name = sharedPreferences.GetString(PersonalInformation.NamePreferenceKey, "");

            try {
                string scratch = sharedPreferences.GetString(PersonalInformation.DateOfBirthPreferenceKey, "");
                if(!string.IsNullOrWhiteSpace(scratch)) {
                    BackpackPlannerState.Instance.PersonalInformation.DateOfBirth = Convert.ToDateTime(scratch);
                }
            } catch(FormatException) {
                // it's k, we'll live
            }

            try {
                string scratch = sharedPreferences.GetString(PersonalInformation.UserSexPreferenceKey, "0");
                BackpackPlannerState.Instance.PersonalInformation.Sex = (UserSex)Convert.ToInt32(scratch);
            } catch(FormatException) {
                // it's k, we'll live
            }

            try {
                string scratch = sharedPreferences.GetString(PersonalInformation.HeightPreferenceKey, "0");
                BackpackPlannerState.Instance.PersonalInformation.HeightInUnits = Convert.ToInt32(scratch);
            } catch(FormatException) {
                // it's k, we'll live
            }

            try {
                string scratch = sharedPreferences.GetString(PersonalInformation.WeightPreferenceKey, "0");
                BackpackPlannerState.Instance.PersonalInformation.WeightInUnits = Convert.ToInt32(scratch);
            } catch(FormatException) {
                // it's k, we'll live
            }
        }

        private void HandleIntent(Intent intent)
        {
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
            }

            if(null != fragment) {
                FragmentTransitionUtil.Transition(this, SupportFragmentManager.BeginTransaction(), Resource.Id.frame_content, fragment);
            }

            menuItem.SetChecked(true);
        }
	}
}
