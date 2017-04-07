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
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;

using EnergonSoftware.BackpackPlanner.Core.Logging;

using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

namespace EnergonSoftware.BackpackPlanner.DAL.Models
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public abstract class BaseModel : INotifyPropertyChanged
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(BaseModel));

#region Events
        public event PropertyChangedEventHandler PropertyChanged;
#endregion

        /// <summary>
        /// Gets the item identifier.
        /// </summary>
        /// <value>
        /// The item identifier.
        /// </value>
        [NotMapped, JsonIgnore]
        public abstract int Id { get; }

#region Database Properties
        /// <summary>
        /// Gets or sets the last update timestamp.
        /// </summary>
        /// <value>
        /// The last update timestamp.
        /// </value>
        [Required]
        public DateTime LastUpdated { get; set; } = DateTime.Now;

        private bool _isDeleted;

        /// <summary>
        /// Gets or sets a value indicating whether this item is deleted.
        /// </summary>
        /// <value>
        /// <c>true</c> if this item is deleted; otherwise, <c>false</c>.
        /// </value>
        [Required]
        public bool IsDeleted
        {
            get => _isDeleted;

            set
            {
                _isDeleted = value;
                NotifyPropertyChanged();
            }
        }
#endregion

        protected void UpdateItemEntries<TE, TI>(DatabaseContext dbContext, ICollection<TE> currentEntryItems, IReadOnlyCollection<TE> newEntryItems)
            where TE: BaseModelEntry<TI> where TI: BaseModel, IBackpackPlannerItem
        {
            // put the new set of items into a dictionary, collapsing any duplicates
            var newItemMap = new Dictionary<int, TE>();
            foreach(TE item in newEntryItems) {
                if(newItemMap.TryGetValue(item.Model.Id, out TE currentItem)) {
                    Logger.Debug($"Found duplicate item {item.Model.Id}, collapsing ({currentItem.Count} + {item.Count})");
                    currentItem.Count += item.Count;
                }  else {
                    newItemMap.Add(item.Model.Id, item);
                }
            }

            // remove any current items not in the new list
            var removeList = (from x in currentEntryItems where !newItemMap.ContainsKey(x.Model.Id) select x).ToList();
            foreach(TE item in removeList) {
                currentEntryItems.Remove(item);
                dbContext.Entry(item).State = EntityState.Deleted;
            }

            // add/update the remaining items
            var currentItemMap = currentEntryItems.ToDictionary(item => item.Model.Id);
            foreach(TE item in newEntryItems) {
                if(currentItemMap.TryGetValue(item.Model.Id, out TE currentItem)) {
                    currentItem.Count = item.Count;
                    dbContext.Entry(currentItem).State = EntityState.Modified;
                }  else {
                    currentEntryItems.Add(item);
                    currentItemMap.Add(item.Model.Id, item);
                    dbContext.Entry(item).State = EntityState.Added;

                    // need to mark this item's model as unchanged
                    // otherwise EF will try to insert it as a new row
                    dbContext.Entry(item.Model).State = EntityState.Unchanged;
                }
            }
        }

        public void OnRemove()
        {
            PropertyChanged = null;
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName="")
        {
            LastUpdated = DateTime.Now;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
