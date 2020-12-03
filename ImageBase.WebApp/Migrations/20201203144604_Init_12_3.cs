using Microsoft.EntityFrameworkCore.Migrations;

namespace ImageBase.WebApp.Migrations
{
    public partial class Init_12_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_catalogs_catalogs_parent_catalog_id",
                table: "catalogs");

            migrationBuilder.AddColumn<string>(
                name: "external_id",
                table: "images",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "file_size",
                table: "images",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "height",
                table: "images",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "large_preview_url",
                table: "images",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "original_url",
                table: "images",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "service_id",
                table: "images",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "small_preview_url",
                table: "images",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "width",
                table: "images",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_images_external_id_service_id",
                table: "images",
                columns: new[] { "external_id", "service_id" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_catalogs_catalogs_parent_catalog_id",
                table: "catalogs",
                column: "parent_catalog_id",
                principalTable: "catalogs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_catalogs_catalogs_parent_catalog_id",
                table: "catalogs");

            migrationBuilder.DropIndex(
                name: "IX_images_external_id_service_id",
                table: "images");

            migrationBuilder.DropColumn(
                name: "external_id",
                table: "images");

            migrationBuilder.DropColumn(
                name: "file_size",
                table: "images");

            migrationBuilder.DropColumn(
                name: "height",
                table: "images");

            migrationBuilder.DropColumn(
                name: "large_preview_url",
                table: "images");

            migrationBuilder.DropColumn(
                name: "original_url",
                table: "images");

            migrationBuilder.DropColumn(
                name: "service_id",
                table: "images");

            migrationBuilder.DropColumn(
                name: "small_preview_url",
                table: "images");

            migrationBuilder.DropColumn(
                name: "width",
                table: "images");

            migrationBuilder.AddForeignKey(
                name: "FK_catalogs_catalogs_parent_catalog_id",
                table: "catalogs",
                column: "parent_catalog_id",
                principalTable: "catalogs",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
