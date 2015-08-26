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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner.Models.Gear;

namespace EnergonSoftware.BackpackPlanner
{
    /// <summary>
    /// Collects gear state
    /// </summary>
    public class GearState
    {
        private readonly HashSet<GearItem> _gearItems = new HashSet<GearItem>();

        /// <summary>
        /// Gets the gear item count.
        /// </summary>
        /// <value>
        /// The gear item count.
        /// </value>
        public int GearItemCount => _gearItems.Count;

#region LoadFromDevice
        /// <summary>
        /// Loads the gear state from the device.
        /// </summary>
        public async Task LoadFromDeviceAsync()
        {
            await LoadGearItemsFromDeviceAsync().ConfigureAwait(false);
        }

        private async Task LoadGearItemsFromDeviceAsync()
        {
await Task.Delay(0).ConfigureAwait(false);
        }
#endregion

#region Gear Items
        /// <summary>
        /// Gets a gear item by identifier.
        /// </summary>
        /// <param name="gearItemId">The gear item identifier.</param>
        /// <returns>The first gear item with the given identifier or null if it isn't in the container</returns>
        public GearItem GetGearItemById(int gearItemId)
        {
            return gearItemId < 1 ? null : _gearItems.FirstOrDefault(gearItem => gearItem.Id == gearItemId);
        }

        /// <summary>
        /// Adds the gear item to the container.
        /// </summary>
        /// <param name="gearItem">The gear item to add.</param>
        /// <returns>The id of the newly added gear item or -1 if it couldn't be added</returns>
        public async Task<int> AddGearItemAsync(GearItem gearItem)
        {
            if(null == gearItem) {
                throw new ArgumentNullException(nameof(gearItem));
            }

            if(gearItem.Id < 1 && null != BackpackPlannerState.Instance.DbConnection) {
                int gearItemId = await BackpackPlannerState.Instance.DbConnection.InsertAsync(gearItem).ConfigureAwait(false);
                gearItem.Id = gearItemId;
            }

            // TODO: handle the error case where
            // we successfully inserted the item in the database
            // but then failed to add it to the collection

            return gearItem.Id < 1 ? -1 : (_gearItems.Add(gearItem) ? gearItem.Id : -1);
        }

        /// <summary>
        /// Deletes the gear item from the container.
        /// </summary>
        /// <param name="gearItem">The gear item to delete.</param>
        public async Task DeleteGearItemAsync(GearItem gearItem)
        {
            if(null == gearItem) {
                throw new ArgumentNullException(nameof(gearItem));
            }

            if(gearItem.Id < 1) {
                throw new ArgumentException("Id cannot be less than 1!", nameof(gearItem));
            }

            if(null != BackpackPlannerState.Instance.DbConnection) {
                await BackpackPlannerState.Instance.DbConnection.DeleteAsync(gearItem).ConfigureAwait(false);
            }

            _gearItems.Remove(gearItem);
        }

        /// <summary>
        /// Deletes all of the gear items.
        /// </summary>
        public async Task DeleteAllGearItemsAsync()
        {
            if(null != BackpackPlannerState.Instance.DbConnection) {
                await BackpackPlannerState.Instance.DbConnection.DeleteAllAsync<GearItem>().ConfigureAwait(false);
            }

            _gearItems.Clear();
        }
#endregion
    }
}
