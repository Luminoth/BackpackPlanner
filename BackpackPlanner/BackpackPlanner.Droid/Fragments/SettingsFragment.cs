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

using System.Globalization;

using Android.Content;
using Android.OS;

using EnergonSoftware.BackpackPlanner.Droid.Util;
using EnergonSoftware.BackpackPlanner.Models.Personal;
using EnergonSoftware.BackpackPlanner.Settings;
using EnergonSoftware.BackpackPlanner.Units.Units;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments
{
    public class SettingsFragment : Android.Support.V7.Preferences.PreferenceFragmentCompat, ISharedPreferencesOnSharedPreferenceChangeListener
    {
#region Controls
        private Android.Support.V7.Preferences.EditTextPreference _namePreference;
        private Android.Support.V7.Preferences.EditTextPreference _birthDatePreference;
        private Android.Support.V7.Preferences.ListPreference _userSexPreference;
        private Android.Support.V7.Preferences.EditTextPreference _heightPreference;
        private Android.Support.V7.Preferences.EditTextPreference _weightPreference;

        private Android.Support.V7.Preferences.ListPreference _unitSystemPreference;
        //private Android.Support.V7.Preferences.ListPreference _currencyPreference;
#endregion

        public override void OnCreatePreferences(Bundle savedInstanceState, string rootKey)
        {
            AddPreferencesFromResource(Resource.Xml.settings);

            _namePreference = (Android.Support.V7.Preferences.EditTextPreference)FindPreference(PersonalInformation.NamePreferenceKey);
            _birthDatePreference = (Android.Support.V7.Preferences.EditTextPreference)FindPreference(PersonalInformation.DateOfBirthPreferenceKey);
            _userSexPreference = (Android.Support.V7.Preferences.ListPreference)FindPreference(PersonalInformation.UserSexPreferenceKey);
            _heightPreference = (Android.Support.V7.Preferences.EditTextPreference)FindPreference(PersonalInformation.HeightPreferenceKey);
            _weightPreference = (Android.Support.V7.Preferences.EditTextPreference)FindPreference(PersonalInformation.WeightPreferenceKey);

            _unitSystemPreference = (Android.Support.V7.Preferences.ListPreference)FindPreference(BackpackPlannerSettings.UnitSystemPreferenceKey);
            //_currencyPreference = (Android.Support.V7.Preferences.ListPreference)FindPreference(BackpackPlannerSettings.CurrencyPreferenceKey);

            UpdateLabels();
            UpdateSummaries();

            Activity.Title = Resources.GetString(Resource.String.title_settings);
        }

        public override void OnResume()
        {
            base.OnResume();

            PreferenceScreen.SharedPreferences.RegisterOnSharedPreferenceChangeListener(this);
        }

        public override void OnPause()
        {
            base.OnPause();

            PreferenceScreen.SharedPreferences.UnregisterOnSharedPreferenceChangeListener(this);
        }

#region Labels
        private void SetHeightLabel()
        {
            _heightPreference.DialogTitle = _heightPreference.Title = Resources.GetString(Resource.String.label_height)
                + " " + BackpackPlannerState.Instance.Settings.Units.GetSmallLengthString();
        }

        private void SetWeightLabel()
        {
            _weightPreference.DialogTitle = _weightPreference.Title = Resources.GetString(Resource.String.label_weight)
                + " " + BackpackPlannerState.Instance.Settings.Units.GetSmallWeightString();
        }
#endregion

#region Summaries
        private void SetNameSummary()
        {
            _namePreference.Summary = string.IsNullOrWhiteSpace(BackpackPlannerState.Instance.PersonalInformation.Name)
                ? Resources.GetString(Resource.String.summary_name)
                : BackpackPlannerState.Instance.PersonalInformation.Name;
        }

        private void SetBirthDateSummary()
        {
            _birthDatePreference.Summary = null == BackpackPlannerState.Instance.PersonalInformation.DateOfBirth
                ? Resources.GetString(Resource.String.summary_birthdate)
                : BackpackPlannerState.Instance.PersonalInformation.DateOfBirth.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        private void SetUserSexSummary()
        {
            _userSexPreference.Summary = BackpackPlannerState.Instance.PersonalInformation.Sex.ToString();
        }

        private void SetHeightSummary()
        {
            _heightPreference.Summary = BackpackPlannerState.Instance.PersonalInformation.HeightInUnits.ToString("N0", CultureInfo.InvariantCulture)
                + " " + BackpackPlannerState.Instance.Settings.Units.GetSmallLengthString();
        }

        private void SetWeightSummary()
        {
            _weightPreference.Summary = BackpackPlannerState.Instance.PersonalInformation.WeightInUnits.ToString("N0", CultureInfo.InvariantCulture)
                + " " + BackpackPlannerState.Instance.Settings.Units.GetSmallWeightString();
        }

        private void SetUnitSystemSummary()
        {
            _unitSystemPreference.Summary = BackpackPlannerState.Instance.Settings.Units.ToString();
        }

        private void SetCurrencySummary()
        {
            //_currencyPreference.Summary = BackpackPlannerState.Instance.Settings.Currency.ToString();
        }
#endregion

        private void UpdateLabels()
        {
            SetHeightLabel();
            SetWeightLabel();
        }

        private void UpdateSummaries()
        {
            SetNameSummary();
            SetBirthDateSummary();
            SetUserSexSummary();
            SetHeightSummary();
            SetWeightSummary();

            SetUnitSystemSummary();
            SetCurrencySummary();
        }

        public void OnSharedPreferenceChanged(ISharedPreferences sharedPreferences, string key)
        {
            SettingsUtil.UpdateFromSharedPreferences(sharedPreferences);

            UpdateLabels();
            UpdateSummaries();
        }
    }
}
