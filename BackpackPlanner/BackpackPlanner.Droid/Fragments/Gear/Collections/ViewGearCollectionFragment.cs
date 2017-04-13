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
    public sealed class ViewGearCollectionFragment : ViewItemFragment<GearCollection>
    {
        protected override int LayoutResource => Resource.Layout.fragment_view_gear_collection;

        protected override int TitleResource => Resource.String.title_view_gear_collection;

        private GearCollectionGearSystemEntries _gearSystemEntries;
        private GearCollectionGearSystemEntries.GearCollectionGearSystemEntryViewHolder _gearSystemEntryViewHolder;

        private GearCollectionGearItemEntries _gearItemEntries;
        private GearCollectionGearItemEntries.GearCollectionGearItemEntryViewHolder _gearItemEntryViewHolder;

        public ViewGearCollectionFragment(GearCollection gearCollection)
            : base(gearCollection)
        {
        }

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
            _gearSystemEntryViewHolder = new GearCollectionGearSystemEntries.GearCollectionGearSystemEntryViewHolder(BaseActivity, view);

            _gearItemEntries.ItemListAdapter = new GearItemEntryListAdapter<GearCollection>(BaseActivity);
            _gearItemEntryViewHolder = new GearCollectionGearItemEntries.GearCollectionGearItemEntryViewHolder(BaseActivity, view);
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
                        _gearSystemEntryViewHolder.SetItemEntryList(_gearSystemEntries);
                        _gearItemEntryViewHolder.SetItemEntryList(_gearItemEntries);

                        progressDialog.Dismiss();

                        UpdateView();
                    });
                }
            );
        }

        protected override void UpdateView()
        {
            base.UpdateView();

            _gearSystemEntryViewHolder.UpdateView(_gearSystemEntries);
            _gearItemEntryViewHolder.UpdateView(_gearItemEntries);
        }

        protected override BaseModelViewHolder<GearCollection> CreateViewHolder(BaseActivity activity, View view)
        {
            return new GearCollectionViewHolder(activity, view);
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
