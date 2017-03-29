/*
   Copyright 2017 Shane Lillie

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

using EnergonSoftware.BackpackPlanner.Core.Permissions;
using EnergonSoftware.BackpackPlanner.Droid.Activities;
using EnergonSoftware.BackpackPlanner.Droid.Util;

using JetBrains.Annotations;

namespace EnergonSoftware.BackpackPlanner.Droid.Permissions
{
    public sealed class DroidPermissionRequestFactory : PermissionRequestFactory
    {
        [CanBeNull]
        public BaseActivity Activity { get; set; }

        public override PermissionRequest Create(PermissionRequest.PermissionType permission)
        {
            return new DroidPermissionRequest(Activity, permission);
        }

// TODO: revisit this at some point to re-integrate the permission denied event handling

/*
        /// <summary>
        /// Checks for the READ_EXTERNAL_STORAGE permission.
        /// </summary>
        public DroidPermissionRequest CreateReadStoragePermissionRequest()
        {
            DroidPermissionRequest permissionRequest = (DroidPermissionRequest)Create(PermissionRequest.PermissionType.ReadStorage);
            permissionRequest.PermissionDeniedEvent += (sender, args) => {
// TODO: fill this out better to try and explain why we need this and then to ask for permission again (with a yes/no or ok/cancel box)
                DialogUtil.ShowOkDialog(Activity, Resource.String.title_storage_permission, Resource.String.label_storage_permission);
            };
            return permissionRequest;
        }

        /// <summary>
        /// Checks for the WRITE_EXTERNAL_STORAGE permission.
        /// </summary>
        public DroidPermissionRequest CreateWriteStoragePermissionRequest()
        {
            DroidPermissionRequest permissionRequest = (DroidPermissionRequest)Create(PermissionRequest.PermissionType.WriteStorage);
            permissionRequest.PermissionDeniedEvent += (sender, args) => {
// TODO: fill this out better to try and explain why we need this and then to ask for permission again (with a yes/no or ok/cancel box)
                DialogUtil.ShowOkDialog(Activity, Resource.String.title_storage_permission, Resource.String.label_storage_permission);
            };
            return permissionRequest;
        }
*/
    }
}