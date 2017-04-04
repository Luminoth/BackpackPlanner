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

using System.Threading.Tasks;

namespace EnergonSoftware.BackpackPlanner.Core.Permissions
{
    /// <summary>
    /// 
    /// </summary>
    public static class PermissionHelper
    {
        /// <summary>
        /// Checks for the read permission.
        /// </summary>
        /// <param name="state">The system state.</param>
        public static async Task CheckReadPermission(BackpackPlannerState state)
        {
            if(!await state.PlatformPermissionRequestFactory.Create(PermissionRequest.PermissionType.ReadStorage).Request(state).ConfigureAwait(false)) {
                throw new PermissionDeniedException(PermissionRequest.PermissionType.ReadStorage, "Read permission denied!");
            }
        }

        /// <summary>
        /// Checks for the write permission.
        /// </summary>
        /// <param name="state">The system state.</param>
        public static async Task CheckWritePermission(BackpackPlannerState state)
        {
            if(!await state.PlatformPermissionRequestFactory.Create(PermissionRequest.PermissionType.WriteStorage).Request(state).ConfigureAwait(false)) {
                throw new PermissionDeniedException(PermissionRequest.PermissionType.WriteStorage, "Write permission denied!");
            }
        }
    }
}
