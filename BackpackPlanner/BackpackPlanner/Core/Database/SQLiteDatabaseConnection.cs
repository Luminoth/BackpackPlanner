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
using System.Threading;
using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner.Core.Logging;

using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Interop;

namespace EnergonSoftware.BackpackPlanner.Core.Database
{
    /// <summary>
    /// Wrapper for a SQLite database connection.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public sealed class SQLiteDatabaseConnection : IDatabaseConnection
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(SQLiteDatabaseConnection));

        private readonly SemaphoreSlim _lock = new SemaphoreSlim(1);

        /// <summary>
        /// Gets a value indicating whether the connection is connected.
        /// </summary>
        /// <value>
        /// <c>true</c> if the connection is connected; otherwise, <c>false</c>.
        /// </value>
        public bool IsConnected => null != Connection;

        /// <summary>
        /// Gets the synchronous connection.
        /// </summary>
        /// <value>
        /// The synchronous connection.
        /// </value>
        public SQLiteConnectionWithLock Connection { get; private set; }

        /// <summary>
        /// Gets the asynchronous connection.
        /// </summary>
        /// <value>
        /// The asynchronous connection.
        /// </value>
        public SQLiteAsyncConnection AsyncConnection { get; private set; }

        private SQLiteConnectionString _connectionString;

#region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if(disposing) {
                _lock.Dispose();
            }
        }
#endregion

        ~SQLiteDatabaseConnection()
        {
            Dispose(false);
        }

        public async Task LockAsync()
        {
            await _lock.WaitAsync().ConfigureAwait(false);
        }

        public void Release()
        {
            _lock.Release();
        }

        public Task ConnectAsync(BackpackPlannerState state)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Connects to the database.
        /// </summary>
        /// <param name="state">The system state.</param>
        /// <param name="sqlitePlatform">The sqlite platform.</param>
        /// <param name="connectionString">The connection string.</param>
        public async Task ConnectAsync(BackpackPlannerState state, ISQLitePlatform sqlitePlatform, SQLiteConnectionString connectionString)
        {
            if(IsConnected) {
                throw new InvalidOperationException("Database connection already connected!");
            }

            _connectionString = connectionString;

            await LockAsync().ConfigureAwait(false);
            try {
                Logger.Debug($"Opening connection to database {_connectionString.ConnectionString}...");
                Connection = new SQLiteConnectionWithLock(sqlitePlatform, connectionString);
                AsyncConnection = new SQLiteAsyncConnection(() => Connection);
            } finally {
                Release();
            }
        }

        /// <summary>
        /// Closes the connection.
        /// </summary>
        public async Task CloseAsync()
        {
            if(!IsConnected) {
                return;
            }

            await LockAsync().ConfigureAwait(false);
            try {
                Logger.Debug($"Closing connection to database {_connectionString.ConnectionString}...");

                AsyncConnection = null;

                Connection.Close();
                Connection = null;

                _connectionString = null;
            } finally {
                Release();
            }
        }
    }
}
