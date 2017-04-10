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

using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Collections;
using EnergonSoftware.BackpackPlanner.Droid.Fragments;
using EnergonSoftware.BackpackPlanner.Units.Units;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear.Collections
{
    public sealed class GearCollectionEntryListAdapter : BaseModelEntryListViewAdapter<GearCollectionEntry, GearCollection>
    {
        private sealed class GearCollectionEntryViewHolder : ViewHolder
        {
            private readonly TextView _textViewName;
            private readonly TextView _textViewTotalWeight;
            private readonly Android.Support.Design.Widget.TextInputLayout _editTextQuantity;

            public GearCollectionEntryViewHolder(View itemView, GearCollectionEntryListAdapter adapter)
                : base(itemView, adapter)
            {
                _textViewName = itemView.FindViewById<TextView>(Resource.Id.view_gear_collection_name);
                _textViewTotalWeight = itemView.FindViewById<TextView>(Resource.Id.view_gear_collection_total_weight);

                _editTextQuantity = itemView.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.view_gear_collection_quantity);
                _editTextQuantity.EditText.AfterTextChanged += (sender, args) =>
                {
                    UpdateTotalWeight();
                };
            }

            protected override void UpdateView()
            {
                base.UpdateView();

                _textViewName.Text = ListItem.Model.Name;
                _editTextQuantity.EditText.Text = ListItem.Count.ToString();

                UpdateTotalWeight();
            }

            private void UpdateTotalWeight()
            {
                ListItem.Count = string.IsNullOrWhiteSpace(_editTextQuantity.EditText.Text)
                    ? 0
                    : Convert.ToInt32(_editTextQuantity.EditText.Text);

                int totalWeightInUnits = (int)ListItem.GetTotalWeightInUnits(Adapter.Fragment.BaseActivity.BackpackPlannerState.Settings);
                _textViewTotalWeight.Text = Java.Lang.String.Format(Adapter.Fragment.BaseActivity.Resources.GetString(Resource.String.label_view_gear_item_total_weight),
                    totalWeightInUnits, Adapter.Fragment.BaseActivity.BackpackPlannerState.Settings.Units.GetSmallWeightString(totalWeightInUnits != 1)
                );
            }
        }

        public override int LayoutResource => Resource.Layout.view_gear_collection_entry;

        public GearCollectionEntryListAdapter(BaseFragment fragment)
            : base(fragment)
        {
        }

        public GearCollectionEntryListAdapter(BaseFragment fragment, GearCollectionEntry[] items)
            : base(fragment, items)
        {
        }

        protected override ViewHolder CreateViewHolder(View itemView)
        {
            return new GearCollectionEntryViewHolder(itemView, this);
        }
    }
}
