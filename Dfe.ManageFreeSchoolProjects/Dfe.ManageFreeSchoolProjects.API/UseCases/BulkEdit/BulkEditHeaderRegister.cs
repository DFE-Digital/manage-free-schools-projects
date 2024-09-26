using Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit.Validations;
using Dfe.ManageFreeSchoolProjects.API.Contracts.BulkEdit;


namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit
{

    public class BulkEditHeaderRegister : IHeaderRegister<BulkEditDto>
    {
        public string IdentifingHeader => HeaderNames.ProjectId;
        public List<HeaderType<BulkEditDto>> GetHeaders()
        {
            return new List<HeaderType<BulkEditDto>>
            {
                new() { Name = HeaderNames.ProjectId, Type = new ProjectIdValidationCommand(), GetFromDto = (x => x.ProjectId) },
                new() { Name = HeaderNames.SchoolName, Type = new TextValidationCommand(100), GetFromDto = (x => x.SchoolName), SetToDto = (v, t) => { t.SchoolName = v; return t; } },
                new() { Name = HeaderNames.LocalAuthority, Type = new TextValidationCommand(10), GetFromDto = (x => x.LocalAuthority)}
            };
        }
    }

}
