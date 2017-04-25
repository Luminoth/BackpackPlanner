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
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Collections;
using EnergonSoftware.BackpackPlanner.Droid.Activities;
using EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear.Items;
using EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear.Systems;
using EnergonSoftware.BackpackPlanner.Droid.DAL.Gear;
using EnergonSoftware.BackpackPlanner.Droid.Util;
using EnergonSoftware.BackpackPlanner.Droid.Views;
using EnergonSoftware.BackpackPlanner.Droid.Views.Gear;

using Microsoft.EntityFrameworkCore;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Collections
{
    public sealed class AddGearCollectionFragment : AddItemFragment<GearCollection>
    {
        protected override int LayoutResource => Resource.Layout.fragment_add_gear_collection;

        protected override int CleanTitleResource => Resource.String.title_add_gear_collection;

        protected override int DirtyTitleResource => Resource.String.title_add_gear_collection_dirty;

        private GearCollectionGearSystemEntries _gearSystemEntries;
        private GearCollectionGearSystemEntries.GearCollectionGearSystemEntryViewHolder _gearSystemEntryViewHolder;

        private GearCollectionGearItemEntries _gearItemEntries;
        private GearCollectionGearItemEntries.GearCollectionGearItemEntryViewHolder _gearItemEntryViewHolder;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _gearSystemEntries = new GearCollectionGearSystemEntries(Item);
            _gearItemEntries = new GearCollectionGearItemEntries(Item);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _gearSystemEntries.ItemListAdapter = new GearSystemEntryListAdapter<GearCollection>(BaseActivity);
            _gearSystemEntries.ItemListAdapter.PropertyChanged += (sender, args) =>
            {
                IsDirty = true;
            };

            _gearSystemEntryViewHolder = new GearCollectionGearSystemEntries.GearCollectionGearSystemEntryViewHolder(BaseActivity, view);
            _gearSystemEntryViewHolder.AddItemEvent += (sender, args) =>
            {
                AddItemEntry(Resource.String.label_add_gear_systems, _gearSystemEntries, _gearSystemEntryViewHolder);
            };

            _gearItemEntries.ItemListAdapter = new GearItemEntryListAdapter<GearCollection>(BaseActivity);
            _gearItemEntries.ItemListAdapter.PropertyChanged += (sender, args) =>
            {
                IsDirty = true;
            };

            _gearItemEntryViewHolder = new GearCollectionGearItemEntries.GearCollectionGearItemEntryViewHolder(BaseActivity, view);
            _gearItemEntryViewHolder.AddItemEvent += (sender, args) =>
            {
                AddItemEntry(Resource.String.label_add_gear_items, _gearItemEntries, _gearItemEntryViewHolder);
            };
        }

        public override void OnResume()
        {
            base.OnResume();

            ProgressDialog progressDialog = DialogUtil.ShowProgressDialog(Activity, Resource.String.label_loading_items, false, true);

            Task.Run(async () =>
                {
                    using(DatabaseContext dbContext = BaseActivity.BackpackPlannerState.DatabaseState.CreateContext()) {
                        _gearSystemEntries.Items = await dbContext.GearSystems.ToListAsync().ConfigureAwait(false);
                        _gearItemEntries.Items = await dbContext.GearItems.ToListAsync().ConfigureAwait(false);
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

            SetItemEntryList(_gearSystemEntries, _gearSystemEntryViewHolder);
            SetItemEntryList(_gearItemEntries, _gearItemEntryViewHolder);
        }

        protected override GearCollection CreateItem()
        {
            return new GearCollection();
        }

        protected override BaseModelViewHolder<GearCollection> CreateViewHolder(BaseActivity activity, View view)
        {
            return new GearCollectionViewHolder(activity, view);
        }

        protected override async Task AddItemAsync(DatabaseContext dbContext)
        {
            await dbContext.GearCollections.AddAsync(Item).ConfigureAwait(false);
        }

        protected override bool Validate()
        {
            if(!base.Validate()) {
                return false;
            }

            if(!_gearSystemEntryViewHolder.Validate()) {
                return false;
            }

            if(!_gearItemEntryViewHolder.Validate()) {
                return false;
            }

            return true;
        }

        protected override void DoDataExchange(DatabaseContext dbContext)
        {
            base.DoDataExchange(dbContext);

            _gearSystemEntryViewHolder.DoDataExchange(Item, _gearSystemEntries, dbContext);
            _gearItemEntryViewHolder.DoDataExchange(Item, _gearItemEntries, dbContext);
        }
    }
}
