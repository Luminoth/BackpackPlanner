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

using System.Collections.Generic;
using System.Linq;

using Android.Views;

using EnergonSoftware.BackpackPlanner.Droid.Fragments;

using JetBrains.Annotations;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters
{
    public abstract class BaseRecyclerListAdapter<T> : Android.Support.V7.Widget.RecyclerView.Adapter where T: class
    {
        protected const int ListItemViewType = 0;

        protected abstract class BaseViewHolder : Android.Support.V7.Widget.RecyclerView.ViewHolder
        {
            protected BaseRecyclerListAdapter<T> Adapter { get; }

            private T _listItem;

            [CanBeNull]
            public T ListItem
            {
                get => _listItem;

                set
                {
                    _listItem = value;
                    UpdateView();
                }
            }

            protected virtual void UpdateView()
            {
            }

            protected BaseViewHolder(View itemView, BaseRecyclerListAdapter<T> adapter)
                : base(itemView)
            {
                Adapter = adapter;
            }
        }

        public RecyclerFragment Fragment { get; }

        public abstract int LayoutResource { get; }

        public override int ItemCount => ListItems.Count;

        [NotNull]
        private List<T> _listItems = new List<T>();

        [NotNull]
        public IReadOnlyCollection<T> ListItems => _listItems;

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
            T item = ListItems.ElementAt(position);
            baseViewHolder.ListItem = item;
        }
#endregion

#region Add/Remove items
        public virtual void SetItems(IReadOnlyCollection<T> items)
        {
            _listItems = null == items ? new List<T>() : new List<T>(items);
        }

        // used when undoing deleting an item
        public virtual void AddItem(T item)
        {
            _listItems.Add(item);
        }

        // used when deleting an item
        public virtual void RemoveItem(T item)
        {
            _listItems.Remove(item);
        }
#endregion

        public override int GetItemViewType(int position)
        {
            return ListItemViewType;
        }

        protected BaseRecyclerListAdapter(RecyclerFragment fragment)
        {
            Fragment = fragment;
        }
    }
}
