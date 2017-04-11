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

using EnergonSoftware.BackpackPlanner.DAL.Models;
using EnergonSoftware.BackpackPlanner.Droid.Adapters;

using JetBrains.Annotations;

namespace EnergonSoftware.BackpackPlanner.Droid.DAL
{
    // ReSharper disable once InconsistentNaming
    public abstract class ItemEntries<TM, TI, TIE>
        where TM: BaseModel<TM>, new()
        where TI: BaseModel<TI>, IBackpackPlannerItem, new()
        where TIE: BaseModelEntry<TIE, TM, TI>, new()
    {
        [CanBeNull]
        private List<TI> _items;

        [CanBeNull]
        public List<TI> Items
        {
            get => _items;

            set
            {
                _items = value;

                if(null == _items) {
                    ItemNames = null;
                    SelectedItems = null;
                } else {
                    ItemNames = (from x in _items select x.Name).ToArray();
                    SelectedItems = new bool[_items.Count];
                }
            }
        }

        public int ItemCount => Items?.Count ?? 0;

        [CanBeNull]
        public string[] ItemNames { get; private set; }

        [CanBeNull]
        public bool[] SelectedItems { get; private set; }

        [CanBeNull]
        public BaseModelEntryListViewAdapter<TIE, TM, TI> ItemListAdapter { get; set; }

        public int EntryCount => ItemListAdapter?.Count ?? 0;

        protected TM Model { get; }

        public abstract TIE GetItemEntry(TI item);

        public void SelectItem(int index, bool isSelected)
        {
            if(null != SelectedItems) {
                SelectedItems[index] = isSelected;
            }
        }

        protected ItemEntries(TM model, IReadOnlyCollection<TIE> entries)
        {
            Model = model;

            ItemListAdapter?.AddAll(entries);
        }
    }
}
