using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarrentlyTheBestAPI.Migrations
{
    /// <inheritdoc />
    public partial class UserDoRenta2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Wypozyczenia",
                newName: "UzytkownikEmail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UzytkownikEmail",
                table: "Wypozyczenia",
                newName: "Email");
        }
    }
}
