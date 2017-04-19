/*
   Copyright 2017 Shane Lillie

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

using Android.Views;
using Android.Widget;

using EnergonSoftware.BackpackPlanner.DAL;
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.Droid.Activities;
using EnergonSoftware.BackpackPlanner.Units.Currency;
using EnergonSoftware.BackpackPlanner.Units.Units;

namespace EnergonSoftware.BackpackPlanner.Droid.Views.Gear
{
    public sealed class GearItemViewHolder : BaseModelViewHolder<GearItem>
    {
        private readonly Android.Support.Design.Widget.TextInputLayout _gearItemNameEditText;
        private readonly Android.Support.Design.Widget.TextInputLayout _gearItemMakeEditText;
        private readonly Android.Support.Design.Widget.TextInputLayout _gearItemModelEditText;
        private readonly Android.Support.Design.Widget.TextInputLayout _gearItemWebsiteEditText;
        private readonly RadioGroup _gearItemCarriedRadioGroup;
        private readonly Android.Support.V7.Widget.SwitchCompat _gearItemConsumableSwitch;
        private readonly Android.Support.Design.Widget.TextInputLayout _gearItemConsumedEditText;
        private readonly Android.Support.Design.Widget.TextInputLayout _gearItemWeightEditText;
        private readonly Android.Support.Design.Widget.TextInputLayout _gearItemCostEditText;
        private readonly Android.Support.Design.Widget.TextInputLayout _gearItemNoteEditText;

        public GearItemViewHolder(BaseActivity activity, View view)
            : base(activity)
        {
            _gearItemNameEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.gear_item_name);
            _gearItemNameEditText.EditText.AfterTextChanged += (sender, args) =>
            {
                NotifyPropertyChanged("Name");
            };

            _gearItemMakeEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.gear_item_make);
            _gearItemMakeEditText.EditText.AfterTextChanged += (sender, args) =>
            {
                NotifyPropertyChanged("Make");
            };

            _gearItemModelEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.gear_item_model);
            _gearItemModelEditText.EditText.AfterTextChanged += (sender, args) =>
            {
                NotifyPropertyChanged("Model");
            };

            _gearItemWebsiteEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.gear_item_website);
            _gearItemWebsiteEditText.EditText.AfterTextChanged += (sender, args) =>
            {
                NotifyPropertyChanged("Website");
            };

            _gearItemCarriedRadioGroup = view.FindViewById<RadioGroup>(Resource.Id.gear_item_carried);
            _gearItemCarriedRadioGroup.CheckedChange += (sender, args) =>
            {
                NotifyPropertyChanged("Carried");
            };

            _gearItemConsumableSwitch = view.FindViewById<Android.Support.V7.Widget.SwitchCompat>(Resource.Id.gear_item_consumable);
            _gearItemConsumableSwitch.CheckedChange += (sender, args) =>
            {
                _gearItemConsumedEditText.Visibility = args.IsChecked ? ViewStates.Visible : ViewStates.Gone;
                NotifyPropertyChanged("Consumable");
            };

            _gearItemConsumedEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.gear_item_consumed);
            _gearItemConsumedEditText.EditText.AfterTextChanged += (sender, args) =>
            {
                NotifyPropertyChanged("Consumed");
            };

            _gearItemWeightEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.gear_item_weight);
            _gearItemWeightEditText.EditText.AfterTextChanged += (sender, args) =>
            {
                NotifyPropertyChanged("Weight");
            };

            _gearItemWeightEditText.Hint = Java.Lang.String.Format(BaseActivity.Resources.GetString(Resource.String.label_gear_item_weight),
                BaseActivity.BackpackPlannerState.Settings.Units.GetSmallWeightString(true)
            );

            _gearItemCostEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.gear_item_cost);
            _gearItemCostEditText.EditText.AfterTextChanged += (sender, args) =>
            {
                NotifyPropertyChanged("Cost");
            };

            _gearItemCostEditText.Hint = Java.Lang.String.Format(BaseActivity.Resources.GetString(Resource.String.label_gear_item_cost),
                BaseActivity.BackpackPlannerState.Settings.Currency.GetCurrencyString()
            );

            _gearItemNoteEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.gear_item_note);
            _gearItemNoteEditText.EditText.AfterTextChanged += (sender, args) =>
            {
                NotifyPropertyChanged("Note");
            };
        }

        public override void UpdateView(GearItem gearItem)
        {
            base.UpdateView(gearItem);

            _gearItemNameEditText.EditText.Text = gearItem.Name;
            _gearItemMakeEditText.EditText.Text = gearItem.Make;
            _gearItemModelEditText.EditText.Text = gearItem.Model;
            _gearItemWebsiteEditText.EditText.Text = gearItem.Url;

            switch(gearItem.Carried)
            {
            case GearCarried.Carried:
                _gearItemCarriedRadioGroup.Check(Resource.Id.gear_item_carried_carried);
                break;
            case GearCarried.Worn:
                _gearItemCarriedRadioGroup.Check(Resource.Id.gear_item_carried_worn);
                break;
            case GearCarried.NotCarried:
                _gearItemCarriedRadioGroup.Check(Resource.Id.gear_item_carried_not_carried);
                break;
            }

            _gearItemConsumableSwitch.Checked = gearItem.IsConsumable;
            _gearItemConsumedEditText.EditText.Text = gearItem.ConsumedPerDay.ToString();
            _gearItemWeightEditText.EditText.Text = ((int)gearItem.GetWeightInUnits(BaseActivity.BackpackPlannerState.Settings)).ToString();
            _gearItemCostEditText.EditText.Text = ((int)gearItem.GetCostInCurrency(BaseActivity.BackpackPlannerState.Settings)).ToString();
            _gearItemNoteEditText.EditText.Text = gearItem.Note;

            if(gearItem.IsConsumable) {
                _gearItemConsumedEditText.Visibility = ViewStates.Visible;
            }
        }

        public override bool Validate()
        {
            bool valid = base.Validate();

            if(string.IsNullOrWhiteSpace(_gearItemNameEditText.EditText.Text)) {
                _gearItemNameEditText.EditText.Error = "A name is required!";
                valid = false;                
            }

            return valid;
        }

        public override void DoDataExchange(GearItem gearItem, DatabaseContext dbContext)
        {
            gearItem.Name = _gearItemNameEditText.EditText.Text;
            gearItem.Make = _gearItemMakeEditText.EditText.Text;
            gearItem.Model = _gearItemModelEditText.EditText.Text;
            gearItem.Url = _gearItemWebsiteEditText.EditText.Text;
            gearItem.SetWeightInUnits(BaseActivity.BackpackPlannerState.Settings, Convert.ToSingle(_gearItemWeightEditText.EditText.Text));
            gearItem.SetCostInCurrency(BaseActivity.BackpackPlannerState.Settings, Convert.ToSingle(_gearItemCostEditText.EditText.Text));
            gearItem.Note = _gearItemNoteEditText.EditText.Text;

            int carriedSelectionResId = _gearItemCarriedRadioGroup.CheckedRadioButtonId;
            switch(carriedSelectionResId)
            {
            case Resource.Id.gear_item_carried_carried:
                gearItem.Carried = GearCarried.Carried;
                break;
            case Resource.Id.gear_item_carried_worn:
                gearItem.Carried = GearCarried.Worn;
                break;
            case Resource.Id.gear_item_carried_not_carried:
                gearItem.Carried = GearCarried.NotCarried;
                break;
            }

            base.DoDataExchange(gearItem, dbContext);
        }
    }
}