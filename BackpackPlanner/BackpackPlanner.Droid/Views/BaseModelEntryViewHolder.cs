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

using System.Linq;

using Android.Views;
using Android.Widget;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.DAL;
using EnergonSoftware.BackpackPlanner.DAL.Models;
using EnergonSoftware.BackpackPlanner.Droid.Activities;
using EnergonSoftware.BackpackPlanner.Droid.Adapters;
using EnergonSoftware.BackpackPlanner.Droid.DAL;
using EnergonSoftware.BackpackPlanner.Droid.Util;

namespace EnergonSoftware.BackpackPlanner.Droid.Views
{
    public abstract class BaseModelEntryViewHolder<TM, TI, TIE> : BaseViewHolder<ItemEntries<TM, TI, TIE>>
        where TM: BaseModel<TM>, new()
        where TI: BaseModel<TI>, IBackpackPlannerItem, new()
        where TIE: BaseModelEntry<TIE, TM, TI>, new()
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(BaseModelEntryViewHolder<TM, TI, TIE>));

        private sealed class FilterListener<TE> : Java.Lang.Object, Filter.IFilterListener
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

        protected abstract int NoItemsResource { get; }

        protected abstract int NoItemsAddedResource { get; }

        protected abstract int ItemListAdapterResource { get; }

        protected abstract int AddItemButtonResource { get; }

        protected abstract int AddItemDialogTitleResource { get; }

        private readonly TextView _noItemsTextView;
        private readonly TextView _noItemsAddedTextView;
        private readonly ListView _itemListView;
        private readonly Android.Support.Design.Widget.FloatingActionButton _addItemButton;

        public override void UpdateView(ItemEntries<TM, TI, TIE> itemEntries)
        {
            base.UpdateView(itemEntries);

            _itemListView.Adapter = itemEntries.ItemListAdapter;

            bool hasItems = null != itemEntries.Items && itemEntries.Items.Any();
            bool hasItemsAdded = itemEntries.EntryCount > 0;

            _noItemsTextView.Visibility = hasItems ? ViewStates.Gone : ViewStates.Visible;
            _noItemsAddedTextView.Visibility =  hasItemsAdded ? ViewStates.Gone : ViewStates.Visible;
            _itemListView.Visibility = hasItemsAdded ? ViewStates.Visible : ViewStates.Gone;
            _addItemButton.Visibility = hasItems ? ViewStates.Visible : ViewStates.Gone;
        }

        public override bool Validate()
        {
            bool valid = base.Validate();

// TODO: validate no item quantities are < 1

            return valid;
        }

        public abstract void DoDataExchange(TM item, ItemEntries<TM, TI, TIE> itemEntries, DatabaseContext dbContext);

        public void SetItemEntryList(ItemEntries<TM, TI, TIE> itemEntries)
        {
            for(int i=0; i<itemEntries.ItemCount; ++i) {
                TI item = itemEntries.Items?[i];
                if(null == item) {
                    Logger.Error($"Found null item at index {i} while setting item entries!");
                    continue;
                }

                TIE entry = itemEntries.GetItemEntry(item);
                if(null != entry) {
                    itemEntries.SelectItem(i, true);
                    itemEntries.ItemListAdapter?.AddItem(entry);
                }
            }

            UpdateView(itemEntries);
        }

        private void UpdateItemEntryList(ItemEntries<TM, TI, TIE> itemEntries, int index, bool isSelected)
        {
            TI item = itemEntries.Items?[index];
            if(null == item) {
                Logger.Error($"Found null item at index {index} while updating item entries!");
                return;
            }

            itemEntries.SelectItem(index, isSelected);
            if(isSelected) {
                TIE entry = new TIE
                {
                    Count = 1
                };
                entry.SetModel(item);

                itemEntries.ItemListAdapter?.AddItem(entry);
            } else {
                itemEntries.ItemListAdapter?.RemoveItem(item);
            }

            UpdateView(itemEntries);
        }

        protected BaseModelEntryViewHolder(BaseActivity activity, View view)
            : base(activity)
        {
            _noItemsTextView = view.FindViewById<TextView>(NoItemsResource);
            _noItemsAddedTextView = view.FindViewById<TextView>(NoItemsAddedResource);

            _itemListView = view.FindViewById<ListView>(ItemListAdapterResource);

            _addItemButton = view.FindViewById<Android.Support.Design.Widget.FloatingActionButton>(AddItemButtonResource);
            _addItemButton.Click += (sender, args) =>
            {
                DialogUtil.ShowMultiChoiceAlertWithSearch(BaseActivity, AddItemDialogTitleResource,
                    Item.ItemNames, Item.SelectedItems,
                    (a, b) =>
                    {
                        Item.ItemListAdapter?.Filter.InvokeFilter(b.NewText, new FilterListener<TIE>(Item.ItemListAdapter));
                    },
                    (a, b) =>
                    {
                        UpdateItemEntryList(Item, b.Which, b.IsChecked);
                    }
                );
            };
        }
    }
}