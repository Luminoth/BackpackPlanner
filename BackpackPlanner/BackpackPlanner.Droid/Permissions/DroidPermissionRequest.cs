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
        public enum PermissionRequestCode
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

        public static PermissionRequestCode GetRequestCodeForPermission(string permissionGroup)
        {
            switch(permissionGroup)
            {
            case Manifest.Permission.ReadCalendar:
                return PermissionRequestCode.ReadCalendar;
            case Manifest.Permission.WriteCalendar:
                return PermissionRequestCode.WriteCalendar;
            case Manifest.Permission.Camera:
                return PermissionRequestCode.Camera;
            case Manifest.Permission.ReadContacts:
                return PermissionRequestCode.ReadContacts;
            case Manifest.Permission.WriteContacts:
                return PermissionRequestCode.WriteContacts;
            case Manifest.Permission.GetAccounts:
                return PermissionRequestCode.GetAccounts;
            case Manifest.Permission.AccessFineLocation:
                return PermissionRequestCode.FineLocation;
            case Manifest.Permission.AccessCoarseLocation:
                return PermissionRequestCode.CoarseLocation;
            case Manifest.Permission.RecordAudio:
                return PermissionRequestCode.RecordAudio;
            case Manifest.Permission.ReadPhoneState:
                return PermissionRequestCode.ReadPhoneState;
            case Manifest.Permission.CallPhone:
                return PermissionRequestCode.CallPhone;
            case Manifest.Permission.ReadCallLog:
                return PermissionRequestCode.ReadCallLog;
            case Manifest.Permission.WriteCallLog:
                return PermissionRequestCode.WriteCallLog;
            case Manifest.Permission.AddVoicemail:
                return PermissionRequestCode.AddVoicemail;
            case Manifest.Permission.UseSip:
                return PermissionRequestCode.UseSIP;
            case Manifest.Permission.ProcessOutgoingCalls:
                return PermissionRequestCode.ProcessOutgoingCalls;
            case Manifest.Permission.BodySensors:
                return PermissionRequestCode.BodySensors;
            case Manifest.Permission.SendSms:
                return PermissionRequestCode.SendSMS;
            case Manifest.Permission.ReceiveSms:
                return PermissionRequestCode.ReceiveSMS;
            case Manifest.Permission.ReadSms:
                return PermissionRequestCode.ReadSMS;
            case Manifest.Permission.ReceiveMms:
                return PermissionRequestCode.ReceiveMMS;
            case Manifest.Permission.ReadExternalStorage:
                return PermissionRequestCode.ReadStorage;
            case Manifest.Permission.WriteExternalStorage:
                return PermissionRequestCode.WriteStorage;
            }
            return PermissionRequestCode.Invalid;
        }

        public static string GetPermissionForRequestCode(PermissionRequestCode requestCode)
        {
            switch(requestCode)
            {
            case PermissionRequestCode.ReadCalendar:
                return Manifest.Permission.ReadCalendar;
            case PermissionRequestCode.WriteCalendar:
                return Manifest.Permission.WriteCalendar;
            case PermissionRequestCode.Camera:
                return Manifest.Permission.Camera;
            case PermissionRequestCode.ReadContacts:
                return Manifest.Permission.ReadContacts;
            case PermissionRequestCode.WriteContacts:
                return Manifest.Permission.WriteContacts;
            case PermissionRequestCode.GetAccounts:
                return Manifest.Permission.GetAccounts;
            case PermissionRequestCode.FineLocation:
                return Manifest.Permission.AccessFineLocation;
            case PermissionRequestCode.CoarseLocation:
                return Manifest.Permission.AccessCoarseLocation;
            case PermissionRequestCode.RecordAudio:
                return Manifest.Permission.RecordAudio;
            case PermissionRequestCode.ReadPhoneState:
                return Manifest.Permission.ReadPhoneState;
            case PermissionRequestCode.CallPhone:
                return Manifest.Permission.CallPhone;
            case PermissionRequestCode.ReadCallLog:
                return Manifest.Permission.ReadCallLog;
            case PermissionRequestCode.WriteCallLog:
                return Manifest.Permission.WriteCallLog;
            case PermissionRequestCode.AddVoicemail:
                return Manifest.Permission.AddVoicemail;
            case PermissionRequestCode.UseSIP:
                return Manifest.Permission.UseSip;
            case PermissionRequestCode.ProcessOutgoingCalls:
                return Manifest.Permission.ProcessOutgoingCalls;
            case PermissionRequestCode.BodySensors:
                return Manifest.Permission.BodySensors;
            case PermissionRequestCode.SendSMS:
                return Manifest.Permission.SendSms;
            case PermissionRequestCode.ReceiveSMS:
                return Manifest.Permission.ReceiveSms;
            case PermissionRequestCode.ReadSMS:
                return Manifest.Permission.ReadSms;
            case PermissionRequestCode.ReceiveMMS:
                return Manifest.Permission.ReceiveMms;
            case PermissionRequestCode.ReadStorage:
                return Manifest.Permission.ReadExternalStorage;
            case PermissionRequestCode.WriteStorage:
                return Manifest.Permission.WriteExternalStorage;
            }
            return string.Empty;
        }

        public override string Permission { get; }


        public BaseActivity Activity { get; }

        public int RequestCode { get; }

        public DroidPermissionRequest(BaseActivity activity, string permission)
        {
            Permission = permission;
            RequestCode = (int)GetRequestCodeForPermission(Permission);

            Activity = activity;
        }

        public override async void Request(BackpackPlannerState state)
        {
            await Activity.CheckPermission(this).ConfigureAwait(false);
        }

        public async void Request(BackpackPlannerState state, Func<Task> showExplanation)
        {
            await Activity.CheckPermission(this, showExplanation).ConfigureAwait(false);
        }
    }
}