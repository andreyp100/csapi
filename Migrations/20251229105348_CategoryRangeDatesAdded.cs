using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;
using NpgsqlTypes;

#nullable disable

namespace csapi.Migrations
{
    /// <inheritdoc />
    public partial class CategoryRangeDatesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateRange",
                table: "Categories");

            migrationBuilder.AddColumn<DateTime>(
                name: "CategoryEndDate",
                table: "Categories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CategoryMonth",
                table: "Categories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CategoryStartDate",
                table: "Categories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CategoryYear",
                table: "Categories",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryEndDate",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CategoryMonth",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CategoryStartDate",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CategoryYear",
                table: "Categories");

            migrationBuilder.AddColumn<NpgsqlRange<LocalDate>>(
                name: "DateRange",
                table: "Categories",
                type: "daterange",
                nullable: false,
                defaultValue: new NpgsqlTypes.NpgsqlRange<NodaTime.LocalDate>(new NodaTime.LocalDate(1, 1, 1), false, new NodaTime.LocalDate(1, 1, 1), false));
        }
    }
}
