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
using System.Threading.Tasks;

using Android.OS;
using Android.Views;
using Android.Widget;

using EnergonSoftware.BackpackPlanner.DAL;
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.Units.Currency;
using EnergonSoftware.BackpackPlanner.Units.Units;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Items
{
    public sealed class AddGearItemFragment : AddItemFragment<GearItem>
    {
        protected override int LayoutResource => Resource.Layout.fragment_add_gear_item;

        protected override int TitleResource => Resource.String.title_add_gear_item;

        protected override int AddItemResource => Resource.Id.fab_add_gear_item;

        protected override int ResetItemResource => Resource.Id.fab_reset_gear_item;

#region Controls
        private Android.Support.Design.Widget.TextInputLayout _gearItemNameEditText;
        private Android.Support.Design.Widget.TextInputLayout _gearItemMakeEditText;
        private Android.Support.Design.Widget.TextInputLayout _gearItemModelEditText;
        private Android.Support.Design.Widget.TextInputLayout _gearItemWebsiteEditText;
        private RadioGroup _gearItemCarriedRadioGroup;
        private Android.Support.V7.Widget.SwitchCompat _gearItemConsumableSwitch;
        private Android.Support.Design.Widget.TextInputLayout _gearItemConsumedEditText;
        private Android.Support.Design.Widget.TextInputLayout _gearItemWeightEditText;
        private Android.Support.Design.Widget.TextInputLayout _gearItemCostEditText;
        private Android.Support.Design.Widget.TextInputLayout _gearItemNoteEditText;
#endregion

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _gearItemNameEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.add_gear_item_name);
            _gearItemMakeEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.add_gear_item_make);
            _gearItemModelEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.add_gear_item_model);
            _gearItemWebsiteEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.add_gear_item_website);
            _gearItemCarriedRadioGroup = view.FindViewById<RadioGroup>(Resource.Id.add_gear_item_carried);
            _gearItemConsumableSwitch = view.FindViewById<Android.Support.V7.Widget.SwitchCompat>(Resource.Id.add_gear_item_consumable);
            _gearItemConsumedEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.add_gear_item_consumed);
            _gearItemWeightEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.add_gear_item_weight);
            _gearItemCostEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.add_gear_item_cost);
            _gearItemNoteEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.add_gear_item_note);

            _gearItemConsumableSwitch.CheckedChange += (sender, args) => {
                _gearItemConsumedEditText.Visibility = args.IsChecked ? ViewStates.Visible : ViewStates.Gone;
            };

            _gearItemWeightEditText.Hint = Java.Lang.String.Format(Activity.Resources.GetString(Resource.String.label_gear_item_weight),
                BaseActivity.BackpackPlannerState.Settings.Units.GetSmallWeightString(true)
            );

            _gearItemCostEditText.Hint = Java.Lang.String.Format(Activity.Resources.GetString(Resource.String.label_gear_item_cost),
                BaseActivity.BackpackPlannerState.Settings.Currency.GetCurrencyString()
            );
        }

        protected override GearItem CreateItem()
        {
            return new GearItem();
        }

        protected override async Task AddItemAsync(DatabaseContext dbContext)
        {
            await dbContext.GearItems.AddAsync(Item).ConfigureAwait(false);
        }

        protected override void OnDoDataExchange()
        {
            Item.Name = _gearItemNameEditText.EditText.Text;
            Item.Make = _gearItemMakeEditText.EditText.Text;
            Item.Model = _gearItemModelEditText.EditText.Text;
            Item.Url = _gearItemWebsiteEditText.EditText.Text;
            Item.SetWeightInUnits(BaseActivity.BackpackPlannerState.Settings, Convert.ToSingle(_gearItemWeightEditText.EditText.Text));
            Item.SetCostInCurrency(BaseActivity.BackpackPlannerState.Settings, Convert.ToSingle(_gearItemCostEditText.EditText.Text));
            Item.Note = _gearItemNoteEditText.EditText.Text;

            int carriedSelectionResId = _gearItemCarriedRadioGroup.CheckedRadioButtonId;
            switch(carriedSelectionResId)
            {
            case Resource.Id.add_gear_item_carried_carried:
                Item.Carried = GearCarried.Carried;
                break;
            case Resource.Id.add_gear_item_carried_worn:
                Item.Carried = GearCarried.Worn;
                break;
            case Resource.Id.add_gear_item_carried_not_carried:
                Item.Carried = GearCarried.NotCarried;
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

        protected override void OnReset()
        {
// TODO
        }
    }
}
