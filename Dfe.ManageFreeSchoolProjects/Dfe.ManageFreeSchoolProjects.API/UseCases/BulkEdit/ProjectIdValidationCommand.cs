namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit
{
    public class ProjectIdValidationCommand() : IValidationCommand
    {
        public ValidationResult Execute(string value)
        {
            return new ValidationResult
            {
                IsValid = true
            };
        }
    }
}
