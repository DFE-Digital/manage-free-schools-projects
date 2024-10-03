namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit.Validations
{
    public class ProjectIdValidationCommand() : IValidationCommand<BulkEditDto>
    {
        public ValidationResult Execute(BulkEditDto data, string value)
        {
            if(data == null)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Enter an existing project ID"
                };
            }

            return new ValidationResult
            {
                IsValid = true
            };
        }
    }
}
