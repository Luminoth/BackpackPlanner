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
using Android.Widget;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Items
{
    public class AddGearItemFragment : BaseFragment
    {
        EditText _gearItemNameEditText;
        // ...
        Spinner _gearItemCarriedSpinner;

        public override int LayoutResource => Resource.Layout.fragment_add_gear_item;

        public override int TitleResource => Resource.String.title_add_gear_item;

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _gearItemNameEditText = view.FindViewById<EditText>(Resource.Id.gear_item_name);

            //...

            _gearItemCarriedSpinner = view.FindViewById<Spinner>(Resource.Id.gear_item_carried);
            ArrayAdapter adapter = ArrayAdapter.CreateFromResource(Activity, Resource.Array.gear_item_carried_entries, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            _gearItemCarriedSpinner.Adapter = adapter;

            Button addGearItemButton = view.FindViewById<Button>(Resource.Id.button_add_gear_item);
            addGearItemButton.Click += (sender, args) => {
                if(Validate()) {
                }
            };
        }

        private bool Validate()
        {
            bool valid = true;

            if(string.IsNullOrWhiteSpace(_gearItemNameEditText.Text)) {
                _gearItemNameEditText.Error = "A name is required!";
                valid = false;                
            }

            return valid;
        }
    }
}
