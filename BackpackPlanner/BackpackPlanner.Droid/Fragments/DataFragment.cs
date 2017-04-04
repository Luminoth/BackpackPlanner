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

using System.Threading.Tasks;

using Android.Widget;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.DAL;
using EnergonSoftware.BackpackPlanner.DAL.Models;
using EnergonSoftware.BackpackPlanner.Droid.Adapters;
using EnergonSoftware.BackpackPlanner.Droid.DAL;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments
{
    /// <summary>
    /// Helper for the data entry fragments
    /// </summary>
    public abstract class DataFragment<T> : BaseFragment where T: BaseModel
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(DataFragment<T>));

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
            for(int i=0; i<itemEntry.Count; ++i) {
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

        public async Task<bool> DoDataExchange(DatabaseContext dbContext)
        {
            Logger.Debug($"DDX {GetType()} => {typeof(T)}");

            if(!OnValidate()) {
                return false;
            }

            await OnDoDataExchange(dbContext).ConfigureAwait(false);

            return true;
        }

        protected abstract void UpdateView();

        protected abstract bool OnValidate();

        protected abstract Task OnDoDataExchange(DatabaseContext dbContext);

        protected abstract void OnReset();
    }
}
