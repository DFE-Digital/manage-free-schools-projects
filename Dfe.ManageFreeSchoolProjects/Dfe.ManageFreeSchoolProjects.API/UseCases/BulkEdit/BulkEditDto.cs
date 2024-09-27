using System.Security.Permissions;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit
{
    public record BulkEditDto: IBulkEditDto
    {
        public string Identifier { get => ProjectId; }
        public string ProjectId { get; set; }
        public string SchoolName { get; set; }
        public string OpeningDate { get; set; }
        public string LocalAuthority { get; set; }
    }

}
