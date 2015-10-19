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

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Models.Personal;
using EnergonSoftware.BackpackPlanner.Settings;
using EnergonSoftware.BackpackPlanner.Units.Currency;
using EnergonSoftware.BackpackPlanner.Units.Units;

namespace EnergonSoftware.BackpackPlanner.Droid.Util
{
    // TODO: try to encapsulate this better in the planner library
    // so that it's easier to read/save settings without duplicating
    // a bunch of code (do like "readInt" or something at the platform level)
    public static class SettingsUtil
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(SettingsUtil));

        // TODO: make use of the key parameter to avoid touching everything
        public static void UpdateFromSharedPreferences(BackpackPlannerState backpackPlannerState, ISharedPreferences sharedPreferences, string key)
        {
            Logger.Debug($"Updating settings from shared preferences (key={key})...");

            // TODO: put these in an UpdateMetaSettingsFromSharedPreferences() method

            backpackPlannerState.Settings.MetaSettings.FirstRun = sharedPreferences.GetBoolean(
                MetaSettings.FirstRunPreferenceKey,
                backpackPlannerState.Settings.MetaSettings.FirstRun);

            // TODO: put these in an UpdateSettingsFromSharedPreferences() method

            backpackPlannerState.Settings.ConnectGooglePlayServices = sharedPreferences.GetBoolean(
                BackpackPlannerSettings.ConnectGooglePlayServicesPreferenceKey,
                backpackPlannerState.Settings.ConnectGooglePlayServices);

            // NOTE: have to read the unit system/currency settings first in order
            // to properly interpret the rest of the settings

            string unitSystemPreference = sharedPreferences.GetString(
                BackpackPlannerSettings.UnitSystemPreferenceKey,
                backpackPlannerState.Settings.Units.ToString());

            UnitSystem unitSystem;
            if(Enum.TryParse(unitSystemPreference, out unitSystem)) {
                backpackPlannerState.Settings.Units = unitSystem;
            } else {
                Logger.Error("Error parsing unit system preference!");
            }

            string currencyPreference = sharedPreferences.GetString(
                BackpackPlannerSettings.CurrencyPreferenceKey,
                backpackPlannerState.Settings.Currency.ToString());

            Currency currency;
            if(Enum.TryParse(currencyPreference, out currency)) {
                backpackPlannerState.Settings.Currency = currency;
            } else {
                Logger.Error("Error parsing currency preference!");
            }

            // TODO: put these in an UpdatePersonalInformationFromSharedPreferences() method

            backpackPlannerState.PersonalInformation.Name = sharedPreferences.GetString(
                PersonalInformation.NamePreferenceKey,
                backpackPlannerState.PersonalInformation.Name);

            string birthDatePreference = sharedPreferences.GetString(
                PersonalInformation.DateOfBirthPreferenceKey,
                backpackPlannerState.PersonalInformation.DateOfBirth?.ToString() ?? string.Empty);

            if(!string.IsNullOrWhiteSpace(birthDatePreference)) {
                try {
                    backpackPlannerState.PersonalInformation.DateOfBirth = Convert.ToDateTime(birthDatePreference);
                } catch(FormatException) {
                    Logger.Error("Error parsing date of birth preference!");
                }
            }

            string userSexPreference = sharedPreferences.GetString(
                PersonalInformation.UserSexPreferenceKey,
                backpackPlannerState.PersonalInformation.Sex.ToString());

            UserSex userSex;
            if(Enum.TryParse(userSexPreference, out userSex)) {
                backpackPlannerState.PersonalInformation.Sex = userSex;
            } else {
                Logger.Error("Error parsing user sex preference!");
            }

            backpackPlannerState.PersonalInformation.HeightInUnits = Convert.ToSingle(sharedPreferences.GetString(
                PersonalInformation.HeightPreferenceKey,
                backpackPlannerState.PersonalInformation.HeightInUnits.ToString(CultureInfo.InvariantCulture)));

            backpackPlannerState.PersonalInformation.WeightInUnits = Convert.ToSingle(sharedPreferences.GetString(
                PersonalInformation.WeightPreferenceKey,
                backpackPlannerState.PersonalInformation.WeightInUnits.ToString(CultureInfo.InvariantCulture)));
        }

        // TODO: make use of the key parameter to avoid touching everything
        public static void SaveToSharedPreferences(BackpackPlannerState backpackPlannerState, ISharedPreferences sharedPreferences, string key)
        {
            Logger.Debug($"Saving settings to shared preferences (key={key})...");

            ISharedPreferencesEditor sharedPreferencesEditor = sharedPreferences.Edit();

            // TODO: put these in a SaveMetaSettingsToSharedPreferences() method

            sharedPreferencesEditor.PutBoolean(MetaSettings.FirstRunPreferenceKey, backpackPlannerState.Settings.MetaSettings.FirstRun);

            // TODO: put these in a SaveSettingsToSharedPreferences() method

            sharedPreferencesEditor.PutBoolean(BackpackPlannerSettings.ConnectGooglePlayServicesPreferenceKey, backpackPlannerState.Settings.ConnectGooglePlayServices);
            sharedPreferencesEditor.PutString(BackpackPlannerSettings.UnitSystemPreferenceKey, backpackPlannerState.Settings.Units.ToString());
            sharedPreferencesEditor.PutString(BackpackPlannerSettings.CurrencyPreferenceKey, backpackPlannerState.Settings.Currency.ToString());

            // TODO: put these in a SavePersonalInformationToSharedPreferences() method

            sharedPreferencesEditor.PutString(PersonalInformation.NamePreferenceKey, backpackPlannerState.PersonalInformation.Name);
            sharedPreferencesEditor.PutString(PersonalInformation.DateOfBirthPreferenceKey, backpackPlannerState.PersonalInformation.DateOfBirth?.ToString() ?? string.Empty);
            sharedPreferencesEditor.PutString(PersonalInformation.UserSexPreferenceKey, backpackPlannerState.PersonalInformation.Sex.ToString());
            sharedPreferencesEditor.PutString(PersonalInformation.HeightPreferenceKey, backpackPlannerState.PersonalInformation.HeightInUnits.ToString(CultureInfo.InvariantCulture));
            sharedPreferencesEditor.PutString(PersonalInformation.WeightPreferenceKey, backpackPlannerState.PersonalInformation.WeightInUnits.ToString(CultureInfo.InvariantCulture));

            sharedPreferencesEditor.Commit();
        }
    }
}
