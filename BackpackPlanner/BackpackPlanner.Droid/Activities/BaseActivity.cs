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
using System.Threading.Tasks;

using Android;
using Android.Content.PM;
using Android.OS;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Core.Util;
using EnergonSoftware.BackpackPlanner.Droid.Util;

namespace EnergonSoftware.BackpackPlanner.Droid.Activities
{
    public class BaseActivity : Android.Support.V7.App.AppCompatActivity
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(BaseActivity));

        private readonly Stopwatch _startupStopwatch = new Stopwatch();

        private readonly Dictionary<PermissionRequest.PermissionRequestCode, PermissionRequest> _permissionRequests = new Dictionary<PermissionRequest.PermissionRequestCode, PermissionRequest>();

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

            PermissionRequest request = _permissionRequests.GetAndRemove((PermissionRequest.PermissionRequestCode)requestCode);
            if(null == request) {
                Logger.Error($"Got request permission result for request code {requestCode} but permission request is missing!");
                return;
            }

            if(grantResults.Length > 0 && grantResults[0] == Permission.Granted) {
                request.SetState(PermissionRequest.PermissionRequestState.Granted);
            } else {
                request.SetState(PermissionRequest.PermissionRequestState.Denied);
            }
            request.Notify(this);
        }


        /// <summary>
        /// Checks for the given permission.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <param name="requestCode">The request code.</param>
        /// <param name="showExplanation">Callback to show explanation.</param>
        /// <returns>The permission request</returns>
        /// <remarks>
        /// Caller must Notify() on the returned request
        /// </remarks>
        public async Task<PermissionRequest> CheckPermission(string permission, PermissionRequest.PermissionRequestCode requestCode, Func<Task> showExplanation=null)
        {
            PermissionRequest request;

            // permission already granted
            if(Permission.Granted == Android.Support.V4.Content.ContextCompat.CheckSelfPermission(this, permission)) {
                request = new PermissionRequest(requestCode);
                request.SetState(PermissionRequest.PermissionRequestState.Granted);
                return request;
            }

            // need to show rationale first? only happens if permission is denied
            if(Android.Support.V4.App.ActivityCompat.ShouldShowRequestPermissionRationale(this, permission)) {
                if(null == showExplanation) {
                    // no rationale to show, so the request is denied
                    request = new PermissionRequest(requestCode);
                    request.SetState(PermissionRequest.PermissionRequestState.Denied);
                    return request;
                }

                // wait for the user to get on board
                await showExplanation().ConfigureAwait(false);

                // re-check the permission (with no explanation this time)
                return await CheckPermission(permission, requestCode, null).ConfigureAwait(false);
            }

            // look for an existing request to return
            if(_permissionRequests.TryGetValue(requestCode, out request)) {
                return request;
            }

            // create a new request
            request = new PermissionRequest(requestCode);
            _permissionRequests.Add(requestCode, request);

            Android.Support.V4.App.ActivityCompat.RequestPermissions(this, new[] { permission }, (int)requestCode);
            request.SetState(PermissionRequest.PermissionRequestState.Requested);

            return request;
        }

        /// <summary>
        /// Checks for the READ_EXTERNAL_STORAGE permission.
        /// </summary>
        /// <param name="requestCode">The request code.</param>
        /// <returns>The permission request</returns>
        /// <remarks>
        /// Caller must Notify() on the returned request
        /// </remarks>
        public async Task<PermissionRequest> CheckStoragePermission(PermissionRequest.PermissionRequestCode requestCode)
        {
            PermissionRequest request = await CheckPermission(Manifest.Permission.ReadExternalStorage, requestCode).ConfigureAwait(false);
            request.PermissionDeniedEvent += (sender, args) => {
                DialogUtil.ShowOkDialog(this, Resource.String.title_storage_permission, Resource.String.label_storage_permission);
            };
            return request;
        }
#endregion

        protected void InitToolbar()
        {
            Toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(Toolbar);
        }
    }
}
