using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedSphere.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditableEntityColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Medicines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Medicines",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Medicines",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedById",
                table: "Medicines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Medicines",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "MedicineIngredients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "MedicineIngredients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MedicineIngredients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedById",
                table: "MedicineIngredients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "MedicineIngredients",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "MedicineImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "MedicineImages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MedicineImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedById",
                table: "MedicineImages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "MedicineImages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Ingredients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Ingredients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedById",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Ingredients",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "MedicineIngredients");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "MedicineIngredients");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MedicineIngredients");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "MedicineIngredients");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "MedicineIngredients");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "MedicineImages");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "MedicineImages");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MedicineImages");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "MedicineImages");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "MedicineImages");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Ingredients");
        }
    }
}
