using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarrentlyTheBestAPI.Migrations
{
    /// <inheritdoc />
    public partial class opinia_email_usun : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AutorEmail",
                table: "Opinie");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AutorEmail",
                table: "Opinie",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
