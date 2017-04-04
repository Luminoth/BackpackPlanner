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
    public sealed class ViewGearItemFragment : ViewItemFragment<GearItem>
    {
        protected override int LayoutResource => Resource.Layout.fragment_view_gear_item;

        protected override int TitleResource => Resource.String.title_view_gear_item;

        protected override int SaveItemResource => Resource.Id.fab_save_gear_item;

        protected override int ResetItemResource => Resource.Id.fab_reset_gear_item;

        protected override int DeleteItemResource => Resource.Id.fab_delete_gear_item;

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

            _gearItemNameEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.view_gear_item_name);
            _gearItemNameEditText.EditText.Text = Item.Name;

            _gearItemMakeEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.view_gear_item_make);
            _gearItemMakeEditText.EditText.Text = Item.Make;

            _gearItemModelEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.view_gear_item_model);
            _gearItemModelEditText.EditText.Text = Item.Model;

            _gearItemWebsiteEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.view_gear_item_website);
            _gearItemWebsiteEditText.EditText.Text = Item.Url;

            _gearItemCarriedRadioGroup = view.FindViewById<RadioGroup>(Resource.Id.view_gear_item_carried);
            switch(Item.Carried)
            {
            case GearCarried.Carried:
                _gearItemCarriedRadioGroup.Check(Resource.Id.view_gear_item_carried_carried);
                break;
            case GearCarried.Worn:
                _gearItemCarriedRadioGroup.Check(Resource.Id.view_gear_item_carried_worn);
                break;
            case GearCarried.NotCarried:
                _gearItemCarriedRadioGroup.Check(Resource.Id.view_gear_item_carried_not_carried);
                break;
            }

            _gearItemConsumableSwitch = view.FindViewById<Android.Support.V7.Widget.SwitchCompat>(Resource.Id.view_gear_item_consumable);
            _gearItemConsumableSwitch.Checked = Item.IsConsumable;

            _gearItemConsumedEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.view_gear_item_consumed);
            _gearItemConsumedEditText.EditText.Text = Item.ConsumedPerDay.ToString();

            _gearItemWeightEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.view_gear_item_weight);
            _gearItemWeightEditText.EditText.Text = ((int)Item.GetWeightInUnits(BaseActivity.BackpackPlannerState.Settings)).ToString();

            _gearItemCostEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.view_gear_item_cost);
            _gearItemCostEditText.EditText.Text = ((int)Item.GetCostInCurrency(BaseActivity.BackpackPlannerState.Settings)).ToString();

            _gearItemNoteEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.view_gear_item_note);
            _gearItemNoteEditText.EditText.Text = Item.Note;

            if(Item.IsConsumable) {
                _gearItemConsumedEditText.Visibility = ViewStates.Visible;
            }

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

        protected override void UpdateView()
        {
        }

        protected override async Task OnDoDataExchange(DatabaseContext dbContext)
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

            await Task.Delay(0).ConfigureAwait(false);
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
