using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication.Migrations
{
    public partial class Bo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_AttendanceType_TypeId",
                table: "Attendances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttendanceType",
                table: "AttendanceType");

            migrationBuilder.RenameTable(
                name: "AttendanceType",
                newName: "Types");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Types",
                table: "Types",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Types_TypeId",
                table: "Attendances",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Types_TypeId",
                table: "Attendances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Types",
                table: "Types");

            migrationBuilder.RenameTable(
                name: "Types",
                newName: "AttendanceType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttendanceType",
                table: "AttendanceType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_AttendanceType_TypeId",
                table: "Attendances",
                column: "TypeId",
                principalTable: "AttendanceType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
