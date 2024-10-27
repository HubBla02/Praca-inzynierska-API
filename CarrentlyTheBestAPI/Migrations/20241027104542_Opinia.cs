using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CarrentlyTheBestAPI.Migrations
{
    /// <inheritdoc />
    public partial class Opinia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Odpowiedz",
                table: "Zgloszenia",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "CzyOpinia",
                table: "Uzytkownicy",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Znizka",
                table: "Uzytkownicy",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Opinie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Autorid = table.Column<int>(type: "integer", nullable: false),
                    Ocena = table.Column<int>(type: "integer", nullable: false),
                    Opis = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opinie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Opinie_Uzytkownicy_Autorid",
                        column: x => x.Autorid,
                        principalTable: "Uzytkownicy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Opinie_Autorid",
                table: "Opinie",
                column: "Autorid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Opinie");

            migrationBuilder.DropColumn(
                name: "CzyOpinia",
                table: "Uzytkownicy");

            migrationBuilder.DropColumn(
                name: "Znizka",
                table: "Uzytkownicy");

            migrationBuilder.AlterColumn<string>(
                name: "Odpowiedz",
                table: "Zgloszenia",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
