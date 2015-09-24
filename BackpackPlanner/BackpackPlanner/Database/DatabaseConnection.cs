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

using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Interop;

namespace EnergonSoftware.BackpackPlanner.Database
{
    /// <summary>
    /// Locking wrapper for a database connection
    /// </summary>
    /// <remarks>
    /// This may not be super necessary because SQLiteConnectionWithLock
    /// has a lock, but it releases that lock in Dispose() which isn't
    /// something that really guarantees the lock is released, right?
    /// </remarks>
    public sealed class DatabaseConnection
    {
        /// <summary>
        /// Gets the lock.
        /// </summary>
        /// <value>
        /// The lock.
        /// </value>
        /// <remarks>
        /// This should be locked before any operation
        /// and released immediately after
        /// </remarks>
        public SemaphoreSlim Lock { get; } = new SemaphoreSlim(1);

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

        /// <summary>
        /// Gets or sets a value indicating whether the connection is connected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the connection is connected; otherwise, <c>false</c>.
        /// </value>
        public bool IsConnected => null != Connection;

        /// <summary>
        /// Connects to the database.
        /// </summary>
        /// <param name="sqlitePlatform">The sqlite platform.</param>
        /// <param name="connectionString">The connection string.</param>
        public async Task ConnectAsync(ISQLitePlatform sqlitePlatform, SQLiteConnectionString connectionString)
        {
            if(IsConnected) {
                throw new InvalidOperationException("Database connection already connected!");
            }

            await Lock.WaitAsync().ConfigureAwait(false);
            try {
                Connection = new SQLiteConnectionWithLock(sqlitePlatform, connectionString);
                AsyncConnection = new SQLiteAsyncConnection(() => Connection);
            } finally {
                Lock.Release();
            }
        }
    }
}
