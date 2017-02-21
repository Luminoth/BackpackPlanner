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
    public sealed class DroidPermissionRequest : PermissionRequest
    {
        private static string PermissionTypeToPermission(PermissionType permissionType)
        {
            switch(permissionType)
            {
            case PermissionType.ReadCalendar:
                return Manifest.Permission.ReadCalendar;
            case PermissionType.WriteCalendar:
                return Manifest.Permission.WriteCalendar;
            case PermissionType.Camera:
                return Manifest.Permission.Camera;
            case PermissionType.ReadContacts:
                return Manifest.Permission.ReadContacts;
            case PermissionType.WriteContacts:
                return Manifest.Permission.WriteContacts;
            case PermissionType.GetAccounts:
                return Manifest.Permission.GetAccounts;
            case PermissionType.FineLocation:
                return Manifest.Permission.AccessFineLocation;
            case PermissionType.CoarseLocation:
                return Manifest.Permission.AccessCoarseLocation;
            case PermissionType.RecordAudio:
                return Manifest.Permission.RecordAudio;
            case PermissionType.ReadPhoneState:
                return Manifest.Permission.ReadPhoneState;
            case PermissionType.CallPhone:
                return Manifest.Permission.CallPhone;
            case PermissionType.ReadCallLog:
                return Manifest.Permission.ReadCallLog;
            case PermissionType.WriteCallLog:
                return Manifest.Permission.ReadCalendar;
            case PermissionType.AddVoiceMail:
                return Manifest.Permission.AddVoicemail;
            case PermissionType.UseSip:
                return Manifest.Permission.UseSip;
            case PermissionType.ProcessOutgoingCalls:
                return Manifest.Permission.ProcessOutgoingCalls;
            case PermissionType.BodySensors:
                return Manifest.Permission.BodySensors;
            case PermissionType.SendSMS:
                return Manifest.Permission.SendSms;
            case PermissionType.ReceiveSMS:
                return Manifest.Permission.ReceiveSms;
            case PermissionType.ReadSMS:
                return Manifest.Permission.ReadSms;
            case PermissionType.ReceiveWapPush:
                return Manifest.Permission.ReceiveWapPush;
            case PermissionType.ReceiveMMS:
                return Manifest.Permission.ReceiveMms;
            case PermissionType.ReadStorage:
                return Manifest.Permission.ReadExternalStorage;
            case PermissionType.WriteStorage:
                return Manifest.Permission.WriteExternalStorage;
            }
            return string.Empty;
        }

        public string Permission { get; }

        private readonly BaseActivity _activity;

        public DroidPermissionRequest(BaseActivity activity, PermissionType permissionType)
            : base(permissionType)
        {
            _activity = activity;
            Permission = PermissionTypeToPermission(permissionType);
        }

        public override async void Request(BackpackPlannerState state)
        {
            await _activity.CheckPermission(this).ConfigureAwait(false);
        }

        public async void Request(BackpackPlannerState state, Func<Task> showExplanation)
        {
            await _activity.CheckPermission(this, showExplanation).ConfigureAwait(false);
        }
    }
}