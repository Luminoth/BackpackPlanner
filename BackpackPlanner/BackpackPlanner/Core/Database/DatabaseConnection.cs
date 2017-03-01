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

namespace EnergonSoftware.BackpackPlanner.Core.Database
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// TODO: this needs to expose all of the required db connection methods
    /// and the SQLiteDatabaseConnection should no longer expose the low-level connections
    /// </remarks>
    public interface IDatabaseConnection : IDisposable
    {
        /// <summary>
        /// Gets a value indicating whether the connection is connected.
        /// </summary>
        /// <value>
        /// <c>true</c> if the connection is connected; otherwise, <c>false</c>.
        /// </value>
        bool IsConnected { get; }

        /// <summary>
        /// Acquires the connection lock.
        /// </summary>
        Task LockAsync();

        /// <summary>
        /// Releases the connection lock.
        /// </summary>
        void Release();

        /// <summary>
        /// Connects to the database.
        /// </summary>
        Task ConnectAsync(BackpackPlannerState state);

        /// <summary>
        /// Closes the connection.
        /// </summary>
        Task CloseAsync();
    }
}
