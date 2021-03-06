﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using crowdFunding.Core.Data;

namespace crowdFunding.Core.Migrations
{
    [DbContext(typeof(CrowdFundingDbContext))]
    [Migration("20200606131911_myMig")]
    partial class myMig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("crowdFunding.Core.Model.BackedProjects", b =>
                {
                    b.Property<int>("BackedProjectsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfBackers")
                        .HasColumnType("int");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProjectCreatorFirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProjectCreatorId")
                        .HasColumnType("int");

                    b.Property<string>("ProjectCreatorLastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("BackedProjectsId");

                    b.HasIndex("UserId");

                    b.ToTable("BackedProjects");
                });

            modelBuilder.Entity("crowdFunding.Core.Model.Photo", b =>
                {
                    b.Property<int>("PhotoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PhotoId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Photo");
                });

            modelBuilder.Entity("crowdFunding.Core.Model.Posts", b =>
                {
                    b.Property<int>("PostsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Post")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("PostsId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("crowdFunding.Core.Model.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("AmountGathered")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("AmountRequired")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DueTo")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfBackers")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("crowdFunding.Core.Model.Reward", b =>
                {
                    b.Property<int>("RewardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RewardPackageId")
                        .HasColumnType("int");

                    b.HasKey("RewardId");

                    b.HasIndex("RewardPackageId");

                    b.ToTable("Reward");
                });

            modelBuilder.Entity("crowdFunding.Core.Model.RewardPackage", b =>
                {
                    b.Property<int>("RewardPackageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal?>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("RewardPackageId");

                    b.HasIndex("ProjectId");

                    b.ToTable("RewardPackage");
                });

            modelBuilder.Entity("crowdFunding.Core.Model.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

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

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("crowdFunding.Core.Model.Video", b =>
                {
                    b.Property<int>("VideoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VideoId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Video");
                });

            modelBuilder.Entity("crowdFunding.Core.Model.BackedProjects", b =>
                {
                    b.HasOne("crowdFunding.Core.Model.User", null)
                        .WithMany("BackedProjectsList")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("crowdFunding.Core.Model.Photo", b =>
                {
                    b.HasOne("crowdFunding.Core.Model.Project", null)
                        .WithMany("Photos")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("crowdFunding.Core.Model.Posts", b =>
                {
                    b.HasOne("crowdFunding.Core.Model.Project", null)
                        .WithMany("Posts")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("crowdFunding.Core.Model.Project", b =>
                {
                    b.HasOne("crowdFunding.Core.Model.User", "User")
                        .WithMany("CreatedProjectsList")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("crowdFunding.Core.Model.Reward", b =>
                {
                    b.HasOne("crowdFunding.Core.Model.RewardPackage", null)
                        .WithMany("Rewards")
                        .HasForeignKey("RewardPackageId");
                });

            modelBuilder.Entity("crowdFunding.Core.Model.RewardPackage", b =>
                {
                    b.HasOne("crowdFunding.Core.Model.Project", null)
                        .WithMany("RewardPackages")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("crowdFunding.Core.Model.Video", b =>
                {
                    b.HasOne("crowdFunding.Core.Model.Project", null)
                        .WithMany("Videos")
                        .HasForeignKey("ProjectId");
                });
#pragma warning restore 612, 618
        }
    }
}
