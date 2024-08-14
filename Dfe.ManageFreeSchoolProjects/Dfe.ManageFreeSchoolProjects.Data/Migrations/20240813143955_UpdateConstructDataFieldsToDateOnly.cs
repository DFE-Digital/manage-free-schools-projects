using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dfe.ManageFreeSchoolProjects.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateConstructDataFieldsToDateOnly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "Temporary accommodation first ready for occupation (Forecast)",
                schema: "dbo",
                table: "constructData",
                type: "date",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Temporary accommodation first ready for occupation (Actual)",
                schema: "dbo",
                table: "constructData",
                type: "date",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Site identified for main school building (Actual)",
                schema: "dbo",
                table: "constructData",
                type: "date",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Practical Completion Certificate issued date (A)",
                schema: "dbo",
                table: "constructData",
                type: "date",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Main School Building first ready for occupation (Forecast)",
                schema: "dbo",
                table: "constructData",
                type: "date",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Main School Building first ready for occupation (Actual)",
                schema: "dbo",
                table: "constructData",
                type: "date",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "HoT Agreed for site for Main School Building (Actual)",
                schema: "dbo",
                table: "constructData",
                type: "date",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date of HoT secured on temporary accommodation site, if required",
                schema: "dbo",
                table: "constructData",
                type: "date",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "LastRefreshDate",
                schema: "dbo",
                table: "constructData",
                type: "date",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastRefreshDate",
                schema: "dbo",
                table: "constructData");

            migrationBuilder.AlterColumn<string>(
                name: "Temporary accommodation first ready for occupation (Forecast)",
                schema: "dbo",
                table: "constructData",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Temporary accommodation first ready for occupation (Actual)",
                schema: "dbo",
                table: "constructData",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Site identified for main school building (Actual)",
                schema: "dbo",
                table: "constructData",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Practical Completion Certificate issued date (A)",
                schema: "dbo",
                table: "constructData",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Main School Building first ready for occupation (Forecast)",
                schema: "dbo",
                table: "constructData",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Main School Building first ready for occupation (Actual)",
                schema: "dbo",
                table: "constructData",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HoT Agreed for site for Main School Building (Actual)",
                schema: "dbo",
                table: "constructData",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Date of HoT secured on temporary accommodation site, if required",
                schema: "dbo",
                table: "constructData",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldUnicode: false,
                oldNullable: true);
        }
    }
}
