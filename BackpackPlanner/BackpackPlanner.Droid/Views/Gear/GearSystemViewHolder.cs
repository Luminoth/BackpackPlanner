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
using System.Globalization;

using Android.Views;
using Android.Widget;

using EnergonSoftware.BackpackPlanner.DAL;
using EnergonSoftware.BackpackPlanner.DAL.Models;
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Systems;
using EnergonSoftware.BackpackPlanner.Droid.Activities;
using EnergonSoftware.BackpackPlanner.Droid.Adapters;
using EnergonSoftware.BackpackPlanner.Droid.Fragments;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Systems;
using EnergonSoftware.BackpackPlanner.Units.Units;

namespace EnergonSoftware.BackpackPlanner.Droid.Views.Gear
{
    public sealed class GearSystemViewHolder : BaseModelViewHolder<GearSystem>
    {
        private readonly Android.Support.Design.Widget.TextInputLayout _gearSystemNameEditText;
        private readonly Android.Support.Design.Widget.TextInputLayout _gearSystemNoteEditText;

        public GearSystemViewHolder(BaseActivity activity, View view)
            : base(activity)
        {
            _gearSystemNameEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.gear_system_name);
            _gearSystemNameEditText.EditText.AfterTextChanged += (sender, args) =>
            {
                NotifyPropertyChanged("Name");
            };

            _gearSystemNoteEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.gear_system_note);
            _gearSystemNoteEditText.EditText.AfterTextChanged += (sender, args) =>
            {
                NotifyPropertyChanged("Note");
            };
        }

        public override void UpdateView(GearSystem gearSystem)
        {
            base.UpdateView(gearSystem);

            _gearSystemNameEditText.EditText.Text = gearSystem.Name;
            _gearSystemNoteEditText.EditText.Text = gearSystem.Note;
        }

        public override bool Validate()
        {
            bool valid = base.Validate();

            if(string.IsNullOrWhiteSpace(_gearSystemNameEditText.EditText.Text)) {
                _gearSystemNameEditText.EditText.Error = "A name is required!";
                valid = false;                
            }

            return valid;
        }

        public override void DoDataExchange(GearSystem gearSystem, DatabaseContext dbContext)
        {
            gearSystem.Name = _gearSystemNameEditText.EditText.Text;
            gearSystem.Note = _gearSystemNoteEditText.EditText.Text;

            base.DoDataExchange(gearSystem, dbContext);
        }
    }

    public sealed class GearSystemListViewHolder : BaseModelRecyclerViewHolder<GearSystem>
    {
        protected override int ToolbarResourceId => Resource.Id.view_gear_system_toolbar;

        protected override int MenuResourceId => Resource.Menu.gear_system_menu;

        protected override int DeleteActionResourceId => Resource.Id.action_delete_gear_system;

        private readonly TextView _textViewItems;
        private readonly TextView _textViewWeight;
        private readonly TextView _textViewCost;

        public GearSystemListViewHolder(View view, BaseRecyclerListAdapter<GearSystem> adapter)
            : base(view, adapter)
        {
            _textViewItems = view.FindViewById<TextView>(Resource.Id.view_gear_system_items);
            _textViewWeight = view.FindViewById<TextView>(Resource.Id.view_gear_system_weight);
            _textViewCost = view.FindViewById<TextView>(Resource.Id.view_gear_system_cost);
        }

        protected override ViewItemFragment<GearSystem> CreateViewItemFragment()
        {
            return new ViewGearSystemFragment();
        }

        public override void UpdateView(GearSystem gearSystem)
        {
            base.UpdateView(gearSystem);

            _textViewItems.Text = Java.Lang.String.Format(Activity.Resources.GetString(Resource.String.label_view_gear_system_items),
                gearSystem.GearItems.Count
            );

            int weightInUnits = (int)gearSystem.GetTotalWeightInUnits(Activity.BackpackPlannerState.Settings);
            _textViewWeight.Text = Java.Lang.String.Format(Activity.Resources.GetString(Resource.String.label_view_gear_system_weight),
                weightInUnits, Activity.BackpackPlannerState.Settings.Units.GetSmallWeightString(weightInUnits != 1)
            );

            string formattedCost = gearSystem.GetTotalCostInCurrency(Activity.BackpackPlannerState.Settings).ToString("C", CultureInfo.CurrentCulture);
            string formattedCostPerWeight = gearSystem.GetCostInCurrencyPerWeight(Activity.BackpackPlannerState.Settings).ToString("C", CultureInfo.CurrentCulture);
            _textViewCost.Text = Java.Lang.String.Format(Activity.Resources.GetString(Resource.String.label_view_gear_system_cost),
                formattedCost, formattedCostPerWeight, Activity.BackpackPlannerState.Settings.Units.GetSmallWeightString(false)
            );
        }
    }

    public sealed class GearSystemEntryViewHolder<T> : BaseModelEntryViewHolder<GearSystemEntry<T>, T, GearSystem>
        where T: BaseModel<T>, new()
    {
        private readonly TextView _textViewName;
        private readonly TextView _textViewTotalWeight;
        private readonly Android.Support.Design.Widget.TextInputLayout _editTextQuantity;

        public GearSystemEntryViewHolder(View view, BaseActivity activity)
            : base(activity)
        {
            _textViewName = view.FindViewById<TextView>(Resource.Id.view_gear_system_name);
            _textViewTotalWeight = view.FindViewById<TextView>(Resource.Id.view_gear_system_total_weight);

            _editTextQuantity = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.view_gear_system_quantity);
            _editTextQuantity.EditText.AfterTextChanged += (sender, args) =>
            {
                UpdateTotalWeight(Item);
                NotifyPropertyChanged("Quantity");
            };
        }

        public override void UpdateView(GearSystemEntry<T> item)
        {
            base.UpdateView(item);

            _textViewName.Text = item.Model.Name;
            _editTextQuantity.EditText.Text = item.Count.ToString();

            UpdateTotalWeight(item);
        }

        private void UpdateTotalWeight(GearSystemEntry<T> item)
        {
            item.Count = string.IsNullOrWhiteSpace(_editTextQuantity.EditText.Text)
                ? 0
                : Convert.ToInt32(_editTextQuantity.EditText.Text);

            int totalWeightInUnits = (int)item.GetTotalWeightInUnits(Activity.BackpackPlannerState.Settings);
            _textViewTotalWeight.Text = Java.Lang.String.Format(Activity.Resources.GetString(Resource.String.label_view_gear_item_total_weight),
                totalWeightInUnits, Activity.BackpackPlannerState.Settings.Units.GetSmallWeightString(totalWeightInUnits != 1)
            );
        }
    }
}