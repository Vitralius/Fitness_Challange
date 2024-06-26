﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fitnessapp.Migrations
{
    /// <inheritdoc />
    public partial class UserChallengeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "challenge",
                columns: table => new
                {
                    challengeId = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_challenge", x => x.challengeId);
                });

            migrationBuilder.CreateTable(
                name: "city",
                columns: table => new
                {
                    cityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cityName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_city", x => x.cityId);
                });

            migrationBuilder.CreateTable(
                name: "favorite",
                columns: table => new
                {
                    favoriteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: true),
                    challengeId = table.Column<int>(type: "int", nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_favorite", x => x.favoriteId);
                });

            migrationBuilder.CreateTable(
                name: "participate",
                columns: table => new
                {
                    participateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    challengeId = table.Column<int>(type: "int", nullable: true),
                    userId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_participate", x => x.participateId);
                });

            migrationBuilder.CreateTable(
                name: "userDetail",
                columns: table => new
                {
                    detailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    city = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    photo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    userId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    bio = table.Column<string>(type: "varchar(450)", unicode: false, maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userDetail", x => x.detailId);
                });

            migrationBuilder.CreateTable(
                name: "userRate",
                columns: table => new
                {
                    rateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    challengeId = table.Column<int>(type: "int", nullable: true),
                    rate = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userRate", x => x.rateId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "challenge");

            migrationBuilder.DropTable(
                name: "city");

            migrationBuilder.DropTable(
                name: "favorite");

            migrationBuilder.DropTable(
                name: "participate");

            migrationBuilder.DropTable(
                name: "userDetail");

            migrationBuilder.DropTable(
                name: "userRate");
        }
    }
}
