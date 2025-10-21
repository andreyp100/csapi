using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace csapi.Migrations
{
    /// <inheritdoc />
    public partial class CategoryIdType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Entries");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Entries",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Entries");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Entries",
                type: "text",
                nullable: true);
        }
    }
}
