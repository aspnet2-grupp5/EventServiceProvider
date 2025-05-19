using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventApi.Migrations
{
    /// <inheritdoc />
    public partial class seedStatuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "StatusId", "StatusName" },
                values: new object[,]
                {
                    { "1", "Active" },
                    { "2", "Past" },
                    { "3", "Draft" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "StatusId",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "StatusId",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "StatusId",
                keyValue: "3");
        }
    }
}
