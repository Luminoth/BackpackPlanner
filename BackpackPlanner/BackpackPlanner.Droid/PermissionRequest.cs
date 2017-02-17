/*
   Copyright 2016 Shane Lillie

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

namespace EnergonSoftware.BackpackPlanner.Droid
{
    public sealed class PermissionRequest
    {
        // based on the dangerous permission list
        public enum PermissionRequestCode
        {
            Calendar = 1001,
            Camera,
            Contacts,
            Location,
            Microphone,
            Phone,
            Sensors,
            SMS,
            Storage
        }

        public enum PermissionRequestState
        {
            Requested,
            Granted,
            Denied
        }

#region Events
        public event EventHandler<EventArgs> PermissionRequestedEvent;
        public event EventHandler<EventArgs> PermissionGrantedEvent;
        public event EventHandler<EventArgs> PermissionDeniedEvent;
#endregion

        public PermissionRequestCode RequestCode { get; }

        public PermissionRequestState State { get; private set; } = PermissionRequestState.Denied;

        public bool IsRequested => PermissionRequestState.Requested == State;

        public bool IsGranted => PermissionRequestState.Granted == State;

        public bool IsDenied => PermissionRequestState.Denied == State; 

        public PermissionRequest(PermissionRequestCode requestCode)
        {
            RequestCode = requestCode;
        }

        public void SetState(PermissionRequestState state)
        {
            State = state;
        }

        public void Notify(object sender)
        {
            switch(State)
            {
            case PermissionRequestState.Requested:
                NotifyRequested(sender);
                break;
            case PermissionRequestState.Granted:
                NotifyGranted(sender);
                break;
            case PermissionRequestState.Denied:
                NotifyDenied(sender);
                break;
            }
        }

        private void NotifyRequested(object sender)
        {
            State = PermissionRequestState.Requested;
            PermissionRequestedEvent?.Invoke(sender, EventArgs.Empty);
        }

        private void NotifyGranted(object sender)
        {
            State = PermissionRequestState.Granted;
            PermissionGrantedEvent?.Invoke(sender, EventArgs.Empty);
        }

        private void NotifyDenied(object sender)
        {
            State = PermissionRequestState.Denied;
            PermissionDeniedEvent?.Invoke(sender, EventArgs.Empty);
        }
    }
}