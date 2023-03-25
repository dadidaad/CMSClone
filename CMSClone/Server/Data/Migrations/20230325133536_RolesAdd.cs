using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMSClone.Server.Data.Migrations
{
    public partial class RolesAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "abada37a-a134-473f-b1e0-2757e0b4bee4", "1e513f89-6041-432b-8c96-c1d0a3b279de", "Student", "STUDENT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bc012725-fdc6-4e92-a8cb-40ea9eb65bc3", "00abca4d-1a0f-4f53-be7b-1bb6fdee17ec", "Teacher", "Teacher" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abada37a-a134-473f-b1e0-2757e0b4bee4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bc012725-fdc6-4e92-a8cb-40ea9eb65bc3");
        }
    }
}
