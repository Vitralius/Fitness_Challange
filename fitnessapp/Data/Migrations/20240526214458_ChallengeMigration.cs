using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fitnessapp.Migrations.ChallengeDatabase
{
    /// <inheritdoc />
    public partial class ChallengeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_challenge",
                columns: table => new
                {
                    challengeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    parentId = table.Column<int>(type: "int", nullable: true),
                    title = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: true),
                    category = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: true),
                    endDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_challenge", x => x.challengeId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_challenge");
        }
    }
}
