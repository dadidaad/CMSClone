using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMSClone.Server.Data.Migrations
{
    public partial class RolesFixNormalizedName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abada37a-a134-473f-b1e0-2757e0b4bee4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bc012725-fdc6-4e92-a8cb-40ea9eb65bc3");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4be22b4d-6c0f-443a-ab3f-e06a9de6905e", "a31a3e34-70e6-4cc8-b87b-940c0d5be206", "Teacher", "TEACHER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c7dc32ee-4b92-4556-9a56-034fed832f43", "d3c98370-b3f8-4337-92b9-80f9c2c75656", "Student", "STUDENT" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4be22b4d-6c0f-443a-ab3f-e06a9de6905e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7dc32ee-4b92-4556-9a56-034fed832f43");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "abada37a-a134-473f-b1e0-2757e0b4bee4", "1e513f89-6041-432b-8c96-c1d0a3b279de", "Student", "STUDENT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bc012725-fdc6-4e92-a8cb-40ea9eb65bc3", "00abca4d-1a0f-4f53-be7b-1bb6fdee17ec", "Teacher", "Teacher" });
        }
    }
}
