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
    // note that for whatever reason settings are always stored as strings
    // even if you say PutBoolean or PutFloat or whatever
    public static class SettingsUtil
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(SettingsUtil));

        // TODO: make use of the key parameter to avoid touching everything
        public static void UpdateFromSharedPreferences(ISharedPreferences sharedPreferences, string key)
        {
            // TODO: put these in an UpdateMetaSettingsFromSharedPreferences() method

            BackpackPlannerState.Instance.Settings.MetaSettings.FirstRun = Convert.ToBoolean(sharedPreferences.GetString(
                MetaSettings.FirstRunPreferenceKey,
                BackpackPlannerState.Instance.Settings.MetaSettings.FirstRun.ToString()));

            // TODO: put these in an UpdateSettingsFromSharedPreferences() method

            BackpackPlannerState.Instance.Settings.ConnectGooglePlayServices = sharedPreferences.GetBoolean(
                BackpackPlannerSettings.ConnectGooglePlayServicesPreferenceKey,
                BackpackPlannerState.Instance.Settings.ConnectGooglePlayServices);

            // NOTE: have to read the unit system/currency settings first in order
            // to properly interpret the rest of the settings

            string unitSystemPreference = sharedPreferences.GetString(
                BackpackPlannerSettings.UnitSystemPreferenceKey,
                BackpackPlannerState.Instance.Settings.Units.ToString());

            UnitSystem unitSystem;
            if(Enum.TryParse(unitSystemPreference, out unitSystem)) {
                BackpackPlannerState.Instance.Settings.Units = unitSystem;
            } else {
                Logger.Error("Error parsing unit system preference!");
            }

            string currencyPreference = sharedPreferences.GetString(
                BackpackPlannerSettings.CurrencyPreferenceKey,
                BackpackPlannerState.Instance.Settings.Currency.ToString());

            Currency currency;
            if(Enum.TryParse(currencyPreference, out currency)) {
                BackpackPlannerState.Instance.Settings.Currency = currency;
            } else {
                Logger.Error("Error parsing currency preference!");
            }

            // TODO: put these in an UpdatePersonalInformationFromSharedPreferences() method

            BackpackPlannerState.Instance.PersonalInformation.Name = sharedPreferences.GetString(
                PersonalInformation.NamePreferenceKey,
                BackpackPlannerState.Instance.PersonalInformation.Name);

            string birthDatePreference = sharedPreferences.GetString(
                PersonalInformation.DateOfBirthPreferenceKey,
                BackpackPlannerState.Instance.PersonalInformation.DateOfBirth?.ToString() ?? string.Empty);

            if(!string.IsNullOrWhiteSpace(birthDatePreference)) {
                try {
                    BackpackPlannerState.Instance.PersonalInformation.DateOfBirth = Convert.ToDateTime(birthDatePreference);
                } catch(FormatException) {
                    Logger.Error("Error parsing date of birth preference!");
                }
            }

            string userSexPreference = sharedPreferences.GetString(
                PersonalInformation.UserSexPreferenceKey,
                BackpackPlannerState.Instance.PersonalInformation.Sex.ToString());

            UserSex userSex;
            if(Enum.TryParse(userSexPreference, out userSex)) {
                BackpackPlannerState.Instance.PersonalInformation.Sex = userSex;
            } else {
                Logger.Error("Error parsing user sex preference!");
            }

            BackpackPlannerState.Instance.PersonalInformation.HeightInUnits = Convert.ToSingle(sharedPreferences.GetString(
                PersonalInformation.HeightPreferenceKey,
                BackpackPlannerState.Instance.PersonalInformation.HeightInUnits.ToString(CultureInfo.InvariantCulture)));

            BackpackPlannerState.Instance.PersonalInformation.WeightInUnits = Convert.ToSingle(sharedPreferences.GetString(
                PersonalInformation.WeightPreferenceKey,
                BackpackPlannerState.Instance.PersonalInformation.WeightInUnits.ToString(CultureInfo.InvariantCulture)));
        }

        // TODO: make use of the key parameter to avoid touching everything
        public static void SaveToSharedPreferences(ISharedPreferences sharedPreferences, string key)
        {
            ISharedPreferencesEditor sharedPreferencesEditor = sharedPreferences.Edit();

            // TODO: put these in a SaveMetaSettingsToSharedPreferences() method

            sharedPreferencesEditor.PutString(MetaSettings.FirstRunPreferenceKey, BackpackPlannerState.Instance.Settings.MetaSettings.FirstRun.ToString());

            // TODO: put these in a SaveSettingsToSharedPreferences() method

            sharedPreferencesEditor.PutBoolean(BackpackPlannerSettings.ConnectGooglePlayServicesPreferenceKey, BackpackPlannerState.Instance.Settings.ConnectGooglePlayServices);
            sharedPreferencesEditor.PutString(BackpackPlannerSettings.UnitSystemPreferenceKey, BackpackPlannerState.Instance.Settings.Units.ToString());
            sharedPreferencesEditor.PutString(BackpackPlannerSettings.CurrencyPreferenceKey, BackpackPlannerState.Instance.Settings.Currency.ToString());

            // TODO: put these in a SavePersonalInformationToSharedPreferences() method

            sharedPreferencesEditor.PutString(PersonalInformation.NamePreferenceKey, BackpackPlannerState.Instance.PersonalInformation.Name);
            sharedPreferencesEditor.PutString(PersonalInformation.DateOfBirthPreferenceKey, BackpackPlannerState.Instance.PersonalInformation.DateOfBirth?.ToString() ?? string.Empty);
            sharedPreferencesEditor.PutString(PersonalInformation.UserSexPreferenceKey, BackpackPlannerState.Instance.PersonalInformation.Sex.ToString());
            sharedPreferencesEditor.PutString(PersonalInformation.HeightPreferenceKey, BackpackPlannerState.Instance.PersonalInformation.HeightInUnits.ToString(CultureInfo.InvariantCulture));
            sharedPreferencesEditor.PutString(PersonalInformation.WeightPreferenceKey, BackpackPlannerState.Instance.PersonalInformation.WeightInUnits.ToString(CultureInfo.InvariantCulture));

            sharedPreferencesEditor.Commit();
        }
    }
}
