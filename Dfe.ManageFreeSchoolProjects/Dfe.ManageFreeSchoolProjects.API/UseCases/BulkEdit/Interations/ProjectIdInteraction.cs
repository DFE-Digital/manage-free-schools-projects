namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit.Interations
{
    public class ProjectIdInteraction : IHeaderDataInteration<BulkEditDto>
    {
        public string GetFromDto(BulkEditDto dto)
        {
            return dto.ProjectId;
        }

        public BulkEditDto ApplyToDto(string value, BulkEditDto dto)
        {
            throw new Exception("ProjectId cannot be changed");
        }

        public string FormatValue(string value)
        {
            return value;
        }
    }
}
