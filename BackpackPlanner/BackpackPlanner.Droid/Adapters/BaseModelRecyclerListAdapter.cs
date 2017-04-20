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
    public abstract class BaseModelRecyclerListAdapter<T> : BaseRecyclerListAdapter<T>, IFilterable
        where T: BaseModel<T>, IBackpackPlannerItem, new()
    {
        protected abstract class BaseModelRecyclerViewHolder
            : BaseRecyclerViewHolder, Android.Support.V7.Widget.Toolbar.IOnMenuItemClickListener
        {
            protected ListItemsFragment<T> ListItemsFragment => (ListItemsFragment<T>)Fragment;

            protected abstract int ToolbarResourceId { get; }

            protected abstract int MenuResourceId { get; }

            protected abstract int DeleteActionResourceId { get; }

            private Android.Support.V7.Widget.Toolbar _toolbar;

            protected abstract ViewItemFragment<T> CreateViewItemFragment();

            protected BaseModelRecyclerViewHolder(View view, BaseRecyclerListAdapter<T> adapter)
                : base(view, adapter)
            {
                InitToolbar();

                view.Click += (sender, args) =>
                {
                    ViewItemFragment<T> viewItemFragment = CreateViewItemFragment();
                    viewItemFragment.SetItem(Item);

                    Fragment.TransitionToFragment(Resource.Id.frame_content, viewItemFragment, null);
                };
            }

            private void InitToolbar()
            {
                _toolbar = ItemView.FindViewById<Android.Support.V7.Widget.Toolbar>(ToolbarResourceId);
                _toolbar.InflateMenu(MenuResourceId);
                _toolbar.SetOnMenuItemClickListener(this);
            }

            public override void UpdateView(T item)
            {
                base.UpdateView(item);

                _toolbar.Title = Item.Name;
            }

            public virtual bool OnMenuItemClick(IMenuItem menuItem)
            {
                if(DeleteActionResourceId == menuItem.ItemId) {
                    ListItemsFragment.DeleteItem(Item);
                    return true;
                }
                return false;
            }
        }

#region Filtering
        private sealed class ItemFilter : Filter
        {
            private readonly BaseModelRecyclerListAdapter<T> _adapter;

            protected override FilterResults PerformFiltering(ICharSequence constraint)
            {
                FilterResults results = new FilterResults();

                string filterConstraint = (constraint?.ToString() ?? string.Empty).ToLower();

                // NOTE: not checking vs whitespace because that might be useful to filter on
                var filteredItemEnumerable = string.IsNullOrEmpty(filterConstraint)
                    ? from item in _adapter.FullListItems select item?.ToJavaObject()
                    : from item in _adapter.FullListItems where null != item && item.Name.ToLower().Contains(filterConstraint) select item.ToJavaObject();

                // convert to JavaObject array
                var filteredObjectArray = filteredItemEnumerable.ToArray();
                results.Values = filteredObjectArray;
                results.Count = filteredObjectArray.Length;

                return results;
            }

            protected override void PublishResults(ICharSequence constraint, FilterResults results)
            {
                // convert back to C# array
                var filteredItems = results.Values.ToArray<ObjectWrapper>();
                _adapter._filteredListItems = from item in filteredItems select (T)item?.Instance;
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
#endregion

        public ListItemsFragment<T> ListItemsFragment => (ListItemsFragment<T>)Fragment;

        [NotNull]
        private IEnumerable<T> _filteredListItems = new List<T>();

        public Filter Filter { get; }

        private readonly FilterListener _filterListener;

        protected override void ProcessItems()
        {
            FilterItems();
        }

#region Filter and Sort
        private void FilterItems()
        {
            if(null == Fragment.FilterView) {
                _filteredListItems = new List<T>(FullListItems);
                _filterListener.OnFilterComplete(FullListItems.Count);
            } else {
                Filter.InvokeFilter(Fragment.FilterView.Query, _filterListener);
            }
        }

        protected abstract IEnumerable<T> SortItemsByPosition(int position, IEnumerable<T> items);

        private void SortFilteredItems()
        {
            ProcessedListItems = null == ListItemsFragment.SortItemsSpinner
                ? new List<T>(_filteredListItems)
                : SortItemsByPosition(ListItemsFragment.SortItemsSpinner.SelectedItemPosition, _filteredListItems).ToList();

            OnSortComplete();
        }

        private void OnSortComplete()
        {
            InjectAds();
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
            _filterListener = new FilterListener(this);
        }
    }
}
