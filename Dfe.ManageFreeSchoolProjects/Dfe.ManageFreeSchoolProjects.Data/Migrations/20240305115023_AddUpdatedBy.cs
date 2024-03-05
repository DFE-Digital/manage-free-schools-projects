using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dfe.ManageFreeSchoolProjects.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdatedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                schema: "dbo",
                table: "KPI",
                type: "int",
                nullable: true)
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "KPIHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "dbo")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

            migrationBuilder.CreateIndex(
                name: "IX_KPI_UpdatedByUserId",
                schema: "dbo",
                table: "KPI",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_KPI_User_UpdatedByUserId",
                schema: "dbo",
                table: "KPI",
                column: "UpdatedByUserId",
                principalSchema: "mfsp",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KPI_User_UpdatedByUserId",
                schema: "dbo",
                table: "KPI");

            migrationBuilder.DropIndex(
                name: "IX_KPI_UpdatedByUserId",
                schema: "dbo",
                table: "KPI");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                schema: "dbo",
                table: "KPI")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "KPIHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "dbo")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");
        }
    }
}
