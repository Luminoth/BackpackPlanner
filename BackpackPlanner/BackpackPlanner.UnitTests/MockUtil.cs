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

using SQLite.Net;
using SQLite.Net.Async;

using EnergonSoftware.BackpackPlanner.Core.Database;

using TypeMock.ArrangeActAssert;

namespace EnergonSoftware.BackpackPlanner.UnitTests
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

            // fake the db wrapper
            SQLiteDatabaseConnection fakeDbConnection = Isolate.Fake.AllInstances<SQLiteDatabaseConnection>(Members.ReturnRecursiveFakes, ConstructorWillBe.Called);
            Isolate.WhenCalled(() => fakeDbConnection.IsConnected).WillReturn(true);
            Isolate.WhenCalled(() => fakeDbConnection.Connection).WillReturn(fakeSQLiteConnection);
            Isolate.WhenCalled(() => fakeDbConnection.AsyncConnection).WillReturn(fakeAsyncSQLiteConnection);
            Isolate.WhenCalled(() => fakeDbConnection.LockAsync()).DoInstead(x => MockTask());
            Isolate.WhenCalled(() => fakeDbConnection.Release()).IgnoreCall();
            Isolate.WhenCalled(() => fakeDbConnection.ConnectAsync(null, null)).DoInstead(x => MockTask());
            Isolate.WhenCalled(() => fakeDbConnection.CloseAsync()).DoInstead(x => MockTask());

            BackpackPlannerState fakePlannerState = Isolate.Fake.AllInstances<BackpackPlannerState>(Members.ReturnRecursiveFakes, ConstructorWillBe.Called);
            Isolate.WhenCalled(() => fakePlannerState.DatabaseConnection).WillReturn(fakeDbConnection);

            return fakeAsyncSQLiteConnection;
        }
    }
}
