using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Cmd.Migrations {
    public partial class TableIssues : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AssigneeKey = table.Column<string>(nullable: true),
                    AssigneeName = table.Column<string>(nullable: true),
                    AssigneeShortName = table.Column<string>(nullable: true),
                    BugCause = table.Column<string>(nullable: true),
                    BugSource = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    DateDayOfWeek = table.Column<int>(nullable: false),
                    DateMonth = table.Column<int>(nullable: false),
                    DateQuarter = table.Column<int>(nullable: false),
                    DateYear = table.Column<int>(nullable: false),
                    DaysInDevelopment = table.Column<double>(nullable: true),
                    DaysInImpediment = table.Column<double>(nullable: true),
                    DaysInProgress = table.Column<double>(nullable: true),
                    DaysInPullRequest = table.Column<double>(nullable: true),
                    DaysInTest = table.Column<double>(nullable: true),
                    DaysToDone = table.Column<double>(nullable: true),
                    DaysToReject = table.Column<double>(nullable: true),
                    DaysToResolve = table.Column<double>(nullable: true),
                    DaysToStart = table.Column<double>(nullable: true),
                    Epic = table.Column<string>(nullable: true),
                    HoursSpent = table.Column<double>(nullable: true),
                    HoursSpentPerAssignee = table.Column<double>(nullable: true),
                    HoursSpentPerAssigneePerc = table.Column<double>(nullable: true),
                    HoursSpentPerOthers = table.Column<double>(nullable: true),
                    HoursSpentPerOthersPerc = table.Column<double>(nullable: true),
                    HoursSpentPerReviewer = table.Column<double>(nullable: true),
                    HoursSpentPerReviewerPerc = table.Column<double>(nullable: true),
                    ImplementerFunding = table.Column<string>(nullable: true),
                    Key = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ProductionFactor = table.Column<string>(nullable: true),
                    ProjectKey = table.Column<string>(nullable: true),
                    ProjectName = table.Column<string>(nullable: true),
                    Release = table.Column<string>(nullable: true),
                    ReviewerKey = table.Column<string>(nullable: true),
                    ReviewerName = table.Column<string>(nullable: true),
                    ReviewerShortName = table.Column<string>(nullable: true),
                    ServiceDesk = table.Column<string>(nullable: true),
                    Sprint = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Storypoints = table.Column<int>(nullable: true),
                    Subtasks = table.Column<int>(nullable: true),
                    TechnologyUpdateFactor = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Issues", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "Issues_Key",
                table: "Issues",
                column: "Key",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(
                name: "Issues");
        }
    }
}
