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

using System;
using System.Linq;
using System.Threading.Tasks;

using Android.Views;
using Android.Widget;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.DAL;
using EnergonSoftware.BackpackPlanner.DAL.Models;
using EnergonSoftware.BackpackPlanner.Droid.Adapters;
using EnergonSoftware.BackpackPlanner.Droid.DAL;
using EnergonSoftware.BackpackPlanner.Droid.Util;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments
{
    /// <summary>
    /// Helper for the data entry fragments
    /// </summary>
    public abstract class DataFragment<T> : BaseFragment where T: BaseModel
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(DataFragment<T>));

        public abstract class ItemEntryViewHolder<TI, TIE>
            where TI: BaseModel, IBackpackPlannerItem where TIE: BaseModelEntry<TI>, new()
        {
            protected abstract int NoItemsResource { get; }

            protected abstract int NoItemsAddedResource { get; }

            protected abstract int ItemListAdapterResource { get; }

            protected abstract int AddItemButtonResource { get; }

            protected abstract int AddItemDialogTitleResource { get; }

#region Controls
            private TextView _noItemsTextView;
            private TextView _noItemsAddedTextView;
            private ListView _itemListView;
            private Android.Support.Design.Widget.FloatingActionButton _addItemButton;
#endregion

            private readonly DataFragment<T> _fragment;

            private readonly T _item;

            private readonly ItemEntries<T, TI, TIE> _itemEntires;

            public void OnViewCreated(View view, IListAdapter itemListAdapter)
            {
                _noItemsTextView = view.FindViewById<TextView>(NoItemsResource);
                _noItemsAddedTextView = view.FindViewById<TextView>(NoItemsAddedResource);

                _itemListView = view.FindViewById<ListView>(ItemListAdapterResource);
                _itemListView.Adapter = itemListAdapter;

                _addItemButton = view.FindViewById<Android.Support.Design.Widget.FloatingActionButton>(AddItemButtonResource);
                _addItemButton.Click += AddItemButtonClickEventHandler;
            }

            public void UpdateView()
            {
                bool hasItems = null != _itemEntires.Items && _itemEntires.Items.Any();
                bool hasItemsAdded = _itemEntires.EntryCount > 0;

                _noItemsTextView.Visibility = hasItems ? ViewStates.Gone : ViewStates.Visible;
                _noItemsAddedTextView.Visibility =  hasItemsAdded ? ViewStates.Gone : ViewStates.Visible;
                _itemListView.Visibility = hasItemsAdded ? ViewStates.Visible : ViewStates.Gone;
                _addItemButton.Visibility = hasItems ? ViewStates.Visible : ViewStates.Gone;
            }

            private void AddItemButtonClickEventHandler(object sender, EventArgs args)
            {
                DialogUtil.ShowMultiChoiceAlertWithSearch(_fragment.Activity, AddItemDialogTitleResource,
                    _itemEntires.ItemNames, _itemEntires.SelectedItems,
                    (a, b) =>
                    {
                        _itemEntires.ItemListAdapter?.Filter.InvokeFilter(b.NewText, new FilterListener<TIE>(_itemEntires.ItemListAdapter));
                    },
                    (a, b) =>
                    {
                        _fragment.UpdateItemEntryList(_item, _itemEntires, b.Which, b.IsChecked);
                    }
                );
            }

            protected ItemEntryViewHolder(DataFragment<T> fragment, T item, ItemEntries<T, TI, TIE> itemEntries)
            {
                _fragment = fragment;
                _item = item;
                _itemEntires = itemEntries;
            }
        }

        protected sealed class FilterListener<TE> : Java.Lang.Object, Filter.IFilterListener
        {
            private readonly BaseListViewAdapter<TE> _adapter;

            public FilterListener(BaseListViewAdapter<TE> adapter)
            {
                _adapter = adapter;
            }

            public void OnFilterComplete(int count)
            {
                // TODO: need a method to build an IComparator
                //_adapter.Sort();
            }
        }

        protected void SetItemEntryList<TI, TE>(T model, ItemEntries<T, TI, TE> itemEntry)
            where TI: BaseModel, IBackpackPlannerItem where TE: BaseModelEntry<TI>
        {
            for(int i=0; i<itemEntry.ItemCount; ++i) {
                TI item = itemEntry.Items?[i];
                if(null == item) {
                    Logger.Error($"Found null item at index {i} while setting item entries!");
                    continue;
                }

                TE entry = itemEntry.GetItemEntry(item);
                if(null != entry) {
                    itemEntry.SelectItem(i, true);
                    itemEntry.ItemListAdapter?.AddItem(entry);
                }
            }
        }

        protected void UpdateItemEntryList<TI, TE>(T model, ItemEntries<T, TI, TE> itemEntry, int index, bool isSelected)
            where TI: BaseModel, IBackpackPlannerItem where TE: BaseModelEntry<TI>, new()
        {
            TI item = itemEntry.Items?[index];
            if(null == item) {
                Logger.Error($"Found null item at index {index} while updating item entries!");
                return;
            }

            itemEntry.SelectItem(index, isSelected);
            if(isSelected) {
                TE entry = new TE
                {
                    Count = 1
                };
                entry.SetModel(item);

                itemEntry.ItemListAdapter?.AddItem(entry);
            } else {
                itemEntry.ItemListAdapter?.RemoveItem(item);
            }

            UpdateView();
        }

        protected abstract void UpdateView();

        protected abstract bool Validate();

        protected abstract Task DoDataExchange(DatabaseContext dbContext);

        protected abstract void Reset();
    }
}
