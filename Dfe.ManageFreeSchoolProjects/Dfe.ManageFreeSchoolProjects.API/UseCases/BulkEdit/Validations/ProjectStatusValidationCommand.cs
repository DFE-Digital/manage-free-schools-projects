using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.Extensions;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit.Validations
{
    public class ProjectStatusValidationCommand() : IValidationCommand<BulkEditDto>
    {
        public ValidationResult Execute(BulkEditDto data, string value)
        {
            try
            {
                value.ToEnumFromDescription<ProjectStatus>();

                return new ValidationResult
                {
                    IsValid = true,
                };
            }
            catch
            {
                if(value == "Cancelled in pre-opening")
                {
                    return new ValidationResult
                    {
                        IsValid = true,
                    };
                }

                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Project Status is not in possible list of statuses"
                };
            }
        }
    }
}
