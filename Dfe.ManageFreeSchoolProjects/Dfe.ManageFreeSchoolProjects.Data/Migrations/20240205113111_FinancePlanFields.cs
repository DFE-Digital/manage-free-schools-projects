using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dfe.ManageFreeSchoolProjects.Data.Migrations
{
    /// <inheritdoc />
    public partial class FinancePlanFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IsPlanSavedInWorkspaceFolder",
                table: "Milestones",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LAAgreedPupilNumbers",
                table: "Milestones",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrustOptInRPA",
                table: "Milestones",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPlanSavedInWorkspaceFolder",
                table: "Milestones");

            migrationBuilder.DropColumn(
                name: "LAAgreedPupilNumbers",
                table: "Milestones");

            migrationBuilder.DropColumn(
                name: "TrustOptInRPA",
                table: "Milestones");
        }
    }
}
