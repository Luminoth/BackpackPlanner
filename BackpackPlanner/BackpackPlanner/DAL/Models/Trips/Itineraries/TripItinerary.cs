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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace EnergonSoftware.BackpackPlanner.DAL.Models.Trips.Itineraries
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class TripItinerary : BaseModel<TripItinerary>, IBackpackPlannerItem
    {
#region Static Helpers
        public static async Task<IReadOnlyCollection<TripItinerary>> GetAll(DatabaseContext dbContext)
        {
            return await dbContext.TripItineraries.ToListAsync().ConfigureAwait(false);
        }
#endregion

        public override int Id => TripItineraryId;

#region Database Properties
        /// <summary>
        /// Gets or sets the trip itinerary identifier.
        /// </summary>
        /// <value>
        /// The trip itinerary identifier.
        /// </value>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TripItineraryId { get; private set; }

        private string _name = string.Empty;

        /// <summary>
        /// Gets or sets the trip itinerary name.
        /// </summary>
        /// <value>
        /// The trip itinerary name.
        /// </value>
        [Required, MaxLength(64)]
        public string Name
        {
            get => _name;

            set
            {
                _name = value ?? string.Empty;
                NotifyPropertyChanged();
            }
        }

        private string _note = string.Empty;

        /// <summary>
        /// Gets or sets the trip itinerary note.
        /// </summary>
        /// <value>
        /// The trip itinerary note.
        /// </value>
        [MaxLength(1024)]
        public string Note
        {
            get => _note;

            set
            {
                _note = value ?? string.Empty;
                NotifyPropertyChanged();
            }
        }
#endregion

        public override TripItinerary DeepCopy()
        {
            TripItinerary tripItinerary = base.DeepCopy();

            tripItinerary.TripItineraryId = TripItineraryId;
            tripItinerary.Name = Name;
            tripItinerary.Note = Note;

            return tripItinerary;
        }

        public TripItinerary()
        {
        }

        public override bool Equals(object obj)
        {
            if(Id < 1) {
                return false;
            }

            TripItinerary tripItinerary = obj as TripItinerary;
            return Id == tripItinerary?.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
