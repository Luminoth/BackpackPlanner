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

using System.Threading;
using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner;

using SQLite.Net;
using SQLite.Net.Async;

using EnergonSoftware.BackpackPlanner.Database;

using TypeMock.ArrangeActAssert;

namespace BackpackPlanner.UnitTests
{
    public static class MockUtil
    {
        public static async Task MockTask()
        {
            await Task.Delay(0).ConfigureAwait(false);
        }

        /// <summary>
        /// Fakes database connections.
        /// </summary>
        /// <return>
        /// The fake-all database connection.
        /// </return>
        public static SQLiteAsyncConnection FakeDbConnections()
        {
            // fake the SQLite connections
            SQLiteConnectionWithLock fakeSQLiteConnection = Isolate.Fake.AllInstances<SQLiteConnectionWithLock>(Members.ReturnRecursiveFakes);
            SQLiteAsyncConnection fakeAsyncSQLiteConnection = Isolate.Fake.AllInstances<SQLiteAsyncConnection>(Members.ReturnRecursiveFakes);

            // TODO: this sucks, is there not a way
            // to have the property correctly initialized?
            // allowing the constructor to be called doesn't do it
            SemaphoreSlim mutex = new SemaphoreSlim(1);

            // fake the db wrapper
            DatabaseConnection fakeDbConnection = Isolate.Fake.AllInstances<DatabaseConnection>(Members.ReturnRecursiveFakes, ConstructorWillBe.Called);
            Isolate.WhenCalled(() => fakeDbConnection.Lock).WillReturn(mutex);
            Isolate.WhenCalled(() => fakeDbConnection.Connection).WillReturn(fakeSQLiteConnection);
            Isolate.WhenCalled(() => fakeDbConnection.AsyncConnection).WillReturn(fakeAsyncSQLiteConnection);
            Isolate.WhenCalled(() => fakeDbConnection.ConnectAsync(null, null)).DoInstead(x => MockTask());

            BackpackPlannerState fakePlannerState = Isolate.Fake.AllInstances<BackpackPlannerState>(Members.ReturnRecursiveFakes, ConstructorWillBe.Called);
            Isolate.WhenCalled(() => fakePlannerState.DatabaseConnection).WillReturn(fakeDbConnection);

            return fakeAsyncSQLiteConnection;
        }
    }
}
