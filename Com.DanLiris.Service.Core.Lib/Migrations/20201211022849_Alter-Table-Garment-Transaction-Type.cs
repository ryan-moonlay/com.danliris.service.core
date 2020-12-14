using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Com.DanLiris.Service.Core.Lib.Migrations
{
    public partial class AlterTableGarmentTransactionType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "COACode",
                table: "GarmentTransactionTypes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "COAId",
                table: "GarmentTransactionTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "COAName",
                table: "GarmentTransactionTypes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "COACode",
                table: "GarmentTransactionTypes");

            migrationBuilder.DropColumn(
                name: "COAId",
                table: "GarmentTransactionTypes");

            migrationBuilder.DropColumn(
                name: "COAName",
                table: "GarmentTransactionTypes");
        }
    }
}
