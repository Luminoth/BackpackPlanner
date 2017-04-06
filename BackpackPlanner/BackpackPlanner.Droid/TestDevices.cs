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

using Android.Gms.Ads;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Core.Util.Crypt;
using EnergonSoftware.BackpackPlanner.DAL.Models.Personal;

namespace EnergonSoftware.BackpackPlanner.Droid
{
    public static class TestDevices
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(TestDevices));

        public static readonly string[] TestDeviceIds = 
        {
            // add test devices here (do not commit to github)
        };

        private static readonly Hasher Hasher = new MD5();

        // TODO: this could be an extension (given a more appropriate name)
        public static AdRequest.Builder AddTestDevices(AdRequest.Builder builder)
        {
            builder.AddTestDevice(AdRequest.DeviceIdEmulator);
            foreach(string testDeviceId in TestDeviceIds) {
                string deviceIdHash = Hasher.HashHexAsync(testDeviceId).Result.ToUpper();
                Logger.Debug($"Adding test device hash {deviceIdHash}");
                builder.AddTestDevice(deviceIdHash);
            }
            return builder;
        }

        public static AdRequest.Builder SetGender(PersonalInformation personalInfo, AdRequest.Builder builder)
        {
            switch(personalInfo.Sex)
            {
            case UserSex.Male:
                return builder.SetGender(AdRequest.GenderMale);
            case UserSex.Female:
                return builder.SetGender(AdRequest.GenderFemale);
            case UserSex.Undeclared:
            case UserSex.Other:
            default:
                return builder.SetGender(AdRequest.GenderUnknown);
            }
        }
    }
}