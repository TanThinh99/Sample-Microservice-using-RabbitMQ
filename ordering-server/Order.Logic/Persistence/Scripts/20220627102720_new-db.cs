using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Order.Logic.Persistence.Scripts
{
    public partial class newdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    clustered_key = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    create_date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    modified_date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    qoh = table.Column<int>(type: "int", nullable: false),
                    clustered_key = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    create_date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    modified_date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "ProductTransaction",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    c_id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    p_id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    clustered_key = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    create_date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    modified_date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTransaction", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_clustered_key",
                table: "Customer",
                column: "clustered_key")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_clustered_key",
                table: "Product",
                column: "clustered_key")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTransaction_clustered_key",
                table: "ProductTransaction",
                column: "clustered_key")
                .Annotation("SqlServer:Clustered", true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ProductTransaction");
        }
    }
}
