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
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Collections;
using EnergonSoftware.BackpackPlanner.Droid.Activities;
using EnergonSoftware.BackpackPlanner.Droid.Adapters;
using EnergonSoftware.BackpackPlanner.Droid.Fragments;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Collections;
using EnergonSoftware.BackpackPlanner.Units.Units;

namespace EnergonSoftware.BackpackPlanner.Droid.Views.Gear
{
    public sealed class GearCollectionViewHolder : BaseModelViewHolder<GearCollection>
    {
        private readonly Android.Support.Design.Widget.TextInputLayout _gearCollectionNameEditText;
        private readonly Android.Support.Design.Widget.TextInputLayout _gearCollectionNoteEditText;

        public GearCollectionViewHolder(BaseActivity activity, View view)
            : base(activity)
        {
            _gearCollectionNameEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.gear_collection_name);
            _gearCollectionNameEditText.EditText.AfterTextChanged += (sender, args) =>
            {
                NotifyPropertyChanged("Name");
            };

            _gearCollectionNoteEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.gear_collection_note);
            _gearCollectionNoteEditText.EditText.AfterTextChanged += (sender, args) =>
            {
                NotifyPropertyChanged("Note");
            };
        }

        public override void UpdateView(GearCollection gearCollection)
        {
            base.UpdateView(gearCollection);

            _gearCollectionNameEditText.EditText.Text = gearCollection.Name;
            _gearCollectionNoteEditText.EditText.Text = gearCollection.Note;
        }

        public override bool Validate()
        {
            bool valid = base.Validate();

            if(string.IsNullOrWhiteSpace(_gearCollectionNameEditText.EditText.Text)) {
                _gearCollectionNameEditText.EditText.Error = "A name is required!";
                valid = false;                
            }

            return valid;
        }

        public override void DoDataExchange(GearCollection gearCollection, DatabaseContext dbContext)
        {
            gearCollection.Name = _gearCollectionNameEditText.EditText.Text;
            gearCollection.Note = _gearCollectionNoteEditText.EditText.Text;

            base.DoDataExchange(gearCollection, dbContext);
        }
    }

    public sealed class GearCollectionListViewHolder : BaseModelRecyclerViewHolder<GearCollection>
    {
        protected override int ToolbarResourceId => Resource.Id.view_gear_collection_toolbar;

        protected override int MenuResourceId => Resource.Menu.gear_collection_menu;

        protected override int DeleteActionResourceId => Resource.Id.action_delete_gear_collection;

        private readonly TextView _textViewSystems;
        private readonly TextView _textViewItems;
        private readonly TextView _textViewWeight;
        private readonly TextView _textViewCost;

        public GearCollectionListViewHolder(View view, BaseRecyclerListAdapter<GearCollection> adapter)
            : base(view, adapter)
        {
            _textViewSystems = view.FindViewById<TextView>(Resource.Id.view_gear_collection_systems);
            _textViewItems = view.FindViewById<TextView>(Resource.Id.view_gear_collection_items);
            _textViewWeight = view.FindViewById<TextView>(Resource.Id.view_gear_collection_weight);
            _textViewCost = view.FindViewById<TextView>(Resource.Id.view_gear_collection_cost);
        }

        protected override ViewItemFragment<GearCollection> CreateViewItemFragment()
        {
            return new ViewGearCollectionFragment();
        }

        public override void UpdateView(GearCollection gearCollection)
        {
            base.UpdateView(gearCollection);

            _textViewSystems.Text = Java.Lang.String.Format(Activity.Resources.GetString(Resource.String.label_view_gear_collection_systems),
                gearCollection.GearSystems.Count
            );

            _textViewItems.Text = Java.Lang.String.Format(Activity.Resources.GetString(Resource.String.label_view_gear_collection_items),
                gearCollection.GearItems.Count, gearCollection.GetTotalGearItemCount()
            );

            int weightInUnits = (int)gearCollection.GetTotalWeightInUnits(Activity.BackpackPlannerState.Settings);
            _textViewWeight.Text = Java.Lang.String.Format(Activity.Resources.GetString(Resource.String.label_view_gear_collection_weight),
                weightInUnits, Activity.BackpackPlannerState.Settings.Units.GetSmallWeightString(weightInUnits != 1)
            );

            string formattedCost = gearCollection.GetTotalCostInCurrency(Activity.BackpackPlannerState.Settings).ToString("C", CultureInfo.CurrentCulture);
            string formattedCostPerWeight = gearCollection.GetCostInCurrencyPerWeightInUnits(Activity.BackpackPlannerState.Settings).ToString("C", CultureInfo.CurrentCulture);
            _textViewCost.Text = Java.Lang.String.Format(Activity.Resources.GetString(Resource.String.label_view_gear_collection_cost),
                formattedCost, formattedCostPerWeight, Activity.BackpackPlannerState.Settings.Units.GetSmallWeightString(false)
            );
        }
    }

    public sealed class GearCollectionEntryViewHolder<T> : BaseModelEntryViewHolder<GearCollectionEntry<T>, T, GearCollection>
        where T: BaseModel<T>, new()
    {
        private readonly TextView _textViewName;
        private readonly TextView _textViewTotalWeight;
        private readonly Android.Support.Design.Widget.TextInputLayout _editTextQuantity;

        public GearCollectionEntryViewHolder(View view, BaseActivity activity)
            : base(activity)
        {
            _textViewName = view.FindViewById<TextView>(Resource.Id.view_gear_collection_name);
            _textViewTotalWeight = view.FindViewById<TextView>(Resource.Id.view_gear_collection_total_weight);

            _editTextQuantity = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.view_gear_collection_quantity);
            _editTextQuantity.EditText.AfterTextChanged += (sender, args) =>
            {
                UpdateTotalWeight(Item);
                NotifyPropertyChanged("Quantity");
            };
        }

        public override void UpdateView(GearCollectionEntry<T> item)
        {
            base.UpdateView(item);

            _textViewName.Text = item.Model.Name;
            _editTextQuantity.EditText.Text = item.Count.ToString();

            UpdateTotalWeight(item);
        }

        private void UpdateTotalWeight(GearCollectionEntry<T> item)
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