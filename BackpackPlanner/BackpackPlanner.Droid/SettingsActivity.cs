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

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Views;

using EnergonSoftware.BackpackPlanner.Droid.Util;
using EnergonSoftware.BackpackPlanner.Models.Personal;

namespace EnergonSoftware.BackpackPlanner.Droid
{
    // this trainwreck is necessary until there's an AppCompatPreferenceFragment to work with
    [Activity(Label = "@string/title_settings")]
    public class SettingsActivity : AppCompatPreferenceActivity, ISharedPreferencesOnSharedPreferenceChangeListener
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            AddPreferencesFromResource(Resource.Xml.settings);

            InitToolBar();

            InitSummaries();
        }

        public override bool OnPreferenceTreeClick(PreferenceScreen preferenceScreen, Preference preference)
        {
            base.OnPreferenceTreeClick(preferenceScreen, preference);

            PreferenceScreen screen = preference as PreferenceScreen;
            if(null != screen) {
                InitNestedToolBar(screen);
            }

            return false;
        }

        protected override void OnResume()
        {
            base.OnResume();

            PreferenceScreen.SharedPreferences.RegisterOnSharedPreferenceChangeListener(this);
        }

        protected override void OnPause()
        {
            base.OnPause();

            PreferenceScreen.SharedPreferences.UnregisterOnSharedPreferenceChangeListener(this);
        }

        private void InitToolBar()
        {
            // first we have to add the toolbar (as a generic view) to the view group
            ViewGroup rootViewGroup = (ViewGroup)FindViewById(Android.Resource.Id.List).Parent.Parent.Parent;
            View view = LayoutInflater.From(this).Inflate(Resource.Layout.settings_toolbar, rootViewGroup, false);
            rootViewGroup.AddView(view, 0);

            // then we can find the toolbar as a toolbar and set it as the action bar
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.settings_toolbar);
            SetSupportActionBar(toolbar);

            // then we can setup the home button
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            //SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_close);

            // enable the home button to work
            toolbar.NavigationClick += (sender, args) => {
                Finish();
            };
        }

        private void InitNestedToolBar(PreferenceScreen preferenceScreen)
        {
            Dialog dialog = preferenceScreen.Dialog;

            // first we have to add the toolbar (as a generic view) to the view group
            ViewGroup rootViewGroup = (ViewGroup)dialog.FindViewById(Android.Resource.Id.List).Parent.Parent.Parent;
            View view = LayoutInflater.From(this).Inflate(Resource.Layout.settings_toolbar, rootViewGroup, false);
            rootViewGroup.AddView(view, 0);

            // then we can find the toolbar as a toolbar and set it as the action bar
            Android.Support.V7.Widget.Toolbar toolbar = dialog.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.settings_toolbar);

            // enable the home button to work
            toolbar.NavigationClick += (sender, args) => {
                dialog.Dismiss();
            };
        }

#region Summaries
        private void SetNameSummary()
        {
            EditTextPreference namePreference = (EditTextPreference)FindPreference(PersonalInformation.NamePreferenceKey);
            namePreference.Summary = string.IsNullOrWhiteSpace(BackpackPlannerState.Instance.PersonalInformation.Name)
                ? Resources.GetString(Resource.String.summary_name)
                : BackpackPlannerState.Instance.PersonalInformation.Name;
        }

        private void SetBirthDateSummary()
        {
            EditTextPreference birthDatePreference = (EditTextPreference)FindPreference(PersonalInformation.DateOfBirthPreferenceKey);
            birthDatePreference.Summary = null == BackpackPlannerState.Instance.PersonalInformation.DateOfBirth
                ? Resources.GetString(Resource.String.summary_birthdate)
                : BackpackPlannerState.Instance.PersonalInformation.DateOfBirth.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        private void SetUserSexSummary()
        {
            ListPreference userSexPreference = (ListPreference)FindPreference(PersonalInformation.UserSexPreferenceKey);
            userSexPreference.Summary = Resources.GetStringArray(Resource.Array.user_sex_entries)[(int)BackpackPlannerState.Instance.PersonalInformation.Sex];
        }

        private void SetHeightSummary()
        {
            EditTextPreference heightPreference = (EditTextPreference)FindPreference(PersonalInformation.HeightPreferenceKey);
            heightPreference.Summary = BackpackPlannerState.Instance.PersonalInformation.HeightInCm.ToString();
        }

        private void SetWeightSummary()
        {
            EditTextPreference weightPreference = (EditTextPreference)FindPreference(PersonalInformation.WeightPreferenceKey);
            weightPreference.Summary = BackpackPlannerState.Instance.PersonalInformation.WeightInGrams.ToString();
        }

        private void SetUnitSystemSummary()
        {
            ListPreference unitSystemPreference = (ListPreference)FindPreference(BackpackPlannerSettings.UnitSystemPreferenceKey);
            unitSystemPreference.Summary = Resources.GetStringArray(Resource.Array.unit_system_entries)[(int)BackpackPlannerState.Instance.Settings.Units];
        }

        private void SetCurrencySummary()
        {
            ListPreference currencyPreference = (ListPreference)FindPreference(BackpackPlannerSettings.CurrencyPreferenceKey);
            currencyPreference.Summary = Resources.GetStringArray(Resource.Array.currency_entries)[(int)BackpackPlannerState.Instance.Settings.Currency];
        }
#endregion

        private void InitSummaries()
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
            switch(key)
            {
            case PersonalInformation.NamePreferenceKey:
                EditTextPreference namePreference = (EditTextPreference)FindPreference(key);
                BackpackPlannerState.Instance.PersonalInformation.Name = namePreference.Text;
                SetNameSummary();
                break;
            case PersonalInformation.DateOfBirthPreferenceKey:
                EditTextPreference birthDatePreference = (EditTextPreference)FindPreference(key);
                BackpackPlannerState.Instance.PersonalInformation.DateOfBirth = Convert.ToDateTime(birthDatePreference.Text);
                SetBirthDateSummary();
                break;
            case PersonalInformation.UserSexPreferenceKey:
                ListPreference userSexPreference = (ListPreference)FindPreference(key);
                BackpackPlannerState.Instance.PersonalInformation.Sex = (UserSex)Convert.ToInt32(userSexPreference.Value);
                SetUserSexSummary();
                break;
            case PersonalInformation.HeightPreferenceKey:
                EditTextPreference heightPreference = (EditTextPreference)FindPreference(key);
                //BackpackPlannerState.Instance.PersonalInformation.HeightInCm = Convert.ToInt32(heightPreference.Text);
                SetHeightSummary();
                break;
            case PersonalInformation.WeightPreferenceKey:
                EditTextPreference weightPreference = (EditTextPreference)FindPreference(key);
                //BackpackPlannerState.Instance.PersonalInformation.WeightInGrams = Convert.ToInt32(weightPreference.Text);
                SetWeightSummary();
                break;
            case BackpackPlannerSettings.UnitSystemPreferenceKey:
                ListPreference unitSystemPreference = (ListPreference)FindPreference(key);
                BackpackPlannerState.Instance.Settings.Units = (UnitSystem)Convert.ToInt32(unitSystemPreference.Value);
                SetUnitSystemSummary();
                break;
            case BackpackPlannerSettings.CurrencyPreferenceKey:
                ListPreference currencyPreference = (ListPreference)FindPreference(key);
                BackpackPlannerState.Instance.Settings.Currency = (Currency)Convert.ToInt32(currencyPreference.Value);
                SetCurrencySummary();
                break;
            }

            OnContentChanged();
        }
    }
}
