using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using NpgsqlTypes;

namespace ImageBase.WebApp.Migrations
{
    public partial class FTSIntegration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_catalogs_catalogs_parent_catalog_id",
                table: "catalogs");

            migrationBuilder.CreateTable(
                name: "images_ft_search",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    image_id = table.Column<long>(nullable: false),
                    image_vector = table.Column<NpgsqlTsVector>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_images_ft_search", x => x.id);
                    table.ForeignKey(
                        name: "FK_images_ft_search_images_image_id",
                        column: x => x.image_id,
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_images_ft_search_image_id",
                table: "images_ft_search",
                column: "image_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_catalogs_catalogs_parent_catalog_id",
                table: "catalogs",
                column: "parent_catalog_id",
                principalTable: "catalogs",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.Sql(@"CREATE OR REPLACE FUNCTION make_tsvector(title TEXT, key_words TEXT,description TEXT)
                    RETURNS tsvector AS $$
                    BEGIN
                    RETURN (setweight(to_tsvector('english', coalesce(title,'')),'A') ||
                    setweight(to_tsvector('russian', coalesce(title,'')), 'A')||
                    setweight(to_tsvector('english', coalesce(key_words,'')), 'B')||
                    setweight(to_tsvector('russian', coalesce(key_words,'')), 'B')||
                    setweight(to_tsvector('english', coalesce(description,'')), 'C')||
                    setweight(to_tsvector('russian', coalesce(description,'')), 'C'));
                    END
                    $$ LANGUAGE 'plpgsql' IMMUTABLE;");
            migrationBuilder.Sql(@"CREATE OR REPLACE FUNCTION image_tsvector_trigger() RETURNS trigger AS $$
                    BEGIN
                      INSERT INTO images_ft_search (image_id,image_vector) 
                                VALUES (NEW.Id, make_tsvector(NEW.title,NEW.key_words,NEW.description))
                                ON CONFLICT (image_id) DO UPDATE 
                                  SET image_id = NEW.Id, 
                                      image_vector = make_tsvector(NEW.title,NEW.key_words,NEW.description);                        
                      RETURN NEW;
                    END
                    $$ LANGUAGE plpgsql;");
            migrationBuilder.Sql(@"CREATE EXTENSION rum;");

            migrationBuilder.Sql(@"CREATE INDEX IF NOT EXISTS image_vector_idx ON ""images_ft_search"" 
                                    USING RUM (""image_vector"");");
            migrationBuilder.Sql(@"CREATE TRIGGER image_search_vector_update
                                    AFTER INSERT OR UPDATE OF title,key_words,description ON ""images"" 
                                    FOR EACH ROW EXECUTE PROCEDURE image_tsvector_trigger();");
            migrationBuilder.Sql(@"CREATE OR REPLACE FUNCTION setweight(query tsquery, weights text) RETURNS tsquery AS $$
                                    SELECT regexp_replace(
                                                query::text, 
                                                '(?<=[^ !])'':?(\*?)A?B?C?D?', ''':\1'||weights, 
                                                'g'
                                            )::tsquery;
                                    $$ LANGUAGE SQL IMMUTABLE;");
            migrationBuilder.Sql(@"CREATE OR REPLACE FUNCTION images_fts(search_query text, weights text,query_limit int,query_offset int) RETURNS 
                                    TABLE (""id"" bigint,title text,description text,key_words text) AS
                                    $$

                                        SELECT images.id, images.title, images.description, images.key_words FROM images, (
                                        SELECT image_id, image_vector, image_vector <=> setweight(plainto_tsquery(search_query), weights) AS weight

                                        FROM images_ft_search

                                        WHERE image_vector  @@ setweight(plainto_tsquery(search_query), weights)

                                        ORDER BY weight ASC

                                        LIMIT query_limit OFFSET query_offset) as ids

                                        WHERE images.id = ids.image_id
                                    $$ LANGUAGE SQL IMMUTABLE; ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_catalogs_catalogs_parent_catalog_id",
                table: "catalogs");

            migrationBuilder.DropTable(
                name: "images_ft_search");

            migrationBuilder.AddForeignKey(
                name: "FK_catalogs_catalogs_parent_catalog_id",
                table: "catalogs",
                column: "parent_catalog_id",
                principalTable: "catalogs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.Sql(@"DROP FUNCTION make_tsvector;");
            migrationBuilder.Sql(@"DROP INDEX image_vector_idx;");
            migrationBuilder.Sql(@"DROP EXTENSION rum;");
            migrationBuilder.Sql(@"DROP TRIGGER image_search_vector_update;");
            migrationBuilder.Sql(@"DROP FUNCTION images_fts;");
            migrationBuilder.Sql(@"DROP FUNCTION setweight;");
            migrationBuilder.Sql(@"DROP FUNCTION image_tsvector_trigger;");
        }
    }
}
