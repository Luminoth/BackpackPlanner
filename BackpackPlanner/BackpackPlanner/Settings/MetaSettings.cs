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

namespace EnergonSoftware.BackpackPlanner.Settings
{
    /// <summary>
    /// 
    /// </summary>
    public class MetaSettings
    {
#region Preference Keys
        /// <summary>
        /// The first run preference key
        /// </summary>
        public const string FirstRunPreferenceKey = "firstRun";
#endregion

        private bool _firstRun = true;

        /// <summary>
        /// Gets or sets a value indicating whether this is the first run of the app or not.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this is the first run of the app; otherwise, <c>false</c>.
        /// </value>
        public bool FirstRun
        {
            get { return _firstRun; }

            set
            {
                _firstRun = value;
                _settingsChangedObj.SettingChanged(this, new SettingsChangedEventArgs
                    {
                        PreferenceKey = FirstRunPreferenceKey
                    }
                );
            }
        }

        // TODO: this should be an interface that
        // has the "SettingChanged" event rather than
        // something specific to this application
        // also, this name sucks...
        private readonly BackpackPlannerSettings _settingsChangedObj;

        /// <summary>
        /// Initializes a new instance of the <see cref="MetaSettings"/> class.
        /// </summary>
        /// <param name="settingsChangedObj">The settings changed object.</param>
        public MetaSettings(BackpackPlannerSettings settingsChangedObj)
        {
            _settingsChangedObj = settingsChangedObj;
        }
    }
}
