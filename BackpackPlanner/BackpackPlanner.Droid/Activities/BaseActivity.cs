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
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Android.Content.PM;
using Android.OS;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Droid.Logging;
using EnergonSoftware.BackpackPlanner.Droid.Permissions;

namespace EnergonSoftware.BackpackPlanner.Droid.Activities
{
    public class BaseActivity : Android.Support.V7.App.AppCompatActivity
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(BaseActivity));

        // this needs to be static for the managers it contains
        private static BackpackPlannerState _backpackPlannerStateInstance;

        // need a separate reference to this because it needs to
        // track the current activity
        private static DroidPermissionRequestFactory _permissionRequestFactory;

        private static bool _preferencesLoaded;

        static BaseActivity()
        {
            CustomLogger.ReverseLogBufferDirection = true;
            CustomLogger.PlatformLogger = new DroidLogger();
        }

        public BackpackPlannerState BackpackPlannerState => _backpackPlannerStateInstance;

#if DEBUG_LIFECYCLE
        private readonly Stopwatch _startupStopwatch = new Stopwatch();
#endif

#region Controls
        public Android.Support.V7.Widget.Toolbar Toolbar { get; private set; }
#endregion

        private readonly Dictionary<DroidPermissionRequest.DroidPermissionRequestCode, List<DroidPermissionRequest>> _permissionRequests = new Dictionary<DroidPermissionRequest.DroidPermissionRequestCode, List<DroidPermissionRequest>>();

#region Activity Lifecycle
        protected override void OnCreate(Bundle savedInstanceState)
        {
#if DEBUG_LIFECYCLE
            Logger.Debug($"OnCreate - {GetType()}");

            _startupStopwatch.Start();
#endif

            base.OnCreate(savedInstanceState);

            if(null == _permissionRequestFactory) {
                _permissionRequestFactory = new DroidPermissionRequestFactory();
            }
            _permissionRequestFactory.Activity = this;

            if(null == _backpackPlannerStateInstance) {
                _backpackPlannerStateInstance = new BackpackPlannerState(
                    new HockeyAppManager(),
                    new DroidSettingsManager(Android.Support.V7.Preferences.PreferenceManager.GetDefaultSharedPreferences(this)),
                    new DroidPlayServicesManager(),
                    _permissionRequestFactory
                );

                // have to do this on the main thread
                _backpackPlannerStateInstance.InitAsync().Wait();
            }

            LoadPreferences(this);

            ((HockeyAppManager)_backpackPlannerStateInstance.PlatformHockeyAppManager).OnCreate(this);
        }

        protected override void OnDestroy()
        {
#if DEBUG_LIFECYCLE
            Logger.Debug($"OnDestroy - {GetType()}");
#endif

            // remove waiting permission requests
            foreach(var kvp in _permissionRequests) {
                kvp.Value.RemoveAll(x => this == x.Activity);
            }

            // this is bad, we only want to do this if the
            // full application is closed, not the activity
            //_backpackPlannerState.Destroy();

            base.OnDestroy();
        }

        protected override void OnStart()
        {
#if DEBUG_LIFECYCLE
            Logger.Debug($"OnStart - {GetType()}");

            if(_startupStopwatch.IsRunning) {
                Logger.Debug($"Time to Activity.Start(): {_startupStopwatch.ElapsedMilliseconds}ms");
            }
#endif

            base.OnStart();
        }

        protected override void OnStop()
        {
#if DEBUG_LIFECYCLE
            Logger.Debug($"OnStop - {GetType()}");
#endif

            base.OnStop();
        }

        protected override void OnResume()
        {
#if DEBUG_LIFECYCLE
            Logger.Debug($"OnResume - {GetType()}");

            if(_startupStopwatch.IsRunning) {
                Logger.Debug($"Time to Activity.OnResume() finish: {_startupStopwatch.ElapsedMilliseconds}ms");
            }
            _startupStopwatch.Stop();
#endif

            base.OnResume();

            ((HockeyAppManager)_backpackPlannerStateInstance.PlatformHockeyAppManager).OnResume(this);
        }

        protected override void OnPause()
        {
#if DEBUG_LIFECYCLE
            Logger.Debug($"OnPause - {GetType()}");
#endif

            ((HockeyAppManager)_backpackPlannerStateInstance.PlatformHockeyAppManager).OnPause(this);

            base.OnPause();
        }
