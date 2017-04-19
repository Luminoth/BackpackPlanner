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

using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Systems;
using EnergonSoftware.BackpackPlanner.Droid.Fragments;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Systems;
using EnergonSoftware.BackpackPlanner.Units.Units;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear.Systems
{
    public sealed class GearSystemListAdapter : BaseModelRecyclerListAdapter<GearSystem>
    {
        private sealed class GearSystemViewHolder : BaseModelRecyclerViewHolder
        {
            protected override int ToolbarResourceId => Resource.Id.view_gear_system_toolbar;

            protected override int MenuResourceId => Resource.Menu.gear_system_menu;

            protected override int DeleteActionResourceId => Resource.Id.action_delete_gear_system;

            private readonly TextView _textViewItems;
            private readonly TextView _textViewWeight;
            private readonly TextView _textViewCost;

            public GearSystemViewHolder(View view, BaseRecyclerListAdapter<GearSystem> adapter)
                : base(view, adapter)
            {
                _textViewItems = view.FindViewById<TextView>(Resource.Id.view_gear_system_items);
                _textViewWeight = view.FindViewById<TextView>(Resource.Id.view_gear_system_weight);
                _textViewCost = view.FindViewById<TextView>(Resource.Id.view_gear_system_cost);
            }

            protected override Android.Support.V4.App.Fragment CreateViewItemFragment()
            {
                return new ViewGearSystemFragment(Item);
            }

            public override void UpdateView(GearSystem gearSystem)
            {
                base.UpdateView(gearSystem);

                _textViewItems.Text = Java.Lang.String.Format(BaseActivity.Resources.GetString(Resource.String.label_view_gear_system_items),
                    gearSystem.GearItems.Count
                );

                int weightInUnits = (int)gearSystem.GetTotalWeightInUnits(BaseActivity.BackpackPlannerState.Settings);
                _textViewWeight.Text = Java.Lang.String.Format(BaseActivity.Resources.GetString(Resource.String.label_view_gear_system_weight),
                    weightInUnits, BaseActivity.BackpackPlannerState.Settings.Units.GetSmallWeightString(weightInUnits != 1)
                );

                string formattedCost = gearSystem.GetTotalCostInCurrency(BaseActivity.BackpackPlannerState.Settings).ToString("C", CultureInfo.CurrentCulture);
                string formattedCostPerWeight = gearSystem.GetCostInCurrencyPerWeight(BaseActivity.BackpackPlannerState.Settings).ToString("C", CultureInfo.CurrentCulture);
                _textViewCost.Text = Java.Lang.String.Format(BaseActivity.Resources.GetString(Resource.String.label_view_gear_system_cost),
                    formattedCost, formattedCostPerWeight, BaseActivity.BackpackPlannerState.Settings.Units.GetSmallWeightString(false)
                );
            }
        }

        protected override int LayoutResource => Resource.Layout.view_gear_system;


        public GearSystemListAdapter(ListItemsFragment<GearSystem> fragment)
            : base(fragment)
        {
        }

        protected override IEnumerable<GearSystem> SortItemsByPosition(int position, IEnumerable<GearSystem> items)
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

        protected override BaseRecyclerViewHolder CreateViewHolder(View view, BaseRecyclerListAdapter<GearSystem> adapter)
        {
            return new GearSystemViewHolder(view, adapter);
        }
    }
}
