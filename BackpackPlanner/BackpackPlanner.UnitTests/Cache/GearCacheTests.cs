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

using EnergonSoftware.BackpackPlanner.Cache;
using EnergonSoftware.BackpackPlanner.Models.Gear.Collections;
using EnergonSoftware.BackpackPlanner.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.Models.Gear.Systems;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using MSTestExtensions;

using SQLite.Net.Async;
using SQLiteNetExtensionsAsync.Extensions;

using TypeMock.ArrangeActAssert;

namespace BackpackPlanner.UnitTests.Cache
{
    [TestClass, Isolated]
    public class GearCacheTests : BaseTest
    {
#region InitDatabase
        [TestMethod]
        public void GearCache_InitDatabase_NullConnection()
        {
            // Arrange

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(GearCache.InitDatabaseAsync(null, 0, 0));
        }

        [TestMethod]
        public async Task GearCache_InitDatabase_OldVersionEqual()
        {
            // Isolate
            SQLiteAsyncConnection fakeAsyncDbConnection = MockUtil.FakeDbConnections();

            // Arrange

            // Act
            await GearCache.InitDatabaseAsync(fakeAsyncDbConnection, 0, 0).ConfigureAwait(false);

            // Assert
        }

        [TestMethod]
        public async Task GearCache_InitDatabase_OldVersionLarger()
        {
            // Isolate
            SQLiteAsyncConnection fakeAsyncDbConnection = MockUtil.FakeDbConnections();

            // Arrange

            // Act
            await GearCache.InitDatabaseAsync(fakeAsyncDbConnection, 1, 0).ConfigureAwait(false);

            // Assert
        }

        [TestMethod]
        public async Task GearCache_InitDatabase_NewDatabase()
        {
            // Isolate
            SQLiteAsyncConnection fakeAsyncDbConnection = MockUtil.FakeDbConnections();

            Isolate.Fake.StaticMethods(typeof(GearItem), Members.CallOriginal);

            // Arrange

            // Act
            await GearCache.InitDatabaseAsync(fakeAsyncDbConnection, -1, 1).ConfigureAwait(false);

            // Assert
            Isolate.Verify.WasCalledWithExactArguments(() => GearItem.CreateTablesAsync(fakeAsyncDbConnection));
        }
#endregion

#region GetGearItemById
        [TestMethod]
        public void GearCache_GetGearItemById_Negative_ArgumentException()
        {
            // Arrange
            GearCache gearCache = new GearCache();

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentException>(gearCache.GetGearItemByIdAsync(-1));
        }

        [TestMethod]
        public void GearCache_GetGearItemById_Zero_ArgumentException()
        {
            // Arrange
            GearCache gearCache = new GearCache();

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentException>(gearCache.GetGearItemByIdAsync(0));
        }

        [TestMethod]
        public async Task GearCache_GetGearItemById_NonExistant_Empty_Null()
        {
            // Isolate
            SQLiteAsyncConnection fakeAsyncDbConnection = MockUtil.FakeDbConnections();
            Isolate.WhenCalled(() => fakeAsyncDbConnection.GetWithChildrenAsync<GearItem>(0)).DoInstead(x => Task.FromResult((GearItem)null));

            // Arrange
            GearCache gearCache = new GearCache();

            // Act
            GearItem gearItem = await gearCache.GetGearItemByIdAsync(1).ConfigureAwait(false);

            // Assert
            Assert.IsNull(gearItem);
        }

        [TestMethod]
        public async Task GearCache_GetGearItemById_NonExistant_Null()
        {
            // Isolate
            SQLiteAsyncConnection fakeAsyncDbConnection = MockUtil.FakeDbConnections();
            Isolate.WhenCalled(() => fakeAsyncDbConnection.GetWithChildrenAsync<GearItem>(0)).DoInstead(x => Task.FromResult((GearItem)null));

            // Arrange
            GearCache gearCache = new GearCache();
            GearItem insertGearItem = new GearItem
            {
                GearItemId = 1
            };
            await gearCache.AddGearItemAsync(insertGearItem).ConfigureAwait(false);

            // Act
            GearItem gearItem = await gearCache.GetGearItemByIdAsync(2).ConfigureAwait(false);

            // Assert
            Assert.IsNull(gearItem);
        }

