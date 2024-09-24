using Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit.Validations;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit
{

    public class BulkEditHeaderRegister : IHeaderRegister<BulkEditDto>
    {
        public string IdentifingHeader => "ProjectId";
        public List<HeaderType<BulkEditDto>> GetHeaders()
        {
            return new List<HeaderType<BulkEditDto>>
            {
                new() { Name = "ProjectId", Type = new ProjectIdValidationCommand(), GetFromDto = (x => x.ProjectId) },
                new() { Name = "SchoolName", Type = new TextValidationCommand(10), GetFromDto = (x => x.SchoolName)  },
                new() { Name = "LocalAuthority", Type = new TextValidationCommand(10), GetFromDto = (x => x.LocalAuthority)}
                //new() { Name = "Region", Type = new TextValidationCommand(100), DatabaseField = ( c => c.Po) },
            };
        }
    }

}
