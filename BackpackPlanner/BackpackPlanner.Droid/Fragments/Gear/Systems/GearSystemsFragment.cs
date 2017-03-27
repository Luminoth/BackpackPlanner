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
using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner.DAL;
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Systems;
using EnergonSoftware.BackpackPlanner.Droid.Adapters;
using EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear;

using Microsoft.EntityFrameworkCore;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Systems
{
    public sealed class GearSystemsFragment : ListItemsFragment<GearSystem>
    {
        protected override int LayoutResource => Resource.Layout.fragment_gear_systems;

        protected override int TitleResource => Resource.String.title_gear_systems;

        protected override int ListLayoutResource => Resource.Id.gear_systems_layout;

        protected override int WhatIsAnItemButtonResource => Resource.Id.button_what_gear_system;

        protected override int WhatIsAnItemTitleResource => Resource.String.title_what_gear_system;

        protected override int WhatIsAnItemTextResource => Resource.String.what_is_a_gear_system;

        protected override int DeleteItemConfirmationTextResource => Resource.String.confirm_delete_gear_system;

        protected override int DeleteItemConfirmationTitleResource => Resource.String.title_delete_confirmation_gear_system;

        protected override int NoItemsResource => Resource.Id.no_gear_systems;

        protected override int SortItemsResource => Resource.Id.gear_systems_sort;

        protected override int AddItemResource => Resource.Id.fab_add_gear_system;

        protected override async Task<List<GearSystem>> GetItemsAsync(DatabaseContext dbContext)
        {
            return await dbContext.GearSystems.ToListAsync().ConfigureAwait(false);
        }

        protected override Android.Support.V4.App.Fragment CreateAddItemFragment()
        {
            return new AddGearSystemFragment();
        }

        protected override BaseListAdapter<GearSystem> CreateAdapter()
        {
            return new GearSystemListAdapter(this);
        }
    }
}
