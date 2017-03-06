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
using Android.Views;

using EnergonSoftware.BackpackPlanner.Models.Gear.Systems;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Systems
{
    public sealed class AddGearSystemFragment : AddItemFragment<GearSystem>
    {
        protected override int LayoutResource => Resource.Layout.fragment_add_gear_system;

        protected override int TitleResource => Resource.String.title_add_gear_system;

        protected override int AddItemResource => Resource.Id.button_add_gear_system;

        protected override bool HasSearchView => false;

#region Controls
        private Android.Support.Design.Widget.TextInputLayout _gearSystemNameEditText;
        private Android.Support.Design.Widget.TextInputLayout _gearSystemNoteEditText;
#endregion

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _gearSystemNameEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.add_gear_system_name);
            _gearSystemNoteEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.add_gear_system_note);
        }

        protected override void OnDoDataExchange()
        {
            Item = new GearSystem(DroidState.Instance.BackpackPlannerState.Settings)
            {
                Name = _gearSystemNameEditText.EditText.Text,
                Note = _gearSystemNoteEditText.EditText.Text
            };
        }

        protected override bool OnValidate()
        {
            bool valid = true;

            if(string.IsNullOrWhiteSpace(_gearSystemNameEditText.EditText.Text)) {
                _gearSystemNameEditText.EditText.Error = "A name is required!";
                valid = false;                
            }

            return valid;
        }
    }
}
