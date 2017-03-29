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

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Runtime.CompilerServices;

namespace EnergonSoftware.BackpackPlanner.DAL.Models
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseModelEntry : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotMapped]
        public abstract BaseModel ItemModel { get; }

        [NotMapped]
        public abstract IBackpackPlannerItem Item { get; }

#region Database Properties
        private int _count;

        /// <summary>
        /// Gets or sets the number of gear items.
        /// </summary>
        /// <value>
        /// The number of gear items.
        /// </value>
        [Required]
        public int Count
        {
            get { return _count; }
            set
            {
                _count = value < 0 ? 0 : value;
                NotifyPropertyChanged();
            }
        }
#endregion

        public void OnRemove()
        {
            PropertyChanged = null;
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}