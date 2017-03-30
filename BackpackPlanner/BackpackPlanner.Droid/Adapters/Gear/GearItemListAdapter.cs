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
using System.Globalization;
using System.Linq;

using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;

using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.Droid.Fragments;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Items;
using EnergonSoftware.BackpackPlanner.Units;
using EnergonSoftware.BackpackPlanner.Units.Units;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear
{
    public sealed class GearItemListAdapter : BaseModelRecyclerListAdapter<GearItem>
    {
        private sealed class GearItemViewHolder : BaseModelViewHolder
        {
            protected override int DeleteActionResourceId => Resource.Id.action_delete_gear_item;

            private readonly Android.Support.V7.Widget.Toolbar _toolbar;

            private readonly TextView _textViewMakeModel;
            private readonly TextView _textViewWeightCategory;
            private readonly TextView _textViewWeight;
            private readonly TextView _textViewCost;

            public GearItemViewHolder(View itemView, BaseModelRecyclerListAdapter<GearItem> adapter)
                : base(itemView, adapter)
            {
                _toolbar = itemView.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.view_gear_item_toolbar);
                _toolbar.InflateMenu(Resource.Menu.gear_item_menu);
                _toolbar.SetOnMenuItemClickListener(this);

                _textViewMakeModel = itemView.FindViewById<TextView>(Resource.Id.view_gear_item_make_model);
                _textViewWeightCategory = itemView.FindViewById<TextView>(Resource.Id.view_gear_item_weight_category);
                _textViewWeight = itemView.FindViewById<TextView>(Resource.Id.view_gear_item_weight);
                _textViewCost = itemView.FindViewById<TextView>(Resource.Id.view_gear_item_cost);
            }

            protected override Android.Support.V4.App.Fragment CreateViewItemFragment()
            {
                return new ViewGearItemFragment
                {
                    Item = ListItem
                };
            }

            protected override void UpdateView()
            {
                _toolbar.Title = ListItem.Name;

                string makeModel = Java.Lang.String.Format(Adapter.Fragment.BaseActivity.Resources.GetString(Resource.String.label_view_gear_item_make_model),
                    ListItem.Make, ListItem.Model
                );

                if(string.IsNullOrWhiteSpace(makeModel)) {
                    _textViewMakeModel.Visibility = ViewStates.Gone;
                    _textViewMakeModel.Text = string.Empty;
                } else {
                    _textViewMakeModel.Visibility = ViewStates.Visible;
                    _textViewMakeModel.Text = makeModel;
                }

                WeightCategory weightCategory = ListItem.GetWeightCategory(Adapter.Fragment.BaseActivity.BackpackPlannerState.Settings);
                _textViewWeightCategory.Text = weightCategory.ShortName();

                // TODO: is there any way to turn this into a core-extension?
                // we would somehow have to convert to the appropriate android color
                Color categoryStickerColor = Color.Gray;
                switch(weightCategory)
                {
                case WeightCategory.None:
                    categoryStickerColor = Color.Gray;
                    break;
                case WeightCategory.Ultralight:
                    categoryStickerColor = Color.White;
                    break;
                case WeightCategory.Light:
                    categoryStickerColor = Color.Aqua;
                    break;
                case WeightCategory.Medium:
                    categoryStickerColor = Color.Green;
                    break;
                case WeightCategory.Heavy:
                    categoryStickerColor = Color.Yellow;
                    break;
                case WeightCategory.ExtraHeavy:
                    categoryStickerColor = Color.Red;
                    break;
                }

                GradientDrawable categoryStickerDrawable = (GradientDrawable)_textViewWeightCategory.Background;
                categoryStickerDrawable.SetColor(categoryStickerColor);
                categoryStickerDrawable.SetStroke(5, Color.Black);

                int weightInUnits = (int)ListItem.GetWeightInUnits(Adapter.Fragment.BaseActivity.BackpackPlannerState.Settings);
                _textViewWeight.Text = Java.Lang.String.Format(Adapter.Fragment.BaseActivity.Resources.GetString(Resource.String.label_view_gear_item_weight),
                    weightInUnits, Adapter.Fragment.BaseActivity.BackpackPlannerState.Settings.Units.GetSmallWeightString(weightInUnits != 1)
                );

                string formattedCost = ListItem.GetCostInCurrency(Adapter.Fragment.BaseActivity.BackpackPlannerState.Settings).ToString("C", CultureInfo.CurrentCulture);
                string formattedCostPerWeight = ListItem.GetCostInCurrencyPerWeightInUnits(Adapter.Fragment.BaseActivity.BackpackPlannerState.Settings).ToString("C", CultureInfo.CurrentCulture);
                _textViewCost.Text = Java.Lang.String.Format(Adapter.Fragment.BaseActivity.Resources.GetString(Resource.String.label_view_gear_item_cost),
                    formattedCost, formattedCostPerWeight, Adapter.Fragment.BaseActivity.BackpackPlannerState.Settings.Units.GetSmallWeightString(false)
                );
            }
        }

        public override int LayoutResource => Resource.Layout.view_gear_item;

        public GearItemListAdapter(ListItemsFragment<GearItem> fragment)
            : base(fragment)
        {
        }

        protected override void SortItemsByPosition(int position)
        {
            switch(position)
            {
            case 0:         // Name
                FilteredListItems = FilteredListItems.OrderBy(x => x.Name, StringComparer.CurrentCulture);
                break;
            case 1:         // Weight
                FilteredListItems = FilteredListItems.OrderBy(x => x.WeightInGrams);
                break;
            case 2:         // Cost
                FilteredListItems = FilteredListItems.OrderBy(x => x.CostInUSDP);
                break;
            case 3:         // Cost / Weight
                // TODO
                break;
            }
        }

        protected override BaseViewHolder CreateViewHolder(View itemView)
        {
            return new GearItemViewHolder(itemView, this);
        }
    }
}
