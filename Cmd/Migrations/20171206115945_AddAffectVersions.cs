using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Cmd.Migrations
{
    public partial class AddAffectVersions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AffectsFullVersion",
                table: "Issues",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AffectsShortVersion",
                table: "Issues",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FixFullVersion",
                table: "Issues",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FixShortVersion",
                table: "Issues",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AffectsFullVersion",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "AffectsShortVersion",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "FixFullVersion",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "FixShortVersion",
                table: "Issues");
        }
    }
}
