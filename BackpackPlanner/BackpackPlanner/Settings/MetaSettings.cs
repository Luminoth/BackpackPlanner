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

using EnergonSoftware.BackpackPlanner.Core.Settings;

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

        public const string TestDataEnteredKey = "testDataEntered";
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
            get => _firstRun;

            set
            {
                _firstRun = value;
                _settingsManager?.PutBoolean(FirstRunPreferenceKey, _firstRun);
            }
        }

#if DEBUG
        private bool _testDataEntered;

        public bool TestDataEntered
        {
            get => _testDataEntered;

            set
            {
                _testDataEntered = value;
                _settingsManager?.PutBoolean(TestDataEnteredKey, _testDataEntered);
            }
        }
#endif

        private readonly SettingsManager _settingsManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="MetaSettings" /> class.
        /// </summary>
        /// <param name="settingsManager">The settings manager.</param>
        public MetaSettings(SettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }
    }
}
