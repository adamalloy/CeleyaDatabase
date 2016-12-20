using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ISGeoDatabase.Data.Migrations
{
    public partial class Victoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubsidencePoint",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Angle = table.Column<double>(nullable: true),
                    Coordinates = table.Column<string>(nullable: false),
                    Country = table.Column<string>(nullable: false),
                    DataType = table.Column<string>(nullable: true),
                    DateAndTime = table.Column<DateTime>(nullable: false),
                    Fault = table.Column<string>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    PrependedText = table.Column<string>(nullable: true),
                    Region = table.Column<string>(nullable: false),
                    Slip = table.Column<double>(nullable: true),
                    StationNumber = table.Column<string>(nullable: true),
                    StationRef = table.Column<string>(nullable: true),
                    Strike = table.Column<double>(nullable: true),
                    TeamNumber = table.Column<string>(nullable: true),
                    Throw = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubsidencePoint", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    SubsidencePointID = table.Column<int>(nullable: true),
                    URL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Image_SubsidencePoint_SubsidencePointID",
                        column: x => x.SubsidencePointID,
                        principalTable: "SubsidencePoint",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Image_SubsidencePointID",
                table: "Image",
                column: "SubsidencePointID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "SubsidencePoint");
        }
    }
}
