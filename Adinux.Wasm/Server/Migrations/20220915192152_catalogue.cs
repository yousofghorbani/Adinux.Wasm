using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adinux.Wasm.Server.Migrations
{
    public partial class catalogue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "UserForms",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CatalogueRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserFormId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogueRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatalogueRequests_UserForms_UserFormId",
                        column: x => x.UserFormId,
                        principalTable: "UserForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatalogueRequestSendTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CatalogueRequestId = table.Column<int>(type: "INTEGER", nullable: false),
                    SendType = table.Column<int>(type: "INTEGER", nullable: false),
                    IsSuccessful = table.Column<bool>(type: "INTEGER", nullable: false),
                    Error = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogueRequestSendTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatalogueRequestSendTypes_CatalogueRequests_CatalogueRequestId",
                        column: x => x.CatalogueRequestId,
                        principalTable: "CatalogueRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogueRequests_UserFormId",
                table: "CatalogueRequests",
                column: "UserFormId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogueRequestSendTypes_CatalogueRequestId",
                table: "CatalogueRequestSendTypes",
                column: "CatalogueRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatalogueRequestSendTypes");

            migrationBuilder.DropTable(
                name: "CatalogueRequests");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "UserForms");
        }
    }
}
