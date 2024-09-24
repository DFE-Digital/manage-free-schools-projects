namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit
{
    public record BulkEditDto: IBulkEditDto
    {
        public string ProjectId { get; set; }
        public string SchoolName { get; set; }
        public string Identifier { get => ProjectId; }
    }

}
