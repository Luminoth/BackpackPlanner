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
    public abstract class BaseModelEntry<T, TM, TI>
        : INotifyPropertyChanged where T: BaseModelEntry<T, TM, TI>, new()
            where TM: BaseModel<TM>, new()
            where TI: BaseModel<TI>, IBackpackPlannerItem, new()
    {
#region Events
        public event PropertyChangedEventHandler PropertyChanged;
#endregion

        /// <summary>
        /// Gets the item entry identifier.
        /// </summary>
        /// <value>
        /// The item entry identifier.
        /// </value>
        [NotMapped, JsonIgnore]
        public abstract int Id { get; }

#region Database Properties
        /// <summary>
        /// Gets or sets the model identifier.
        /// </summary>
        /// <value>
        /// The model identifier.
        /// </value>
        /// <remarks>
        /// This should have the Required and ForeignKey("Model") attributes
        /// </remarks>
        public abstract int ModelId { get; protected set; }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        /// <remarks>
        /// If this is set, the ModelId should also be set to match it
        /// </remarks>
        public abstract TI Model { get; protected set; }

        private int _count;

        /// <summary>
        /// Gets or sets the number of items.
        /// </summary>
        /// <value>
        /// The number of items.
        /// </value>
        [Required]
        public int Count
        {
            get => _count;

            set
            {
                _count = value < 0 ? 0 : value;
                NotifyPropertyChanged();
            }
        }
#endregion

        public virtual T DeepCopy()
        {
            return new T
            {
                ModelId = ModelId,
                Model = Model,          // TODO: should we copy this as well? we shouldn't be able to edit it, so probably not?
                Count = Count
            };
        }

        public void SetModel(TI model, bool notify=true)
        {
            Model = model;
            ModelId = model?.Id ?? 0;

            if(notify) {
                NotifyPropertyChanged(nameof(Model));
            }
        }

        public void OnRemove()
        {
            PropertyChanged = null;
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected BaseModelEntry(TI model)
        {
            SetModel(model, false);
        }

        protected BaseModelEntry()
        {
        }
    }
}