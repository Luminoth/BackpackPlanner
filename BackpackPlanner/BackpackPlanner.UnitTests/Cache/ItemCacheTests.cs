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
using EnergonSoftware.BackpackPlanner.Models;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using MSTestExtensions;

using SQLite.Net.Async;
using SQLiteNetExtensionsAsync.Extensions;

using TypeMock.ArrangeActAssert;

namespace EnergonSoftware.BackpackPlanner.UnitTests.Cache
{
    [Isolated]
    public class ItemCacheTests<T, TV>: BaseTest where T: ItemCache<TV>, new() where TV: class, IItem, new()
    {
#region GetItemById
        [TestMethod]
        public void ItemCache_GetItemById_Negative_ArgumentException()
        {
            // Arrange
            T itemCache = new T();

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentException>(itemCache.GetItemByIdAsync(-1));
        }

        [TestMethod]
        public void ItemCache_GetItemById_Zero_ArgumentException()
        {
            // Arrange
            T itemCache = new T();

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentException>(itemCache.GetItemByIdAsync(0));
        }

        [TestMethod]
        public async Task ItemCache_GetItemById_NonExistant_Empty_Null()
        {
            // Isolate
            SQLiteAsyncConnection fakeAsyncDbConnection = MockUtil.FakeDbConnections();
            Isolate.WhenCalled(() => fakeAsyncDbConnection.GetWithChildrenAsync<TV>(0)).DoInstead(x => Task.FromResult((TV)null));

            // Arrange
            T itemCache = new T();

            // Act
            TV item = await itemCache.GetItemByIdAsync(1).ConfigureAwait(false);

            // Assert
            Assert.IsNull(item);
        }

        [TestMethod]
        public async Task ItemCache_GetItemById_NonExistant_Null()
        {
            // Isolate
            SQLiteAsyncConnection fakeAsyncDbConnection = MockUtil.FakeDbConnections();
            Isolate.WhenCalled(() => fakeAsyncDbConnection.GetWithChildrenAsync<TV>(0)).DoInstead(x => Task.FromResult((TV)null));

            // Arrange
            T itemCache = new T();
            TV insertItem = new TV
            {
                Id = 1
            };
            await itemCache.AddItemAsync(insertItem).ConfigureAwait(false);

            // Act
            TV item = await itemCache.GetItemByIdAsync(2).ConfigureAwait(false);

            // Assert
            Assert.IsNull(item);
        }

        [TestMethod]
        public async Task ItemCache_GetItemById_Success()
        {
            // Arrange
            T itemCache = new T();
            TV insertItem = new TV
            {
                Id = 1
            };
            await itemCache.AddItemAsync(insertItem).ConfigureAwait(false);

            // Act
            TV item = await itemCache.GetItemByIdAsync(1).ConfigureAwait(false);

            // Assert
            Assert.IsNotNull(item);
            Assert.AreEqual(1, item.Id);
        }
#endregion

#region AddItem
        [TestMethod]
        public void ItemCache_AddItem_Null_ArgumentNullException()
        {
            // Arrange
            T itemCache = new T();

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(itemCache.AddItemAsync(null));
        }

        [TestMethod]
        public async Task ItemCache_AddItem_NewItem_Success()
        {
            // Isolate
            SQLiteAsyncConnection fakeAsyncDbConnection = MockUtil.FakeDbConnections();
            Isolate.WhenCalled(() => fakeAsyncDbConnection.InsertWithChildrenAsync(null)).DoInstead(x =>
                {
                    ((TV)x.Parameters[1]).Id = 1;
                    return Task.FromResult(1);
                }
            );

            // Arrange
            T itemCache = new T();
            TV item = new TV();

            // Act
            int itemId = await itemCache.AddItemAsync(item).ConfigureAwait(false);

            // Assert
            Assert.AreEqual(1, itemId);
            Assert.AreEqual(itemId, item.Id);
            Assert.AreEqual(1, itemCache.ItemCount);
        }

