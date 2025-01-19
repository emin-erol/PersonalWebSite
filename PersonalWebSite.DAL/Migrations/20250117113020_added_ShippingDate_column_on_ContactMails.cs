﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalWebSite.DAL.Migrations
{
    /// <inheritdoc />
    public partial class added_ShippingDate_column_on_ContactMails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ShippingDate",
                table: "ContactMails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippingDate",
                table: "ContactMails");
        }
    }
}
