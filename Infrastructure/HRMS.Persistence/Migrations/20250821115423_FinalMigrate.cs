using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FinalMigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_Companies_CompanyId",
                table: "Candidates");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Companies",
                newName: "CompanyName");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "JobPostings",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<string>(
                name: "EmploymentType",
                table: "JobPostings",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Candidates",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationStatus",
                table: "Candidates",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyRequests_RequestedById",
                table: "CompanyRequests",
                column: "RequestedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Companies_CompanyId",
                table: "Candidates",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyRequests_AspNetUsers_RequestedById",
                table: "CompanyRequests",
                column: "RequestedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_Companies_CompanyId",
                table: "Candidates");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyRequests_AspNetUsers_RequestedById",
                table: "CompanyRequests");

            migrationBuilder.DropIndex(
                name: "IX_CompanyRequests_RequestedById",
                table: "CompanyRequests");

            migrationBuilder.DropColumn(
                name: "EmploymentType",
                table: "JobPostings");

            migrationBuilder.DropColumn(
                name: "ApplicationStatus",
                table: "Candidates");

            migrationBuilder.RenameColumn(
                name: "CompanyName",
                table: "Companies",
                newName: "Name");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "JobPostings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Candidates",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Companies_CompanyId",
                table: "Candidates",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
