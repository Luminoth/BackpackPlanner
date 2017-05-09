/*
   Copyright 2017 Shane Lillie

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

using EnergonSoftware.BackpackPlanner.DAL.Models;
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Collections;
using EnergonSoftware.BackpackPlanner.Droid.Activities;
using EnergonSoftware.BackpackPlanner.Droid.Views;
using EnergonSoftware.BackpackPlanner.Droid.Views.Gear;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear.Collections
{
    public sealed class GearCollectionEntryListAdapter<T> : BaseModelEntryListViewAdapter<GearCollectionEntry<T>, T, GearCollection>
        where T: BaseModel<T>, new()
    {
        public override int LayoutResource => Resource.Layout.view_gear_collection_entry;

        public GearCollectionEntryListAdapter(BaseActivity activity)
            : base(activity)
        {
        }

        public GearCollectionEntryListAdapter(BaseActivity activity, GearCollectionEntry<T>[] items)
            : base(activity, items)
        {
        }

        protected override BaseViewHolder<GearCollectionEntry<T>> CreateViewHolder(View view)
        {
            GearCollectionEntryViewHolder<T> viewHolder = new GearCollectionEntryViewHolder<T>(view, BaseActivity);
            viewHolder.PropertyChanged += (sender, args) =>
            {
                NotifyPropertyChanged(args.PropertyName);
            };
            return viewHolder;
        }
    }
}
