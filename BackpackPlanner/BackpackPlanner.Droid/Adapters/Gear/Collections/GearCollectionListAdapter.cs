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

using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Android.Views;
using Android.Widget;

using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Collections;
using EnergonSoftware.BackpackPlanner.Droid.Fragments;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Collections;
using EnergonSoftware.BackpackPlanner.Units.Units;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear.Collections
{
    public sealed class GearCollectionListAdapter : BaseModelRecyclerListAdapter<GearCollection>
    {
        private sealed class GearCollectionViewHolder : BaseModelRecyclerViewHolder
        {
            protected override int ToolbarResourceId => Resource.Id.view_gear_collection_toolbar;

            protected override int MenuResourceId => Resource.Menu.gear_collection_menu;

            protected override int DeleteActionResourceId => Resource.Id.action_delete_gear_collection;

            private readonly TextView _textViewSystems;
            private readonly TextView _textViewItems;
            private readonly TextView _textViewWeight;
            private readonly TextView _textViewCost;

            public GearCollectionViewHolder(View view, BaseRecyclerListAdapter<GearCollection> adapter)
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

                _textViewSystems.Text = Java.Lang.String.Format(BaseActivity.Resources.GetString(Resource.String.label_view_gear_collection_systems),
                    gearCollection.GearSystems.Count
                );

                _textViewItems.Text = Java.Lang.String.Format(BaseActivity.Resources.GetString(Resource.String.label_view_gear_collection_items),
                    gearCollection.GearItems.Count, gearCollection.GetTotalGearItemCount()
                );

                int weightInUnits = (int)gearCollection.GetTotalWeightInUnits(BaseActivity.BackpackPlannerState.Settings);
                _textViewWeight.Text = Java.Lang.String.Format(BaseActivity.Resources.GetString(Resource.String.label_view_gear_collection_weight),
                    weightInUnits, BaseActivity.BackpackPlannerState.Settings.Units.GetSmallWeightString(weightInUnits != 1)
                );

                string formattedCost = gearCollection.GetTotalCostInCurrency(BaseActivity.BackpackPlannerState.Settings).ToString("C", CultureInfo.CurrentCulture);
                string formattedCostPerWeight = gearCollection.GetCostInCurrencyPerWeightInUnits(BaseActivity.BackpackPlannerState.Settings).ToString("C", CultureInfo.CurrentCulture);
                _textViewCost.Text = Java.Lang.String.Format(BaseActivity.Resources.GetString(Resource.String.label_view_gear_collection_cost),
                    formattedCost, formattedCostPerWeight, BaseActivity.BackpackPlannerState.Settings.Units.GetSmallWeightString(false)
                );
            }
        }

        protected override int LayoutResource => Resource.Layout.view_gear_collection;

        public GearCollectionListAdapter(ListItemsFragment<GearCollection> fragment)
            : base(fragment)
        {
        }

        protected override IEnumerable<GearCollection> SortItemsByPosition(int position, IEnumerable<GearCollection> items)
        {
            switch(position)
            {
            case 0:         // Name
                return from x in items orderby x?.Name select x;
            case 1:         // Items
                // TODO
                return items;
            case 2:         // Weight
                // TODO
                return items;
            case 3:         // Cost
                // TODO
                return items;
            case 4:         // Cost / Weight
                // TODO
                return items;
            }
            return items;
        }

        protected override BaseRecyclerViewHolder CreateViewHolder(View view, BaseRecyclerListAdapter<GearCollection> adapter)
        {
            return new GearCollectionViewHolder(view, adapter);
        }
    }
}
