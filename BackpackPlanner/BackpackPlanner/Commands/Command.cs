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

namespace EnergonSoftware.BackpackPlanner.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Command
    {
        /// <summary>
        /// Does the action.
        /// </summary>
        /// <param name="state">The system state.</param>
        public virtual async Task DoActionAsync(BackpackPlannerState state)
        {
            await Task.Delay(0).ConfigureAwait(false);
        }

        /// <summary>
        /// Does the action in a background task.
        /// </summary>
        /// <param name="state">The system state.</param>
        /// <param name="actionFinishedCallback">The action finished callback.</param>
        public void DoActionInBackground(BackpackPlannerState state, Action<Command> actionFinishedCallback)
        {
            Task.Run(async () =>
                {
                    await DoActionAsync(state).ConfigureAwait(false);
                    actionFinishedCallback?.Invoke(this);
                }
            );
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        /// <param name="state">The system state.</param>
        public virtual async Task UndoActionAsync(BackpackPlannerState state)
        {
            await Task.Delay(0).ConfigureAwait(false);
        }

        /// <summary>
        /// Undoes the action in a background task.
        /// </summary>
        /// <param name="state">The system state.</param>
        /// <param name="actionFinishedCallback">The action finished callback.</param>
        public void UndoActionInBackground(BackpackPlannerState state, Action<Command> actionFinishedCallback)
        {
            Task.Run(async () =>
                {
                    await UndoActionAsync(state).ConfigureAwait(false);
                    actionFinishedCallback?.Invoke(this);
                }
            );
        }
    }
}
