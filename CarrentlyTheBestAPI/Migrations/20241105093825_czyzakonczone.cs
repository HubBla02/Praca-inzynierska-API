using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarrentlyTheBestAPI.Migrations
{
    /// <inheritdoc />
    public partial class czyzakonczone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CzyZakonczone",
                table: "Wypozyczenia",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CzyZakonczone",
                table: "Wypozyczenia");
        }
    }
}
