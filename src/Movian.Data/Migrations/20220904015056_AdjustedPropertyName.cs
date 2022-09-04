using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movian.Data.Migrations
{
    public partial class AdjustedPropertyName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SuplierType",
                table: "TB_Supplier",
                newName: "SupplierType");

            migrationBuilder.RenameColumn(
                name: "CompleteAddress",
                table: "TB_Address",
                newName: "StreetName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SupplierType",
                table: "TB_Supplier",
                newName: "SuplierType");

            migrationBuilder.RenameColumn(
                name: "StreetName",
                table: "TB_Address",
                newName: "CompleteAddress");
        }
    }
}
