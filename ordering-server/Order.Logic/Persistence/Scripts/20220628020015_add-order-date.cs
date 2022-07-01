﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Order.Logic.Persistence.Scripts
{
    public partial class addorderdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "order_date",
                table: "ProductTransaction",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "order_date",
                table: "ProductTransaction");
        }
    }
}