        [TestMethod]
        public async Task ItemCache_AddItem_ExistingItem_Success()
        {
            // Arrange
            T itemCache = new T();
            TV item = new TV
            {
                Id = 1
            };

            // Act
            int itemId = await itemCache.AddItemAsync(item).ConfigureAwait(false);

            // Assert
            Assert.AreEqual(1, itemId);
            Assert.AreEqual(itemId, item.Id);
            Assert.AreEqual(1, itemCache.ItemCount);
        }

        [TestMethod]
        public async Task ItemCache_AddItem_DuplicateItem_InvalidId()
        {
            // Arrange
            T itemCache = new T();
            TV item = new TV
            {
                Id = 1
            };

            // Act
            int itemId_First = await itemCache.AddItemAsync(item).ConfigureAwait(false);
            int itemId_Second = await itemCache.AddItemAsync(item).ConfigureAwait(false);

            // Assert
            Assert.AreEqual(1, itemId_First);
            Assert.AreEqual(itemId_First, item.Id);
            Assert.AreEqual(-1, itemId_Second);
            Assert.AreEqual(1, itemCache.ItemCount);
        }
#endregion

#region RemoveItem
        [TestMethod]
        public void ItemCache_RemoveItem_Null_ArgumentNullException()
        {
            // Arrange
            T itemCache = new T();

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(itemCache.RemoveItemAsync(null));
        }

        [TestMethod]
        public void ItemCache_RemoveItem_InvalidId_Negative_ArgumentException()
        {
            // Arrange
            T itemCache = new T();
            TV item = new TV
            {
                Id = -1
            };

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentException>(itemCache.RemoveItemAsync(item));
        }

        [TestMethod]
        public void ItemCache_RemoveItem_InvalidId_Zero_ArgumentException()
        {
            // Arrange
            T itemCache = new T();
            TV item = new TV
            {
                Id = 0
            };

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentException>(itemCache.RemoveItemAsync(item));
        }

        [TestMethod]
        public async Task ItemCache_RemoveItem_Success()
        {
            // Isolate
            SQLiteAsyncConnection fakeAsyncDbConnection = MockUtil.FakeDbConnections();
            Isolate.WhenCalled(() => fakeAsyncDbConnection.InsertWithChildrenAsync(null)).DoInstead(x =>
                {
                    ((TV)x.Parameters[1]).Id = 1;
                    return Task.FromResult(1);
                }
            );

            // Arrange
            T itemCache = new T();
            TV item = new TV();

            await itemCache.AddItemAsync(item).ConfigureAwait(false);

            // Act
            await itemCache.RemoveItemAsync(item).ConfigureAwait(false);

            // Assert
            Assert.AreEqual(0, itemCache.ItemCount);
        }
#endregion

#region RemoveAllItems
        [TestMethod]
        public async Task ItemCache_RemoveAllItems_Empty_Success()
        {
            // Isolate
            SQLiteAsyncConnection fakeAsyncDbConnection = MockUtil.FakeDbConnections();
            Isolate.WhenCalled(() => fakeAsyncDbConnection.InsertWithChildrenAsync(null)).DoInstead(x =>
                {
                    ((TV)x.Parameters[0]).Id = 1;
                    return Task.FromResult(1);
                }
            );

            // Arrange
            T itemCache = new T();

            // Act
            await itemCache.RemoveAllItemsAsync().ConfigureAwait(false);

            // Assert
            Assert.AreEqual(0, itemCache.ItemCount);
        }

        [TestMethod]
        public async Task ItemCache_RemoveAllItems_Success()
        {
            // Isolate
            SQLiteAsyncConnection fakeAsyncDbConnection = MockUtil.FakeDbConnections();
            Isolate.WhenCalled(() => fakeAsyncDbConnection.InsertWithChildrenAsync(null)).DoInstead(x =>
                {
                    ((TV)x.Parameters[1]).Id = 1;
                    return Task.FromResult(1);
                }
            );

            // Arrange
            T itemCache = new T();

            for(int i=0; i<100; ++i) {
                await itemCache.AddItemAsync(new TV { Id = i }).ConfigureAwait(false);
            }

            // Act
            await itemCache.RemoveAllItemsAsync().ConfigureAwait(false);

            // Assert
            Assert.AreEqual(0, itemCache.ItemCount);
        }
#endregion
    }
}
