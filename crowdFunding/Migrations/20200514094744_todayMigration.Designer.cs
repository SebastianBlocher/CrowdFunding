﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using crowdFunding;

namespace crowdFunding.Migrations
{
    [DbContext(typeof(CrowdFundingDbContext))]
    [Migration("20200514094744_todayMigration")]
    partial class todayMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("crowdFunding.Backer", b =>
                {
                    b.Property<int>("BackerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BackerId");

                    b.ToTable("Backer");
                });

            modelBuilder.Entity("crowdFunding.BackerRewardPackage", b =>
                {
                    b.Property<int>("BackerId")
                        .HasColumnType("int");

                    b.Property<int>("RewardPackageId")
                        .HasColumnType("int");

                    b.HasKey("BackerId", "RewardPackageId");

                    b.HasIndex("RewardPackageId");

                    b.ToTable("BackerRewardPackage");
                });

            modelBuilder.Entity("crowdFunding.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProjectCreatorId")
                        .HasColumnType("int");

                    b.HasKey("ProjectId");

                    b.HasIndex("ProjectCreatorId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("crowdFunding.ProjectBacker", b =>
                {
                    b.Property<int>("BackerId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("BackerId", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectBacker");
                });

            modelBuilder.Entity("crowdFunding.ProjectCreator", b =>
                {
                    b.Property<int>("ProjectCreatorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProjectCreatorId");

                    b.ToTable("ProjectCreator");
                });

            modelBuilder.Entity("crowdFunding.Reward", b =>
                {
                    b.Property<int>("RewardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RewardPackageId")
                        .HasColumnType("int");

                    b.HasKey("RewardId");

                    b.HasIndex("RewardPackageId");

                    b.ToTable("Reward");
                });

            modelBuilder.Entity("crowdFunding.RewardPackage", b =>
                {
                    b.Property<int>("RewardPackageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Ammount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("RewardPackageId");

                    b.HasIndex("ProjectId");

                    b.ToTable("RewardPackage");
                });

            modelBuilder.Entity("crowdFunding.BackerRewardPackage", b =>
                {
                    b.HasOne("crowdFunding.Backer", "Backer")
                        .WithMany()
                        .HasForeignKey("BackerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("crowdFunding.RewardPackage", "RewardPackage")
                        .WithMany()
                        .HasForeignKey("RewardPackageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("crowdFunding.Project", b =>
                {
                    b.HasOne("crowdFunding.ProjectCreator", null)
                        .WithMany("ProjectList")
                        .HasForeignKey("ProjectCreatorId");
                });

            modelBuilder.Entity("crowdFunding.ProjectBacker", b =>
                {
                    b.HasOne("crowdFunding.Backer", "Backer")
                        .WithMany()
                        .HasForeignKey("BackerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("crowdFunding.Project", "Project")
                        .WithMany("ProjectBackers")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("crowdFunding.Reward", b =>
                {
                    b.HasOne("crowdFunding.RewardPackage", null)
                        .WithMany("Reward")
                        .HasForeignKey("RewardPackageId");
                });

            modelBuilder.Entity("crowdFunding.RewardPackage", b =>
                {
                    b.HasOne("crowdFunding.Project", null)
                        .WithMany("Rewards")
                        .HasForeignKey("ProjectId");
                });
#pragma warning restore 612, 618
        }
    }
}