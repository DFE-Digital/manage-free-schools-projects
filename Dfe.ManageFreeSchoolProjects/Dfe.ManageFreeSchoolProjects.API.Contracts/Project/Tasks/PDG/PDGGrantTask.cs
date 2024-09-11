namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks.PDG;

public record PDGGrantTask
{
    public decimal? InitialGrant { get; set; }
    public decimal? RevisedGrant { get; set; }
}