using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMSClone.Server.Data.Migrations
{
    public partial class AddModelsToCMSClonev1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourses_AspNetUsers_UserId",
                table: "StudentCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourses_Courses_CourseId",
                table: "StudentCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentCourses",
                table: "StudentCourses");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2505696a-127e-4d8c-a457-99d04b0c24ef");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5b8cbef3-14ee-455f-8a1f-670d2f637444");

            migrationBuilder.RenameTable(
                name: "StudentCourses",
                newName: "CourseJoins");

            migrationBuilder.RenameIndex(
                name: "IX_StudentCourses_CourseId",
                table: "CourseJoins",
                newName: "IX_CourseJoins_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseJoins",
                table: "CourseJoins",
                columns: new[] { "UserId", "CourseId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6876ebdb-b5d6-4f9b-be70-363d3415e6a1", "162f1b1f-0d50-4434-83e3-f9c67e8b61d2", "Teacher", "TEACHER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "90cabdef-3caf-44ba-8bda-25c0dd49f2cf", "cf77b3ca-1723-4046-9120-3c3466f7ca75", "Student", "STUDENT" });

            migrationBuilder.AddForeignKey(
                name: "FK_CourseJoins_AspNetUsers_UserId",
                table: "CourseJoins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseJoins_Courses_CourseId",
                table: "CourseJoins",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseJoins_AspNetUsers_UserId",
                table: "CourseJoins");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseJoins_Courses_CourseId",
                table: "CourseJoins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseJoins",
                table: "CourseJoins");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6876ebdb-b5d6-4f9b-be70-363d3415e6a1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "90cabdef-3caf-44ba-8bda-25c0dd49f2cf");

            migrationBuilder.RenameTable(
                name: "CourseJoins",
                newName: "StudentCourses");

            migrationBuilder.RenameIndex(
                name: "IX_CourseJoins_CourseId",
                table: "StudentCourses",
                newName: "IX_StudentCourses_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentCourses",
                table: "StudentCourses",
                columns: new[] { "UserId", "CourseId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2505696a-127e-4d8c-a457-99d04b0c24ef", "495074cd-e214-4204-bdbb-155c20c60300", "Teacher", "TEACHER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5b8cbef3-14ee-455f-8a1f-670d2f637444", "a5b9c615-1612-4d21-b7ef-a3d8efd0f9a8", "Student", "STUDENT" });

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourses_AspNetUsers_UserId",
                table: "StudentCourses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourses_Courses_CourseId",
                table: "StudentCourses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
