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
            throw new ProjectIdCannotChangeException();
        }

        public string FormatValue(string value)
        {
            return value;
        }
    }
    
    public class ProjectIdCannotChangeException : Exception
    {
        public ProjectIdCannotChangeException() : base("ProjectId cannot be changed")
        {
        }
    }
}
