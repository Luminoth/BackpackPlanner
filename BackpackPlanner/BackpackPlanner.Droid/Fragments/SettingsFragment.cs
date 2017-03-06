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
using Android.OS;

using EnergonSoftware.BackpackPlanner.Droid.Activities;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Util;
using EnergonSoftware.BackpackPlanner.Models.Personal;
using EnergonSoftware.BackpackPlanner.Settings;
using EnergonSoftware.BackpackPlanner.Units.Units;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments
{
    public sealed class SettingsFragment : Android.Support.V7.Preferences.PreferenceFragmentCompat, ISharedPreferencesOnSharedPreferenceChangeListener
    {
        public BaseActivity BaseActivity => (BaseActivity)Activity;

#region Controls
        private Android.Support.V7.Preferences.PreferenceCategory _personalInformationCategory;
        //private Android.Support.V7.Preferences.EditTextPreference _namePreference;
        //private Android.Support.V7.Preferences.EditTextPreference _birthDatePreference;
        //private Android.Support.V7.Preferences.ListPreference _userSexPreference;
        //private Android.Support.V7.Preferences.EditTextPreference _heightPreference;
        private Android.Support.V7.Preferences.EditTextPreference _weightPreference;

        private Android.Support.V7.Preferences.PreferenceCategory _unitsCategory;
        private Android.Support.V7.Preferences.ListPreference _unitSystemPreference;
        private Android.Support.V7.Preferences.ListPreference _currencyPreference;
#endregion

        public override void OnCreatePreferences(Bundle savedInstanceState, string rootKey)
        {
            AddPreferencesFromResource(Resource.Xml.settings);

// TODO: remove any of these that are unused

            _personalInformationCategory = (Android.Support.V7.Preferences.PreferenceCategory)FindPreference(PersonalInformation.PreferenceKey);
            //_namePreference = (Android.Support.V7.Preferences.EditTextPreference)FindPreference(PersonalInformation.NamePreferenceKey);

            //_birthDatePreference = (Android.Support.V7.Preferences.EditTextPreference)FindPreference(PersonalInformation.DateOfBirthPreferenceKey);
            /*_birthDatePreference.PreferenceClick += (sender, args) => {
                DateTime dateTime = DateTime.Now;
                try {
                    dateTime = Convert.ToDateTime(_birthDatePreference.Text);
                } catch(FormatException) {
                }

                DatePickerFragment picker = new DatePickerFragment(dateTime);
                picker.DateSetEvent += (s, a) => {
                    _birthDatePreference.Text = a.Date.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture);
                };
                picker.Show(Activity.SupportFragmentManager, null);
            };*/

            //_userSexPreference = (Android.Support.V7.Preferences.ListPreference)FindPreference(PersonalInformation.UserSexPreferenceKey);
            //_heightPreference = (Android.Support.V7.Preferences.EditTextPreference)FindPreference(PersonalInformation.HeightPreferenceKey);
            _weightPreference = (Android.Support.V7.Preferences.EditTextPreference)FindPreference(PersonalInformation.WeightPreferenceKey);

            _unitsCategory = (Android.Support.V7.Preferences.PreferenceCategory)FindPreference(BackpackPlannerSettings.UnitsPreferenceKey);
            _unitSystemPreference = (Android.Support.V7.Preferences.ListPreference)FindPreference(BackpackPlannerSettings.UnitSystemPreferenceKey);
            _currencyPreference = (Android.Support.V7.Preferences.ListPreference)FindPreference(BackpackPlannerSettings.CurrencyPreferenceKey);

#if !DEBUG
            PreferenceScreen.RemovePreference(_personalInformationCategory);
            _unitsCategory.RemovePreference(_currencyPreference);
#endif

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
        /*private void SetHeightLabel()
        {
            _heightPreference.DialogTitle = _heightPreference.Title = Java.Lang.String.Format(Activity.Resources.GetString(Resource.String.label_height),
                DroidState.Instance.BackpackPlannerState.Settings.Units.GetSmallLengthString(true)
            );
        }*/

        private void SetWeightLabel()
        {
            _weightPreference.DialogTitle = _weightPreference.Title = Java.Lang.String.Format(Activity.Resources.GetString(Resource.String.label_weight),
                DroidState.Instance.BackpackPlannerState.Settings.Units.GetSmallWeightString(true)
            );
        }
#endregion

#region Summaries
        /*private void SetNameSummary()
        {
            _namePreference.Summary = string.IsNullOrWhiteSpace(DroidState.Instance.BackpackPlannerState.PersonalInformation.Name)
                ? Resources.GetString(Resource.String.summary_name)
                : DroidState.Instance.BackpackPlannerState.PersonalInformation.Name;
        }*/

        /*private void SetBirthDateSummary()
        {
            _birthDatePreference.Summary = DroidState.Instance.BackpackPlannerState.PersonalInformation.DateOfBirth?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) ?? Resources.GetString(Resource.String.summary_birthdate);
        }*/

        /*private void SetUserSexSummary()
        {
            _userSexPreference.Summary = DroidState.Instance.BackpackPlannerState.PersonalInformation.Sex.ToString();
        }*/

        /*private void SetHeightSummary()
        {
            // TODO: make this a resource
            _heightPreference.Summary = ((int)DroidState.Instance.BackpackPlannerState.PersonalInformation.HeightInUnits)
                + " " + DroidState.Instance.BackpackPlannerState.Settings.Units.GetSmallLengthString(true);
        }*/

        private void SetWeightSummary()
        {
            // TODO: make this a resource
            _weightPreference.Summary = ((int)DroidState.Instance.BackpackPlannerState.PersonalInformation.WeightInUnits)
                + " " + DroidState.Instance.BackpackPlannerState.Settings.Units.GetSmallWeightString(true);
        }

        private void SetUnitSystemSummary()
        {
            _unitSystemPreference.Summary = DroidState.Instance.BackpackPlannerState.Settings.Units.ToString();
        }

        private void SetCurrencySummary()
        {
            _currencyPreference.Summary = DroidState.Instance.BackpackPlannerState.Settings.Currency.ToString();
        }
#endregion

        private void UpdateLabels()
        {
            //SetHeightLabel();
            SetWeightLabel();
        }

        private void UpdateSummaries()
        {
            //SetNameSummary();
            //SetBirthDateSummary();
            //SetUserSexSummary();
            //SetHeightSummary();
            SetWeightSummary();

            SetUnitSystemSummary();
            SetCurrencySummary();
        }

        public void OnSharedPreferenceChanged(ISharedPreferences sharedPreferences, string key)
        {
            DroidState.Instance.BackpackPlannerState.PlatformSettingsManager.UpdateFromPreferences(key, DroidState.Instance.BackpackPlannerState.Settings, DroidState.Instance.BackpackPlannerState.PersonalInformation);

// TODO: if the connect google play services setting is switched from off to on
// we need to do the connection here

            UpdateLabels();
            UpdateSummaries();
        }
    }
}
