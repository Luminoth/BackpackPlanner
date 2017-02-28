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

        public static PermissionType GetPermissionForDroidRequestCode(DroidPermissionRequestCode requestCode)
        {
            switch(requestCode)
            {
            case DroidPermissionRequestCode.ReadStorage:
                return PermissionType.ReadStorage;
            case DroidPermissionRequestCode.WriteStorage:
                return PermissionType.WriteStorage;
            }
            return PermissionType.Invalid;
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

        public static string GetDroidPermissionForRequestCode(DroidPermissionRequestCode requestCode)
        {
            switch(requestCode)
            {
            case DroidPermissionRequestCode.ReadCalendar:
                return Manifest.Permission.ReadCalendar;
            case DroidPermissionRequestCode.WriteCalendar:
                return Manifest.Permission.WriteCalendar;
            case DroidPermissionRequestCode.Camera:
                return Manifest.Permission.Camera;
            case DroidPermissionRequestCode.ReadContacts:
                return Manifest.Permission.ReadContacts;
            case DroidPermissionRequestCode.WriteContacts:
                return Manifest.Permission.WriteContacts;
            case DroidPermissionRequestCode.GetAccounts:
                return Manifest.Permission.GetAccounts;
            case DroidPermissionRequestCode.FineLocation:
                return Manifest.Permission.AccessFineLocation;
            case DroidPermissionRequestCode.CoarseLocation:
                return Manifest.Permission.AccessCoarseLocation;
            case DroidPermissionRequestCode.RecordAudio:
                return Manifest.Permission.RecordAudio;
            case DroidPermissionRequestCode.ReadPhoneState:
                return Manifest.Permission.ReadPhoneState;
            case DroidPermissionRequestCode.CallPhone:
                return Manifest.Permission.CallPhone;
            case DroidPermissionRequestCode.ReadCallLog:
                return Manifest.Permission.ReadCallLog;
            case DroidPermissionRequestCode.WriteCallLog:
                return Manifest.Permission.WriteCallLog;
            case DroidPermissionRequestCode.AddVoicemail:
                return Manifest.Permission.AddVoicemail;
            case DroidPermissionRequestCode.UseSIP:
                return Manifest.Permission.UseSip;
            case DroidPermissionRequestCode.ProcessOutgoingCalls:
                return Manifest.Permission.ProcessOutgoingCalls;
            case DroidPermissionRequestCode.BodySensors:
                return Manifest.Permission.BodySensors;
            case DroidPermissionRequestCode.SendSMS:
                return Manifest.Permission.SendSms;
            case DroidPermissionRequestCode.ReceiveSMS:
                return Manifest.Permission.ReceiveSms;
            case DroidPermissionRequestCode.ReadSMS:
                return Manifest.Permission.ReadSms;
            case DroidPermissionRequestCode.ReceiveMMS:
                return Manifest.Permission.ReceiveMms;
            case DroidPermissionRequestCode.ReadStorage:
                return Manifest.Permission.ReadExternalStorage;
            case DroidPermissionRequestCode.WriteStorage:
                return Manifest.Permission.WriteExternalStorage;
            }
            return string.Empty;
        }

        public string DroidPermission { get; }

        public BaseActivity Activity { get; }

        public int RequestCode { get; }

        public DroidPermissionRequest(BaseActivity activity, PermissionType permission)
            : base(permission)
        {
            DroidPermission = GetDroidPermissionForPermission(permission);
            RequestCode = (int)GetRequestCodeForDroidPermission(DroidPermission);

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