using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;
using NpgsqlTypes;

#nullable disable

namespace csapi.Migrations
{
    /// <inheritdoc />
    public partial class AddedDateRangeToCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<NpgsqlRange<LocalDate>>(
                name: "DateRange",
                table: "Categories",
                type: "daterange",
                nullable: false,
                defaultValue: new NpgsqlTypes.NpgsqlRange<NodaTime.LocalDate>(new NodaTime.LocalDate(1, 1, 1), false, new NodaTime.LocalDate(1, 1, 1), false));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateRange",
                table: "Categories");
        }
    }
}
