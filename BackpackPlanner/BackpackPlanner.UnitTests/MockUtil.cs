using EnergonSoftware.BackpackPlanner;

using SQLite.Net;
using SQLite.Net.Async;
using TypeMock.ArrangeActAssert;

namespace BackpackPlanner.UnitTests
{
    public static class MockUtil
    {
        /// <summary>
        /// Fakes database connections.
        /// </summary>
        /// <return>
        /// The fake-all database connection.
        /// </return>
        public static SQLiteAsyncConnection FakeDbConnections()
        {
            SQLiteConnectionWithLock fakedbConnection = Isolate.Fake.AllInstances<SQLiteConnectionWithLock>(Members.ReturnRecursiveFakes);
            SQLiteAsyncConnection fakeAsyncDbConnection = Isolate.Fake.AllInstances<SQLiteAsyncConnection>(Members.ReturnRecursiveFakes);

            BackpackPlannerState fakePlannerState = Isolate.Fake.AllInstances<BackpackPlannerState>(Members.ReturnRecursiveFakes);
            // TODO: fix this
            //Isolate.WhenCalled(() => fakePlannerState.GetDatabaseConnection()).WillReturn(fakedbConnection);

            return fakeAsyncDbConnection;
        }
    }
}
