﻿// <auto-generated />
using Cmd.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Cmd.Migrations
{
    [DbContext(typeof(MySqlContext))]
    partial class MySqlContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("Cmd.Database.MySqlIssue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AssigneeKey");

                    b.Property<string>("AssigneeName");

                    b.Property<string>("AssigneeShortName");

                    b.Property<string>("BugCause");

                    b.Property<string>("BugSource");

                    b.Property<DateTime>("Date");

                    b.Property<int>("DateDayOfWeek");

                    b.Property<int>("DateMonth");

                    b.Property<int>("DateQuarter");

                    b.Property<int>("DateYear");

                    b.Property<double?>("DaysInDevelopment");

                    b.Property<double?>("DaysInImpediment");

                    b.Property<double?>("DaysInProgress");

                    b.Property<double?>("DaysInPullRequest");

                    b.Property<double?>("DaysInTest");

                    b.Property<double?>("DaysToDone");

                    b.Property<double?>("DaysToReject");

                    b.Property<double?>("DaysToResolve");

                    b.Property<double?>("DaysToStart");

                    b.Property<string>("Epic");

                    b.Property<double?>("HoursSpent");

                    b.Property<double?>("HoursSpentPerAssignee");

                    b.Property<double?>("HoursSpentPerAssigneePerc");

                    b.Property<double?>("HoursSpentPerOthers");

                    b.Property<double?>("HoursSpentPerOthersPerc");

                    b.Property<double?>("HoursSpentPerReviewer");

                    b.Property<double?>("HoursSpentPerReviewerPerc");

                    b.Property<string>("ImplementerFunding");

                    b.Property<string>("Key");

                    b.Property<string>("LabelBacklog");

                    b.Property<string>("LabelNaoPlanejado");

                    b.Property<string>("Name");

                    b.Property<string>("ProductionFactor");

                    b.Property<string>("ProjectKey");

                    b.Property<string>("ProjectName");

                    b.Property<string>("ReviewerKey");

                    b.Property<string>("ReviewerName");

                    b.Property<string>("ReviewerShortName");

                    b.Property<string>("ServiceDesk");

                    b.Property<string>("Sprint");

                    b.Property<string>("SprintRelease");

                    b.Property<string>("Status");

                    b.Property<int?>("Storypoints");

                    b.Property<int?>("Subtasks");

                    b.Property<string>("TechnologyUpdateFactor");

                    b.Property<string>("Title");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("Key")
                        .IsUnique()
                        .HasName("Issues_Key");

                    b.ToTable("Issues");
                });
#pragma warning restore 612, 618
        }
    }
}