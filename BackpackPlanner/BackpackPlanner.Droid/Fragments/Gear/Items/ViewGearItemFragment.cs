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

using Android.Views;

using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.Droid.Activities;
using EnergonSoftware.BackpackPlanner.Droid.Views;
using EnergonSoftware.BackpackPlanner.Droid.Views.Gear;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Items
{
    public sealed class ViewGearItemFragment : ViewItemFragment<GearItem>
    {
        protected override int LayoutResource => Resource.Layout.fragment_view_gear_item;

        protected override int CleanTitleResource => Resource.String.title_view_gear_item;

        protected override int DirtyTitleResource => Resource.String.title_view_gear_item_dirty;

        public ViewGearItemFragment(GearItem gearItem)
            : base(gearItem)
        {
        }

        protected override BaseModelViewHolder<GearItem> CreateViewHolder(BaseActivity activity, View view)
        {
            return new GearItemViewHolder(activity, view);
        }
    }
}
