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

using EnergonSoftware.BackpackPlanner.Models.Gear.Items;

namespace EnergonSoftware.BackpackPlanner.Actions.Gear.Items
{
    /// <summary>
    /// Deletes a gear item.
    /// </summary>
    public sealed class DeleteGearItemAction : IAction
    {
        /// <summary>
        /// Gets or sets the gear item to delete.
        /// </summary>
        /// <value>
        /// The gear item to delete.
        /// </value>
        public GearItem Item { get; set; }

// TODO: this could probably subclass a generic "DeleteItemAction"

        public void DoAction()
        {
        }

        public void UndoAction()
        {
        }
    }
}
