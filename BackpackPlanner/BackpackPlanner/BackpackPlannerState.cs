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

using SQLite.Net.Async;

namespace EnergonSoftware.BackpackPlanner
{
    /// <summary>
    /// Collects the general library state
    /// </summary>
    public class BackpackPlannerState
    {
        public static BackpackPlannerState Instance => new BackpackPlannerState();

        private readonly GearState _gearState = new GearState();

        /// <summary>
        /// Gets or sets the database connection.
        /// </summary>
        /// <value>
        /// The database connection.
        /// </value>
        public SQLiteAsyncConnection DbConnection { get; set; }

        /// <summary>
        /// Loads the library state from the device.
        /// </summary>
        public async Task LoadFromDeviceAsync()
        {
            await _gearState.LoadFromDeviceAsync().ConfigureAwait(false);
        }

        private BackpackPlannerState()
        {
        }
    }
}
