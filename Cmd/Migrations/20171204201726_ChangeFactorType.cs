using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Cmd.Migrations {
    public partial class ChangeFactorType : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.RenameColumn(
                name: "ProductionFactor",
                table: "Issues",
                newName: "Productization");

            migrationBuilder.RenameColumn(
                name: "TechnologyUpdateFactor",
                table: "Issues",
                newName: "TechnologyUpdate");

            migrationBuilder.AddColumn<double>(
                name: "ProductizationFactor",
                table: "Issues",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TechnologyUpdateFactor",
                table: "Issues",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropColumn(
                name: "TechnologyUpdateFactor",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "ProductizationFactor",
                table: "Issues");

            migrationBuilder.RenameColumn(
                name: "TechnologyUpdate",
                table: "Issues",
                newName: "TechnologyUpdateFactor");

            migrationBuilder.RenameColumn(
                name: "Productization",
                table: "Issues",
                newName: "ProductionFactor");
        }
    }
}
