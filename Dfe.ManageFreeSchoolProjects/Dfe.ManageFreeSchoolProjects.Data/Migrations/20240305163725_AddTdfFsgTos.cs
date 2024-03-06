using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dfe.ManageFreeSchoolProjects.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTdfFsgTos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE FUNCTION [dbo].[tdf_FSG_TOS]  
                (  
                 @TOS NVARCHAR(10)
                )  
  
                RETURNS TABLE  
                AS  
                RETURN  
                (  
                 SELECT fp.*  
                 FROM [dbo].[Property] fp
                 WHERE fp.[TOS] = @TOS  
                )              
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP FUNCTION [dbo].[tdf_FSG_TOS] 
            ");
        }
    }
}
