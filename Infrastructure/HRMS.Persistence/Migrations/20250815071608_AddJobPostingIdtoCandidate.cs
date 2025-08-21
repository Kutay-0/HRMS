using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddJobPostingIdtoCandidate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobPostingId",
                table: "Candidates",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobPostingId",
                table: "Candidates");
        }
    }
}
