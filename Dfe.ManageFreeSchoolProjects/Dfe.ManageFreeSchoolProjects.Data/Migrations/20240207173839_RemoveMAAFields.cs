using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dfe.ManageFreeSchoolProjects.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMAAFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MAACommentsOnDecisionToApprove",
                table: "Milestones");

            migrationBuilder.DropColumn(
                name: "MAASharepointLink",
                table: "Milestones");

            migrationBuilder.AddColumn<bool>(
                name: "FsgPreOpeningMilestonesFundingArrangementAgreedBetweenLaAndSponsor",
                table: "Milestones",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FsgPreOpeningMilestonesFundingArrangementAgreedBetweenLaAndSponsor",
                table: "Milestones");

            migrationBuilder.AddColumn<string>(
                name: "MAACommentsOnDecisionToApprove",
                table: "Milestones",
                type: "varchar(999)",
                unicode: false,
                maxLength: 999,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MAASharepointLink",
                table: "Milestones",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);
        }
    }
}
