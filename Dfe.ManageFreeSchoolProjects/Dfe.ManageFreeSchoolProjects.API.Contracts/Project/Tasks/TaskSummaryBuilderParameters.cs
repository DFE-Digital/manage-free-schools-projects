namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks
{
    public record TaskSummaryBuilderParameters
    {
        public SchoolType? SchoolType { get; set; }
        public string? ApplicationWave { get; set; }
        public TaskSummaryResponse TaskSummary { get; set; }
    }
}
