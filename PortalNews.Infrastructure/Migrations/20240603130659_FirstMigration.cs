using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PortalNews.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_journalists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password_hash = table.Column<byte[]>(type: "bytea", nullable: false),
                    password_salt = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_journalists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_types_news",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    journalist_id = table.Column<int>(type: "integer", nullable: false),
                    type_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_types_news", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_types_news_tb_journalists_journalist_id",
                        column: x => x.journalist_id,
                        principalTable: "tb_journalists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_news",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    journalist_id = table.Column<int>(type: "integer", nullable: false),
                    type_news_id = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    news_body = table.Column<string>(type: "text", nullable: false),
                    published_date = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_news", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_news_tb_journalists_journalist_id",
                        column: x => x.journalist_id,
                        principalTable: "tb_journalists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_news_tb_types_news_type_news_id",
                        column: x => x.type_news_id,
                        principalTable: "tb_types_news",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_journalists_email",
                table: "tb_journalists",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_news_journalist_id",
                table: "tb_news",
                column: "journalist_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_news_type_news_id",
                table: "tb_news",
                column: "type_news_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_types_news_journalist_id",
                table: "tb_types_news",
                column: "journalist_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_news");

            migrationBuilder.DropTable(
                name: "tb_types_news");

            migrationBuilder.DropTable(
                name: "tb_journalists");
        }
    }
}
