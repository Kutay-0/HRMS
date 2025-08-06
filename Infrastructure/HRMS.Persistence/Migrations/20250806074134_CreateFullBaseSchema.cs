using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateFullBaseSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Candidates");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Candidates",
                newName: "ApplicationUserId");

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
                name: "IX_Candidates_ApplicationUserId",
                table: "Candidates",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_CompanyId",
                table: "Candidates",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateJobPosting_JobPostingsId",
                table: "CandidateJobPosting",
                column: "JobPostingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_AspNetUsers_ApplicationUserId",
                table: "Candidates",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Companies_CompanyId",
                table: "Candidates",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_AspNetUsers_ApplicationUserId",
                table: "Candidates");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_Companies_CompanyId",
                table: "Candidates");

            migrationBuilder.DropTable(
                name: "CandidateJobPosting");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_ApplicationUserId",
                table: "Candidates");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_CompanyId",
                table: "Candidates");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Candidates",
                newName: "FullName");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Candidates",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
