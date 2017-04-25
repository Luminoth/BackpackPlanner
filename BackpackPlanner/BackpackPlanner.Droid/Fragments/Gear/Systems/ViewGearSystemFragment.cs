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
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Systems;
using EnergonSoftware.BackpackPlanner.Droid.Activities;
using EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear.Items;
using EnergonSoftware.BackpackPlanner.Droid.DAL.Gear;
using EnergonSoftware.BackpackPlanner.Droid.Util;
using EnergonSoftware.BackpackPlanner.Droid.Views;
using EnergonSoftware.BackpackPlanner.Droid.Views.Gear;

using Microsoft.EntityFrameworkCore;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Systems
{
    public sealed class ViewGearSystemFragment : ViewItemFragment<GearSystem>
    {
        protected override int LayoutResource => Resource.Layout.fragment_view_gear_system;

        protected override int CleanTitleResource => Resource.String.title_view_gear_system;

        protected override int DirtyTitleResource => Resource.String.title_view_gear_system_dirty;

        private GearSystemGearItemEntries _gearItemEntries;
        private GearSystemGearItemEntries.GearSystemGearItemEntryViewHolder _gearItemEntryViewHolder;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _gearItemEntries = new GearSystemGearItemEntries(Item);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _gearItemEntries.ItemListAdapter = new GearItemEntryListAdapter<GearSystem>(BaseActivity);
            _gearItemEntryViewHolder = new GearSystemGearItemEntries.GearSystemGearItemEntryViewHolder(BaseActivity, view);
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

            SetItemEntryList(_gearItemEntries, _gearItemEntryViewHolder);
        }

        protected override BaseModelViewHolder<GearSystem> CreateViewHolder(BaseActivity activity, View view)
        {
            return new GearSystemViewHolder(activity, view);
        }

        protected override bool Validate()
        {
            if(!base.Validate()) {
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

            _gearItemEntryViewHolder.DoDataExchange(Item, _gearItemEntries, dbContext);
        }
    }
}
