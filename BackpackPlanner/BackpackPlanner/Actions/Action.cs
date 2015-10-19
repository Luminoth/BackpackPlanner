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

using EnergonSoftware.BackpackPlanner.Core.Database;
using EnergonSoftware.BackpackPlanner.Settings;

namespace EnergonSoftware.BackpackPlanner.Actions
{
    /// <summary>
    /// 
    /// </summary>
    // TODO: rename this so it doesn't conflict with System.Action
    public abstract class Action
    {
        /// <summary>
        /// Does the action.
        /// </summary>
        /// <param name="databaseState">State of the database.</param>
        /// <param name="settings">The settings.</param>
        public abstract Task DoActionAsync(DatabaseState databaseState, BackpackPlannerSettings settings);

        /// <summary>
        /// Does the action in a background task.
        /// </summary>
        /// <param name="databaseState">State of the database.</param>
        /// <param name="settings">The settings.</param>
        /// <param name="actionFinishedCallback">The action finished callback.</param>
        public void DoActionInBackground(DatabaseState databaseState, BackpackPlannerSettings settings, Action<Action> actionFinishedCallback)
        {
            Task.Run(async () =>
            {
                await DoActionAsync(databaseState, settings).ConfigureAwait(false);
                actionFinishedCallback?.Invoke(this);
            }
            );
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        /// <param name="databaseState">State of the database.</param>
        /// <param name="settings">The settings.</param>
        public async virtual Task UndoActionAsync(DatabaseState databaseState, BackpackPlannerSettings settings)
        {
            await ValidateDatabaseStateAsync(databaseState).ConfigureAwait(false);
        }

        /// <summary>
        /// Undoes the action in a background task.
        /// </summary>
        /// <param name="databaseState">State of the database.</param>
        /// <param name="settings">The settings.</param>
        /// <param name="actionFinishedCallback">The action finished callback.</param>
        public void UndoActionInBackground(DatabaseState databaseState, BackpackPlannerSettings settings, Action<Action> actionFinishedCallback)
        {
            Task.Run(async () =>
            {
                await UndoActionAsync(databaseState, settings).ConfigureAwait(false);
                actionFinishedCallback?.Invoke(this);
            }
            );
        }

        /// <summary>
        /// Validates the database state.
        /// </summary>
        /// <param name="databaseState">The database state.</param>
        protected async Task ValidateDatabaseStateAsync(DatabaseState databaseState)
        {
            if(null == databaseState) {
                throw new ArgumentNullException(nameof(databaseState));
            }

            while(!databaseState.IsInitialized) {
                await Task.Delay(1).ConfigureAwait(false);
            }
        }
    }
}
