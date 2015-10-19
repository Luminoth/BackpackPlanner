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

using EnergonSoftware.BackpackPlanner.Settings;
using EnergonSoftware.BackpackPlanner.Units.Units;

namespace EnergonSoftware.BackpackPlanner.Models.Personal
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class PersonalInformation
    {
#region Preference Keys
        /// <summary>
        /// The name preference key
        /// </summary>
        public const string NamePreferenceKey = "personalInformationName";

        /// <summary>
        /// The date of birth preference key
        /// </summary>
        public const string DateOfBirthPreferenceKey = "personalInformationBirthdate";

        /// <summary>
        /// The user sex preference key
        /// </summary>
        public const string UserSexPreferenceKey = "personalInformationSex";

        /// <summary>
        /// The height preference key
        /// </summary>
        public const string HeightPreferenceKey = "personalInformationHeight";

        /// <summary>
        /// The weight preference key
        /// </summary>
        public const string WeightPreferenceKey = "personalInformationWeight";
#endregion

        private string _name = string.Empty;

        /// <summary>
        /// Gets or sets the user's name.
        /// </summary>
        /// <value>
        /// The user's name.
        /// </value>
        public string Name
        {
            get { return _name; }

            set
            {
                _name = value;
                _settings.SettingChanged(this, new SettingsChangedEventArgs
                    {
                        PreferenceKey = NamePreferenceKey
                    }
                );
            }
        }

        private DateTime? _dateOfBirth = null;

        /// <summary>
        /// Gets or sets the user's date of birth.
        /// </summary>
        /// <value>
        /// The user's date of birth.
        /// </value>
        public DateTime? DateOfBirth
        {
            get { return _dateOfBirth; }

            set
            {
                _dateOfBirth = value;
                _settings.SettingChanged(this, new SettingsChangedEventArgs
                    {
                        PreferenceKey = DateOfBirthPreferenceKey
                    }
                );
            }
        }

        private UserSex _userSex = UserSex.Undeclared;

        /// <summary>
        /// Gets or sets the user's sex.
        /// </summary>
        /// <value>
        /// The user's sex.
        /// </value>
        public UserSex Sex
        {
            get { return _userSex; }

            set
            {
                _userSex = value;
                _settings.SettingChanged(this, new SettingsChangedEventArgs
                    {
                        PreferenceKey = UserSexPreferenceKey
                    }
                );
            }
        }

        private int _heightInCm;

        /// <summary>
        /// Gets or sets the user's height in centimeters.
        /// </summary>
        /// <value>
        /// The user's height in centimeters.
        /// </value>
        public int HeightInCm
        {
            get { return _heightInCm; }

            set
            {
                _heightInCm = value < 0 ? 0 : value;
                _settings.SettingChanged(this, new SettingsChangedEventArgs
                    {
                        PreferenceKey = HeightPreferenceKey
                    }
                );
            }
        }

        /// <summary>
        /// Gets or sets the user's height in length units.
        /// </summary>
        /// <value>
        /// The user's height in length units.
        /// </value>
        public double HeightInUnits
        {
            get { return _settings.Units.LengthFromCentimeters(HeightInCm); }
            set { HeightInCm = (int)_settings.Units.CentimetersFromLength(value); }
        }

        private int _weightInGrams;

        /// <summary>
        /// Gets or sets the user's weight in grams.
        /// </summary>
        /// <value>
        /// The user's weight in grams.
        /// </value>
        public int WeightInGrams
        {
            get { return _weightInGrams; }

            set
            {
                _weightInGrams = value < 0 ? 0 : value;
                _settings.SettingChanged(this, new SettingsChangedEventArgs
                    {
                        PreferenceKey = WeightPreferenceKey
                    }
                );
            }
        }

        /// <summary>
        /// Gets or sets the user's weight in weight units.
        /// </summary>
        /// <value>
        /// The user's weight in weight units.
        /// </value>
        public double WeightInUnits
        {
            get { return _settings.Units.WeightFromGrams(WeightInGrams); }
            set { WeightInGrams = (int)_settings.Units.GramsFromWeight(value); }
        }

        private readonly BackpackPlannerSettings _settings;

        public PersonalInformation(BackpackPlannerSettings settings)
        {
            if(null == settings) {
                throw new ArgumentNullException(nameof(settings));
            }

            _settings = settings;
        }
    }
}
