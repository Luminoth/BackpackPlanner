using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using EnergonSoftware.BackpackPlanner.DAL;
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.DAL.Models.Meals;

namespace EnergonSoftware.BackpackPlanner.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20170403232009_v1")]
    partial class v1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.2.0-preview1-24224");

            modelBuilder.Entity("EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Collections.GearCollection", b =>
                {
                    b.Property<int>("GearCollectionId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<string>("Note")
                        .HasMaxLength(1024);

                    b.HasKey("GearCollectionId");

                    b.ToTable("GearCollections");
                });

            modelBuilder.Entity("EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Collections.GearCollectionEntry", b =>
                {
                    b.Property<int>("GearCollectionEntryId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Count");

                    b.Property<int>("ModelId");

                    b.Property<int?>("TripPlanId");

                    b.HasKey("GearCollectionEntryId");

                    b.HasIndex("ModelId");

                    b.HasIndex("TripPlanId");

                    b.ToTable("GearCollectionEntry");
                });

            modelBuilder.Entity("EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Items.GearItem", b =>
                {
                    b.Property<int>("GearItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Carried");

                    b.Property<int>("ConsumedPerDay");

                    b.Property<int>("CostInUSDP");

                    b.Property<bool>("IsConsumable");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("Make")
                        .HasMaxLength(32);

                    b.Property<string>("Model")
                        .HasMaxLength(32);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<string>("Note")
                        .HasMaxLength(1024);

                    b.Property<string>("Url")
                        .HasMaxLength(2048);

                    b.Property<int>("WeightInGrams");

                    b.HasKey("GearItemId");

                    b.ToTable("GearItems");
                });

            modelBuilder.Entity("EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Items.GearItemEntry", b =>
                {
                    b.Property<int>("GearItemEntryId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Count");

                    b.Property<int?>("GearCollectionId");

                    b.Property<int?>("GearSystemId");

                    b.Property<int>("ModelId");

                    b.Property<int?>("TripPlanId");

                    b.HasKey("GearItemEntryId");

                    b.HasIndex("GearCollectionId");

                    b.HasIndex("GearSystemId");

                    b.HasIndex("ModelId");

                    b.HasIndex("TripPlanId");

                    b.ToTable("GearItemEntry");
                });

            modelBuilder.Entity("EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Systems.GearSystem", b =>
                {
                    b.Property<int>("GearSystemId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<string>("Note")
                        .HasMaxLength(1024);

                    b.HasKey("GearSystemId");

                    b.ToTable("GearSystems");
                });

            modelBuilder.Entity("EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Systems.GearSystemEntry", b =>
                {
                    b.Property<int>("GearSystemEntryId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Count");

                    b.Property<int?>("GearCollectionId");

                    b.Property<int>("ModelId");

                    b.Property<int?>("TripPlanId");

                    b.HasKey("GearSystemEntryId");

                    b.HasIndex("GearCollectionId");

                    b.HasIndex("ModelId");

                    b.HasIndex("TripPlanId");

                    b.ToTable("GearSystemEntry");
                });

            modelBuilder.Entity("EnergonSoftware.BackpackPlanner.DAL.Models.Meals.Meal", b =>
                {
                    b.Property<int>("MealId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Calories");

                    b.Property<int>("CostInUSDP");

                    b.Property<int>("FiberInGrams");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<int>("MealTime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<string>("Note")
                        .HasMaxLength(1024);

                    b.Property<int>("ProteinInGrams");

                    b.Property<int>("ServingCount");

                    b.Property<string>("Url")
                        .HasMaxLength(2048);

                    b.Property<int>("WeightInGrams");

                    b.HasKey("MealId");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("EnergonSoftware.BackpackPlanner.DAL.Models.Meals.MealEntry", b =>
                {
                    b.Property<int>("MealEntryId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Count");

                    b.Property<int>("ModelId");

                    b.Property<int?>("TripPlanId");

                    b.HasKey("MealEntryId");

                    b.HasIndex("ModelId");

                    b.HasIndex("TripPlanId");

                    b.ToTable("MealEntry");
                });

            modelBuilder.Entity("EnergonSoftware.BackpackPlanner.DAL.Models.Trips.Itineraries.TripItinerary", b =>
                {
                    b.Property<int>("TripItineraryId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<string>("Note")
                        .HasMaxLength(1024);

                    b.HasKey("TripItineraryId");

                    b.ToTable("TripItineraries");
                });

            modelBuilder.Entity("EnergonSoftware.BackpackPlanner.DAL.Models.Trips.Plans.TripPlan", b =>
                {
                    b.Property<int>("TripPlanId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<string>("Note")
                        .HasMaxLength(1024);

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("TripItineraryId");

                    b.HasKey("TripPlanId");

                    b.ToTable("TripPlans");
                });

            modelBuilder.Entity("EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Collections.GearCollectionEntry", b =>
                {
                    b.HasOne("EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Collections.GearCollection", "Model")
                        .WithMany()
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EnergonSoftware.BackpackPlanner.DAL.Models.Trips.Plans.TripPlan")
                        .WithMany("GearCollections")
                        .HasForeignKey("TripPlanId");
                });

            modelBuilder.Entity("EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Items.GearItemEntry", b =>
                {
                    b.HasOne("EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Collections.GearCollection")
                        .WithMany("GearItems")
                        .HasForeignKey("GearCollectionId");

                    b.HasOne("EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Systems.GearSystem")
                        .WithMany("GearItems")
                        .HasForeignKey("GearSystemId");

                    b.HasOne("EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Items.GearItem", "Model")
                        .WithMany()
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EnergonSoftware.BackpackPlanner.DAL.Models.Trips.Plans.TripPlan")
                        .WithMany("GearItems")
                        .HasForeignKey("TripPlanId");
                });

            modelBuilder.Entity("EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Systems.GearSystemEntry", b =>
                {
                    b.HasOne("EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Collections.GearCollection")
                        .WithMany("GearSystems")
                        .HasForeignKey("GearCollectionId");

                    b.HasOne("EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Systems.GearSystem", "Model")
                        .WithMany()
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EnergonSoftware.BackpackPlanner.DAL.Models.Trips.Plans.TripPlan")
                        .WithMany("GearSystems")
                        .HasForeignKey("TripPlanId");
                });

            modelBuilder.Entity("EnergonSoftware.BackpackPlanner.DAL.Models.Meals.MealEntry", b =>
                {
                    b.HasOne("EnergonSoftware.BackpackPlanner.DAL.Models.Meals.Meal", "Model")
                        .WithMany()
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EnergonSoftware.BackpackPlanner.DAL.Models.Trips.Plans.TripPlan")
                        .WithMany("Meals")
                        .HasForeignKey("TripPlanId");
                });
        }
    }
}
