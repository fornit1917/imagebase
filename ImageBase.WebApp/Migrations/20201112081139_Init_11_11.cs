using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ImageBase.WebApp.Migrations
{
    public partial class Init_11_11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    key_words = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_images", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    role_name = table.Column<string>(maxLength: 256, nullable: true),
                    normalized_role_name = table.Column<string>(maxLength: 256, nullable: true),
                    concurrency_stamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    user_name = table.Column<string>(maxLength: 256, nullable: true),
                    normalized_user_name = table.Column<string>(maxLength: 256, nullable: true),
                    email = table.Column<string>(maxLength: 256, nullable: true),
                    normalized_email = table.Column<string>(maxLength: 256, nullable: true),
                    email_confirmed = table.Column<bool>(nullable: false),
                    password_hash = table.Column<string>(nullable: true),
                    security_stamp = table.Column<string>(nullable: true),
                    concurrency_stamp = table.Column<string>(nullable: true),
                    phone_number = table.Column<string>(nullable: true),
                    phone_number_confirmed = table.Column<bool>(nullable: false),
                    two_factor_enabled = table.Column<bool>(nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(nullable: true),
                    lockout_enabled = table.Column<bool>(nullable: false),
                    access_failed_count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles_claims",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_id = table.Column<string>(nullable: false),
                    claim_type = table.Column<string>(nullable: true),
                    claim_value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles_claims", x => x.id);
                    table.ForeignKey(
                        name: "FK_roles_claims_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catalogs",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(maxLength: 30, nullable: false),
                    parent_catalog_id = table.Column<int>(nullable: true),
                    user_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catalogs", x => x.id);
                    table.ForeignKey(
                        name: "FK_catalogs_catalogs_parent_catalog_id",
                        column: x => x.parent_catalog_id,
                        principalTable: "catalogs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catalogs_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "users_claims",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(nullable: false),
                    claim_type = table.Column<string>(nullable: true),
                    claim_value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_claims", x => x.id);
                    table.ForeignKey(
                        name: "FK_users_claims_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users_logins",
                columns: table => new
                {
                    login_provider = table.Column<string>(nullable: false),
                    provider_key = table.Column<string>(nullable: false),
                    provider_display_name = table.Column<string>(nullable: true),
                    user_id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_logins", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "FK_users_logins_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users_roles",
                columns: table => new
                {
                    users_id = table.Column<string>(nullable: false),
                    roles_id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_roles", x => new { x.users_id, x.roles_id });
                    table.ForeignKey(
                        name: "FK_users_roles_roles_roles_id",
                        column: x => x.roles_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_users_roles_users_users_id",
                        column: x => x.users_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users_tokens",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    login_provider = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_tokens", x => new { x.id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "FK_users_tokens_users_id",
                        column: x => x.id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "images_catalogs",
                columns: table => new
                {
                    image_id = table.Column<long>(nullable: false),
                    catalog_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_images_catalogs", x => new { x.catalog_id, x.image_id });
                    table.ForeignKey(
                        name: "FK_images_catalogs_catalogs_catalog_id",
                        column: x => x.catalog_id,
                        principalTable: "catalogs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_images_catalogs_images_image_id",
                        column: x => x.image_id,
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_catalogs_parent_catalog_id",
                table: "catalogs",
                column: "parent_catalog_id");

            migrationBuilder.CreateIndex(
                name: "IX_catalogs_user_id",
                table: "catalogs",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_images_catalogs_image_id",
                table: "images_catalogs",
                column: "image_id");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "roles",
                column: "normalized_role_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_roles_claims_role_id",
                table: "roles_claims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "users",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "users",
                column: "normalized_user_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_claims_user_id",
                table: "users_claims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_logins_user_id",
                table: "users_logins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_roles_roles_id",
                table: "users_roles",
                column: "roles_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "images_catalogs");

            migrationBuilder.DropTable(
                name: "roles_claims");

            migrationBuilder.DropTable(
                name: "users_claims");

            migrationBuilder.DropTable(
                name: "users_logins");

            migrationBuilder.DropTable(
                name: "users_roles");

            migrationBuilder.DropTable(
                name: "users_tokens");

            migrationBuilder.DropTable(
                name: "catalogs");

            migrationBuilder.DropTable(
                name: "images");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
