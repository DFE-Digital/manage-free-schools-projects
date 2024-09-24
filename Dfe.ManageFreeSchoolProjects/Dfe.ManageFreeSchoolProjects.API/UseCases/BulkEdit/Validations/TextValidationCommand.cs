namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit.Validations
{
    public class TextValidationCommand(int maxLength) : IValidationCommand<BulkEditDto>
    {
        public ValidationResult Execute(BulkEditDto data, string value)
        {
            return new ValidationResult
            {
                IsValid = value.Length <= maxLength,
                errorMessage = value.Length <= maxLength ? null : $"Value exceeds maximum length of {maxLength}."
            };
        }
    }
}
