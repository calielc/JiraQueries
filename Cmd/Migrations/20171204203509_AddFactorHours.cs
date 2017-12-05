using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Cmd.Migrations
{
    public partial class AddFactorHours : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ProductizationHours",
                table: "Issues",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TechnologyUpdateHours",
                table: "Issues",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductizationHours",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "TechnologyUpdateHours",
                table: "Issues");
        }
    }
}
