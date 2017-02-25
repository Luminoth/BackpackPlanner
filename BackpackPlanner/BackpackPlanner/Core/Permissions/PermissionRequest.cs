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
    public abstract class PermissionRequest
    {
#region Events
        public event EventHandler<EventArgs> PermissionGrantedEvent;

        public event EventHandler<EventArgs> PermissionDeniedEvent;
#endregion

        public abstract string Permission { get; }

        /// <summary>
        /// Requests the specified permission type.
        /// </summary>
        /// <param name="state">Application state.</param>
        public abstract void Request(BackpackPlannerState state);

        /// <summary>
        /// Notifies listeners that the permission request has completed.
        /// </summary>
        /// <param name="granted">Whether or not the permission was granted.</param>
        public void Notify(bool granted)
        {
            if(granted) {
                NotifyGranted();
            } else {
                NotifyDenied();
            }
        }

        private void NotifyGranted()
        {
            PermissionGrantedEvent?.Invoke(this, EventArgs.Empty);
        }

        private void NotifyDenied()
        {
            PermissionDeniedEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
