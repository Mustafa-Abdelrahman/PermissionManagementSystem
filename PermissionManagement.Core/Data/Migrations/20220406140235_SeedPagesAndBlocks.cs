using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PermissionManagement.Web.Data.Migrations
{
    public partial class SeedPagesAndBlocks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Page 1" });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Page 2" });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Permissions Management" });

            migrationBuilder.InsertData(
                table: "Blocks",
                columns: new[] { "Id", "Name", "PageId" },
                values: new object[,]
                {
                    { 1, "Block 1", 1 },
                    { 2, "Block 2", 1 },
                    { 3, "Block 3", 1 },
                    { 4, "Block 4", 2 },
                    { 5, "Block 5", 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
