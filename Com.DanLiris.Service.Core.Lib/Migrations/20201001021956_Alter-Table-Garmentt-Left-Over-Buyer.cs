using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Com.DanLiris.Service.Core.Lib.Migrations
{
    public partial class AlterTableGarmenttLeftOverBuyer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NIK",
                table: "GarmentLeftoverWarehouseBuyers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "GarmentLeftoverWarehouseBuyers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NIK",
                table: "GarmentLeftoverWarehouseBuyers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "GarmentLeftoverWarehouseBuyers");
        }
    }
}
