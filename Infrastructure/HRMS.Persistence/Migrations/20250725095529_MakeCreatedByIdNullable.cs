using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MakeCreatedByIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_ApplicationUsers_CreatedById",
                table: "Candidates");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Candidates",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_ApplicationUsers_CreatedById",
                table: "Candidates",
                column: "CreatedById",
                principalTable: "ApplicationUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_ApplicationUsers_CreatedById",
                table: "Candidates");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Candidates",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_ApplicationUsers_CreatedById",
                table: "Candidates",
                column: "CreatedById",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
