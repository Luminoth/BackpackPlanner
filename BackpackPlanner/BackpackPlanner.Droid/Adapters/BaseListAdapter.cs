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
using EnergonSoftware.BackpackPlanner.Models;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters
{
    public abstract class BaseListAdapter<T> : Android.Support.V7.Widget.RecyclerView.Adapter where T: DatabaseItem
    {
        protected abstract class BaseViewHolder : Android.Support.V7.Widget.RecyclerView.ViewHolder, Android.Support.V7.Widget.Toolbar.IOnMenuItemClickListener
        {
            protected abstract int DeleteActionResourceId { get; }

            private T _listItem;

            public T ListItem
            {
                get { return _listItem; }

                set
                {
                    _listItem = value;
                    UpdateView();
                }
            }

            protected abstract Android.Support.V4.App.Fragment CreateViewItemFragment();

            protected abstract void UpdateView();

            protected BaseViewHolder(View itemView, BaseFragment fragment) : base(itemView)
            {
                itemView.Click += (sender, args) => {
                    fragment.TransitionToFragment(Resource.Id.frame_content, CreateViewItemFragment(), null);
                };
            }

            public virtual bool OnMenuItemClick(IMenuItem menuItem)
            {
                if(DeleteActionResourceId == menuItem.ItemId) {
                    // TODO: delete Action
                    return true;
                }
                return false;
            }
        }

        public ListItemsFragment<T> Fragment { get; }

        public abstract int LayoutResource { get; }

        public override int ItemCount => FilteredListItems?.Count() ?? 0;

        private IReadOnlyCollection<T> _listItems = new List<T>();  

        public IReadOnlyCollection<T> ListItems
        {
            get { return _listItems; }

            set
            {
                if(null == value) {
                    _listItems = new List<T>();
                    FilteredListItems = new List<T>();
                } else {
                    _listItems = new List<T>(value);
                    FilteredListItems = new List<T>(ListItems);
                }
            }
        }

        private IEnumerable<T> _filteredListItems = new List<T>();

        protected IEnumerable<T> FilteredListItems
        {
            get { return _filteredListItems; }

            set
            {
                _filteredListItems = value ?? new List<T>(ListItems);
                NotifyDataSetChanged();
            }
        }

        public void SortByItemSelectedEventHander(object sender, AdapterView.ItemSelectedEventArgs args)
        {
            // TODO: could/would this be made clearer somehow by using args.Id?
            SortItemsByPosition(args.Position);
        }

        // TODO: should this work like sorting
        // where this base handles the event
        // and calls an abstract FilterBy*() method?
        public abstract void FilterItems(object sender, Android.Support.V7.Widget.SearchView.QueryTextChangeEventArgs args);

        protected abstract void SortItemsByPosition(int position);

        protected abstract BaseViewHolder CreateViewHolder(View itemView);

        public override Android.Support.V7.Widget.RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(LayoutResource, parent, false);
            return CreateViewHolder(itemView);
        }

        public override void OnBindViewHolder(Android.Support.V7.Widget.RecyclerView.ViewHolder holder, int position)
        {
            BaseViewHolder baseViewHolder = (BaseViewHolder)holder;
            T gearItem = FilteredListItems.ElementAt(position);
            baseViewHolder.ListItem = gearItem;
        }

        protected BaseListAdapter(ListItemsFragment<T> fragment)
        {
            Fragment = fragment;
        }
    }
}
