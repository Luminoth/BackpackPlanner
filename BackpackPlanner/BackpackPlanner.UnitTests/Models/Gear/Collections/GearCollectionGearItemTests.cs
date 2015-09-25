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

using EnergonSoftware.BackpackPlanner.Models.Gear.Collections;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using SQLite.Net.Async;

using TypeMock.ArrangeActAssert;

namespace EnergonSoftware.BackpackPlanner.UnitTests.Models.Gear.Collections
{
    [TestClass, Isolated]
    public class GearCollectionGearItemTests
    {
        [TestMethod]
        public async Task GearCollectionGearItem_CreateTablesAsync()
        {
            // Isolate
            SQLiteAsyncConnection fakeAsyncDbConnection = Isolate.Fake.Instance<SQLiteAsyncConnection>(Members.ReturnRecursiveFakes);

            // Arrange

            // Act
            await GearCollectionGearItem.CreateTablesAsync(fakeAsyncDbConnection).ConfigureAwait(false);

            // Assert
            Isolate.Verify.WasCalledWithAnyArguments(() => fakeAsyncDbConnection.CreateTableAsync<GearCollectionGearItem>());
        }

        [TestMethod]
        public void GearCollectionGearItem_GearCollectionId_Default()
        {
            // Arrange
            GearCollectionGearItem gearCollectionGearItem = new GearCollectionGearItem();

            // Act

            // Assert
            Assert.IsTrue(gearCollectionGearItem.GearCollectionId < 1);
        }

        [TestMethod]
        public void GearCollectionGearItem_GearItemId_Default()
        {
            // Arrange
            GearCollectionGearItem gearCollectionGearItem = new GearCollectionGearItem();

            // Act

            // Assert
            Assert.IsTrue(gearCollectionGearItem.GearItemId < 1);
        }
    }
}
