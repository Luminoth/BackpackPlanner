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

using Android.OS;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments
{
    // TODO: there's some text on this that needs to be dynamic
    // need to figure out a way to get that updated
    public class SettingsFragment : Android.Support.V7.Preferences.PreferenceFragmentCompat
    {
        public override void OnCreatePreferences(Bundle savedInstanceState, string rootKey)
        {
            AddPreferencesFromResource(Resource.Xml.settings);

            Activity.Title = Resources.GetString(Resource.String.title_settings);
        }
    }
}
