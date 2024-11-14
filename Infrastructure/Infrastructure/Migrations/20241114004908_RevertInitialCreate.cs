using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RevertInitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Milks",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "uuid_generate_v4()");

            migrationBuilder.CreateTable(
                name: "RoutineTemplates",
                columns: table => new
                {
                    RoutineTemplateId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoutineName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutineTemplates", x => x.RoutineTemplateId);
                });

            migrationBuilder.CreateTable(
                name: "UserRoutines",
                columns: table => new
                {
                    UserRoutineId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    RoutineTemplateId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoutines", x => x.UserRoutineId);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseTemplates",
                columns: table => new
                {
                    ExerciseId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExerciseTemplateId = table.Column<int>(type: "integer", nullable: false),
                    RoutineTemplateId = table.Column<int>(type: "integer", nullable: false),
                    ExerciseName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseTemplates", x => x.ExerciseId);
                    table.ForeignKey(
                        name: "FK_ExerciseTemplates_RoutineTemplates_RoutineTemplateId",
                        column: x => x.RoutineTemplateId,
                        principalTable: "RoutineTemplates",
                        principalColumn: "RoutineTemplateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserExercises",
                columns: table => new
                {
                    UserExerciseId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserRoutineId = table.Column<int>(type: "integer", nullable: false),
                    ExerciseTemplateId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserExercises", x => x.UserExerciseId);
                    table.ForeignKey(
                        name: "FK_UserExercises_UserRoutines_UserRoutineId",
                        column: x => x.UserRoutineId,
                        principalTable: "UserRoutines",
                        principalColumn: "UserRoutineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseDetailTemplates",
                columns: table => new
                {
                    ExerciseDetailId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DetailTemplateId = table.Column<int>(type: "integer", nullable: false),
                    ExerciseTemplateId = table.Column<int>(type: "integer", nullable: false),
                    Series = table.Column<int>(type: "integer", nullable: false),
                    Repetitions = table.Column<string>(type: "text", nullable: true),
                    RestTime = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseDetailTemplates", x => x.ExerciseDetailId);
                    table.ForeignKey(
                        name: "FK_ExerciseDetailTemplates_ExerciseTemplates_ExerciseTemplateId",
                        column: x => x.ExerciseTemplateId,
                        principalTable: "ExerciseTemplates",
                        principalColumn: "ExerciseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserExerciseDetails",
                columns: table => new
                {
                    UserDetailId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserExerciseId = table.Column<int>(type: "integer", nullable: false),
                    Series = table.Column<int>(type: "integer", nullable: false),
                    Repetitions = table.Column<string>(type: "text", nullable: true),
                    RestTime = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserExerciseDetails", x => x.UserDetailId);
                    table.ForeignKey(
                        name: "FK_UserExerciseDetails_UserExercises_UserExerciseId",
                        column: x => x.UserExerciseId,
                        principalTable: "UserExercises",
                        principalColumn: "UserExerciseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseDetailTemplates_ExerciseTemplateId",
                table: "ExerciseDetailTemplates",
                column: "ExerciseTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseTemplates_RoutineTemplateId",
                table: "ExerciseTemplates",
                column: "RoutineTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_UserExerciseDetails_UserExerciseId",
                table: "UserExerciseDetails",
                column: "UserExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserExercises_UserRoutineId",
                table: "UserExercises",
                column: "UserRoutineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseDetailTemplates");

            migrationBuilder.DropTable(
                name: "UserExerciseDetails");

            migrationBuilder.DropTable(
                name: "ExerciseTemplates");

            migrationBuilder.DropTable(
                name: "UserExercises");

            migrationBuilder.DropTable(
                name: "RoutineTemplates");

            migrationBuilder.DropTable(
                name: "UserRoutines");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Milks",
                type: "uuid",
                nullable: false,
                defaultValueSql: "uuid_generate_v4()",
                oldClrType: typeof(Guid),
                oldType: "uuid");
        }
    }
}