        [TestMethod]
        public async Task GearCache_GetGearItemById_Success()
        {
            // Arrange
            GearCache gearCache = new GearCache();
            GearItem insertGearItem = new GearItem
            {
                GearItemId = 1
            };
            await gearCache.AddGearItemAsync(insertGearItem).ConfigureAwait(false);

            // Act
            GearItem gearItem = await gearCache.GetGearItemByIdAsync(1).ConfigureAwait(false);

            // Assert
            Assert.IsNotNull(gearItem);
            Assert.AreEqual(1, gearItem.GearItemId);
        }
#endregion

#region AddGearItem
        /*[TestMethod]
        public void GearCache_AddGearItem_Null_ArgumentNullException()
        {
            // Arrange
            GearCache gearCache = new GearCache();

            // Act
            ExceptionAssert.Throws<ArgumentNullException>(async () => await gearCache.AddGearItemAsync(null).ConfigureAwait(false));

            // Assert
        }*/

        [TestMethod]
        public async Task GearCache_AddGearItem_NewItem_Success()
        {
            // Isolate
            SQLiteAsyncConnection fakeAsyncDbConnection = MockUtil.FakeDbConnections();
            Isolate.WhenCalled(() => fakeAsyncDbConnection.InsertWithChildrenAsync(null)).DoInstead(x =>
                {
                    ((GearItem)x.Parameters[1]).GearItemId = 1;
                    return Task.FromResult(1);
                }
            );

            // Arrange
            GearCache gearCache = new GearCache();
            GearItem gearItem = new GearItem();

            // Act
            int gearItemId = await gearCache.AddGearItemAsync(gearItem).ConfigureAwait(false);

            // Assert
            Assert.AreEqual(1, gearItemId);
            Assert.AreEqual(gearItemId, gearItem.GearItemId);
            Assert.AreEqual(1, gearCache.GearItemCount);
        }

        [TestMethod]
        public async Task GearCache_AddGearItem_ExistingItem_Success()
        {
            // Arrange
            GearCache gearCache = new GearCache();
            GearItem gearItem = new GearItem
            {
                GearItemId = 1
            };

            // Act
            int gearItemId = await gearCache.AddGearItemAsync(gearItem).ConfigureAwait(false);

            // Assert
            Assert.AreEqual(1, gearItemId);
            Assert.AreEqual(gearItemId, gearItem.GearItemId);
            Assert.AreEqual(1, gearCache.GearItemCount);
        }

        [TestMethod]
        public async Task GearCache_AddGearItem_DuplicateItem_InvalidId()
        {
            // Arrange
            GearCache gearCache = new GearCache();
            GearItem gearItem = new GearItem
            {
                GearItemId = 1
            };

            // Act
            int gearItemId_First = await gearCache.AddGearItemAsync(gearItem).ConfigureAwait(false);
            int gearItemId_Second = await gearCache.AddGearItemAsync(gearItem).ConfigureAwait(false);

            // Assert
            Assert.AreEqual(1, gearItemId_First);
            Assert.AreEqual(gearItemId_First, gearItem.GearItemId);
            Assert.AreEqual(-1, gearItemId_Second);
            Assert.AreEqual(1, gearCache.GearItemCount);
        }
#endregion

#region RemoveGearItem
        [TestMethod]
        public void GearCache_RemoveGearItem_Null_ArgumentNullException()
        {
            // Arrange
            GearCache gearCache = new GearCache();

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(gearCache.RemoveGearItemAsync(null));
        }

        [TestMethod]
        public void GearCache_RemoveGearItem_InvalidId_Negative_ArgumentException()
        {
            // Arrange
            GearCache gearCache = new GearCache();
            GearItem gearItem = new GearItem
            {
                GearItemId = -1
            };

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentException>(gearCache.RemoveGearItemAsync(gearItem));
        }

        [TestMethod]
        public void GearCache_RemoveGearItem_InvalidId_Zero_ArgumentException()
        {
            // Arrange
            GearCache gearCache = new GearCache();
            GearItem gearItem = new GearItem
            {
                GearItemId = 0
            };

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentException>(gearCache.RemoveGearItemAsync(gearItem));
        }

