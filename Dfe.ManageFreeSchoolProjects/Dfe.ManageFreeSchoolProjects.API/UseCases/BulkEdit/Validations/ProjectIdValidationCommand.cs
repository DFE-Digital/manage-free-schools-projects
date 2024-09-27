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
                    ErrorMessage = "Project Id does not exist"
                };
            }

            return new ValidationResult
            {
                IsValid = true
            };
        }
    }
}
