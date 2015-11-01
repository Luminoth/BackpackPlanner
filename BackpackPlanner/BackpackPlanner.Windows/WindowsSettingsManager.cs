/*
   Copyright 2015 Shane Lillie

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

using Windows.Storage;

using EnergonSoftware.BackpackPlanner.Core.Settings;

namespace EnergonSoftware.BackpackPlanner.Windows
{
    internal sealed class WindowsSettingsManager : SettingsManager
    {
#region Get
        public override string GetString(string key, string defaultValue)
        {
            object obj;
            return ApplicationData.Current.LocalSettings.Values.TryGetValue(key, out obj) ? obj.ToString() : defaultValue;
        }

        public override int GetInt(string key, int defaultValue)
        {
            object obj;
            if(!ApplicationData.Current.LocalSettings.Values.TryGetValue(key, out obj)) {
                return defaultValue;;
            }

            try {
                return Convert.ToInt32(obj);
            } catch(FormatException) {
                return defaultValue;
            }
        }

        public override float GetFloat(string key, float defaultValue)
        {
            object obj;
            if(!ApplicationData.Current.LocalSettings.Values.TryGetValue(key, out obj)) {
                return defaultValue;;
            }

            try {
                return Convert.ToSingle(obj);
            } catch(FormatException) {
                return defaultValue;
            }
        }

        public override bool GetBoolean(string key, bool defaultValue)
        {
            object obj;
            if(!ApplicationData.Current.LocalSettings.Values.TryGetValue(key, out obj)) {
                return defaultValue;;
            }

            try {
                return Convert.ToBoolean(obj);
            } catch(FormatException) {
                return defaultValue;
            }
        }
#endregion

#region Put
        public override void PutString(string key, string value)
        {
            ApplicationData.Current.LocalSettings.Values[key] = value;
        }

        public override void PutInt(string key, int value)
        {
            ApplicationData.Current.LocalSettings.Values[key] = value;
        }

        public override void PutFloat(string key, float value)
        {
            ApplicationData.Current.LocalSettings.Values[key] = value;
        }

        public override void PutBoolean(string key, bool value)
        {
            ApplicationData.Current.LocalSettings.Values[key] = value;
        }
#endregion
    }
}
