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

using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Collections;
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Systems;
using EnergonSoftware.BackpackPlanner.DAL.Models.Meals;
using EnergonSoftware.BackpackPlanner.DAL.Models.Trips.Itineraries;
using EnergonSoftware.BackpackPlanner.DAL.Models.Trips.Plans;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EnergonSoftware.BackpackPlanner.DAL
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<GearItem> GearItems { get; private set; }
        public virtual DbSet<GearSystem> GearSystems { get; private set; }
        public virtual DbSet<GearCollection> GearCollections { get; private set; }

        public virtual DbSet<Meal> Meals { get; private set; }

        public virtual DbSet<TripItinerary> TripItineraries { get; private set; }
        public virtual DbSet<TripPlan> TripPlans { get; private set; }

        public string DatabasePath { get; }

        public DatabaseContext(string databasePath)
        {
            DatabasePath = databasePath;
        }

        public DatabaseContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={DatabasePath}");
        }

        private void SetPropertyAccessModeField<T>(ModelBuilder modelBuilder, string propertyName) where T: class
        {
            IMutableNavigation navigation = modelBuilder.Entity<T>().Metadata.FindNavigation(propertyName);
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetPropertyAccessModeField<GearSystem>(modelBuilder, nameof(GearSystem.GearItems));

            SetPropertyAccessModeField<GearCollection>(modelBuilder, nameof(GearCollection.GearSystems));
            SetPropertyAccessModeField<GearCollection>(modelBuilder, nameof(GearCollection.GearItems));

            SetPropertyAccessModeField<TripPlan>(modelBuilder, nameof(TripPlan.GearCollections));
            SetPropertyAccessModeField<TripPlan>(modelBuilder, nameof(TripPlan.GearSystems));
            SetPropertyAccessModeField<TripPlan>(modelBuilder, nameof(TripPlan.GearItems));
            SetPropertyAccessModeField<TripPlan>(modelBuilder, nameof(TripPlan.Meals));
        }
    }
}
