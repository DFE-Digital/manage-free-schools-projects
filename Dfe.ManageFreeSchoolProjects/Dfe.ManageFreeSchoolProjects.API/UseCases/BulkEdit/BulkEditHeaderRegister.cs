using Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit.Validations;
using Dfe.ManageFreeSchoolProjects.API.Contracts.BulkEdit;
using Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit.Interactions;
using Dfe.ManageFreeSchoolProjects.API.UseCases.LocalAuthority;


namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit
{

    public class BulkEditHeaderRegister(ILocalAuthorityCache localAuthorityCache) : IHeaderRegister<BulkEditDto>
    {
        public string IdentifingHeader => HeaderNames.ProjectId;
        public List<HeaderType<BulkEditDto>> GetHeaders()
        {
            return new List<HeaderType<BulkEditDto>>
            {
                new() { Name = HeaderNames.ProjectId, Validation = new ProjectIdValidationCommand(), DataInteraction = new ProjectIdInteraction() },
                new() { Name = HeaderNames.OpeningDate, Validation = new DateValidationCommand(), DataInteraction = new OpeningDateInteraction() },
                new() { Name = HeaderNames.ProjectStatus, Validation = new ProjectStatusValidationCommand(), DataInteraction = new ProjectStatusInteraction() },
                new() { Name = HeaderNames.LocalAuthority, Validation = new LACodeValidationCommand(localAuthorityCache), DataInteraction = new LACodeInteraction(localAuthorityCache) },

            };
        }
    }
}
