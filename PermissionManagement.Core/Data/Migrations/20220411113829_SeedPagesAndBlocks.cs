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
                columns: new[] { "Id", "AssociatedRole", "Name" },
                values: new object[] { 1, "Member", "Page1" });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "Id", "AssociatedRole", "Name" },
                values: new object[] { 2, "Member", "Page2" });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "Id", "AssociatedRole", "Name" },
                values: new object[] { 3, "Administrator", "PermissionsManagement" });

            migrationBuilder.InsertData(
                table: "Blocks",
                columns: new[] { "Id", "Name", "PageId" },
                values: new object[,]
                {
                    { 1, "Block1", 1 },
                    { 2, "Block2", 1 },
                    { 3, "Block3", 1 },
                    { 4, "Block4", 2 },
                    { 5, "Block5", 2 }
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
