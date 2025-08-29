using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FinalMigrate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmploymentType",
                table: "JobPostings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmploymentType",
                table: "JobPostings",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
