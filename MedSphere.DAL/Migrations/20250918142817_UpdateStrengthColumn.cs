using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedSphere.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStrengthColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Strength",
                table: "MedicineIngredients");

            migrationBuilder.AddColumn<int>(
                name: "StrengthMg",
                table: "MedicineIngredients",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StrengthMg",
                table: "MedicineIngredients");

            migrationBuilder.AddColumn<string>(
                name: "Strength",
                table: "MedicineIngredients",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
