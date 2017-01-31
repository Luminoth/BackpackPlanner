using System;

namespace EnergonSoftware.BackpackPlanner.Droid
{
    public sealed class PermissionRequest
    {
        public const int StoragePermissionRequestCode = 1001;

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

        public int RequestCode { get; }

        public PermissionRequestState State { get; private set; } = PermissionRequestState.Denied;

        public bool IsRequested => PermissionRequestState.Requested == State;

        public bool IsGranted => PermissionRequestState.Granted == State;

        public bool IsDenied => PermissionRequestState.Denied == State; 

        public PermissionRequest(int requestCode)
        {
            RequestCode = requestCode;
        }

        public void NotifyRequested(object sender)
        {
            State = PermissionRequestState.Requested;
            PermissionRequestedEvent?.Invoke(sender, EventArgs.Empty);
        }

        public void NotifyGranted(object sender)
        {
            State = PermissionRequestState.Granted;
            PermissionGrantedEvent?.Invoke(sender, EventArgs.Empty);
        }

        public void NotifyDenied(object sender)
        {
            State = PermissionRequestState.Denied;
            PermissionDeniedEvent?.Invoke(sender, EventArgs.Empty);
        }
    }
}