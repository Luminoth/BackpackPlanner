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

using EnergonSoftware.BackpackPlanner.Cache.Trips;
using EnergonSoftware.BackpackPlanner.Models.Trips.Plans;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using MSTestExtensions;

using SQLite.Net.Async;

using TypeMock.ArrangeActAssert;

namespace EnergonSoftware.BackpackPlanner.UnitTests.Cache.Trips
{
    [TestClass, Isolated]
    public class TripPlanCacheTests : ItemCacheTests<TripPlanCache, TripPlan>
    {
#region InitDatabase
        [TestMethod]
        public void TripPlanCache_InitDatabase_NullConnection()
        {
            // Arrange

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(TripPlanCache.InitDatabaseAsync(null, 0, 0));
        }

        [TestMethod]
        public async Task TripPlanCache_InitDatabase_OldVersionEqual()
        {
            // Isolate
            SQLiteAsyncConnection fakeAsyncDbConnection = MockUtil.FakeDbConnections();

            // Arrange

            // Act
            await TripPlanCache.InitDatabaseAsync(fakeAsyncDbConnection, 0, 0).ConfigureAwait(false);

            // Assert
        }

        [TestMethod]
        public async Task TripPlanCache_InitDatabase_OldVersionLarger()
        {
            // Isolate
            SQLiteAsyncConnection fakeAsyncDbConnection = MockUtil.FakeDbConnections();

            // Arrange

            // Act
            await TripPlanCache.InitDatabaseAsync(fakeAsyncDbConnection, 1, 0).ConfigureAwait(false);

            // Assert
        }

        [TestMethod]
        public async Task TripPlanCache_InitDatabase_NewDatabase()
        {
            // Isolate
            SQLiteAsyncConnection fakeAsyncDbConnection = MockUtil.FakeDbConnections();

            Isolate.Fake.StaticMethods(typeof(TripPlan), Members.CallOriginal);

            // Arrange

            // Act
            await TripPlanCache.InitDatabaseAsync(fakeAsyncDbConnection, -1, 2).ConfigureAwait(false);

            // Assert
            Isolate.Verify.WasCalledWithExactArguments(() => TripPlan.CreateTablesAsync(fakeAsyncDbConnection));
        }
#endregion
    }
}
