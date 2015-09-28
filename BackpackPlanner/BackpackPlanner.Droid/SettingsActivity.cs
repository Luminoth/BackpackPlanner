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
using EnergonSoftware.BackpackPlanner.Units;

// TODO: when the Xamarin Support Library Preference v7 is out
// replace this activity with the PreferenceFragmentCompat fragment
// (can also trash the AppCompatPreferenceActivity class)
// https://plus.google.com/+AndroidDevelopers/posts/9kZ3SsXdT2T
// http://developer.android.com/reference/android/support/v7/preference/package-summary.html
// https://www.nuget.org/packages/Xamarin.Android.Support.v7.Preference/
// http://developer.android.com/reference/android/support/v7/preference/PreferenceFragmentCompat.html

namespace EnergonSoftware.BackpackPlanner.Droid
{
    // this trainwreck is necessary until there's an AppCompatPreferenceFragment to work with
    [Activity(Label = "@string/title_settings")]
    public sealed class SettingsActivity : AppCompatPreferenceActivity, ISharedPreferencesOnSharedPreferenceChangeListener
    {
#region Controls
        private EditTextPreference _namePreference;
        private EditTextPreference _birthDatePreference;
        private ListPreference _userSexPreference;
        private EditTextPreference _heightPreference;
        private EditTextPreference _weightPreference;

        private ListPreference _unitSystemPreference;
        //private ListPreference _currencyPreference;
#endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            InitToolBar();

            AddPreferencesFromResource(Resource.Xml.settings);

            _namePreference = (EditTextPreference)FindPreference(PersonalInformation.NamePreferenceKey);
            _birthDatePreference = (EditTextPreference)FindPreference(PersonalInformation.DateOfBirthPreferenceKey);
            _userSexPreference = (ListPreference)FindPreference(PersonalInformation.UserSexPreferenceKey);
            _heightPreference = (EditTextPreference)FindPreference(PersonalInformation.HeightPreferenceKey);
            _weightPreference = (EditTextPreference)FindPreference(PersonalInformation.WeightPreferenceKey);

            _unitSystemPreference = (ListPreference)FindPreference(BackpackPlannerSettings.UnitSystemPreferenceKey);
            //_currencyPreference = (ListPreference)FindPreference(BackpackPlannerSettings.CurrencyPreferenceKey);
        }

        [Obsolete("deprecated")]
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

            InitLabels();
            InitSummaries();

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
            _userSexPreference.Summary = Resources.GetStringArray(Resource.Array.user_sex_entries)[(int)BackpackPlannerState.Instance.PersonalInformation.Sex];
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
            _unitSystemPreference.Summary = Resources.GetStringArray(Resource.Array.unit_system_entries)[(int)BackpackPlannerState.Instance.Settings.Units];
        }

        /*private void SetCurrencySummary()
        {
            _currencyPreference.Summary = Resources.GetStringArray(Resource.Array.currency_entries)[(int)BackpackPlannerState.Instance.Settings.Currency];
        }*/
#endregion

        private void InitLabels()
        {
            SetHeightLabel();
            SetWeightLabel();
        }

        private void InitSummaries()
        {
            SetNameSummary();
            SetBirthDateSummary();
            SetUserSexSummary();
            SetHeightSummary();
            SetWeightSummary();

            SetUnitSystemSummary();
            //SetCurrencySummary();
        }

        public void OnSharedPreferenceChanged(ISharedPreferences sharedPreferences, string key)
        {
            switch(key)
            {
            case PersonalInformation.NamePreferenceKey:
                BackpackPlannerState.Instance.PersonalInformation.Name = _namePreference.Text;
                SetNameSummary();
                break;
            case PersonalInformation.DateOfBirthPreferenceKey:
                BackpackPlannerState.Instance.PersonalInformation.DateOfBirth = Convert.ToDateTime(_birthDatePreference.Text);
                SetBirthDateSummary();
                break;
            case PersonalInformation.UserSexPreferenceKey:
                BackpackPlannerState.Instance.PersonalInformation.Sex = (UserSex)Convert.ToInt32(_userSexPreference.Value);
                SetUserSexSummary();
                break;
            case PersonalInformation.HeightPreferenceKey:
                BackpackPlannerState.Instance.PersonalInformation.HeightInUnits = Convert.ToInt32(_heightPreference.Text);
                SetHeightSummary();
                break;
            case PersonalInformation.WeightPreferenceKey:
                BackpackPlannerState.Instance.PersonalInformation.WeightInUnits = Convert.ToInt32(_weightPreference.Text);
                SetWeightSummary();
                break;
            case BackpackPlannerSettings.UnitSystemPreferenceKey:
                BackpackPlannerState.Instance.Settings.Units = (UnitSystem)Convert.ToInt32(_unitSystemPreference.Value);
                SetUnitSystemSummary();

                // TODO: package these up in a single method call?
                SetHeightLabel();
                SetHeightSummary();

                SetWeightLabel();
                SetWeightSummary();
                break;
            /*case BackpackPlannerSettings.CurrencyPreferenceKey:
                BackpackPlannerState.Instance.Settings.Currency = (Currency)Convert.ToInt32(_currencyPreference.Value);
                SetCurrencySummary();
                break;*/
            }

            OnContentChanged();
        }
    }
}
