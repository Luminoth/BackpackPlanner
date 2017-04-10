using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EnergonSoftware.BackpackPlanner.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GearCollections",
                columns: table => new
                {
                    GearCollectionId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    Note = table.Column<string>(maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GearCollections", x => x.GearCollectionId);
                });

            migrationBuilder.CreateTable(
                name: "GearItems",
                columns: table => new
                {
                    GearItemId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Carried = table.Column<int>(nullable: false),
                    ConsumedPerDay = table.Column<int>(nullable: false),
                    CostInUSDP = table.Column<int>(nullable: false),
                    IsConsumable = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    Make = table.Column<string>(maxLength: 32, nullable: true),
                    Model = table.Column<string>(maxLength: 32, nullable: true),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    Note = table.Column<string>(maxLength: 1024, nullable: true),
                    Url = table.Column<string>(maxLength: 2048, nullable: true),
                    WeightInGrams = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GearItems", x => x.GearItemId);
                });

            migrationBuilder.CreateTable(
                name: "GearSystems",
                columns: table => new
                {
                    GearSystemId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    Note = table.Column<string>(maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GearSystems", x => x.GearSystemId);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    MealId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Calories = table.Column<int>(nullable: false),
                    CostInUSDP = table.Column<int>(nullable: false),
                    FiberInGrams = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    MealTime = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    Note = table.Column<string>(maxLength: 1024, nullable: true),
                    ProteinInGrams = table.Column<int>(nullable: false),
                    ServingCount = table.Column<int>(nullable: false),
                    Url = table.Column<string>(maxLength: 2048, nullable: true),
                    WeightInGrams = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.MealId);
                });

            migrationBuilder.CreateTable(
                name: "TripItineraries",
                columns: table => new
                {
                    TripItineraryId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    Note = table.Column<string>(maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripItineraries", x => x.TripItineraryId);
                });

            migrationBuilder.CreateTable(
                name: "TripPlans",
                columns: table => new
                {
                    TripPlanId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EndDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    Note = table.Column<string>(maxLength: 1024, nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    TripItineraryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripPlans", x => x.TripPlanId);
                    table.ForeignKey(
                        name: "FK_TripPlans_TripItineraries_TripItineraryId",
                        column: x => x.TripItineraryId,
                        principalTable: "TripItineraries",
                        principalColumn: "TripItineraryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GearCollectionEntry",
                columns: table => new
                {
                    GearCollectionEntryId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Count = table.Column<int>(nullable: false),
                    ModelId = table.Column<int>(nullable: false),
                    TripPlanId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GearCollectionEntry", x => x.GearCollectionEntryId);
                    table.ForeignKey(
                        name: "FK_GearCollectionEntry_GearCollections_ModelId",
                        column: x => x.ModelId,
                        principalTable: "GearCollections",
                        principalColumn: "GearCollectionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GearCollectionEntry_TripPlans_TripPlanId",
                        column: x => x.TripPlanId,
                        principalTable: "TripPlans",
                        principalColumn: "TripPlanId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GearItemEntry",
                columns: table => new
                {
                    GearItemEntryId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Count = table.Column<int>(nullable: false),
                    GearCollectionId = table.Column<int>(nullable: true),
                    GearSystemId = table.Column<int>(nullable: true),
                    ModelId = table.Column<int>(nullable: false),
                    TripPlanId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GearItemEntry", x => x.GearItemEntryId);
                    table.ForeignKey(
                        name: "FK_GearItemEntry_GearCollections_GearCollectionId",
                        column: x => x.GearCollectionId,
                        principalTable: "GearCollections",
                        principalColumn: "GearCollectionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GearItemEntry_GearSystems_GearSystemId",
                        column: x => x.GearSystemId,
                        principalTable: "GearSystems",
                        principalColumn: "GearSystemId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GearItemEntry_GearItems_ModelId",
                        column: x => x.ModelId,
                        principalTable: "GearItems",
                        principalColumn: "GearItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GearItemEntry_TripPlans_TripPlanId",
                        column: x => x.TripPlanId,
                        principalTable: "TripPlans",
                        principalColumn: "TripPlanId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GearSystemEntry",
                columns: table => new
                {
                    GearSystemEntryId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Count = table.Column<int>(nullable: false),
                    GearCollectionId = table.Column<int>(nullable: true),
                    ModelId = table.Column<int>(nullable: false),
                    TripPlanId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GearSystemEntry", x => x.GearSystemEntryId);
                    table.ForeignKey(
                        name: "FK_GearSystemEntry_GearCollections_GearCollectionId",
                        column: x => x.GearCollectionId,
                        principalTable: "GearCollections",
                        principalColumn: "GearCollectionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GearSystemEntry_GearSystems_ModelId",
                        column: x => x.ModelId,
                        principalTable: "GearSystems",
                        principalColumn: "GearSystemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GearSystemEntry_TripPlans_TripPlanId",
                        column: x => x.TripPlanId,
                        principalTable: "TripPlans",
                        principalColumn: "TripPlanId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MealEntry",
                columns: table => new
                {
                    MealEntryId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Count = table.Column<int>(nullable: false),
                    ModelId = table.Column<int>(nullable: false),
                    TripPlanId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealEntry", x => x.MealEntryId);
                    table.ForeignKey(
                        name: "FK_MealEntry_Meals_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Meals",
                        principalColumn: "MealId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealEntry_TripPlans_TripPlanId",
                        column: x => x.TripPlanId,
                        principalTable: "TripPlans",
                        principalColumn: "TripPlanId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GearCollectionEntry_ModelId",
                table: "GearCollectionEntry",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_GearCollectionEntry_TripPlanId",
                table: "GearCollectionEntry",
                column: "TripPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_GearItemEntry_GearCollectionId",
                table: "GearItemEntry",
                column: "GearCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_GearItemEntry_GearSystemId",
                table: "GearItemEntry",
                column: "GearSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_GearItemEntry_ModelId",
                table: "GearItemEntry",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_GearItemEntry_TripPlanId",
                table: "GearItemEntry",
                column: "TripPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_GearSystemEntry_GearCollectionId",
                table: "GearSystemEntry",
                column: "GearCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_GearSystemEntry_ModelId",
                table: "GearSystemEntry",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_GearSystemEntry_TripPlanId",
                table: "GearSystemEntry",
                column: "TripPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_MealEntry_ModelId",
                table: "MealEntry",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_MealEntry_TripPlanId",
                table: "MealEntry",
                column: "TripPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_TripPlans_TripItineraryId",
                table: "TripPlans",
                column: "TripItineraryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GearCollectionEntry");

            migrationBuilder.DropTable(
                name: "GearItemEntry");

            migrationBuilder.DropTable(
                name: "GearSystemEntry");

            migrationBuilder.DropTable(
                name: "MealEntry");

            migrationBuilder.DropTable(
                name: "GearItems");

            migrationBuilder.DropTable(
                name: "GearCollections");

            migrationBuilder.DropTable(
                name: "GearSystems");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "TripPlans");

            migrationBuilder.DropTable(
                name: "TripItineraries");
        }
    }
}
