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

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.DAL.Models.Personal;
using EnergonSoftware.BackpackPlanner.Settings;
using EnergonSoftware.BackpackPlanner.Units.Currency;
using EnergonSoftware.BackpackPlanner.Units.Units;

namespace EnergonSoftware.BackpackPlanner.Core.Settings
{
    /// <summary>
    /// Platform settings manager
    /// </summary>
    // TODO: this should be split into a core interface
    // and a non-core abstract class that implements load/update
    public abstract class SettingsManager
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(SettingsManager));

        /// <summary>
        /// Loads all of the settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="personalInformation">The personal information.</param>
        public void Load(BackpackPlannerSettings settings, PersonalInformation personalInformation)
        {
            // TODO: put these in a LoadMetaSettings() method

            settings.MetaSettings.FirstRun = GetBoolean(MetaSettings.FirstRunPreferenceKey, settings.MetaSettings.FirstRun);

            // TODO: put these in a LoadSettings() method

            settings.ConnectGooglePlayServices = GetBoolean(BackpackPlannerSettings.ConnectGooglePlayServicesPreferenceKey, settings.ConnectGooglePlayServices);

            // NOTE: have to read the unit system/currency settings first in order
            // to properly interpret the rest of the settings

            string unitSystemPreference = GetString(BackpackPlannerSettings.UnitSystemPreferenceKey, settings.Units.ToString());

            UnitSystem unitSystem;
            if(Enum.TryParse(unitSystemPreference, out unitSystem)) {
                settings.Units = unitSystem;
            } else {
                Logger.Error("Error parsing unit system preference!");
            }

            string currencyPreference = GetString(BackpackPlannerSettings.CurrencyPreferenceKey, settings.Currency.ToString());

            Currency currency;
            if(Enum.TryParse(currencyPreference, out currency)) {
                settings.Currency = currency;
            } else {
                Logger.Error("Error parsing currency preference!");
            }

            // TODO: put these in a LoadPersonalInformation() method

            personalInformation.Name = GetString(PersonalInformation.NamePreferenceKey, personalInformation.Name);

            string birthDatePreference = GetString(PersonalInformation.DateOfBirthPreferenceKey, personalInformation.DateOfBirth?.ToString() ?? string.Empty);

            if(!string.IsNullOrWhiteSpace(birthDatePreference)) {
                try {
                    personalInformation.DateOfBirth = Convert.ToDateTime(birthDatePreference);
                } catch(FormatException) {
                    Logger.Error("Error parsing date of birth preference!");
                }
            }

            string userSexPreference = GetString(PersonalInformation.UserSexPreferenceKey, personalInformation.Sex.ToString());

            UserSex userSex;
            if(Enum.TryParse(userSexPreference, out userSex)) {
                personalInformation.Sex = userSex;
            } else {
                Logger.Error("Error parsing user sex preference!");
            }

            personalInformation.HeightInUnits = GetFloat(PersonalInformation.HeightPreferenceKey, personalInformation.HeightInUnits);

            personalInformation.WeightInUnits = GetFloat(PersonalInformation.WeightPreferenceKey, personalInformation.WeightInUnits);
        }

        /// <summary>
        /// Updates a setting from the prferences.
        /// </summary>
        /// <param name="key">The setting key.</param>
        /// <param name="settings">The settings.</param>
        /// <param name="personalInformation">The personal information.</param>
        public void UpdateFromPreferences(string key, BackpackPlannerSettings settings, PersonalInformation personalInformation)
        {
            switch(key)
            {
            // TODO: put these in an UpdateMetaSettingsFromPreferences() method
            case MetaSettings.FirstRunPreferenceKey:
                settings.MetaSettings.FirstRun = GetBoolean(MetaSettings.FirstRunPreferenceKey, settings.MetaSettings.FirstRun);
                break;
            // TODO: put these in an UpdateSettingsFromPreferences() method
            case BackpackPlannerSettings.ConnectGooglePlayServicesPreferenceKey:
                settings.ConnectGooglePlayServices = GetBoolean(BackpackPlannerSettings.ConnectGooglePlayServicesPreferenceKey, settings.ConnectGooglePlayServices);
                break;
            case BackpackPlannerSettings.UnitSystemPreferenceKey:
                string unitSystemPreference = GetString(BackpackPlannerSettings.UnitSystemPreferenceKey, settings.Units.ToString());

                if(Enum.TryParse(unitSystemPreference, out UnitSystem unitSystem)) {
                    settings.Units = unitSystem;
                } else {
                    Logger.Error("Error parsing unit system preference!");
                }
                break;
            case BackpackPlannerSettings.CurrencyPreferenceKey:
                string currencyPreference = GetString(BackpackPlannerSettings.CurrencyPreferenceKey, settings.Currency.ToString());

                if(Enum.TryParse(currencyPreference, out Currency currency)) {
                    settings.Currency = currency;
                } else {
                    Logger.Error("Error parsing currency preference!");
                }
                break;
            // TODO: put these in an UpdatePersonalInformationFromPreferences() method
            case PersonalInformation.NamePreferenceKey:
                personalInformation.Name = GetString(PersonalInformation.NamePreferenceKey, personalInformation.Name);
                break;
            case PersonalInformation.DateOfBirthPreferenceKey:
                string birthDatePreference = GetString(PersonalInformation.DateOfBirthPreferenceKey, personalInformation.DateOfBirth?.ToString() ?? string.Empty);

                if(!string.IsNullOrWhiteSpace(birthDatePreference)) {
                    try {
                        personalInformation.DateOfBirth = Convert.ToDateTime(birthDatePreference);
                    } catch(FormatException) {
                        Logger.Error("Error parsing date of birth preference!");
                    }
                }
                break;
            case PersonalInformation.UserSexPreferenceKey:
                string userSexPreference = GetString(PersonalInformation.UserSexPreferenceKey, personalInformation.Sex.ToString());

                if(Enum.TryParse(userSexPreference, out UserSex userSex)) {
                    personalInformation.Sex = userSex;
                } else {
                    Logger.Error("Error parsing user sex preference!");
                }
                break;
            case PersonalInformation.HeightPreferenceKey:
                personalInformation.HeightInUnits = GetFloat(PersonalInformation.HeightPreferenceKey, personalInformation.HeightInUnits);
                break;
            case PersonalInformation.WeightPreferenceKey:
                personalInformation.WeightInUnits = GetFloat(PersonalInformation.WeightPreferenceKey, personalInformation.WeightInUnits);
                break;
            default:
                Logger.Warn($"Unhandled preference key: {key}");
                break;
            }
        }

#region Get
        public abstract string GetString(string key, string defaultValue);

        public abstract int GetInt(string key, int defaultValue);

        public abstract float GetFloat(string key, float defaultValue);

        public abstract bool GetBoolean(string key, bool defaultValue);
#endregion

#region Put
        public abstract void PutString(string key, string value);

        public abstract void PutInt(string key, int value);

        public abstract void PutFloat(string key, float value);

        public abstract void PutBoolean(string key, bool value);
#endregion
    }
}
