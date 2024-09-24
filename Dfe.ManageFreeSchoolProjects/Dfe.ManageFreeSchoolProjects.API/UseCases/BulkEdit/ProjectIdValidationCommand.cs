namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit
{
    public class ProjectIdValidationCommand() : IValidationCommand<BulkEditDto>
    {
        public ValidationResult Execute(BulkEditDto data, string value)
        {
            return new ValidationResult
            {
                IsValid = true
            };
        }
    }
}
