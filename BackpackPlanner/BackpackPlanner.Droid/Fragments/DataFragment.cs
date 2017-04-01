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

using Android.Widget;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.DAL.Models;
using EnergonSoftware.BackpackPlanner.Droid.Adapters;
using EnergonSoftware.BackpackPlanner.Droid.DAL;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments
{
    /// <summary>
    /// Helper for the data entry fragments
    /// </summary>
    public abstract class DataFragment : BaseFragment
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(DataFragment));

        protected sealed class FilterListener<T> : Java.Lang.Object, Filter.IFilterListener
        {
            private readonly BaseListViewAdapter<T> _adapter;

            public FilterListener(BaseListViewAdapter<T> adapter)
            {
                _adapter = adapter;
            }

            public void OnFilterComplete(int count)
            {
                // TODO: need a method to build an IComparator
                //_adapter.Sort();
            }
        }

        protected void SetItemEntryList<TM, TI, TE>(TM model, ItemEntries<TM, TI, TE> itemEntry)
            where TM: BaseModel where TI: BaseModel, IBackpackPlannerItem where TE: BaseModelEntry<TI>
        {
            for(int i=0; i<itemEntry.Items.Length; ++i) {
                TI item = itemEntry.Items[i];
                if(null == item) {
                    Logger.Error($"Found null item at index {i} while setting item entries!");
                    continue;
                }

                TE entry = itemEntry.GetItemEntry(item);
                if(null != entry) {
                    itemEntry.SelectedItems[i] = true;
                    itemEntry.ItemListAdapter.AddItem(entry);
                }
            }
        }

        protected void UpdateItemEntryList<TM, TI, TE>(TM model, ItemEntries<TM, TI, TE> itemEntry, int index, bool isSelected)
            where TM: BaseModel where TI: BaseModel, IBackpackPlannerItem where TE: BaseModelEntry<TI>, new()
        {
            TI item = itemEntry.Items[index];
            if(null == item) {
                Logger.Error($"Found null item at index {index} while updating item entries!");
                return;
            }

            itemEntry.SelectedItems[index] = isSelected;
            if(isSelected) {
                TE entry = new TE
                {
                    Model = item,
                    Count = 1
                };
                itemEntry.ItemListAdapter.AddItem(entry);
            } else {
                itemEntry.ItemListAdapter.RemoveItem(item);
            }

            UpdateView();
        }

        public bool DoDataExchange()
        {
            if(!OnValidate()) {
                return false;
            }

            OnDoDataExchange();

            return true;
        }

        protected abstract void UpdateView();

        protected abstract bool OnValidate();

        protected abstract void OnDoDataExchange();

        protected abstract void OnReset();
    }
}