        [TestMethod]
        public async Task GearCache_RemoveGearItem_Success()
        {
            // Isolate
            SQLiteAsyncConnection fakeAsyncDbConnection = MockUtil.FakeDbConnections();
            Isolate.WhenCalled(() => fakeAsyncDbConnection.InsertWithChildrenAsync(null)).DoInstead(x =>
                {
                    ((GearItem)x.Parameters[1]).GearItemId = 1;
                    return Task.FromResult(1);
                }
            );

            // Arrange
            GearCache gearCache = new GearCache();
            GearItem gearItem = new GearItem();

            await gearCache.AddGearItemAsync(gearItem).ConfigureAwait(false);

            // Act
            await gearCache.RemoveGearItemAsync(gearItem).ConfigureAwait(false);

            // Assert
            Assert.AreEqual(0, gearCache.GearItemCount);
        }
#endregion

#region RemoveAllGearItems
        [TestMethod]
        public async Task GearCache_RemoveAllGearItems_Empty_Success()
        {
            // Isolate
            SQLiteAsyncConnection fakeAsyncDbConnection = MockUtil.FakeDbConnections();
            Isolate.WhenCalled(() => fakeAsyncDbConnection.InsertWithChildrenAsync(null)).DoInstead(x =>
                {
                    ((GearCollection)x.Parameters[0]).GearCollectionId = 1;
                    return Task.FromResult(1);
                }
            );

            // Arrange
            GearCache gearCache = new GearCache();

            // Act
            await gearCache.RemoveAllGearItemsAsync().ConfigureAwait(false);

            // Assert
            Assert.AreEqual(0, gearCache.GearItemCount);
        }

        [TestMethod]
        public async Task GearCache_RemoveAllGearItems_Success()
        {
            // Isolate
            SQLiteAsyncConnection fakeAsyncDbConnection = MockUtil.FakeDbConnections();
            Isolate.WhenCalled(() => fakeAsyncDbConnection.InsertWithChildrenAsync(null)).DoInstead(x =>
                {
                    ((GearItem)x.Parameters[1]).GearItemId = 1;
                    return Task.FromResult(1);
                }
            );

            // Arrange
            GearCache gearCache = new GearCache();

            for(int i=0; i<100; ++i) {
                await gearCache.AddGearItemAsync(new GearItem { GearItemId = i }).ConfigureAwait(false);
            }

            // Act
            await gearCache.RemoveAllGearItemsAsync().ConfigureAwait(false);

            // Assert
            Assert.AreEqual(0, gearCache.GearItemCount);
        }
#endregion

#region GetGearSystemById
        [TestMethod]
        public void GearCache_GetGearSystemById_Negative_ArgumentException()
        {
            // Arrange
            GearCache gearCache = new GearCache();

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentException>(gearCache.GetGearSystemByIdAsync(-1));
        }

        [TestMethod]
        public void GearCache_GetGearSystemById_Zero_ArgumentException()
        {
            // Arrange
            GearCache gearCache = new GearCache();

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentException>(gearCache.GetGearSystemByIdAsync(0));
        }

        [TestMethod]
        public async Task GearCache_GetGearSystemById_NonExistant_Empty_Null()
        {
            // Isolate
            SQLiteAsyncConnection fakeAsyncDbConnection = MockUtil.FakeDbConnections();
            Isolate.WhenCalled(() => fakeAsyncDbConnection.GetWithChildrenAsync<GearSystem>(0)).DoInstead(x => Task.FromResult((GearSystem)null));

            // Arrange
            GearCache gearCache = new GearCache();

            // Act
            GearSystem gearSystem = await gearCache.GetGearSystemByIdAsync(1).ConfigureAwait(false);

            // Assert
            Assert.IsNull(gearSystem);
        }

