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

using EnergonSoftware.BackpackPlanner.DAL.Models;
using EnergonSoftware.BackpackPlanner.Droid.Fragments;
using EnergonSoftware.BackpackPlanner.Droid.Util;

using Java.Lang;

using JetBrains.Annotations;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters
{
#if ENABLE_ADS
    public abstract class BaseModelRecyclerListAdapter<T> : BaseRecyclerListAdapterWithAds<T>, IFilterable where T: BaseModel, IBackpackPlannerItem

#else
    public abstract class BaseModelRecyclerListAdapter<T> : BaseRecyclerListAdapter<T>, IFilterable where T: BaseModel, IBackpackPlannerItem
#endif
    {
        protected abstract class BaseModelViewHolder : BaseViewHolder, Android.Support.V7.Widget.Toolbar.IOnMenuItemClickListener
        {
            protected BaseModelRecyclerListAdapter<T> BaseModelAdapter => (BaseModelRecyclerListAdapter<T>)Adapter;

            protected abstract int DeleteActionResourceId { get; }

            protected abstract Android.Support.V4.App.Fragment CreateViewItemFragment();

            protected BaseModelViewHolder(View itemView, BaseModelRecyclerListAdapter<T> adapter)
                : base(itemView, adapter)
            {
                itemView.Click += (sender, args) => {
                    Adapter.Fragment.TransitionToFragment(Resource.Id.frame_content, CreateViewItemFragment(), null);
                };
            }

            public virtual bool OnMenuItemClick(IMenuItem menuItem)
            {
                if(DeleteActionResourceId == menuItem.ItemId) {
                    BaseModelAdapter.ListItemsFragment.DeleteItem(ListItem);
                    return true;
                }
                return false;
            }
        }

        private sealed class ItemFilter : Filter
        {
            private readonly BaseModelRecyclerListAdapter<T> _adapter;

            protected override FilterResults PerformFiltering(ICharSequence constraint)
            {
                FilterResults results = new FilterResults();

                string filterConstraint = (constraint?.ToString() ?? string.Empty).ToLower();

                // NOTE: not checking vs whitespace because that might be useful to filter on
                var filteredItemEnumerable = string.IsNullOrEmpty(filterConstraint)
                    ? from item in _adapter.ListItems select item?.ToJavaObject()
                    : from item in _adapter.ListItems where null != item && item.Name.ToLower().Contains(filterConstraint) select item.ToJavaObject();

                var filteredObjectArray = filteredItemEnumerable.ToArray();
                results.Values = filteredObjectArray;
                results.Count = filteredObjectArray.Length;

                return results;
            }

            protected override void PublishResults(ICharSequence constraint, FilterResults results)
            {
                var filteredItems = results.Values.ToArray<ObjectWrapper>();
                _adapter.FilteredListItems = from item in filteredItems select (T)item?.Instance;
            }

            public ItemFilter(BaseModelRecyclerListAdapter<T> adapter)
            {
                _adapter = adapter;
            }
        }

        private sealed class FilterListener : Java.Lang.Object, Filter.IFilterListener
        {
            private readonly BaseModelRecyclerListAdapter<T> _adapter;

            public FilterListener(BaseModelRecyclerListAdapter<T> adapter)
            {
                _adapter = adapter;
            }

            public void OnFilterComplete(int count)
            {
                _adapter.SortFilteredItems();
            }
        }

        public ListItemsFragment<T> ListItemsFragment => (ListItemsFragment<T>)Fragment;

        public override int ItemCount => FilteredListItems.Count();

        [NotNull]
        private IEnumerable<T> _filteredListItems = new List<T>();

        [NotNull]
        protected IEnumerable<T> FilteredListItems
        {
            get => _filteredListItems;

            set
            {
                _filteredListItems = value ?? new List<T>(ListItems);

                NotifyDataSetChanged();
            }
        }

        public Filter Filter { get; }

        public override void OnBindViewHolder(Android.Support.V7.Widget.RecyclerView.ViewHolder holder, int position)
        {
            base.OnBindViewHolder(holder, position);

            BaseViewHolder baseViewHolder = (BaseViewHolder)holder;
            T item = FilteredListItems.ElementAt(position);
            baseViewHolder.ListItem = item;
        }

#region Add/Remove Items
        public override void SetItems(IReadOnlyCollection<T> items)
        {
            base.SetItems(items);

            FilterItems();
        } 

        public override void AddItem(T item)
        {
            base.AddItem(item);

            FilterItems();
        }

        public override void RemoveItem(T item)
        {
            base.RemoveItem(item);

            FilterItems();
        }
#endregion

#region Filter and Sort
        private void FilterItems()
        {
            if(null == Fragment.FilterView) {
                // setting null here will ensure we re-create the full list
                FilteredListItems = null;
                return;
            }

            Filter.InvokeFilter(Fragment.FilterView.Query, new FilterListener(this));
        }

        protected abstract void SortItemsByPosition(int position);

        private void SortFilteredItems()
        {
            if(null == ListItemsFragment.SortItemsSpinner) {
                return;
            }
            SortItemsByPosition(ListItemsFragment.SortItemsSpinner.SelectedItemPosition);
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

        protected BaseModelRecyclerListAdapter(ListItemsFragment<T> fragment)
            : base(fragment)
        {
            Filter = new ItemFilter(this);
        }
    }
}
