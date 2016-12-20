using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ISGeoDatabase.Data.Migrations
{
    public partial class Stephen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Region",
                table: "SubsidencePoint",
                maxLength: 70,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "SubsidencePoint",
                maxLength: 70,
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Region",
                table: "SubsidencePoint",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "SubsidencePoint",
                nullable: false);
        }
    }
}
