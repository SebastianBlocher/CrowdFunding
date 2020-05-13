using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace crowdFunding
{
    public class CrowdFundingDbContext : DbContext
    {
        private readonly string ConnectionString =
            "Server =localhost; " +
            "Database =CrowdFunding; " +
            "User Id =sa; " +
            "Password =admin!@#123;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<ProjectCreator>()
                .ToTable("ProjectCreator");

            modelBuilder
                .Entity<Project>()
                .ToTable("Project");

            modelBuilder
                .Entity<Backer>()
                .ToTable("Backer");

            modelBuilder
                .Entity<ProjectBacker>()
                .ToTable("ProjectBacker");

            modelBuilder
                .Entity<ProjectBacker>()
                .HasKey(pb => new { pb.BackerId, pb.ProjectId });

            modelBuilder
               .Entity<Reward>()
               .ToTable("Reward");

            modelBuilder
               .Entity<RewardPackage>()
               .ToTable("RewardPackage");

            modelBuilder
               .Entity<BackerRewardPackage>()
               .ToTable("BackerRewardPackage");

            modelBuilder
                .Entity<BackerRewardPackage>()
                .HasKey(br => new { br.BackerId, br.RewardPackageId });
        }
    }
}
