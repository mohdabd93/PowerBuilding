using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Calories = table.Column<int>(type: "int", nullable: false),
                    Protein = table.Column<int>(type: "int", nullable: false),
                    FoodItems = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tip = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Supplements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsEssential = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayNumber = table.Column<int>(type: "int", nullable: false),
                    DayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Focus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRestDay = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sets = table.Column<int>(type: "int", nullable: false),
                    Reps = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rest = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    WorkoutDayId = table.Column<int>(type: "int", nullable: true),
                    WorkoutDayId1 = table.Column<int>(type: "int", nullable: true),
                    WorkoutDayId2 = table.Column<int>(type: "int", nullable: true),
                    WorkoutDayId3 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercises_WorkoutDays_WorkoutDayId",
                        column: x => x.WorkoutDayId,
                        principalTable: "WorkoutDays",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Exercises_WorkoutDays_WorkoutDayId1",
                        column: x => x.WorkoutDayId1,
                        principalTable: "WorkoutDays",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Exercises_WorkoutDays_WorkoutDayId2",
                        column: x => x.WorkoutDayId2,
                        principalTable: "WorkoutDays",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Exercises_WorkoutDays_WorkoutDayId3",
                        column: x => x.WorkoutDayId3,
                        principalTable: "WorkoutDays",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_WorkoutDayId",
                table: "Exercises",
                column: "WorkoutDayId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_WorkoutDayId1",
                table: "Exercises",
                column: "WorkoutDayId1");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_WorkoutDayId2",
                table: "Exercises",
                column: "WorkoutDayId2");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_WorkoutDayId3",
                table: "Exercises",
                column: "WorkoutDayId3");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "Supplements");

            migrationBuilder.DropTable(
                name: "WorkoutDays");
        }
    }
}
