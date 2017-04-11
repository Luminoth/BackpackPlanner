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
                name: "GearItemEntry<GearCollection>",
                columns: table => new
                {
                    GearItemEntryId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Count = table.Column<int>(nullable: false),
                    GearCollectionId = table.Column<int>(nullable: true),
                    ModelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GearItemEntry<GearCollection>", x => x.GearItemEntryId);
                    table.ForeignKey(
                        name: "FK_GearItemEntry<GearCollection>_GearCollections_GearCollectionId",
                        column: x => x.GearCollectionId,
                        principalTable: "GearCollections",
                        principalColumn: "GearCollectionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GearItemEntry<GearCollection>_GearItems_ModelId",
                        column: x => x.ModelId,
                        principalTable: "GearItems",
                        principalColumn: "GearItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GearItemEntry<GearSystem>",
                columns: table => new
                {
                    GearItemEntryId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Count = table.Column<int>(nullable: false),
                    GearSystemId = table.Column<int>(nullable: true),
                    ModelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GearItemEntry<GearSystem>", x => x.GearItemEntryId);
                    table.ForeignKey(
                        name: "FK_GearItemEntry<GearSystem>_GearSystems_GearSystemId",
                        column: x => x.GearSystemId,
                        principalTable: "GearSystems",
                        principalColumn: "GearSystemId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GearItemEntry<GearSystem>_GearItems_ModelId",
                        column: x => x.ModelId,
                        principalTable: "GearItems",
                        principalColumn: "GearItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GearSystemEntry<GearCollection>",
                columns: table => new
                {
                    GearSystemEntryId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Count = table.Column<int>(nullable: false),
                    GearCollectionId = table.Column<int>(nullable: true),
                    ModelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GearSystemEntry<GearCollection>", x => x.GearSystemEntryId);
                    table.ForeignKey(
                        name: "FK_GearSystemEntry<GearCollection>_GearCollections_GearCollectionId",
                        column: x => x.GearCollectionId,
                        principalTable: "GearCollections",
                        principalColumn: "GearCollectionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GearSystemEntry<GearCollection>_GearSystems_ModelId",
                        column: x => x.ModelId,
                        principalTable: "GearSystems",
                        principalColumn: "GearSystemId",
                        onDelete: ReferentialAction.Cascade);
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
                name: "GearCollectionEntry<TripPlan>",
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
                    table.PrimaryKey("PK_GearCollectionEntry<TripPlan>", x => x.GearCollectionEntryId);
                    table.ForeignKey(
                        name: "FK_GearCollectionEntry<TripPlan>_GearCollections_ModelId",
                        column: x => x.ModelId,
                        principalTable: "GearCollections",
                        principalColumn: "GearCollectionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GearCollectionEntry<TripPlan>_TripPlans_TripPlanId",
                        column: x => x.TripPlanId,
                        principalTable: "TripPlans",
                        principalColumn: "TripPlanId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GearItemEntry<TripPlan>",
                columns: table => new
                {
                    GearItemEntryId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Count = table.Column<int>(nullable: false),
                    ModelId = table.Column<int>(nullable: false),
                    TripPlanId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GearItemEntry<TripPlan>", x => x.GearItemEntryId);
                    table.ForeignKey(
                        name: "FK_GearItemEntry<TripPlan>_GearItems_ModelId",
                        column: x => x.ModelId,
                        principalTable: "GearItems",
                        principalColumn: "GearItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GearItemEntry<TripPlan>_TripPlans_TripPlanId",
                        column: x => x.TripPlanId,
                        principalTable: "TripPlans",
                        principalColumn: "TripPlanId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GearSystemEntry<TripPlan>",
                columns: table => new
                {
                    GearSystemEntryId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Count = table.Column<int>(nullable: false),
                    ModelId = table.Column<int>(nullable: false),
                    TripPlanId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GearSystemEntry<TripPlan>", x => x.GearSystemEntryId);
                    table.ForeignKey(
                        name: "FK_GearSystemEntry<TripPlan>_GearSystems_ModelId",
                        column: x => x.ModelId,
                        principalTable: "GearSystems",
                        principalColumn: "GearSystemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GearSystemEntry<TripPlan>_TripPlans_TripPlanId",
                        column: x => x.TripPlanId,
                        principalTable: "TripPlans",
                        principalColumn: "TripPlanId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MealEntry<TripPlan>",
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
                    table.PrimaryKey("PK_MealEntry<TripPlan>", x => x.MealEntryId);
                    table.ForeignKey(
                        name: "FK_MealEntry<TripPlan>_Meals_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Meals",
                        principalColumn: "MealId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealEntry<TripPlan>_TripPlans_TripPlanId",
                        column: x => x.TripPlanId,
                        principalTable: "TripPlans",
                        principalColumn: "TripPlanId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GearCollectionEntry<TripPlan>_ModelId",
                table: "GearCollectionEntry<TripPlan>",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_GearCollectionEntry<TripPlan>_TripPlanId",
                table: "GearCollectionEntry<TripPlan>",
                column: "TripPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_GearItemEntry<GearCollection>_GearCollectionId",
                table: "GearItemEntry<GearCollection>",
                column: "GearCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_GearItemEntry<GearCollection>_ModelId",
                table: "GearItemEntry<GearCollection>",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_GearItemEntry<GearSystem>_GearSystemId",
                table: "GearItemEntry<GearSystem>",
                column: "GearSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_GearItemEntry<GearSystem>_ModelId",
                table: "GearItemEntry<GearSystem>",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_GearItemEntry<TripPlan>_ModelId",
                table: "GearItemEntry<TripPlan>",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_GearItemEntry<TripPlan>_TripPlanId",
                table: "GearItemEntry<TripPlan>",
                column: "TripPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_GearSystemEntry<GearCollection>_GearCollectionId",
                table: "GearSystemEntry<GearCollection>",
                column: "GearCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_GearSystemEntry<GearCollection>_ModelId",
                table: "GearSystemEntry<GearCollection>",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_GearSystemEntry<TripPlan>_ModelId",
                table: "GearSystemEntry<TripPlan>",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_GearSystemEntry<TripPlan>_TripPlanId",
                table: "GearSystemEntry<TripPlan>",
                column: "TripPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_MealEntry<TripPlan>_ModelId",
                table: "MealEntry<TripPlan>",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_MealEntry<TripPlan>_TripPlanId",
                table: "MealEntry<TripPlan>",
                column: "TripPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_TripPlans_TripItineraryId",
                table: "TripPlans",
                column: "TripItineraryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GearCollectionEntry<TripPlan>");

            migrationBuilder.DropTable(
                name: "GearItemEntry<GearCollection>");

            migrationBuilder.DropTable(
                name: "GearItemEntry<GearSystem>");

            migrationBuilder.DropTable(
                name: "GearItemEntry<TripPlan>");

            migrationBuilder.DropTable(
                name: "GearSystemEntry<GearCollection>");

            migrationBuilder.DropTable(
                name: "GearSystemEntry<TripPlan>");

            migrationBuilder.DropTable(
                name: "MealEntry<TripPlan>");

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
