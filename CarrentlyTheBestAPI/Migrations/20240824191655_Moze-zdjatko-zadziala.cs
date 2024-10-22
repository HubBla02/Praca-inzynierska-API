using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarrentlyTheBestAPI.Migrations
{
    /// <inheritdoc />
    public partial class Mozezdjatkozadziala : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Zdjecie",
                table: "Pojazdy");

            migrationBuilder.AddColumn<string>(
                name: "SciezkaDoZdjecia",
                table: "Pojazdy",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SciezkaDoZdjecia",
                table: "Pojazdy");

            migrationBuilder.AddColumn<byte[]>(
                name: "Zdjecie",
                table: "Pojazdy",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
