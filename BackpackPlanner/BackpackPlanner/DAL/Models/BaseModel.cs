﻿/*
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

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

using Newtonsoft.Json;

namespace EnergonSoftware.BackpackPlanner.DAL.Models
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public abstract class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the item identifier.
        /// </summary>
        /// <value>
        /// The item identifier.
        /// </value>
        [NotMapped, JsonIgnore]
        public abstract int Id { get; }

#region Database Properties
        /// <summary>
        /// Gets or sets the last update timestamp.
        /// </summary>
        /// <value>
        /// The last update timestamp.
        /// </value>
        [Required]
        public DateTime LastUpdated { get; set; } = DateTime.Now;

        private bool _isDeleted;

        /// <summary>
        /// Gets or sets a value indicating whether this item is deleted.
        /// </summary>
        /// <value>
        /// <c>true</c> if this item is deleted; otherwise, <c>false</c>.
        /// </value>
        [Required]
        public bool IsDeleted
        {
            get { return _isDeleted; }
            set
            {
                _isDeleted = value;
                NotifyPropertyChanged();
            }
        }
#endregion

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName="")
        {
            LastUpdated = DateTime.Now;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
