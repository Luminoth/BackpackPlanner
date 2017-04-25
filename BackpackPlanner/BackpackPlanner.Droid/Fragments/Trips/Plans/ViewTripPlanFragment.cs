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

using System.Threading.Tasks;

using Android.App;
using Android.OS;
using Android.Views;

using EnergonSoftware.BackpackPlanner.DAL;
using EnergonSoftware.BackpackPlanner.DAL.Models.Trips.Plans;
using EnergonSoftware.BackpackPlanner.Droid.Activities;
using EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear.Collections;
using EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear.Items;
using EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear.Systems;
using EnergonSoftware.BackpackPlanner.Droid.Adapters.Meals;
using EnergonSoftware.BackpackPlanner.Droid.DAL.Gear;
using EnergonSoftware.BackpackPlanner.Droid.DAL.Meals;
using EnergonSoftware.BackpackPlanner.Droid.Util;
using EnergonSoftware.BackpackPlanner.Droid.Views;
using EnergonSoftware.BackpackPlanner.Droid.Views.Trips;

using Microsoft.EntityFrameworkCore;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Trips.Plans
{
    public sealed class ViewTripPlanFragment : ViewItemFragment<TripPlan>
    {
        protected override int LayoutResource => Resource.Layout.fragment_view_trip_plan;

        protected override int CleanTitleResource => Resource.String.title_view_trip_plan;

        protected override int DirtyTitleResource => Resource.String.title_view_trip_plan_dirty;

        private TripPlanGearCollectionEntries _gearCollectionEntries;
        private TripPlanGearCollectionEntries.TripPlanGearCollectionEntryViewHolder _gearCollectionEntryViewHolder;

        private TripPlanGearSystemEntries _gearSystemEntries;
        private TripPlanGearSystemEntries.TripPlanGearSystemEntryViewHolder _gearSystemEntryViewHolder;

        private TripPlanGearItemEntries _gearItemEntries;
        private TripPlanGearItemEntries.TripPlanGearItemEntryViewHolder _gearItemEntryViewHolder;

        private TripPlanMealEntries _mealEntries;
        private TripPlanMealEntries.TripPlanMealEntryViewHolder _mealEntryViewHolder;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _gearCollectionEntries = new TripPlanGearCollectionEntries(Item);
            _gearSystemEntries = new TripPlanGearSystemEntries(Item);
            _gearItemEntries = new TripPlanGearItemEntries(Item);
            _mealEntries = new TripPlanMealEntries(Item);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _gearCollectionEntries.ItemListAdapter = new GearCollectionEntryListAdapter<TripPlan>(BaseActivity);
            _gearCollectionEntries.ItemListAdapter.PropertyChanged += (sender, args) =>
            {
                IsDirty = true;
            };

            _gearCollectionEntryViewHolder = new TripPlanGearCollectionEntries.TripPlanGearCollectionEntryViewHolder(BaseActivity, view);
            _gearCollectionEntryViewHolder.AddItemEvent += (sender, args) =>
            {
                AddItemEntry(Resource.String.label_add_gear_collections, _gearCollectionEntries, _gearCollectionEntryViewHolder);
            };

            _gearSystemEntries.ItemListAdapter = new GearSystemEntryListAdapter<TripPlan>(BaseActivity);
            _gearSystemEntries.ItemListAdapter.PropertyChanged += (sender, args) =>
            {
                IsDirty = true;
            };

            _gearSystemEntryViewHolder = new TripPlanGearSystemEntries.TripPlanGearSystemEntryViewHolder(BaseActivity, view);
            _gearSystemEntryViewHolder.AddItemEvent += (sender, args) =>
            {
                AddItemEntry(Resource.String.label_add_gear_systems, _gearSystemEntries, _gearSystemEntryViewHolder);
            };

            _gearItemEntries.ItemListAdapter = new GearItemEntryListAdapter<TripPlan>(BaseActivity);
            _gearItemEntries.ItemListAdapter.PropertyChanged += (sender, args) =>
            {
                IsDirty = true;
            };

            _gearItemEntryViewHolder = new TripPlanGearItemEntries.TripPlanGearItemEntryViewHolder(BaseActivity, view);
            _gearItemEntryViewHolder.AddItemEvent += (sender, args) =>
            {
                AddItemEntry(Resource.String.label_add_gear_items, _gearItemEntries, _gearItemEntryViewHolder);
            };

            _mealEntries.ItemListAdapter = new MealEntryListAdapter<TripPlan>(BaseActivity);
            _mealEntries.ItemListAdapter.PropertyChanged += (sender, args) =>
            {
                IsDirty = true;
            };

            _mealEntryViewHolder = new TripPlanMealEntries.TripPlanMealEntryViewHolder(BaseActivity, view);
            _mealEntryViewHolder.AddItemEvent += (sender, args) =>
            {
                AddItemEntry(Resource.String.label_add_meals, _mealEntries, _mealEntryViewHolder);
            };
        }

        public override void OnResume()
        {
            base.OnResume();

            ProgressDialog progressDialog = DialogUtil.ShowProgressDialog(Activity, Resource.String.label_loading_items, false, true);

            Task.Run(async () =>
                {
                    using(DatabaseContext dbContext = BaseActivity.BackpackPlannerState.DatabaseState.CreateContext()) {
                        _gearCollectionEntries.Items = await dbContext.GearCollections.ToListAsync().ConfigureAwait(false);
                        _gearSystemEntries.Items = await dbContext.GearSystems.ToListAsync().ConfigureAwait(false);
                        _gearItemEntries.Items = await dbContext.GearItems.ToListAsync().ConfigureAwait(false);
                        _mealEntries.Items = await dbContext.Meals.ToListAsync().ConfigureAwait(false);
                    }

                    Activity.RunOnUiThread(() =>
                    {
                        progressDialog.Dismiss();

                        UpdateView();
                    });
                }
            );
        }

        protected override void UpdateView()
        {
            base.UpdateView();

            SetItemEntryList(_gearCollectionEntries, _gearCollectionEntryViewHolder);
            SetItemEntryList(_gearSystemEntries, _gearSystemEntryViewHolder);
            SetItemEntryList(_gearItemEntries, _gearItemEntryViewHolder);
            SetItemEntryList(_mealEntries, _mealEntryViewHolder);
        }

        protected override BaseModelViewHolder<TripPlan> CreateViewHolder(BaseActivity activity, View view)
        {
            return new TripPlanViewHolder(activity, view);
        }

        protected override bool Validate()
        {
            if(!base.Validate()) {
                return false;
            }

            if(!_gearCollectionEntryViewHolder.Validate()) {
                return false;
            }

            if(!_gearSystemEntryViewHolder.Validate()) {
                return false;
            }

            if(!_gearItemEntryViewHolder.Validate()) {
                return false;
            }

            if(!_mealEntryViewHolder.Validate()) {
                return false;
            }

            return true;
        }

        protected override void DoDataExchange(DatabaseContext dbContext)
        {
            base.DoDataExchange(dbContext);

            _gearCollectionEntryViewHolder.DoDataExchange(Item, _gearCollectionEntries, dbContext);
            _gearSystemEntryViewHolder.DoDataExchange(Item, _gearSystemEntries, dbContext);
            _gearItemEntryViewHolder.DoDataExchange(Item, _gearItemEntries, dbContext);
            _mealEntryViewHolder.DoDataExchange(Item, _mealEntries, dbContext);
        }
    }
}
