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

namespace EnergonSoftware.BackpackPlanner.Actions
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Action
    {
        /// <summary>
        /// Does the action.
        /// </summary>
        public abstract Task DoActionAsync();

        /// <summary>
        /// Does the action in a background task.
        /// </summary>
        public void DoActionInBackground(Action<Action> actionFinishedCallback)
        {
            Task.Run(async () => {
                    await DoActionAsync().ConfigureAwait(false);
                    actionFinishedCallback?.Invoke(this);
                }
            );
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public virtual Task UndoActionAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Undoes the action in a background task.
        /// </summary>
        public void UndoActionInBackground(Action<Action> actionFinishedCallback)
        {
            Task.Run(async () => {
                    await UndoActionAsync().ConfigureAwait(false);
                    actionFinishedCallback?.Invoke(this);
                }
            );
        }
    }
}
