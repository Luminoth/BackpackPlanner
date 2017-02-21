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
using System.Diagnostics;
using System.Threading.Tasks;

using Android.Content.PM;
using Android.OS;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Core.Permissions;
using EnergonSoftware.BackpackPlanner.Droid.Permissions;

namespace EnergonSoftware.BackpackPlanner.Droid.Activities
{
    public class BaseActivity : Android.Support.V7.App.AppCompatActivity
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(BaseActivity));

        private readonly Stopwatch _startupStopwatch = new Stopwatch();

#region Controls
        public Android.Support.V7.Widget.Toolbar Toolbar { get; private set; }
#endregion

#region Activity Lifecycle
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _startupStopwatch.Start();

            DroidState.Instance.OnCreate(this).Wait();
        }

        protected override void OnStart()
        {
            base.OnStart();

            Logger.Debug($"OnStart - {GetType()}");

            if(_startupStopwatch.IsRunning) {
                Logger.Debug($"Time to Activity.Start(): {_startupStopwatch.ElapsedMilliseconds}ms");
            }
        }

        protected override void OnResume()
        {
            base.OnResume();

            Logger.Debug($"OnResume - {GetType()}");

            DroidState.Instance.OnResume(this);

            if(_startupStopwatch.IsRunning) {
                Logger.Debug($"Time to Activity.OnResume() finish: {_startupStopwatch.ElapsedMilliseconds}ms");
            }
            _startupStopwatch.Stop();
        }

        protected override void OnPause()
        {
            base.OnPause();

            Logger.Debug($"OnPause - {GetType()}");

            DroidState.Instance.OnPause(this);
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

            DroidState.Instance.BackpackPlannerState.NotifyPermissionRequests((PermissionRequest.PermissionType)requestCode, grantResults.Length > 0 && grantResults[0] == Permission.Granted);
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
            Logger.Info($"Requesting permission {permissionRequest.Permission}...");

            // permission already granted
            if(Permission.Granted == Android.Support.V4.Content.ContextCompat.CheckSelfPermission(this, permissionRequest.Permission)) {
                permissionRequest.Notify(true);
                return;
            }

            // need to show rationale first? only happens if permission is denied
            if(Android.Support.V4.App.ActivityCompat.ShouldShowRequestPermissionRationale(this, permissionRequest.Permission)) {
                if(null == showExplanation) {
                    // no rationale to show, so the request is denied
                    permissionRequest.Notify(false);
                    return;
                }

                // wait for the user to get on board
                await showExplanation().ConfigureAwait(false);

                // re-check the permission (with no explanation this time)
                await CheckPermission(permissionRequest, null).ConfigureAwait(false);
                return;
            }

            // storage the request
            DroidState.Instance.BackpackPlannerState.AddPermissionRequest(permissionRequest);

            // request the permission
            Android.Support.V4.App.ActivityCompat.RequestPermissions(this, new[] { permissionRequest.Permission }, (int)permissionRequest.Type);
        }
#endregion

        protected void InitToolbar()
        {
            Toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(Toolbar);
        }
    }
}
