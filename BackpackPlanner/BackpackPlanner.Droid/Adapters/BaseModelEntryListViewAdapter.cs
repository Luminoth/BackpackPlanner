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
using EnergonSoftware.BackpackPlanner.Droid.Activities;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters
{
    public abstract class BaseModelEntryListViewAdapter<T, TM, TV> : BaseListViewAdapter<T>
        where T: BaseModelEntry<T, TM, TV>, new()
        where TM: BaseModel<TM>, new()
        where TV: BaseModel<TV>, IBackpackPlannerItem, new()
    {
        protected BaseActivity BaseActivity { get; }

        private readonly List<T> _items = new List<T>();

        public IReadOnlyCollection<T> Items => _items;

        public void AddItem(T item)
        {
            _items.Add(item);
            Add(item);
        }

        public void AddAll(IReadOnlyCollection<T> items)
        {
            foreach(T item in items) {
                AddItem(item);
            }
        } 

        public void RemoveItem(T item)
        {
            _items.Remove(item);
            Remove(item);
        }

        public void RemoveItem(TV item)
        {
            var removeItems = (from entry in Items where entry.Model.Id == item.Id select entry).ToList();
            foreach(T entry in removeItems) {
                RemoveItem(entry);
            }
        }

        public void RemoveAll(IReadOnlyCollection<T> items)
        {
            foreach(T item in items) {
                RemoveItem(item);
            }
        } 

        public void ClearItems()
        {
            _items.Clear();
            Clear();
        }

        protected BaseModelEntryListViewAdapter(BaseActivity activity)
            : base(activity)
        {
            BaseActivity = activity;
        }

        protected BaseModelEntryListViewAdapter(BaseActivity activity, T[] items)
            : base(activity, items)
        {
            BaseActivity = activity;
        }
    }
}
