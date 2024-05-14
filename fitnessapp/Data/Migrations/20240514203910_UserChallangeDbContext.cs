using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fitnessapp.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserChallangeDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_challange",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: true),
                    category = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: true),
                    endDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    userId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_challange", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_challange");
        }
    }
}
