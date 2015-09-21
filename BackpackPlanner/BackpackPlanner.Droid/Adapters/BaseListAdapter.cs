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
using System.Linq;

using Android.Views;
using Android.Widget;

using EnergonSoftware.BackpackPlanner.Droid.Fragments;

// sorting: http://stackoverflow.com/questions/29795299/what-is-the-sortedlistt-working-with-recyclerview-adapter
// NOTE: can't get the android SortedList working, so for now doing it the linq way

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters
{
    public abstract class BaseListAdapter<T> : Android.Support.V7.Widget.RecyclerView.Adapter
    {
        protected abstract class BaseViewHolder : Android.Support.V7.Widget.RecyclerView.ViewHolder
        {
            private T _listItem;

            public T ListItem
            {
                get { return _listItem; }
                set { _listItem = value; UpdateView(); }
            }

            protected abstract Android.Support.V4.App.Fragment CreateViewItemFragment();

            protected abstract void UpdateView();

            protected BaseViewHolder(View itemView, BaseFragment fragment) : base(itemView)
            {
                itemView.Click += (sender, args) => {
                    fragment.TransitionToFragment(Resource.Id.frame_content, CreateViewItemFragment(), null);
                };
            }
        }

        public ListItemsFragment<T> Fragment { get; }

        public abstract int LayoutResource { get; }

        public override int ItemCount => ListItems?.Count ?? 0;

        protected ICollection<T> ListItems { get; }

        public abstract void SortByItemSelectedEventHander(object sender, AdapterView.ItemSelectedEventArgs args);

        protected abstract BaseViewHolder CreateViewHolder(View itemView);

        public override Android.Support.V7.Widget.RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(LayoutResource, parent, false);
            return CreateViewHolder(itemView);
        }

        public override void OnBindViewHolder(Android.Support.V7.Widget.RecyclerView.ViewHolder holder, int position)
        {
            BaseViewHolder baseViewHolder = (BaseViewHolder)holder;
            T gearItem = ListItems.ElementAt(position);
            baseViewHolder.ListItem = gearItem;
        }

        protected BaseListAdapter(ListItemsFragment<T> fragment, IEnumerable<T> listItems)
        {
            Fragment = fragment;

            ListItems = listItems.ToList();
        }
    }
}