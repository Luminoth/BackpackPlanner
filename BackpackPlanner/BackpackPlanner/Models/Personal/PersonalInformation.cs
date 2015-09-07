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

        /// <summary>
        /// Gets or sets the user's name.
        /// </summary>
        /// <value>
        /// The user's name.
        /// </value>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's date of birth.
        /// </summary>
        /// <value>
        /// The user's date of birth.
        /// </value>
        public DateTime? DateOfBirth { get; set; } = null;

        /// <summary>
        /// Gets or sets the user's sex.
        /// </summary>
        /// <value>
        /// The user's sex.
        /// </value>
        public UserSex Sex { get; set; } = UserSex.Undeclared;

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
            set { _heightInCm = value < 0 ? 0 : value; }
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
            set { _weightInGrams = value < 0 ? 0 : value; }
        }
    }
}
