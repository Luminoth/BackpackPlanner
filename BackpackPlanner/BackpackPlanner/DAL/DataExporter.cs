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

using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Collections;
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Systems;
using EnergonSoftware.BackpackPlanner.DAL.Models.Meals;
using EnergonSoftware.BackpackPlanner.DAL.Models.Trips.Itineraries;
using EnergonSoftware.BackpackPlanner.DAL.Models.Trips.Plans;

using Newtonsoft.Json;

namespace EnergonSoftware.BackpackPlanner.DAL
{
    public static class DataExporter
    {
// TODO: all model classes need to be serializable

        [Serializable]
        private sealed class ExportedData
        {
// TODO: version this

            public List<GearItem> GearItems { get; set; }
            public List<GearSystem> GearSystem { get; set; }
            public List<GearCollection> GearCollections { get; set; }

            public List<Meal> Meals { get; set; }

            public List<TripItinerary> TripItineraries { get; set; }
            public List<TripPlan> TripPlans { get; set; }

        }

        public static string ToJson(DatabaseContext dbContext)
        {
            ExportedData data = new ExportedData();

            return JsonConvert.SerializeObject(data);
        }

        public static bool FromJson(DatabaseContext dbContext, string json)
        {
// TODO: this won't work, we have to first check the version and then deserialize manually based on that
            ExportedData data = JsonConvert.DeserializeObject<ExportedData>(json);

// TODO: add the data to the database

            return true;
        }
    }
}
