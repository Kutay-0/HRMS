using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCandidate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateJobPosting");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_JobPostingId",
                table: "Candidates",
                column: "JobPostingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_JobPostings_JobPostingId",
                table: "Candidates",
                column: "JobPostingId",
                principalTable: "JobPostings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_JobPostings_JobPostingId",
                table: "Candidates");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_JobPostingId",
                table: "Candidates");

            migrationBuilder.CreateTable(
                name: "CandidateJobPosting",
                columns: table => new
                {
                    CandidatesId = table.Column<int>(type: "integer", nullable: false),
                    JobPostingsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateJobPosting", x => new { x.CandidatesId, x.JobPostingsId });
                    table.ForeignKey(
                        name: "FK_CandidateJobPosting_Candidates_CandidatesId",
                        column: x => x.CandidatesId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateJobPosting_JobPostings_JobPostingsId",
                        column: x => x.JobPostingsId,
                        principalTable: "JobPostings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateJobPosting_JobPostingsId",
                table: "CandidateJobPosting",
                column: "JobPostingsId");
        }
    }
}
