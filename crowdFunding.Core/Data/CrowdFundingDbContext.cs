﻿using crowdFunding.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace crowdFunding.Core.Data
{
    public class CrowdFundingDbContext : DbContext
    {
        public CrowdFundingDbContext(DbContextOptions<CrowdFundingDbContext> options)
            : base(options) { }

        private readonly string ConnectionString =
            "Server =localhost; " +
            "Database = CrowdFunding; " +
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
                .Entity<User>()
                .ToTable("User");

            modelBuilder
                .Entity<Project>()
                .ToTable("Project");

            modelBuilder
                .Entity<BackedProjects>()
                .ToTable("BackedProjects");

            modelBuilder
                .Entity<Reward>()
                .ToTable("Reward");

            modelBuilder
               .Entity<RewardPackage>()
               .ToTable("RewardPackage");

            modelBuilder
                .Entity<Posts>()
                .ToTable("Posts");

            modelBuilder
                .Entity<Photo>()
                .ToTable("Photo");

            modelBuilder
                .Entity<Video>()
                .ToTable("Video");
        }
    }
}
