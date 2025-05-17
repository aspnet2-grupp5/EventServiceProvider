using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Events",
                newName: "EventTitle");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Events",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "BookingPercentage",
                table: "Events",
                newName: "SoldQuantity");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Events",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "CategoryId",
                table: "Events",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LocationId",
                table: "Events",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StatusId",
                table: "Events",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AdminName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    MemberId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.MemberId);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    StatusId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.StatusId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_CategoryId",
                table: "Events",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_LocationId",
                table: "Events",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_StatusId",
                table: "Events",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Categories_CategoryId",
                table: "Events",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Locations_LocationId",
                table: "Events",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Statuses_StatusId",
                table: "Events",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Categories_CategoryId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Locations_LocationId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Statuses_StatusId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_Events_CategoryId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_LocationId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_StatusId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "SoldQuantity",
                table: "Events",
                newName: "BookingPercentage");

            migrationBuilder.RenameColumn(
                name: "EventTitle",
                table: "Events",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Events",
                newName: "Status");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Events",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
