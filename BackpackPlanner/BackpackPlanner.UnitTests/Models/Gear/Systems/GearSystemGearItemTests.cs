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

using EnergonSoftware.BackpackPlanner.Models.Gear.Systems;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using SQLite.Net.Async;

using TypeMock.ArrangeActAssert;

namespace EnergonSoftware.BackpackPlanner.UnitTests.Models.Gear.Systems
{
    [TestClass]
    public class GearSystemGearItemTests
    {
        [TestMethod]
        public async Task GearSystemGearItem_CreateTablesAsync()
        {
            // Isolate
            SQLiteAsyncConnection fakeAsyncDbConnection = Isolate.Fake.Instance<SQLiteAsyncConnection>(Members.ReturnRecursiveFakes);

            // Arrange

            // Act
            await GearSystemGearItem.CreateTablesAsync(fakeAsyncDbConnection).ConfigureAwait(false);

            // Assert
            Isolate.Verify.WasCalledWithAnyArguments(() => fakeAsyncDbConnection.CreateTableAsync<GearSystemGearItem>());
        }

        [TestMethod]
        public void GearSystemGearItem_GearSystemId_Default()
        {
            // Arrange
            GearSystemGearItem gearSystemGearItem = new GearSystemGearItem();

            // Act

            // Assert
            Assert.IsTrue(gearSystemGearItem.GearSystemId < 1);
        }

        [TestMethod]
        public void GearSystemGearItem_GearItemId_Default()
        {
            // Arrange
            GearSystemGearItem gearSystemGearItem = new GearSystemGearItem();

            // Act

            // Assert
            Assert.IsTrue(gearSystemGearItem.GearItemId < 1);
        }
    }
}
