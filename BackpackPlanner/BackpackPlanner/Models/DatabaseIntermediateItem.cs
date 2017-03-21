/*
   Copyright 2017 Shane Lillie

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

using EnergonSoftware.BackpackPlanner.Settings;

using SQLite.Net.Attributes;

namespace EnergonSoftware.BackpackPlanner.Models
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class DatabaseIntermediateItem
    {
        [Ignore]
        public DatabaseItem Parent { get; private set; }

        [Ignore]
        public DatabaseItem Child { get; private set; }

        private int _count;

        /// <summary>
        /// Gets or sets the number of gear items.
        /// </summary>
        /// <value>
        /// The number of gear items.
        /// </value>
        [Ignore]
        public int Count
        {
            get { return _count; }

            set { _count = value < 0 ? 0 : value; }
        }

        /// <summary>
        /// Gets the planner settings.
        /// </summary>
        /// <value>
        /// The planner settings.
        /// </value>
        [Ignore]
        protected BackpackPlannerSettings Settings { get; set; }

        protected DatabaseIntermediateItem()
        {
        }

        protected DatabaseIntermediateItem(DatabaseItem parent, DatabaseItem child, BackpackPlannerSettings settings)
        {
            Settings = settings;
            Parent = parent;
            Child = child;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public abstract class DatabaseIntermediateItem<T, TV> : DatabaseIntermediateItem where T: DatabaseItem where TV: DatabaseItem
    {
        [Ignore]
        public new T Parent { get; private set; }

        [Ignore]
        public new TV Child { get; private set; }

        protected DatabaseIntermediateItem()
        {
        }

        protected DatabaseIntermediateItem(T parent, TV child, BackpackPlannerSettings settings)
            : base(parent, child, settings)
        {
            Parent = parent;
            Child = child;
        }
    }
}
