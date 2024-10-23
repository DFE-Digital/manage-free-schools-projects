using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dfe.ManageFreeSchoolProjects.Data.Migrations
{
    /// <inheritdoc />
    public partial class ConstructDataNewFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Contractor for site for main school building appointed [Actual]",
                schema: "dbo",
                table: "constructData",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Contractor for site for main school building appointed [Forecast]",
                schema: "dbo",
                table: "constructData",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Contractor for temporary site appointed [Actual]",
                schema: "dbo",
                table: "constructData",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Contractor for temporary site appointed [Forecast]",
                schema: "dbo",
                table: "constructData",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Date main site planning approval granted",
                schema: "dbo",
                table: "constructData",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Date of planning decision for main site main planning record [Actual]",
                schema: "dbo",
                table: "constructData",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Date of planning decision for main site main planning record [Forecast]",
                schema: "dbo",
                table: "constructData",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Date of planning decision for temporary site main planning record [Actual]",
                schema: "dbo",
                table: "constructData",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Date of planning decision for temporary site main planning record [Forecast]",
                schema: "dbo",
                table: "constructData",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Date temporary site planning approval granted",
                schema: "dbo",
                table: "constructData",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HoTs agreed for site for main school building [Forecast]",
                schema: "dbo",
                table: "constructData",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HoTs agreed for temporary site [Forecast]",
                schema: "dbo",
                table: "constructData",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Main site address",
                schema: "dbo",
                table: "constructData",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Temporary site address",
                schema: "dbo",
                table: "constructData",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Temporary site planning decision",
                schema: "dbo",
                table: "constructData",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Temporary site planning risk",
                schema: "dbo",
                table: "constructData",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Temporary site postcode",
                schema: "dbo",
                table: "constructData",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Will the project open in temporary accommodation?",
                schema: "dbo",
                table: "constructData",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contractor for site for main school building appointed [Actual]",
                schema: "dbo",
                table: "constructData");

            migrationBuilder.DropColumn(
                name: "Contractor for site for main school building appointed [Forecast]",
                schema: "dbo",
                table: "constructData");

            migrationBuilder.DropColumn(
                name: "Contractor for temporary site appointed [Actual]",
                schema: "dbo",
                table: "constructData");

            migrationBuilder.DropColumn(
                name: "Contractor for temporary site appointed [Forecast]",
                schema: "dbo",
                table: "constructData");

            migrationBuilder.DropColumn(
                name: "Date main site planning approval granted",
                schema: "dbo",
                table: "constructData");

            migrationBuilder.DropColumn(
                name: "Date of planning decision for main site main planning record [Actual]",
                schema: "dbo",
                table: "constructData");

            migrationBuilder.DropColumn(
                name: "Date of planning decision for main site main planning record [Forecast]",
                schema: "dbo",
                table: "constructData");

            migrationBuilder.DropColumn(
                name: "Date of planning decision for temporary site main planning record [Actual]",
                schema: "dbo",
                table: "constructData");

            migrationBuilder.DropColumn(
                name: "Date of planning decision for temporary site main planning record [Forecast]",
                schema: "dbo",
                table: "constructData");

            migrationBuilder.DropColumn(
                name: "Date temporary site planning approval granted",
                schema: "dbo",
                table: "constructData");

            migrationBuilder.DropColumn(
                name: "HoTs agreed for site for main school building [Forecast]",
                schema: "dbo",
                table: "constructData");

            migrationBuilder.DropColumn(
                name: "HoTs agreed for temporary site [Forecast]",
                schema: "dbo",
                table: "constructData");

            migrationBuilder.DropColumn(
                name: "Main site address",
                schema: "dbo",
                table: "constructData");

            migrationBuilder.DropColumn(
                name: "Temporary site address",
                schema: "dbo",
                table: "constructData");

            migrationBuilder.DropColumn(
                name: "Temporary site planning decision",
                schema: "dbo",
                table: "constructData");

            migrationBuilder.DropColumn(
                name: "Temporary site planning risk",
                schema: "dbo",
                table: "constructData");

            migrationBuilder.DropColumn(
                name: "Temporary site postcode",
                schema: "dbo",
                table: "constructData");

            migrationBuilder.DropColumn(
                name: "Will the project open in temporary accommodation?",
                schema: "dbo",
                table: "constructData");
        }
    }
}