        [TestMethod]
        public async Task GearCache_GetGearSystemById_NonExistant_Null()
        {
            // Isolate
            SQLiteAsyncConnection fakeAsyncDbConnection = MockUtil.FakeDbConnections();
            Isolate.WhenCalled(() => fakeAsyncDbConnection.GetWithChildrenAsync<GearSystem>(0)).DoInstead(x => Task.FromResult((GearSystem)null));

            // Arrange
            GearCache gearCache = new GearCache();
            GearSystem insertGearSystem = new GearSystem
            {
                GearSystemId = 1
            };
            await gearCache.AddGearSystemAsync(insertGearSystem).ConfigureAwait(false);

            // Act
            GearSystem gearSystem = await gearCache.GetGearSystemByIdAsync(2).ConfigureAwait(false);

            // Assert
            Assert.IsNull(gearSystem);
        }

        [TestMethod]
        public async Task GearCache_GetGearSystemById_Success()
        {
            // Arrange
            GearCache gearCache = new GearCache();
            GearSystem insertGearSystem = new GearSystem
            {
                GearSystemId = 1
            };
            await gearCache.AddGearSystemAsync(insertGearSystem).ConfigureAwait(false);

            // Act
            GearSystem gearSystem = await gearCache.GetGearSystemByIdAsync(1).ConfigureAwait(false);

            // Assert
            Assert.IsNotNull(gearSystem);
            Assert.AreEqual(1, gearSystem.GearSystemId);
        }
#endregion

#region AddGearSystem
        /*[TestMethod]
        public void GearCache_AddGearSystem_Null_ArgumentNullException()
        {
            // Arrange
            GearCache gearCache = new GearCache();

            // Act
            ExceptionAssert.Throws<ArgumentNullException>(async () => await gearCache.AddGearSystemAsync(null).ConfigureAwait(false));

            // Assert
        }*/

        [TestMethod]
        public async Task GearCache_AddGearSystem_NewSystem_Success()
        {
            // Isolate
            SQLiteAsyncConnection fakeAsyncDbConnection = MockUtil.FakeDbConnections();
            Isolate.WhenCalled(() => fakeAsyncDbConnection.InsertWithChildrenAsync(null)).DoInstead(x =>
                {
                    ((GearSystem)x.Parameters[1]).GearSystemId = 1;
                    return Task.FromResult(1);
                }
            );

            // Arrange
            GearCache gearCache = new GearCache();
            GearSystem gearSystem = new GearSystem();

            // Act
            int gearSystemId = await gearCache.AddGearSystemAsync(gearSystem).ConfigureAwait(false);

            // Assert
            Assert.AreEqual(1, gearSystemId);
            Assert.AreEqual(gearSystemId, gearSystem.GearSystemId);
            Assert.AreEqual(1, gearCache.GearSystemCount);
        }

        [TestMethod]
        public async Task GearCache_AddGearSystem_ExistingSystem_Success()
        {
            // Arrange
            GearCache gearCache = new GearCache();
            GearSystem gearSystem = new GearSystem
            {
                GearSystemId = 1
            };

            // Act
            int gearSystemId = await gearCache.AddGearSystemAsync(gearSystem).ConfigureAwait(false);

            // Assert
            Assert.AreEqual(1, gearSystemId);
            Assert.AreEqual(gearSystemId, gearSystem.GearSystemId);
            Assert.AreEqual(1, gearCache.GearSystemCount);
        }

        [TestMethod]
        public async Task GearCache_AddGearSystem_DuplicateSystem_InvalidId()
        {
            // Arrange
            GearCache gearCache = new GearCache();
            GearSystem gearSystem = new GearSystem
            {
                GearSystemId = 1
            };

            // Act
            int gearSystemId_First = await gearCache.AddGearSystemAsync(gearSystem).ConfigureAwait(false);
            int gearSystemId_Second = await gearCache.AddGearSystemAsync(gearSystem).ConfigureAwait(false);

            // Assert
            Assert.AreEqual(1, gearSystemId_First);
            Assert.AreEqual(gearSystemId_First, gearSystem.GearSystemId);
            Assert.AreEqual(-1, gearSystemId_Second);
            Assert.AreEqual(1, gearCache.GearSystemCount);
        }
#endregion

#region RemoveGearSystem
        [TestMethod]
        public void GearCache_RemoveGearSystem_Null_ArgumentNullException()
        {
            // Arrange
            GearCache gearCache = new GearCache();

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(gearCache.RemoveGearSystemAsync(null));
        }

