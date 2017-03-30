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
using EnergonSoftware.BackpackPlanner.Droid.Fragments;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters
{
// TODO: rename BaseModelEntryListViewAdapter
    public abstract class BaseModelEntryListAdapter<T, TV> : BaseListViewAdapter<T> where T: BaseModelEntry<TV> where TV: BaseModel, IBackpackPlannerItem
    {
        private readonly List<T> _items = new List<T>();

        public IReadOnlyCollection<T> Items => _items;

        public void AddItem(T item)
        {
            _items.Add(item);
            Add(item);
        }

        public void RemoveItem(T item)
        {
            _items.Remove(item);
            Remove(item);
        }

        public void RemoveItem(TV item)
        {
            var removeItems = from entry in Items where entry.Model.Id == item.Id select entry;
            foreach(T entry in removeItems) {
                RemoveItem(entry);
            }
        }

        protected BaseModelEntryListAdapter(BaseFragment fragment)
            : base(fragment)
        {
        }

        protected BaseModelEntryListAdapter(BaseFragment fragment, T[] items)
            : base(fragment, items)
        {
        }
    }
}
