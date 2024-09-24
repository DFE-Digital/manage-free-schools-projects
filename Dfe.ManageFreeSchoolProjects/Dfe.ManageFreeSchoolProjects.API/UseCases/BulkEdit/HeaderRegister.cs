namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit
{
    public record HeaderType<T>
    {
            public string Name { get; set; }

            public IValidationCommand Type { get; set; }

            public Func<T, string> GetFromDto { get; set; }
    }

    public record BulkEditDto: IBulkEditDto
    {
        public string ProjectId { get; set; }
        public string SchoolName { get; set; }
        public string Identifier { get => ProjectId; }
    }

    public class HeaderRegister : IHeaderRegister<BulkEditDto>
    {
        public string IdentifingHeader => "ProjectId";
        public List<HeaderType<BulkEditDto>> GetHeaders()
        {
            return new List<HeaderType<BulkEditDto>>
            {
                new() { Name = "ProjectId", Type = new ProjectIdValidationCommand(), GetFromDto = (x => x.ProjectId) },
                new() { Name = "SchoolName", Type = new TextValidationCommand(5), GetFromDto = (x => x.SchoolName)  },
                //new() { Name = "Region", Type = new TextValidationCommand(100), DatabaseField = ( c => c.Po) },
            };
        }
    }

}
