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

        private readonly Dictionary<int, PermissionRequest> _permissionRequests = new Dictionary<int, PermissionRequest>();

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

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            PermissionRequest request = _permissionRequests.GetAndRemove(requestCode);
            if(null == request) {
                Logger.Error($"Got request permission result for request code {requestCode} but permission request is missing!");
                return;
            }

            if(grantResults.Length > 0 && grantResults[0] == Permission.Granted) {
                request.NotifyGranted(this);
            } else {
                request.NotifyDenied(this);
            }
        }

        public async Task<PermissionRequest> CheckPermission(string permission, int requestCode, Func<Task> showExplanation=null)
        {
            PermissionRequest request;

            if(Permission.Granted == Android.Support.V4.Content.ContextCompat.CheckSelfPermission(this, permission)) {
                request = new PermissionRequest(requestCode);
                request.NotifyGranted(this);
                return request;
            }

            if(Android.Support.V4.App.ActivityCompat.ShouldShowRequestPermissionRationale(this, permission)) {
                if(null == showExplanation) {
                    request = new PermissionRequest(requestCode);
                    request.NotifyDenied(this);
                    return request;
                }

                await showExplanation().ConfigureAwait(false);

                return await CheckPermission(permission, requestCode, null).ConfigureAwait(false);
            }

            if(_permissionRequests.TryGetValue(requestCode, out request)) {
                return request;
            }

            request = new PermissionRequest(requestCode);
            _permissionRequests.Add(requestCode, request);

            Android.Support.V4.App.ActivityCompat.RequestPermissions(this, new[] { permission }, requestCode);

            return request;
        }

        public async Task<PermissionRequest> CheckStoragePermission(int requestCode)
        {
            PermissionRequest request = await CheckPermission(Manifest.Permission.ReadExternalStorage, requestCode).ConfigureAwait(false);
            request.PermissionDeniedEvent += (sender, args) => {
                DialogUtil.ShowOkDialog(this, Resource.String.title_storage_permission, Resource.String.label_storage_permission);
            };

            if(request.IsDenied) {
                request.NotifyDenied(this);
            }
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
