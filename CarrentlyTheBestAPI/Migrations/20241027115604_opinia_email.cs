using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarrentlyTheBestAPI.Migrations
{
    /// <inheritdoc />
    public partial class opinia_email : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Opinie_Uzytkownicy_Autorid",
                table: "Opinie");

            migrationBuilder.RenameColumn(
                name: "Autorid",
                table: "Opinie",
                newName: "AutorId");

            migrationBuilder.RenameIndex(
                name: "IX_Opinie_Autorid",
                table: "Opinie",
                newName: "IX_Opinie_AutorId");

            migrationBuilder.AddColumn<string>(
                name: "AutorEmail",
                table: "Opinie",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Opinie_Uzytkownicy_AutorId",
                table: "Opinie",
                column: "AutorId",
                principalTable: "Uzytkownicy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Opinie_Uzytkownicy_AutorId",
                table: "Opinie");

            migrationBuilder.DropColumn(
                name: "AutorEmail",
                table: "Opinie");

            migrationBuilder.RenameColumn(
                name: "AutorId",
                table: "Opinie",
                newName: "Autorid");

            migrationBuilder.RenameIndex(
                name: "IX_Opinie_AutorId",
                table: "Opinie",
                newName: "IX_Opinie_Autorid");

            migrationBuilder.AddForeignKey(
                name: "FK_Opinie_Uzytkownicy_Autorid",
                table: "Opinie",
                column: "Autorid",
                principalTable: "Uzytkownicy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
