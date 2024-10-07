using System.Globalization;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit.Interactions
{
    public class OpeningDateInteraction : IHeaderDataInteraction<BulkEditDto>
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

        public string FormatValue(string value)
        {
            if(DateTime.TryParse(value, new CultureInfo("en-GB"), out var date))
            {
                return date.ToString("dd/MM/yyyy");
            }

            return value;
        }
    }
}
