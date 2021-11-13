using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication.Migrations
{
    public partial class Aott : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Attendances",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AttendanceType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_TypeId",
                table: "Attendances",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_AttendanceType_TypeId",
                table: "Attendances",
                column: "TypeId",
                principalTable: "AttendanceType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_AttendanceType_TypeId",
                table: "Attendances");

            migrationBuilder.DropTable(
                name: "AttendanceType");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_TypeId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Attendances");
        }
    }
}
