namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit.Interactions
{
    public class ProjectStatusInteraction : IHeaderDataInteraction<BulkEditDto>
    {
        public BulkEditDto ApplyToDto(string value, BulkEditDto dto)
        {
            dto.ProjectStatus = value;
            return dto;
        }

        public string GetFromDto(BulkEditDto dto)
        {
            return dto.ProjectStatus;
        }

        public string FormatValue(string value)
        {
            return value;
        }
    }
}