        [TestMethod]
        public void GearCache_RemoveGearSystem_InvalidId_Negative_ArgumentException()
        {
            // Arrange
            GearCache gearCache = new GearCache();
            GearSystem gearSystem = new GearSystem
            {
                GearSystemId = -1
            };

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentException>(gearCache.RemoveGearSystemAsync(gearSystem));
        }

        [TestMethod]
        public void GearCache_RemoveGearSystem_InvalidId_Zero_ArgumentException()
        {
            // Arrange
            GearCache gearCache = new GearCache();
            GearSystem gearSystem = new GearSystem
            {
                GearSystemId = 0
            };

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentException>(gearCache.RemoveGearSystemAsync(gearSystem));
        }

        [TestMethod]
        public async Task GearCache_RemoveGearSystem_Success()
        {
            // Isolate
            SQLiteAsyncConnection fakeAsyncDbConnection = MockUtil.FakeDbConnections();
            Isolate.WhenCalled(() => fakeAsyncDbConnection.InsertWithChildrenAsync(null)).DoInstead(x =>
                {
                    ((GearSystem)x.Parameters[1]).GearSystemId = 1;
                    return Task.FromResult(1);
                }
            );

            // Arrange
            GearCache gearCache = new GearCache();
            GearSystem gearSystem = new GearSystem();

            await gearCache.AddGearSystemAsync(gearSystem).ConfigureAwait(false);

            // Act
            await gearCache.RemoveGearSystemAsync(gearSystem).ConfigureAwait(false);

            // Assert
            Assert.AreEqual(0, gearCache.GearSystemCount);
        }
#endregion

#region RemoveAllGearSystems
        [TestMethod]
        public async Task GearCache_RemoveAllGearSystems_Empty_Success()
        {
            // Isolate
            SQLiteAsyncConnection fakeAsyncDbConnection = MockUtil.FakeDbConnections();
            Isolate.WhenCalled(() => fakeAsyncDbConnection.InsertWithChildrenAsync(null)).DoInstead(x =>
                {
                    ((GearCollection)x.Parameters[0]).GearCollectionId = 1;
                    return Task.FromResult(1);
                }
            );

            // Arrange
            GearCache gearCache = new GearCache();

            // Act
            await gearCache.RemoveAllGearSystemsAsync().ConfigureAwait(false);

            // Assert
            Assert.AreEqual(0, gearCache.GearSystemCount);
        }

        [TestMethod]
        public async Task GearCache_RemoveAllGearSystems_Success()
        {
            // Isolate
            SQLiteAsyncConnection fakeAsyncDbConnection = MockUtil.FakeDbConnections();
            Isolate.WhenCalled(() => fakeAsyncDbConnection.InsertWithChildrenAsync(null)).DoInstead(x =>
                {
                    ((GearSystem)x.Parameters[1]).GearSystemId = 1;
                    return Task.FromResult(1);
                }
            );

            // Arrange
            GearCache gearCache = new GearCache();

            for(int i=0; i<100; ++i) {
                await gearCache.AddGearSystemAsync(new GearSystem { GearSystemId = i }).ConfigureAwait(false);
            }

            // Act
            await gearCache.RemoveAllGearSystemsAsync().ConfigureAwait(false);

            // Assert
            Assert.AreEqual(0, gearCache.GearSystemCount);
        }
#endregion

#region GetGearCollectionById
        [TestMethod]
        public void GearCache_GetGearCollectionById_Negative_ArgumentException()
        {
            // Arrange
            GearCache gearCache = new GearCache();

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentException>(gearCache.GetGearCollectionByIdAsync(-1));
        }

        [TestMethod]
        public void GearCache_GetGearCollectionById_Zero_ArgumentException()
        {
            // Arrange
            GearCache gearCache = new GearCache();

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentException>(gearCache.GetGearCollectionByIdAsync(0));
        }

        [TestMethod]
        public async Task GearCache_GetGearCollectionById_NonExistant_Empty_Null()
        {
            // Isolate
            SQLiteAsyncConnection fakeAsyncDbConnection = MockUtil.FakeDbConnections();
            Isolate.WhenCalled(() => fakeAsyncDbConnection.GetWithChildrenAsync<GearCollection>(0)).DoInstead(x => Task.FromResult((GearCollection)null));

            // Arrange
            GearCache gearCache = new GearCache();

            // Act
            GearCollection gearCollection = await gearCache.GetGearCollectionByIdAsync(1).ConfigureAwait(false);

            // Assert
            Assert.IsNull(gearCollection);
        }

