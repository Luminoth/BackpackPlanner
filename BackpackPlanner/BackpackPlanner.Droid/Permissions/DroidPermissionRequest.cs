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
using System.Threading.Tasks;

using Android;

using EnergonSoftware.BackpackPlanner.Core.Permissions;
using EnergonSoftware.BackpackPlanner.Droid.Activities;

namespace EnergonSoftware.BackpackPlanner.Droid.Permissions
{
    // https://developer.android.com/guide/topics/permissions/requesting.html
    // https://developer.android.com/reference/android/Manifest.permission_group.html
    public sealed class DroidPermissionRequest : PermissionRequest
    {
        // values here must be under 16 bits
        // http://stackoverflow.com/questions/33331073/android-what-to-choose-for-requestcode-values
        // https://android.googlesource.com/platform/frameworks/support/+/86f3b80ddf7f9aa5c5b7afe77217cb75632d62a2%5E%21/#F0
        public enum DroidPermissionRequestCode
        {
            Invalid = -1,

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
            AddVoicemail,
            UseSIP,
            ProcessOutgoingCalls,

            BodySensors,

            SendSMS,
            ReceiveSMS,
            ReadSMS,
            ReceiveMMS,

            ReadStorage,
            WriteStorage
        }

        private static string GetDroidPermissionForPermission(PermissionType permission)
        {
            switch(permission)
            {
            case PermissionType.ReadStorage:
                return Manifest.Permission.ReadExternalStorage;
            case PermissionType.WriteStorage:
                return Manifest.Permission.WriteExternalStorage;
            }
            return string.Empty;
        }

        private static DroidPermissionRequestCode GetRequestCodeForDroidPermission(string droidPermission)
        {
            switch(droidPermission)
            {
            case Manifest.Permission.ReadCalendar:
                return DroidPermissionRequestCode.ReadCalendar;
            case Manifest.Permission.WriteCalendar:
                return DroidPermissionRequestCode.WriteCalendar;
            case Manifest.Permission.Camera:
                return DroidPermissionRequestCode.Camera;
            case Manifest.Permission.ReadContacts:
                return DroidPermissionRequestCode.ReadContacts;
            case Manifest.Permission.WriteContacts:
                return DroidPermissionRequestCode.WriteContacts;
            case Manifest.Permission.GetAccounts:
                return DroidPermissionRequestCode.GetAccounts;
            case Manifest.Permission.AccessFineLocation:
                return DroidPermissionRequestCode.FineLocation;
            case Manifest.Permission.AccessCoarseLocation:
                return DroidPermissionRequestCode.CoarseLocation;
            case Manifest.Permission.RecordAudio:
                return DroidPermissionRequestCode.RecordAudio;
            case Manifest.Permission.ReadPhoneState:
                return DroidPermissionRequestCode.ReadPhoneState;
            case Manifest.Permission.CallPhone:
                return DroidPermissionRequestCode.CallPhone;
            case Manifest.Permission.ReadCallLog:
                return DroidPermissionRequestCode.ReadCallLog;
            case Manifest.Permission.WriteCallLog:
                return DroidPermissionRequestCode.WriteCallLog;
            case Manifest.Permission.AddVoicemail:
                return DroidPermissionRequestCode.AddVoicemail;
            case Manifest.Permission.UseSip:
                return DroidPermissionRequestCode.UseSIP;
            case Manifest.Permission.ProcessOutgoingCalls:
                return DroidPermissionRequestCode.ProcessOutgoingCalls;
            case Manifest.Permission.BodySensors:
                return DroidPermissionRequestCode.BodySensors;
            case Manifest.Permission.SendSms:
                return DroidPermissionRequestCode.SendSMS;
            case Manifest.Permission.ReceiveSms:
                return DroidPermissionRequestCode.ReceiveSMS;
            case Manifest.Permission.ReadSms:
                return DroidPermissionRequestCode.ReadSMS;
            case Manifest.Permission.ReceiveMms:
                return DroidPermissionRequestCode.ReceiveMMS;
            case Manifest.Permission.ReadExternalStorage:
                return DroidPermissionRequestCode.ReadStorage;
            case Manifest.Permission.WriteExternalStorage:
                return DroidPermissionRequestCode.WriteStorage;
            }
            return DroidPermissionRequestCode.Invalid;
        }

        public string DroidPermission { get; }

        public BaseActivity Activity { get; }

        public DroidPermissionRequestCode RequestCode { get; }

        private bool _isGranted;

        private bool _isNotified;

        public DroidPermissionRequest(BaseActivity activity, PermissionType permission)
            : base(permission)
        {
            DroidPermission = GetDroidPermissionForPermission(permission);
            RequestCode = GetRequestCodeForDroidPermission(DroidPermission);

            Activity = activity;
        }

        public override Task<bool> Request(BackpackPlannerState state)
        {
            return Task.Run(async () =>
                {
                    await Activity.CheckPermission(this).ConfigureAwait(false);
                    while(!_isNotified) {
                        await Task.Delay(1).ConfigureAwait(false);
                    }
                    return _isGranted;
                }
            );
        }

        public Task<bool> Request(BackpackPlannerState state, Func<Task> showExplanation)
        {
            return Task.Run(async () =>
                {
                    await Activity.CheckPermission(this, showExplanation).ConfigureAwait(false);
                    while(!_isNotified) {
                        await Task.Delay(1).ConfigureAwait(false);
                    }
                    return _isGranted;
                }
            );
        }

        public void Notify(bool granted)
        {
            _isGranted = granted;
            _isNotified = true;
        }
    }
}