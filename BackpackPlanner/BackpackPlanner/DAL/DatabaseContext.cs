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

using EnergonSoftware.BackpackPlanner.Core.Logging;
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
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(DatabaseContext));

        public virtual DbSet<GearItem> GearItems { get; private set; }
        public virtual DbSet<GearSystem> GearSystems { get; private set; }
        public virtual DbSet<GearCollection> GearCollections { get; private set; }

        public virtual DbSet<Meal> Meals { get; private set; }

        public virtual DbSet<TripItinerary> TripItineraries { get; private set; }
        public virtual DbSet<TripPlan> TripPlans { get; private set; }

        public string DatabasePath { get; }

        //public bool FilterDeleted { get; } = true;

        public DatabaseContext(string databasePath)
        {
            DatabasePath = databasePath;
        }

        public DatabaseContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Logger.Info($"Connecting to database at {DatabasePath}...");
            optionsBuilder.UseSqlite($"Data Source={DatabasePath}");
        }

        private void SetPropertyAccessModeField<T>(ModelBuilder modelBuilder, string propertyName) where T: class
        {
            IMutableNavigation navigation = modelBuilder.Entity<T>().Metadata.FindNavigation(propertyName);
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
/*
EF Core 2.0 stuff:
            // https://www.youtube.com/watch?v=VYa7EdMnP4E&t=2368s

            // map fields to conceptual-properties (this replaces SetPropertyAccessModeField() method and calls)
            // TODO: do this for all of the relevant fields
            modelBuilder.Entity<TripPlan>()
                .Property<int>("TripPlanId")
                .HasField("_id");

            // add conceptual IsDeleted property for database tracking
            modelBuilder.Entity<TripPlan>()
                .Property<bool>("IsDeleted");

            // TODO: remove the IsDeleted field from the models (how do we set it then???)
            // should the id field be removed as well maybe?

            // filter out deleted items if we should (lazy evaluated)
            modelBuilder.Entity<TripPlan>()
                .HasQueryFilter(tripPlan => !FilterDeleted || !EF.Property<bool>(tripPlan, "IsDeleted"));
            modelBuilder.Entity<TripItinerary>()
                .HasQueryFilter(tripItinerary => !FilterDeleted || !EF.Property<bool>(tripItinerary, "IsDeleted"));
            modelBuilder.Entity<Meal>()
                .HasQueryFilter(meal => !FilterDeleted || !EF.Property<bool>(meal, "IsDeleted"));
            modelBuilder.Entity<GearCollection>()
                .HasQueryFilter(gearCollection => !FilterDeleted || !EF.Property<bool>(gearCollection, "IsDeleted"));
            modelBuilder.Entity<GearSystem>()
                .HasQueryFilter(gearSystem => !FilterDeleted || !EF.Property<bool>(gearSystem, "IsDeleted"));
            modelBuilder.Entity<GearItem>()
                .HasQueryFilter(gearItem => !FilterDeleted || !EF.Property<bool>(gearItem, "IsDeleted"));
*/

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
