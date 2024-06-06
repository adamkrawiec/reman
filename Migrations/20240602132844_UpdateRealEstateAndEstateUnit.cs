using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace reman.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRealEstateAndEstateUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "RealEstates",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "HouseNumber",
                table: "RealEstates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "RealEstates",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FlatNumber",
                table: "EstateUnits",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "area",
                table: "EstateUnits",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HouseNumber",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "FlatNumber",
                table: "EstateUnits");

            migrationBuilder.DropColumn(
                name: "area",
                table: "EstateUnits");
        }
    }
}
