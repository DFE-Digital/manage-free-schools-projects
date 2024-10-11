namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit.Validations
{
    public class TextValidationCommand(int maxLength) : IValidationCommand<BulkEditDto>
    {
        public ValidationResult Execute(ValidationCommandParameters<BulkEditDto> parameters)
        {
            return new ValidationResult
            {
                IsValid = parameters.Value.Length <= maxLength,
                ErrorMessage = parameters.Value.Length <= maxLength ? null : $"Value exceeds maximum length of {maxLength}."
            };
        }
    }
}
