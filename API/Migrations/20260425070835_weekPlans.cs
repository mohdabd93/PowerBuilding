using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class weekPlans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WeekPlanId",
                table: "WorkoutDays",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WeekPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekPlans", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutDays_WeekPlanId",
                table: "WorkoutDays",
                column: "WeekPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutDays_WeekPlans_WeekPlanId",
                table: "WorkoutDays",
                column: "WeekPlanId",
                principalTable: "WeekPlans",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutDays_WeekPlans_WeekPlanId",
                table: "WorkoutDays");

            migrationBuilder.DropTable(
                name: "WeekPlans");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutDays_WeekPlanId",
                table: "WorkoutDays");

            migrationBuilder.DropColumn(
                name: "WeekPlanId",
                table: "WorkoutDays");
        }
    }
}
