﻿/*
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
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// based on the Android dangerous permission list
        /// https://developer.android.com/guide/topics/permissions/requesting.html#normal-dangerous
        /// TODO: if there are other system-level permissions
        /// to request, they should be added here
        /// </remarks>
        public enum PermissionType
        {
            ReadCalendar = 1001,

            WriteCalendar,

            Camera,

            ReadContacts,

            WriteContacts,

            GetAccounts,

            FineLocation,

            CoarseLocation,

            RecordAudio,

            ReadPhoneState,

            CallPhone,

            ReadCallLog,

            WriteCallLog,

            AddVoiceMail,

            UseSip,

            ProcessOutgoingCalls,

            BodySensors,

            SendSMS,

            ReceiveSMS,

            ReadSMS,

            ReceiveWapPush,

            ReceiveMMS,

            ReadStorage,

            WriteStorage
        }

#region Events
        public event EventHandler<EventArgs> PermissionGrantedEvent;

        public event EventHandler<EventArgs> PermissionDeniedEvent;
#endregion

        /// <summary>
        /// Gets the permission type.
        /// </summary>
        /// <value>
        /// The permission type.
        /// </value>
        public PermissionType Type { get; }

        protected PermissionRequest(PermissionType permissionType)
        {
            Type = permissionType;
        }

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