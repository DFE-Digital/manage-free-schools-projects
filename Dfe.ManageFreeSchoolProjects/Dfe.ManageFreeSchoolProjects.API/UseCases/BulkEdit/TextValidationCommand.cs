namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit
{
    public class TextValidationCommand(int maxLength) : IValidationCommand
    {
        public ValidationResult Execute(string value)
        {
            return new ValidationResult
            {
                IsValid = value.Length <= maxLength,
                errorMessage = value.Length <= maxLength ? null : $"Value exceeds maximum length of {maxLength}."
            };
        }
    }
}
