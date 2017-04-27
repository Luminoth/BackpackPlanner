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
using EnergonSoftware.BackpackPlanner.Units;
using EnergonSoftware.BackpackPlanner.Units.Currency;
using EnergonSoftware.BackpackPlanner.Units.Units;

namespace EnergonSoftware.BackpackPlanner.Settings
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BackpackPlannerSettings
    {
#region Preference Keys
        /// <summary>
        /// The connect google play services preference key
        /// </summary>
        public const string ConnectGooglePlayServicesPreferenceKey = "connectGooglePlayServices";

        /// <summary>
        /// The google play services user preference key
        /// </summary>
        public const string GooglePlayServicesUserPreferenceKey = "googlePlayServicesUser";

        /// <summary>
        /// The units preference key
        /// </summary>
        public const string UnitsPreferenceKey = "units";

        /// <summary>
        /// The unit system preference key
        /// </summary>
        public const string UnitSystemPreferenceKey = "unitSystem";

        /// <summary>
        /// The currency preference key
        /// </summary>
        public const string CurrencyPreferenceKey = "currency";
#endregion

        /// <summary>
        /// Gets the meta settings.
        /// </summary>
        /// <value>
        /// The meta settings.
        /// </value>
        public MetaSettings MetaSettings { get; }

        private bool _connectGooglePlayServices;

        /// <summary>
        /// Gets or sets a value indicating whether or not to connect google play services.
        /// </summary>
        /// <value>
        /// <c>true</c> if should connect to google play services; otherwise, <c>false</c>.
        /// </value>
        public bool ConnectGooglePlayServices
        {
            get => _connectGooglePlayServices;

            set
            {
                _connectGooglePlayServices = value;
                _settingsManager?.PutBoolean(ConnectGooglePlayServicesPreferenceKey, _connectGooglePlayServices);
            }
        }

        private string _googlePlayServicesUser = string.Empty;

        /// <summary>
        /// Gets or sets the google play services user.
        /// </summary>
        /// <value>
        /// The google play services user.
        /// </value>
        public string GooglePlayServicesUser
        {
            get => _googlePlayServicesUser;

            set
            {
                _googlePlayServicesUser = value ?? string.Empty;
                _settingsManager?.PutString(GooglePlayServicesUserPreferenceKey, _googlePlayServicesUser);
            }
        }

        private UnitSystem _units = UnitSystem.Metric;

        /// <summary>
        /// Gets or sets the unit system to use.
        /// </summary>
        /// <value>
        /// The unit system to use.
        /// </value>
        public UnitSystem Units
        {
            get => _units;

            set
            {
                _units = value;
                _settingsManager?.PutString(UnitSystemPreferenceKey, _units.ToString());
            }
        }

        private Currency _currency = Currency.UnitedStatesDollar;

        /// <summary>
        /// Gets or sets the currency to use.
        /// </summary>
        /// <value>
        /// The currency to use.
        /// </value>
        public Currency Currency
        {
            get => _currency;

            set
            {
                _currency = value;
                _settingsManager?.PutString(CurrencyPreferenceKey, _currency.ToString());
            }
        }

#region Weight Categories
        /// <summary>
        /// Gets the ultralight weight category maximum weight in grams.
        /// </summary>
        /// <value>
        /// The ultralight weight category maximum weight in grams.
        /// </value>
        /// <remarks>
        /// 225 grams is about 8 ounces.
        /// </remarks>
        public int UltralightWeightCategoryMaxWeightInGrams { get; } = 225;

        /// <summary>
        /// Gets the light weight category maximum weight in grams.
        /// </summary>
        /// <value>
        /// The light weight category maximum weight in grams.
        /// </value>
        /// <remarks>
        /// 450 grams is about 16 ounces.
        /// </remarks>
        public int LightWeightCategoryMaxWeightInGrams { get; } = 450;

        /// <summary>
        /// Gets the medium weight category maximum weight in grams.
        /// </summary>
        /// <value>
        /// The medium weight category maximum weight in grams.
        /// </value>
        /// <remarks>
        /// 1360 grams is about 3 pounds.
        /// </remarks>
        public int MediumWeightCategoryMaxWeightInGrams { get; } = 1360;

        /// <summary>
        /// Gets the heavy weight category maximum weight in grams.
        /// </summary>
        /// <value>
        /// The heavy weight category maximum weight in grams.
        /// </value>
        /// <remarks>
        /// 2270 grams is about 5 pounds.
        /// </remarks>
        public int HeavyWeightCategoryMaxWeightInGrams { get; } = 2270;
#endregion

#region Weight Classes
        /// <summary>
        /// Gets the ultralight class maximum weight in grams.
        /// </summary>
        /// <value>
        /// The ultralight class maximum weight in grams.
        /// </value>
        /// <remarks>
        /// 4500 grams is about 10 pounds.
        /// </remarks>
        public int UltralightClassMaxWeightInGrams { get; } = 4500;

        /// <summary>
        /// Gets the lightweight class maximum weight in grams.
        /// </summary>
        /// <value>
        /// The lightweight class maximum weight in grams.
        /// </value>
        /// <remarks>
        /// 9000 grams is about 20 pounds.
        /// </remarks>
        public int LightweightClassMaxWeightInGrams { get; } = 9000;
#endregion

        private readonly SettingsManager _settingsManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="BackpackPlannerSettings" /> class.
        /// </summary>
        /// <param name="settingsManager">The settings manager.</param>
        public BackpackPlannerSettings(SettingsManager settingsManager)
        {
            _settingsManager = settingsManager;

            MetaSettings = new MetaSettings(_settingsManager);
        }

        public WeightClass GetWeightClass(int weightInGrams)
        {
            if(weightInGrams < UltralightClassMaxWeightInGrams) {
                return WeightClass.Ultralight;
            }

            if(weightInGrams < LightweightClassMaxWeightInGrams) {
                return WeightClass.Lightweight;
            }

            return WeightClass.Traditional;
        }

        public WeightCategory GetWeightCategory(int weightInGrams)
        {
            if(weightInGrams <= 0) {
                return WeightCategory.None;
            }

            if(weightInGrams < UltralightWeightCategoryMaxWeightInGrams) {
                return WeightCategory.Ultralight;
            }

            if(weightInGrams < LightWeightCategoryMaxWeightInGrams) {
                return WeightCategory.Light;
            }

            if(weightInGrams < MediumWeightCategoryMaxWeightInGrams) {
                return WeightCategory.Medium;
            }

            if(weightInGrams < HeavyWeightCategoryMaxWeightInGrams) {
                return WeightCategory.Heavy;
            }

            return WeightCategory.ExtraHeavy;
        }
    }
}
