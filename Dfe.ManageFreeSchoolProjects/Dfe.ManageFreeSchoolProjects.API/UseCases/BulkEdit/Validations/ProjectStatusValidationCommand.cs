using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.Extensions;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit.Validations
{
    public class ProjectStatusValidationCommand() : IValidationCommand<BulkEditDto>
    {
        public ValidationResult Execute(ValidationCommandParameters<BulkEditDto> parameters)
        {

            HashSet<ProjectStatus> PresumptionOnly = 
                [
                    ProjectStatus.Preopening, 
                    ProjectStatus.Cancelled, 
                    ProjectStatus.Open, 
                    ProjectStatus.Closed, 
                    ProjectStatus.WithdrawnDuringPreOpening
                ];

            ProjectStatus? status = null;

            if(parameters.Value.Equals("cancelled in pre-opening", StringComparison.OrdinalIgnoreCase))
            {
                status = ProjectStatus.Cancelled;
            }

            if (status == null)
            {
                status = GetStatusUsingDatabaseName(parameters.Value);
            }

            if (status == null)
            {
                status = GetStatusFromDescription(parameters.Value);
            }

            if (status == null)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Select an existing project status"
                };
            }

            if (parameters.Data != null && parameters.Data.ApplicationWave == "FS - Presumption" && !PresumptionOnly.Contains(status.Value))
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Select a presumption route project status"
                };
            }

            return new ValidationResult
            {
                IsValid = true,
            };

        }

        private static ProjectStatus? GetStatusUsingDatabaseName(string value)
        {
            if (value.Equals("pre-opening", StringComparison.OrdinalIgnoreCase))
            {
                return ProjectStatus.Preopening;
            }

            var status = ProjectMapper.ToProjectStatusType(value);

            if (status == ProjectStatus.Preopening)
            {
                return null;
            }

            return status;
        }

        private static ProjectStatus? GetStatusFromDescription(string value)
        {
            try
            {
                return value.ToEnumFromDescription<ProjectStatus>();
            }
            catch
            {
                return null;
            }
        }
    }
}
