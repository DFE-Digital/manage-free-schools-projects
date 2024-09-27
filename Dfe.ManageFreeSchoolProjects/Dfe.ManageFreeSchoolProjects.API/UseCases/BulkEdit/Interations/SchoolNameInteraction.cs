namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit.Interations
{
    public class SchoolNameInteraction : IHeaderDataInteration<BulkEditDto>
    {
        public string GetFromDto(BulkEditDto dto)
        {
            return dto.SchoolName;
        }

        public BulkEditDto ApplyToDto(string value, BulkEditDto dto)
        {
            dto.SchoolName = value;
            return dto;
        }
    }
}
