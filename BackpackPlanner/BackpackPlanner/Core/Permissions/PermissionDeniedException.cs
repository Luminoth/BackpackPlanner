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

using System;

namespace EnergonSoftware.BackpackPlanner.Core.Permissions
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Exception" />
    public sealed class PermissionDeniedException : Exception
    {
        /// <summary>
        /// Gets the permission that was requested.
        /// </summary>
        /// <value>
        /// The permission that was requested.
        /// </value>
        public PermissionRequest.PermissionType Permission { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionDeniedException"/> class.
        /// </summary>
        /// <param name="permission">The permission that was requested.</param>
        public PermissionDeniedException(PermissionRequest.PermissionType permission)
        {
            Permission = permission;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionDeniedException"/> class.
        /// </summary>
        /// <param name="permission">The permission that was requested.</param>
        /// <param name="message">The message.</param>
        public PermissionDeniedException(PermissionRequest.PermissionType permission, string message)
            : base(message)
        {
            Permission = permission;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionDeniedException"/> class.
        /// </summary>
        /// <param name="permission">The permission that was requested.</param>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner exception.</param>
        public PermissionDeniedException(PermissionRequest.PermissionType permission, string message, Exception inner)
            : base(message, inner)
        {
            Permission = permission;
        }
    }
}
