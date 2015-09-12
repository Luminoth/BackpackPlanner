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

using Android.OS;
using Android.Views;

using EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear.Systems;
using EnergonSoftware.BackpackPlanner.Models.Gear.Systems;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Systems
{
    public class GearSystemsFragment : ListItemsFragment<GearSystem>
    {
        protected override int LayoutResource => Resource.Layout.fragment_gear_systems;

        protected override int TitleResource => Resource.String.title_gear_systems;

        protected override int ListLayoutResource => Resource.Id.gear_systems_layout;

        protected override int NoItemsResource => Resource.Id.no_gear_systems;

        protected override int SortItemsResource => Resource.Id.gear_systems_sort;

        private List<GearSystem> _gearSystems = new List<GearSystem>(); 

        protected override int ItemCount => _gearSystems.Count;

        protected override int AddItemResource => Resource.Id.fab_add_gear_system;

        protected override Android.Support.V4.App.Fragment CreateAddItemFragment()
        {
            return new AddGearSystemFragment();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // TODO
            _gearSystems = new List<GearSystem>();
            for(int i=0; i<20; ++i) {
                _gearSystems.Add(new GearSystem());
            }
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            Layout.SetAdapter(
                new GearSystemListAdapter(this)
                {
                    GearSystems = _gearSystems
                }
            );
        }
    }
}
