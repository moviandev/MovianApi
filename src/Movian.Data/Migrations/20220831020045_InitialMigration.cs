﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movian.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_Suplier",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "varchar(200)", nullable: false),
                    Document = table.Column<string>(type: "varchar(14)", nullable: false),
                    Active = table.Column<bool>(type: "integer", nullable: false),
                    SuplierType = table.Column<int>(type: "varchar(2)", nullable: false),
                    Id_Address = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Id_Suplier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_Address",
                columns: table => new
                {
                    Id_Address = table.Column<Guid>(type: "TEXT", nullable: false),
                    Id_Suplier = table.Column<Guid>(type: "TEXT", nullable: false),
                    CompleteAddress = table.Column<string>(type: "varchar(100)", nullable: false),
                    Neighborhood = table.Column<string>(type: "varchar(100)", nullable: true),
                    ZipCode = table.Column<string>(type: "varchar(8)", nullable: true),
                    City = table.Column<string>(type: "varchar(100)", nullable: true),
                    State = table.Column<string>(type: "varchar(50)", nullable: true),
                    AdditionalAddressData = table.Column<string>(type: "varchar(200)", nullable: true),
                    Number = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Id_Address", x => x.Id_Address);
                    table.ForeignKey(
                        name: "FK_TB_Address_TB_Suplier_Id_Suplier",
                        column: x => x.Id_Suplier,
                        principalTable: "TB_Suplier",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TB_Products",
                columns: table => new
                {
                    Id_Product = table.Column<Guid>(type: "TEXT", nullable: false),
                    Id_Supplier = table.Column<Guid>(type: "TEXT", nullable: false),
                    Value = table.Column<decimal>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "varchar(200)", nullable: false),
                    Description = table.Column<string>(type: "varchar(1000)", nullable: false),
                    Image = table.Column<string>(type: "varchar(100)", nullable: false),
                    CreatedIn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Active = table.Column<bool>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Id_Product", x => x.Id_Product);
                    table.ForeignKey(
                        name: "FK_TB_Products_TB_Suplier_Id_Supplier",
                        column: x => x.Id_Supplier,
                        principalTable: "TB_Suplier",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_Address_Id_Suplier",
                table: "TB_Address",
                column: "Id_Suplier",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_Products_Id_Supplier",
                table: "TB_Products",
                column: "Id_Supplier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_Address");

            migrationBuilder.DropTable(
                name: "TB_Products");

            migrationBuilder.DropTable(
                name: "TB_Suplier");
        }
    }
}
