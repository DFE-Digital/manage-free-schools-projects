using Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit.Validations;
using Dfe.ManageFreeSchoolProjects.API.Contracts.BulkEdit;
using Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit.Interations;


namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit
{

    public class BulkEditHeaderRegister : IHeaderRegister<BulkEditDto>
    {
        public string IdentifingHeader => HeaderNames.ProjectId;
        public List<HeaderType<BulkEditDto>> GetHeaders()
        {
            return new List<HeaderType<BulkEditDto>>
            {
                new() { Name = HeaderNames.ProjectId, Type = new ProjectIdValidationCommand(), DataInteration = new ProjectIdInteraction() },
                new() { Name = HeaderNames.SchoolName, Type = new TextValidationCommand(100), DataInteration = new SchoolNameInteraction() },
                new() { Name = HeaderNames.OpeningDate, Type = new DateValidationCommand(), DataInteration = new OpeningDateInteration() },
            };
        }
    }
}
