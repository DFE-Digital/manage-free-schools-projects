using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dfe.ManageFreeSchoolProjects.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeConstructDataTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HoTs agreed for temporary site [Forecast]",
                schema: "dbo",
                table: "constructData",
                newName: "HoTs agreed for temporary site (Forecast)");

            migrationBuilder.RenameColumn(
                name: "HoTs agreed for site for main school building [Forecast]",
                schema: "dbo",
                table: "constructData",
                newName: "HoTs agreed for site for main school building (Forecast)");

            migrationBuilder.RenameColumn(
                name: "Date of planning decision for temporary site main planning record [Forecast]",
                schema: "dbo",
                table: "constructData",
                newName: "Date of planning decision for temporary site main planning record (Forecast)");

            migrationBuilder.RenameColumn(
                name: "Date of planning decision for temporary site main planning record [Actual]",
                schema: "dbo",
                table: "constructData",
                newName: "Date of planning decision for temporary site main planning record (Actual)");

            migrationBuilder.RenameColumn(
                name: "Date of planning decision for main site main planning record [Forecast]",
                schema: "dbo",
                table: "constructData",
                newName: "Date of planning decision for main site main planning record (Forecast)");

            migrationBuilder.RenameColumn(
                name: "Date of planning decision for main site main planning record [Actual]",
                schema: "dbo",
                table: "constructData",
                newName: "Date of planning decision for main site main planning record (Actual)");

            migrationBuilder.RenameColumn(
                name: "Contractor for temporary site appointed [Forecast]",
                schema: "dbo",
                table: "constructData",
                newName: "Contractor for temporary site appointed (Forecast)");

            migrationBuilder.RenameColumn(
                name: "Contractor for temporary site appointed [Actual]",
                schema: "dbo",
                table: "constructData",
                newName: "Contractor for temporary site appointed (Actual)");

            migrationBuilder.RenameColumn(
                name: "Contractor for site for main school building appointed [Forecast]",
                schema: "dbo",
                table: "constructData",
                newName: "Contractor for site for main school building appointed (Forecast)");

            migrationBuilder.RenameColumn(
                name: "Contractor for site for main school building appointed [Actual]",
                schema: "dbo",
                table: "constructData",
                newName: "Contractor for site for main school building appointed (Actual)");

            migrationBuilder.AlterColumn<string>(
                name: "Will the project open in temporary accommodation?",
                schema: "dbo",
                table: "constructData",
                type: "varchar(10)",
                unicode: false,
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Temporary site postcode",
                schema: "dbo",
                table: "constructData",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Temporary site planning risk",
                schema: "dbo",
                table: "constructData",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Temporary site planning decision",
                schema: "dbo",
                table: "constructData",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Temporary site address",
                schema: "dbo",
                table: "constructData",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Main site address",
                schema: "dbo",
                table: "constructData",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date temporary site planning approval granted",
                schema: "dbo",
                table: "constructData",
                type: "date",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date main site planning approval granted",
                schema: "dbo",
                table: "constructData",
                type: "date",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "HoTs agreed for temporary site (Forecast)",
                schema: "dbo",
                table: "constructData",
                type: "date",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "HoTs agreed for site for main school building (Forecast)",
                schema: "dbo",
                table: "constructData",
                type: "date",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date of planning decision for temporary site main planning record (Forecast)",
                schema: "dbo",
                table: "constructData",
                type: "date",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date of planning decision for temporary site main planning record (Actual)",
                schema: "dbo",
                table: "constructData",
                type: "date",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date of planning decision for main site main planning record (Forecast)",
                schema: "dbo",
                table: "constructData",
                type: "date",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date of planning decision for main site main planning record (Actual)",
                schema: "dbo",
                table: "constructData",
                type: "date",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Contractor for temporary site appointed (Forecast)",
                schema: "dbo",
                table: "constructData",
                type: "date",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Contractor for temporary site appointed (Actual)",
                schema: "dbo",
                table: "constructData",
                type: "date",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Contractor for site for main school building appointed (Forecast)",
                schema: "dbo",
                table: "constructData",
                type: "date",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Contractor for site for main school building appointed (Actual)",
                schema: "dbo",
                table: "constructData",
                type: "date",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HoTs agreed for temporary site (Forecast)",
                schema: "dbo",
                table: "constructData",
                newName: "HoTs agreed for temporary site [Forecast]");

            migrationBuilder.RenameColumn(
                name: "HoTs agreed for site for main school building (Forecast)",
                schema: "dbo",
                table: "constructData",
                newName: "HoTs agreed for site for main school building [Forecast]");

            migrationBuilder.RenameColumn(
                name: "Date of planning decision for temporary site main planning record (Forecast)",
                schema: "dbo",
                table: "constructData",
                newName: "Date of planning decision for temporary site main planning record [Forecast]");

            migrationBuilder.RenameColumn(
                name: "Date of planning decision for temporary site main planning record (Actual)",
                schema: "dbo",
                table: "constructData",
                newName: "Date of planning decision for temporary site main planning record [Actual]");

            migrationBuilder.RenameColumn(
                name: "Date of planning decision for main site main planning record (Forecast)",
                schema: "dbo",
                table: "constructData",
                newName: "Date of planning decision for main site main planning record [Forecast]");

            migrationBuilder.RenameColumn(
                name: "Date of planning decision for main site main planning record (Actual)",
                schema: "dbo",
                table: "constructData",
                newName: "Date of planning decision for main site main planning record [Actual]");

            migrationBuilder.RenameColumn(
                name: "Contractor for temporary site appointed (Forecast)",
                schema: "dbo",
                table: "constructData",
                newName: "Contractor for temporary site appointed [Forecast]");

            migrationBuilder.RenameColumn(
                name: "Contractor for temporary site appointed (Actual)",
                schema: "dbo",
                table: "constructData",
                newName: "Contractor for temporary site appointed [Actual]");

            migrationBuilder.RenameColumn(
                name: "Contractor for site for main school building appointed (Forecast)",
                schema: "dbo",
                table: "constructData",
                newName: "Contractor for site for main school building appointed [Forecast]");

            migrationBuilder.RenameColumn(
                name: "Contractor for site for main school building appointed (Actual)",
                schema: "dbo",
                table: "constructData",
                newName: "Contractor for site for main school building appointed [Actual]");

            migrationBuilder.AlterColumn<string>(
                name: "Will the project open in temporary accommodation?",
                schema: "dbo",
                table: "constructData",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldUnicode: false,
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Temporary site postcode",
                schema: "dbo",
                table: "constructData",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Temporary site planning risk",
                schema: "dbo",
                table: "constructData",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Temporary site planning decision",
                schema: "dbo",
                table: "constructData",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Temporary site address",
                schema: "dbo",
                table: "constructData",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Main site address",
                schema: "dbo",
                table: "constructData",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Date temporary site planning approval granted",
                schema: "dbo",
                table: "constructData",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Date main site planning approval granted",
                schema: "dbo",
                table: "constructData",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HoTs agreed for temporary site [Forecast]",
                schema: "dbo",
                table: "constructData",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HoTs agreed for site for main school building [Forecast]",
                schema: "dbo",
                table: "constructData",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Date of planning decision for temporary site main planning record [Forecast]",
                schema: "dbo",
                table: "constructData",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Date of planning decision for temporary site main planning record [Actual]",
                schema: "dbo",
                table: "constructData",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Date of planning decision for main site main planning record [Forecast]",
                schema: "dbo",
                table: "constructData",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Date of planning decision for main site main planning record [Actual]",
                schema: "dbo",
                table: "constructData",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Contractor for temporary site appointed [Forecast]",
                schema: "dbo",
                table: "constructData",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Contractor for temporary site appointed [Actual]",
                schema: "dbo",
                table: "constructData",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Contractor for site for main school building appointed [Forecast]",
                schema: "dbo",
                table: "constructData",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Contractor for site for main school building appointed [Actual]",
                schema: "dbo",
                table: "constructData",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldUnicode: false,
                oldNullable: true);
        }
    }
}
