using Azure.Core;
using Dfe.ManageFreeSchoolProjects.API.Contracts.BulkEdit;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit.Validations
{
    public class ProjectIdValidationCommand() : IValidationCommand<BulkEditDto>
    {
        public ValidationResult Execute(ValidationCommandParameters<BulkEditDto> parameters)
        {
            if (parameters.Data == null)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Enter an existing project ID"
                };
            }

            var IdColumnIndex = parameters.Request.Headers.Find(x => string.Compare(x.Name, HeaderNames.ProjectId, true) == 0).Index;

            var duplicated = parameters.Request.Rows
                .Where(row => row.FileRowIndex != parameters.CurrentRowIndex)
                .Any(row => parameters.Value == row.Columns[IdColumnIndex].Value);

            if (duplicated)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "The project ID must be unique"
                };
            }


            return new ValidationResult
            {
                IsValid = true
            };
        }
    }
}
