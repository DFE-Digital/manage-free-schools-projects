using Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit.Validations;
using Dfe.ManageFreeSchoolProjects.API.Contracts.BulkEdit;
using Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit.Interations;
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
                new() { Name = HeaderNames.ProjectId, Type = new ProjectIdValidationCommand(), DataInteration = new ProjectIdInteraction() },
                new() { Name = HeaderNames.OpeningDate, Type = new DateValidationCommand(), DataInteration = new OpeningDateInteration() },
                new() { Name = HeaderNames.ProjectStatus, Type = new ProjectStatusValidationCommand(), DataInteration = new ProjectStatusInteraction() },
                new() { Name = HeaderNames.LACode, Type = new LACodeValidationCommand(localAuthorityCache), DataInteration = new LACodeInteraction(localAuthorityCache) },

            };
        }
    }
}
