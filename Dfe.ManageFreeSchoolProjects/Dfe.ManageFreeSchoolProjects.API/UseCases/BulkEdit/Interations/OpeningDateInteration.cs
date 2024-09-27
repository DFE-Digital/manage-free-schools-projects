namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit.Interations
{
    public class OpeningDateInteration : IHeaderDataInteration<BulkEditDto>
    {
        public string GetFromDto(BulkEditDto dto)
        {
            return dto.OpeningDate;
        }

        public BulkEditDto ApplyToDto(string value, BulkEditDto dto)
        {
            dto.OpeningDate = value;
            return dto;
        }
    }
}
