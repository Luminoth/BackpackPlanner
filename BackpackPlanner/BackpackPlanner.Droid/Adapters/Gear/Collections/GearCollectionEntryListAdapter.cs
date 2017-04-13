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

using EnergonSoftware.BackpackPlanner.DAL.Models;
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Collections;
using EnergonSoftware.BackpackPlanner.Droid.Activities;
using EnergonSoftware.BackpackPlanner.Droid.Views;
using EnergonSoftware.BackpackPlanner.Units.Units;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear.Collections
{
    public sealed class GearCollectionEntryListAdapter<T> : BaseModelEntryListViewAdapter<GearCollectionEntry<T>, T, GearCollection>
        where T: BaseModel<T>, new()
    {
        private sealed class GearCollectionEntryViewHolder : BaseViewHolder<GearCollectionEntry<T>>
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

                int totalWeightInUnits = (int)item.GetTotalWeightInUnits(BaseActivity.BackpackPlannerState.Settings);
                _textViewTotalWeight.Text = Java.Lang.String.Format(BaseActivity.Resources.GetString(Resource.String.label_view_gear_item_total_weight),
                    totalWeightInUnits, BaseActivity.BackpackPlannerState.Settings.Units.GetSmallWeightString(totalWeightInUnits != 1)
                );
            }
        }

        public override int LayoutResource => Resource.Layout.view_gear_collection_entry;

        public GearCollectionEntryListAdapter(BaseActivity activity)
            : base(activity)
        {
        }

        public GearCollectionEntryListAdapter(BaseActivity activity, GearCollectionEntry<T>[] items)
            : base(activity, items)
        {
        }

        protected override BaseViewHolder<GearCollectionEntry<T>> CreateViewHolder(View view)
        {
            return new GearCollectionEntryViewHolder(view, BaseActivity);
        }
    }
}