        [TestMethod]
        public async Task GearCache_GetGearCollectionById_NonExistant_Null()
        {
            // Isolate
            SQLiteAsyncConnection fakeAsyncDbConnection = MockUtil.FakeDbConnections();
            Isolate.WhenCalled(() => fakeAsyncDbConnection.GetWithChildrenAsync<GearCollection>(0)).DoInstead(x => Task.FromResult((GearCollection)null));

            // Arrange
            GearCache gearCache = new GearCache();
            GearCollection insertGearCollection = new GearCollection
            {
                GearCollectionId = 1
            };
            await gearCache.AddGearCollectionAsync(insertGearCollection).ConfigureAwait(false);

            // Act
            GearCollection gearCollection = await gearCache.GetGearCollectionByIdAsync(2).ConfigureAwait(false);

            // Assert
            Assert.IsNull(gearCollection);
        }

        [TestMethod]
        public async Task GearCache_GetGearCollectionById_Success()
        {
            // Arrange
            GearCache gearCache = new GearCache();
            GearCollection insertGearCollection = new GearCollection
            {
                GearCollectionId = 1
            };
            await gearCache.AddGearCollectionAsync(insertGearCollection).ConfigureAwait(false);

            // Act
            GearCollection gearCollection = await gearCache.GetGearCollectionByIdAsync(1).ConfigureAwait(false);

            // Assert
            Assert.IsNotNull(gearCollection);
            Assert.AreEqual(1, gearCollection.GearCollectionId);
        }
#endregion

#region AddGearCollection
        /*[TestMethod]
        public void GearCache_AddGearCollection_Null_ArgumentNullException()
        {
            // Arrange
            GearCache gearCache = new GearCache();

            // Act
            ExceptionAssert.Throws<ArgumentNullException>(async () => await gearCache.AddGearCollectionAsync(null).ConfigureAwait(false));

            // Assert
        }*/

        [TestMethod]
        public async Task GearCache_AddGearCollection_NewCollection_Success()
        {
            // Isolate
            SQLiteAsyncConnection fakeAsyncDbConnection = MockUtil.FakeDbConnections();
            Isolate.WhenCalled(() => fakeAsyncDbConnection.InsertWithChildrenAsync(null)).DoInstead(x =>
                {
                    ((GearCollection)x.Parameters[1]).GearCollectionId = 1;
                    return Task.FromResult(1);
                }
            );

            // Arrange
            GearCache gearCache = new GearCache();
            GearCollection gearCollection = new GearCollection();

            // Act
            int gearCollectionId = await gearCache.AddGearCollectionAsync(gearCollection).ConfigureAwait(false);

            // Assert
            Assert.AreEqual(1, gearCollectionId);
            Assert.AreEqual(gearCollectionId, gearCollection.GearCollectionId);
            Assert.AreEqual(1, gearCache.GearCollectionCount);
        }

        [TestMethod]
        public async Task GearCache_AddGearCollection_ExistingCollection_Success()
        {
            // Arrange
            GearCache gearCache = new GearCache();
            GearCollection gearCollection = new GearCollection
            {
                GearCollectionId = 1
            };

            // Act
            int gearCollectionId = await gearCache.AddGearCollectionAsync(gearCollection).ConfigureAwait(false);

            // Assert
            Assert.AreEqual(1, gearCollectionId);
            Assert.AreEqual(gearCollectionId, gearCollection.GearCollectionId);
            Assert.AreEqual(1, gearCache.GearCollectionCount);
        }

