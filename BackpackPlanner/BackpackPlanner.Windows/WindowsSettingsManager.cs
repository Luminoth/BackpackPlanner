﻿/*
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

using EnergonSoftware.BackpackPlanner.Core.Settings;

namespace EnergonSoftware.BackpackPlanner.Windows
{
    // TODO: write this
    internal sealed class WindowsSettingsManager : SettingsManager
    {
#region Get
        public override string GetString(string key, string defaultValue)
        {
            return defaultValue;
        }

        public override int GetInt(string key, int defaultValue)
        {
            return defaultValue;
        }

        public override float GetFloat(string key, float defaultValue)
        {
            return defaultValue;
        }

        public override bool GetBoolean(string key, bool defaultValue)
        {
            return defaultValue;
        }
#endregion

#region Put
        public override void PutString(string key, string value)
        {
        }

        public override void PutInt(string key, int value)
        {
        }

        public override void PutFloat(string key, float value)
        {
        }

        public override void PutBoolean(string key, bool value)
        {
        }
#endregion
    }
}