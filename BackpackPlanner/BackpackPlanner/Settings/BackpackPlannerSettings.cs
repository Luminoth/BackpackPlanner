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
        /// The unit system preference key
        /// </summary>
        public const string UnitSystemPreferenceKey = "unitSystem";

        /// <summary>
        /// The currency preference key
        /// </summary>
        public const string CurrencyPreferenceKey = "currency";
#endregion

#region Events
        /// <summary>
        /// Occurs when a setting is changed.
        /// </summary>
        public event EventHandler<SettingsChangedEventArgs> SettingsChangedEvent;
#endregion

        /// <summary>
        /// Gets the meta settings.
        /// </summary>
        /// <value>
        /// The meta settings.
        /// </value>
        public MetaSettings MetaSettings { get; private set; }

        private bool _connectGooglePlayServices;

        /// <summary>
        /// Gets or sets a value indicating whether [connect google play services].
        /// </summary>
        /// <value>
        /// <c>true</c> if [connect google play services]; otherwise, <c>false</c>.
        /// </value>
        public bool ConnectGooglePlayServices
        {
            get { return _connectGooglePlayServices; }

            set
            {
                _connectGooglePlayServices = value;
                SettingChanged(this, new SettingsChangedEventArgs
                    {
                        PreferenceKey = ConnectGooglePlayServicesPreferenceKey
                    }
                );
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
            get { return _units; }

            set
            {
                _units = value;
                SettingChanged(this, new SettingsChangedEventArgs
                    {
                        PreferenceKey = UnitSystemPreferenceKey
                    }
                );
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
            get { return _currency; }

            set
            {
                _currency = value;
                SettingChanged(this, new SettingsChangedEventArgs
                    {
                        PreferenceKey = CurrencyPreferenceKey
                    }
                );
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

        /// <summary>
        /// Initializes a new instance of the <see cref="BackpackPlannerSettings" /> class.
        /// </summary>
        public BackpackPlannerSettings()
        {
            MetaSettings = new MetaSettings(this);
        }

        /// <summary>
        /// Notifies that a setting has changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="EnergonSoftware.BackpackPlanner.Settings.SettingsChangedEventArgs" /> instance containing the event data.</param>
        public void SettingChanged(object sender, SettingsChangedEventArgs args)
        {
            SettingsChangedEvent?.Invoke(sender, args);
        }

        /// <summary>
        /// Resets the settings changed event.
        /// </summary>
        public void ResetSettingsChangedEvent()
        {
            SettingsChangedEvent = null;
        }
    }
}
