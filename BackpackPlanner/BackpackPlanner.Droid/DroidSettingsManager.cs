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
using System.Globalization;

using Android.Content;

using EnergonSoftware.BackpackPlanner.Core.Settings;

namespace EnergonSoftware.BackpackPlanner.Droid
{
    public class DroidSettingsManager : SettingsManager
    {
        private readonly ISharedPreferences _sharedPreferences;

        public DroidSettingsManager(ISharedPreferences sharedPreferences)
        {
            _sharedPreferences = sharedPreferences;
        }

#region Get
        public override string GetString(string key, string defaultValue)
        {
            return _sharedPreferences?.GetString(key, defaultValue) ?? defaultValue;
        }

        public override int GetInt(string key, int defaultValue)
        {
            string value = GetString(key, defaultValue.ToString(CultureInfo.InvariantCulture));
            try {
                return Convert.ToInt32(value);
            } catch(FormatException) {
                return defaultValue;
            }
        }

        public override float GetFloat(string key, float defaultValue)
        {
            string value = GetString(key, defaultValue.ToString(CultureInfo.InvariantCulture));
            try {
                return Convert.ToSingle(value);
            } catch(FormatException) {
                return defaultValue;
            }
        }

        public override bool GetBoolean(string key, bool defaultValue)
        {
            return _sharedPreferences?.GetBoolean(key, defaultValue) ?? defaultValue;
        }
#endregion

#region Put
        public override void PutString(string key, string value)
        {
            ISharedPreferencesEditor sharedPreferencesEditor = _sharedPreferences?.Edit();
            sharedPreferencesEditor?.PutString(key, value);
            sharedPreferencesEditor?.Commit();
        }

        public override void PutInt(string key, int value)
        {
            ISharedPreferencesEditor sharedPreferencesEditor = _sharedPreferences?.Edit();
            sharedPreferencesEditor?.PutString(key, value.ToString());
            sharedPreferencesEditor?.Commit();
        }

        public override void PutFloat(string key, float value)
        {
            ISharedPreferencesEditor sharedPreferencesEditor = _sharedPreferences?.Edit();
            sharedPreferencesEditor?.PutString(key, value.ToString(CultureInfo.InvariantCulture));
            sharedPreferencesEditor?.Commit();
        }

        public override void PutBoolean(string key, bool value)
        {
            ISharedPreferencesEditor sharedPreferencesEditor = _sharedPreferences?.Edit();
            sharedPreferencesEditor?.PutBoolean(key, value);
            sharedPreferencesEditor?.Commit();
        }
#endregion
    }
}