#endregion

#region Permissions
        // https://developer.android.com/training/permissions/index.html
        // https://developer.android.com/training/permissions/requesting.html

        /// <summary>
        /// Permission request callback
        /// </summary>
        /// <param name="requestCode">The request code.</param>
        /// <param name="permissions">The permissions.</param>
        /// <param name="grantResults">The grant results.</param>
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            DroidPermissionRequest.DroidPermissionRequestCode droidRequestCode = (DroidPermissionRequest.DroidPermissionRequestCode)requestCode;
            bool granted = grantResults.Length > 0 && grantResults[0] == Permission.Granted;

            Logger.Info($"Got permission result for request code {droidRequestCode}: {granted}");

            List<DroidPermissionRequest> requests;
            if(!_permissionRequests.TryGetValue(droidRequestCode, out requests)) {
                Logger.Warn($"Attempt to notify for request code {droidRequestCode}, which does not exist!");
                return;
            }

            foreach(DroidPermissionRequest request in requests) {
                request.Notify(granted);
            }
            requests.Clear();
        }

        /// <summary>
        /// Checks for the given permission.
        /// </summary>
        /// <param name="permissionRequest">The permission request.</param>
        /// <param name="showExplanation">Callback to show explanation.</param>
        /// <returns>The permission request</returns>
        /// <remarks>
        /// Caller must Notify() on the returned request
        /// </remarks>
        public async Task CheckPermission(DroidPermissionRequest permissionRequest, Func<Task> showExplanation=null)
        {
            // TODO: does this  need to be forced onto the UI thread?

            Logger.Info($"Checking permission {permissionRequest.Permission} (DroidPermission={permissionRequest.DroidPermission}, RequestCode={permissionRequest.RequestCode})...");

            // permission already granted
            if(Permission.Granted == Android.Support.V4.Content.ContextCompat.CheckSelfPermission(this, permissionRequest.DroidPermission)) {
                Logger.Info("Permission already granted, notifying...");
                permissionRequest.Notify(true);
                return;
            }

            // need to show rationale first? only happens if permission is denied
            if(Android.Support.V4.App.ActivityCompat.ShouldShowRequestPermissionRationale(this, permissionRequest.DroidPermission)) {
                Logger.Info("Permission rationale required...");

                if(null == showExplanation) {
                    Logger.Info("No rationale specified, notifying denied...");

                    // no rationale to show, so the request is denied
                    permissionRequest.Notify(false);
                    return;
                }

                // wait for the user to get on board
                Logger.Info("Showing rationale...");
                await showExplanation().ConfigureAwait(false);

                // re-check the permission (with no explanation this time)
                Logger.Info("Re-checking permission...");
                await CheckPermission(permissionRequest, null).ConfigureAwait(false);
                return;
            }

            List<DroidPermissionRequest> requests;
            if(!_permissionRequests.TryGetValue(permissionRequest.RequestCode, out requests)) {
                requests = new List<DroidPermissionRequest>();
                _permissionRequests.Add(permissionRequest.RequestCode, requests);
            }

            if(!requests.Any()) {
                Logger.Info($"Requesting permission {permissionRequest.Permission} ({permissionRequest.DroidPermission}) using request code {(int)permissionRequest.RequestCode}...");
                Android.Support.V4.App.ActivityCompat.RequestPermissions(this, new[] { permissionRequest.DroidPermission }, (int)permissionRequest.RequestCode);
            }

            requests.Add(permissionRequest);
        }
#endregion

        private void LoadPreferences(BaseActivity activity)
        {
            if(_preferencesLoaded) {
                return;
            }

            Logger.Debug("Setting default preferences...");
            Android.Support.V7.Preferences.PreferenceManager.SetDefaultValues(activity, Resource.Xml.settings, false);

            Logger.Debug("Loading preferences...");
            _backpackPlannerStateInstance.PlatformSettingsManager.Load(_backpackPlannerStateInstance.Settings, _backpackPlannerStateInstance.PersonalInformation);

            _preferencesLoaded = true;
        }

        protected void InitToolbar()
        {
            Toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(Toolbar);
        }
    }
}
