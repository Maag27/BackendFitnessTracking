using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Milks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Litters = table.Column<int>(type: "integer", nullable: false),
                    Farm = table.Column<string>(type: "text", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Milks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserMetrics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    Nombre = table.Column<string>(type: "text", nullable: true),
                    Apellido = table.Column<string>(type: "text", nullable: true),
                    Edad = table.Column<int>(type: "integer", nullable: false),
                    Peso = table.Column<double>(type: "double precision", nullable: false),
                    Altura = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMetrics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cows",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Race = table.Column<string>(type: "text", nullable: true),
                    MilkId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cows_Milks_MilkId",
                        column: x => x.MilkId,
                        principalTable: "Milks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cows_MilkId",
                table: "Cows",
                column: "MilkId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cows");

            migrationBuilder.DropTable(
                name: "UserMetrics");

            migrationBuilder.DropTable(
                name: "Milks");
        }
    }
}
