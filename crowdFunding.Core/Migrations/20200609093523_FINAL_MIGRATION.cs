﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace crowdFunding.Core.Migrations
{
    public partial class FINAL_MIGRATION : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "User",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
