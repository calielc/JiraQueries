using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Cmd.Migrations
{
    public partial class addLabels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Release",
                table: "Issues",
                newName: "SprintRelease");

            migrationBuilder.AddColumn<string>(
                name: "LabelBacklog",
                table: "Issues",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LabelNaoPlanejado",
                table: "Issues",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LabelBacklog",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "LabelNaoPlanejado",
                table: "Issues");

            migrationBuilder.RenameColumn(
                name: "SprintRelease",
                table: "Issues",
                newName: "Release");
        }
    }
}
