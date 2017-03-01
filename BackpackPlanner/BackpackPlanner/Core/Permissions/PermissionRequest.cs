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
    public abstract class PermissionRequest
    {
        /// <summary>
        /// Abstracted permission types that we need to be able to request.
        /// </summary>
        public enum PermissionType
        {
            Invalid = -1,

            ReadStorage = 1,
            WriteStorage
        }

        /// <summary>
        /// Gets the permission being requested.
        /// </summary>
        /// <value>
        /// The permission being requested.
        /// </value>
        public PermissionType Permission { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionRequest"/> class.
        /// </summary>
        /// <param name="permission">The permission to request.</param>
        protected PermissionRequest(PermissionType permission)
        {
            Permission = permission;
        }

        /// <summary>
        /// Requests the specified permission type.
        /// </summary>
        /// <param name="state">Application state.</param>
        public abstract Task<bool> Request(BackpackPlannerState state);
    }
}