        [TestMethod]
        public async Task GearCache_AddGearCollection_DuplicateCollection_InvalidId()
        {
            // Arrange
            GearCache gearCache = new GearCache();
            GearCollection gearCollection = new GearCollection
            {
                GearCollectionId = 1
            };

            // Act
            int gearCollectionId_First = await gearCache.AddGearCollectionAsync(gearCollection).ConfigureAwait(false);
            int gearCollectionId_Second = await gearCache.AddGearCollectionAsync(gearCollection).ConfigureAwait(false);

            // Assert
            Assert.AreEqual(1, gearCollectionId_First);
            Assert.AreEqual(gearCollectionId_First, gearCollection.GearCollectionId);
            Assert.AreEqual(-1, gearCollectionId_Second);
            Assert.AreEqual(1, gearCache.GearCollectionCount);
        }
#endregion

#region RemoveGearCollection
        [TestMethod]
        public void GearCache_RemoveGearCollection_Null_ArgumentNullException()
        {
            // Arrange
            GearCache gearCache = new GearCache();

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(gearCache.RemoveGearCollectionAsync(null));
        }

        [TestMethod]
        public void GearCache_RemoveGearCollection_InvalidId_Negative_ArgumentException()
        {
            // Arrange
            GearCache gearCache = new GearCache();
            GearCollection gearCollection = new GearCollection
            {
                GearCollectionId = -1
            };

            // Act
            Assert.ThrowsAsync<ArgumentException>(gearCache.RemoveGearCollectionAsync(gearCollection));

            // Assert
        }

        [TestMethod]
        public void GearCache_RemoveGearCollection_InvalidId_Zero_ArgumentException()
        {
            // Arrange
            GearCache gearCache = new GearCache();
            GearCollection gearCollection = new GearCollection
            {
                GearCollectionId = 0
            };

            // Act
            Assert.ThrowsAsync<ArgumentException>(gearCache.RemoveGearCollectionAsync(gearCollection));

            // Assert
        }

        [TestMethod]
        public async Task GearCache_RemoveGearCollection_Success()
        {
            // Isolate
            SQLiteAsyncConnection fakeAsyncDbConnection = MockUtil.FakeDbConnections();
            Isolate.WhenCalled(() => fakeAsyncDbConnection.InsertWithChildrenAsync(null)).DoInstead(x =>
                {
                    ((GearCollection)x.Parameters[1]).GearCollectionId = 1;
                    return Task.FromResult(1);
                }
            );

            // Arrange
            GearCache gearCache = new GearCache();
            GearCollection gearCollection = new GearCollection();

            await gearCache.AddGearCollectionAsync(gearCollection).ConfigureAwait(false);

            // Act
            await gearCache.RemoveGearCollectionAsync(gearCollection).ConfigureAwait(false);

            // Assert
            Assert.AreEqual(0, gearCache.GearCollectionCount);
        }
#endregion

#region RemoveAllGearCollections
        [TestMethod]
        public async Task GearCache_RemoveAllGearCollections_Empty_Success()
        {
            // Isolate
            SQLiteAsyncConnection fakeAsyncDbConnection = MockUtil.FakeDbConnections();
            Isolate.WhenCalled(() => fakeAsyncDbConnection.InsertWithChildrenAsync(null)).DoInstead(x =>
                {
                    ((GearCollection)x.Parameters[0]).GearCollectionId = 1;
                    return Task.FromResult(1);
                }
            );

            // Arrange
            GearCache gearCache = new GearCache();

            // Act
            await gearCache.RemoveAllGearCollectionsAsync().ConfigureAwait(false);

            // Assert
            Assert.AreEqual(0, gearCache.GearCollectionCount);
        }

        [TestMethod]
        public async Task GearCache_RemoveAllGearCollections_Success()
        {
            // Isolate
            SQLiteAsyncConnection fakeAsyncDbConnection = MockUtil.FakeDbConnections();
            Isolate.WhenCalled(() => fakeAsyncDbConnection.InsertWithChildrenAsync(null)).DoInstead(x =>
                {
                    ((GearCollection)x.Parameters[1]).GearCollectionId = 1;
                    return Task.FromResult(1);
                }
            );

            // Arrange
            GearCache gearCache = new GearCache();

            for(int i=0; i<100; ++i) {
                await gearCache.AddGearCollectionAsync(new GearCollection { GearCollectionId = i }).ConfigureAwait(false);
            }

            // Act
            await gearCache.RemoveAllGearCollectionsAsync().ConfigureAwait(false);

            // Assert
            Assert.AreEqual(0, gearCache.GearCollectionCount);
        }
#endregion
    }
}
