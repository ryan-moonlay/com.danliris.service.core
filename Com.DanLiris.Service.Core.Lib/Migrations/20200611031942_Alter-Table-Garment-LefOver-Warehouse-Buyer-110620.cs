using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Com.DanLiris.Service.Core.Lib.Migrations
{
    public partial class AlterTableGarmentLefOverWarehouseBuyer110620 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NPWP",
                table: "GarmentLeftoverWarehouseBuyers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WPName",
                table: "GarmentLeftoverWarehouseBuyers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NPWP",
                table: "GarmentLeftoverWarehouseBuyers");

            migrationBuilder.DropColumn(
                name: "WPName",
                table: "GarmentLeftoverWarehouseBuyers");
        }
    }
}
