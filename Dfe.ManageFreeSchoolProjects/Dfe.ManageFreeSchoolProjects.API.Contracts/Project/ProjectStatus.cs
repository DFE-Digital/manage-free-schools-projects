using System.ComponentModel;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project;

public enum ProjectStatus
{
    [Description("Pre-opening")]
    Preopening,
    
    [Description("Open")]
    Open,
    
    [Description("Closed")]
    Closed,
    
    [Description("Cancelled")]
    Cancelled,
    
    [Description("Withdrawn in pre-opening")]
    WithdrawnDuringPreOpening,

    [Description("Application competition stage")]
    ApplicationCompetitionStage,

    [Description("Application stage")]
    ApplicationStage,

    [Description("Open - not included in figures")]
    OpenNotIncludedInFigures,

    [Description("Pre-opening - not included in figures")]
    PreopeningNotIncludedInFigures,

    [Description("Rejected")]
    Rejected,

    [Description("Withdrawn in application stage")]
    WithdrawnDuringApplication,
}