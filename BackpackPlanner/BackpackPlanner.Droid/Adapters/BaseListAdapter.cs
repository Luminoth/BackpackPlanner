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
using EnergonSoftware.BackpackPlanner.Droid.Util;
using EnergonSoftware.BackpackPlanner.Models;

using Java.Lang;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters
{
// TODO: rename this BaseRecyclerItemListAdapter
    public abstract class BaseListAdapter<T> : Android.Support.V7.Widget.RecyclerView.Adapter, IFilterable where T: DatabaseItem, IBackpackPlannerItem
    {
        protected abstract class BaseViewHolder : Android.Support.V7.Widget.RecyclerView.ViewHolder, Android.Support.V7.Widget.Toolbar.IOnMenuItemClickListener
        {
            protected abstract int DeleteActionResourceId { get; }

            protected BaseListAdapter<T> Adapter { get; }

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

            protected BaseViewHolder(View itemView, BaseListAdapter<T> adapter)
                : base(itemView)
            {
                Adapter = adapter;

                itemView.Click += (sender, args) => {
                    Adapter.Fragment.TransitionToFragment(Resource.Id.frame_content, CreateViewItemFragment(), null);
                };
            }

            public virtual bool OnMenuItemClick(IMenuItem menuItem)
            {
                if(DeleteActionResourceId == menuItem.ItemId) {
                    Adapter.Fragment.DeleteItem(ListItem);
                    return true;
                }
                return false;
            }
        }

        private sealed class ItemFilter : Filter
        {
            private readonly BaseListAdapter<T> _adapter;

            protected override FilterResults PerformFiltering(ICharSequence constraint)
            {
                FilterResults results = new FilterResults();

                string filterConstraint = (constraint?.ToString() ?? string.Empty).ToLower();

                // NOTE: not checking vs whitespace because that might be useful to filter on
                var filteredItemEnumerable = string.IsNullOrEmpty(filterConstraint)
                    ? from item in _adapter.ListItems select item.ToJavaObject()
                    : from item in _adapter.ListItems where item.Name.ToLower().Contains(filterConstraint) select item.ToJavaObject();

                var filteredObjectArray = filteredItemEnumerable.ToArray();
                results.Values = filteredObjectArray;
                results.Count = filteredObjectArray.Length;

                return results;
            }

            protected override void PublishResults(ICharSequence constraint, FilterResults results)
            {
                var filteredItems = results.Values.ToArray<ObjectWrapper>();
                _adapter.FilteredListItems = from item in filteredItems select (T)item.Instance;
            }

            public ItemFilter(BaseListAdapter<T> adapter)
            {
                _adapter = adapter;
            }
        }

        private sealed class FilterListener : Java.Lang.Object, Filter.IFilterListener
        {
            private readonly BaseListAdapter<T> _adapter;

            public FilterListener(BaseListAdapter<T> adapter)
            {
                _adapter = adapter;
            }

            public void OnFilterComplete(int count)
            {
                _adapter.SortFilteredItems();
            }
        }

        public ListItemsFragment<T> Fragment { get; }

        public abstract int LayoutResource { get; }

        public override int ItemCount => FilteredListItems?.Count() ?? 0;

        private List<T> _listItems = new List<T>();

        public IReadOnlyCollection<T> ListItems
        {
            get { return _listItems; }

            set
            {
                _listItems = null == value ? new List<T>() : new List<T>(value);

                FilterItems();
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

        public Filter Filter { get; }

#region ViewHolder
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
#endregion

#region Add/Remove items
        public void AddItem(T item)
        {
            _listItems.Add(item);

            FilterItems();
        }

        public void RemoveItem(T item)
        {
            _listItems.Remove(item);

            FilterItems();
        }
#endregion

#region Filter and Sort
        private void FilterItems()
        {
            if(null == Fragment.FilterView) {
                FilteredListItems = null;
                return;
            }

            Filter.InvokeFilter(Fragment.FilterView.Query, new FilterListener(this));
        }

        protected abstract void SortItemsByPosition(int position);

        private void SortFilteredItems()
        {
            if(null == Fragment.SortItemsSpinner) {
                return;
            }
            SortItemsByPosition(Fragment.SortItemsSpinner.SelectedItemPosition);
        }

        public void SortByItemSelectedEventHander(object sender, AdapterView.ItemSelectedEventArgs args)
        {
            SortFilteredItems();
        }

        public void FilterItemsEventHandler(object sender, Android.Support.V7.Widget.SearchView.QueryTextChangeEventArgs args)
        {
            FilterItems();
        }
#endregion

        protected BaseListAdapter(ListItemsFragment<T> fragment)
        {
            Fragment = fragment;
            Filter = new ItemFilter(this);
        }
    }
}
