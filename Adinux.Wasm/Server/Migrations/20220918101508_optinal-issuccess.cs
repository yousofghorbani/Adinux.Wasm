using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adinux.Wasm.Server.Migrations
{
    public partial class optinalissuccess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsSuccessful",
                table: "CatalogueRequestSendTypes",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "INTEGER");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsSuccessful",
                table: "CatalogueRequestSendTypes",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldNullable: true);
        }
    }
}
