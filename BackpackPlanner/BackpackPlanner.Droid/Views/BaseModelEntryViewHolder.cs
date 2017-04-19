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

using System;
using System.Linq;

using Android.Views;
using Android.Widget;

using EnergonSoftware.BackpackPlanner.DAL;
using EnergonSoftware.BackpackPlanner.DAL.Models;
using EnergonSoftware.BackpackPlanner.Droid.Activities;
using EnergonSoftware.BackpackPlanner.Droid.DAL;

namespace EnergonSoftware.BackpackPlanner.Droid.Views
{
    public abstract class BaseModelEntryViewHolder<TM, TI, TIE> : BaseViewHolder<ItemEntries<TM, TI, TIE>>
        where TM: BaseModel<TM>, new()
        where TI: BaseModel<TI>, IBackpackPlannerItem, new()
        where TIE: BaseModelEntry<TIE, TM, TI>, new()
    {
#region Events
        public event EventHandler AddItemEvent;
#endregion

        protected abstract int NoItemsResource { get; }

        protected abstract int NoItemsAddedResource { get; }

        protected abstract int ItemListAdapterResource { get; }

        protected abstract int AddItemButtonResource { get; }

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

        public virtual bool Validate()
        {
            bool valid = true;

// TODO: validate no item quantities are < 1

            return valid;
        }

        public virtual void DoDataExchange(TM item, ItemEntries<TM, TI, TIE> itemEntries, DatabaseContext dbContext)
        {
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
                AddItemEvent?.Invoke(this, EventArgs.Empty);
            };
        }
    }
}