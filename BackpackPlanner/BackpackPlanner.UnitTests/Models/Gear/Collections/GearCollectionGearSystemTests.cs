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

namespace BackpackPlanner.UnitTests.Models.Gear.Collections
{
    [TestClass, Isolated]
    public class GearCollectionGearSystemTests
    {
        [TestMethod]
        public async Task GearCollectionGearSystem_CreateTablesAsync()
        {
            // Isolate
            SQLiteAsyncConnection fakeAsyncDbConnection = Isolate.Fake.Instance<SQLiteAsyncConnection>(Members.ReturnRecursiveFakes);

            // Arrange

            // Act
            await GearCollectionGearSystem.CreateTablesAsync(fakeAsyncDbConnection).ConfigureAwait(false);

            // Assert
            Isolate.Verify.WasCalledWithAnyArguments(() => fakeAsyncDbConnection.CreateTableAsync<GearCollectionGearSystem>());
        }

        [TestMethod]
        public void GearCollectionGearSystem_GearCollectionId_Default()
        {
            // Arrange
            GearCollectionGearSystem gearCollectionGearSystem = new GearCollectionGearSystem();

            // Act

            // Assert
            Assert.IsTrue(gearCollectionGearSystem.GearCollectionId < 1);
        }

        [TestMethod]
        public void GearCollectionGearSystem_GearSystemId_Default()
        {
            // Arrange
            GearCollectionGearSystem gearCollectionGearSystem = new GearCollectionGearSystem();

            // Act

            // Assert
            Assert.IsTrue(gearCollectionGearSystem.GearSystemId < 1);
        }
    }
}
