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
using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner;
using EnergonSoftware.BackpackPlanner.Models.Gear;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BackpackPlanner.UnitTests
{
    [TestClass]
    public class GearStateTests
    {
        [TestMethod]
        public void GearState_GetGearItemById_NonExistant()
        {
            // Arrange
            GearState gearState = new GearState();

            // Act

            // Assert
            Assert.IsNull(gearState.GetGearItemById(-1));
            Assert.IsNull(gearState.GetGearItemById(0));
            Assert.IsNull(gearState.GetGearItemById(1));
        }

#region AddGearItem
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public async Task GearState_AddGearItem_Null()
        {
            // Arrange
            GearState gearState = new GearState();

            // Act
            await gearState.AddGearItemAsync(null).ConfigureAwait(false);

            // Assert
        }

        [TestMethod]
        public async Task GearState_AddGearItem_NewItem_NoDatabaseAsync()
        {
            // Arrange
            GearState gearState = new GearState();
            GearItem gearItem = new GearItem();

            // Act
            int gearItemId = await gearState.AddGearItemAsync(gearItem).ConfigureAwait(false);

            // Assert
            Assert.AreEqual(-1, gearItemId);
            Assert.AreEqual(gearItemId, gearItem.Id);
            Assert.AreEqual(0, gearState.GearItemCount);
        }

        [TestMethod]
        public async Task GearState_AddGearItem_ExistingItem()
        {
            // Arrange
            GearState gearState = new GearState();
            GearItem gearItem = new GearItem
            {
                Id = 1
            };

            // Act
            int gearItemId = await gearState.AddGearItemAsync(gearItem).ConfigureAwait(false);

            // Assert
            Assert.AreEqual(1, gearItemId);
            Assert.AreEqual(gearItemId, gearItem.Id);
            Assert.AreEqual(1, gearState.GearItemCount);
        }

        [TestMethod]
        public async Task GearState_AddGearItem_DuplicateItem()
        {
            // Arrange
            GearState gearState = new GearState();
            GearItem gearItem = new GearItem
            {
                Id = 1
            };

            // Act
            int gearItemId_First = await gearState.AddGearItemAsync(gearItem).ConfigureAwait(false);
            int gearItemId_Second = await gearState.AddGearItemAsync(gearItem).ConfigureAwait(false);

            // Assert
            Assert.AreEqual(1, gearItemId_First);
            Assert.AreEqual(gearItemId_First, gearItem.Id);
            Assert.AreEqual(-1, gearItemId_Second);
            Assert.AreEqual(1, gearState.GearItemCount);
        }
#endregion

#region DeleteGearItem
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public async Task GearState_DeleteGearItem_Null()
        {
            // Arrange
            GearState gearState = new GearState();

            // Act
            await gearState.DeleteGearItemAsync(null).ConfigureAwait(false);

            // Assert
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public async Task GearState_DeleteGearItem_InvalidId_Zero()
        {
            // Arrange
            GearState gearState = new GearState();
            GearItem gearItem = new GearItem
            {
                Id = 0
            };

            // Act
            await gearState.DeleteGearItemAsync(gearItem).ConfigureAwait(false);

            // Assert
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public async Task GearState_DeleteGearItem_InvalidId_Negative()
        {
            // Arrange
            GearState gearState = new GearState();
            GearItem gearItem = new GearItem
            {
                Id = -1
            };

            // Act
            await gearState.DeleteGearItemAsync(gearItem).ConfigureAwait(false);

            // Assert
        }

        [TestMethod]
        public async Task GearState_DeleteGearItem_NoDatabaseAsync()
        {
            // Arrange
            GearState gearState = new GearState();
            GearItem gearItem = new GearItem
            {
                Id = 1
            };
            await gearState.AddGearItemAsync(gearItem).ConfigureAwait(false);

            // Act
            await gearState.DeleteGearItemAsync(gearItem).ConfigureAwait(false);

            // Assert
            Assert.AreEqual(0, gearState.GearItemCount);
        }
#endregion

        [TestMethod]
        public async Task GearState_DeleteAllGearItems()
        {
            // Arrange
            GearState gearState = new GearState();

            for(int i=0; i<100; ++i) {
                await gearState.AddGearItemAsync(new GearItem { Id = i }).ConfigureAwait(false);
            }

            // Act
            await gearState.DeleteAllGearItemsAsync().ConfigureAwait(false);

            // Assert
            Assert.AreEqual(0, gearState.GearItemCount);
        }
    }
}
