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

using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;

using EnergonSoftware.BackpackPlanner.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.Units;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Items
{
    public class AddGearItemFragment : DataFragment
    {
#region Controls
        private TextInputLayout _gearItemNameEditText;
        private TextInputLayout _gearItemMakeEditText;
        private TextInputLayout _gearItemModelEditText;
        private TextInputLayout _gearItemWebsiteEditText;
        private RadioGroup _gearItemCarriedRadioGroup;
        private Android.Support.V7.Widget.SwitchCompat _gearItemConsumableSwitch;
        private TextInputLayout _gearItemConsumedEditText;
        private TextInputLayout _gearItemWeightEditText;
        private TextInputLayout _gearItemCostEditText;
        private TextInputLayout _gearItemNoteEditText;
#endregion

        private GearItem _gearItem;

        public override int LayoutResource => Resource.Layout.fragment_add_gear_item;

        public override int TitleResource => Resource.String.title_add_gear_item;

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _gearItemNameEditText = view.FindViewById<TextInputLayout>(Resource.Id.gear_item_name);
            _gearItemMakeEditText = view.FindViewById<TextInputLayout>(Resource.Id.gear_item_make);
            _gearItemModelEditText = view.FindViewById<TextInputLayout>(Resource.Id.gear_item_model);
            _gearItemWebsiteEditText = view.FindViewById<TextInputLayout>(Resource.Id.gear_item_website);
            _gearItemCarriedRadioGroup = view.FindViewById<RadioGroup>(Resource.Id.gear_item_carried);
            _gearItemConsumableSwitch = view.FindViewById<Android.Support.V7.Widget.SwitchCompat>(Resource.Id.gear_item_consumable);
            _gearItemConsumedEditText = view.FindViewById<TextInputLayout>(Resource.Id.gear_item_consumed);
            _gearItemWeightEditText = view.FindViewById<TextInputLayout>(Resource.Id.gear_item_weight);
            _gearItemCostEditText = view.FindViewById<TextInputLayout>(Resource.Id.gear_item_cost);
            _gearItemNoteEditText = view.FindViewById<TextInputLayout>(Resource.Id.gear_item_note);

            _gearItemConsumableSwitch.CheckedChange += (sender, args) => {
                _gearItemConsumedEditText.Visibility = args.IsChecked ? ViewStates.Visible : ViewStates.Gone;
            };

            _gearItemWeightEditText.SetHint(
                Resources.GetString(Resource.String.label_gear_item_weight) + " " + BackpackPlannerState.Instance.Settings.Units.GetSmallWeightString());
            _gearItemCostEditText.SetHint(
                Resources.GetString(Resource.String.label_gear_item_cost) + " " + BackpackPlannerState.Instance.Settings.Currency.GetCurrencyString());

            Button addGearItemButton = view.FindViewById<Button>(Resource.Id.button_add_gear_item);
            addGearItemButton.Click += (sender, args) => {
                if(!DoDataExchange()) {
                    return;
                }

                // TODO: add the item!

                Activity.SupportFragmentManager.PopBackStack();
            };
        }

        protected override void OnDoDataExchange()
        {
            _gearItem = new GearItem
            {
                Name = _gearItemNameEditText.EditText.Text,
                Make = _gearItemMakeEditText.EditText.Text,
                Model = _gearItemModelEditText.EditText.Text,
                Url = _gearItemWebsiteEditText.EditText.Text,
                WeightInUnits = Convert.ToDouble(_gearItemWeightEditText.EditText.Text),
                CostInCurrency = Convert.ToDouble(_gearItemCostEditText.EditText.Text),
                Note = _gearItemNoteEditText.EditText.Text
            };

            int carriedSelectionResId = _gearItemCarriedRadioGroup.CheckedRadioButtonId;
            switch(carriedSelectionResId)
            {
            case Resource.Id.gear_item_carried_carried:
                _gearItem.Carried = GearCarried.Carried;
                break;
            case Resource.Id.gear_item_carried_worn:
                _gearItem.Carried = GearCarried.Worn;
                break;
            case Resource.Id.gear_item_carried_not_carried:
                _gearItem.Carried = GearCarried.NotCarried;
                break;
            }
        }

        protected override bool OnValidate()
        {
            bool valid = true;

            if(string.IsNullOrWhiteSpace(_gearItemNameEditText.EditText.Text)) {
                _gearItemNameEditText.EditText.Error = "A name is required!";
                valid = false;                
            }

            return valid;
        }
    }
}
