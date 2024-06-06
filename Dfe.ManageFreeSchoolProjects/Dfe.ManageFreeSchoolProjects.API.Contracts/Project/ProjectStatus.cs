using System.ComponentModel;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project;

public enum ProjectStatus
{
    [Description("Pre-opening")]
    Preopening,
    
    [Description("Open")]
    Open,
    
    [Description("Closed")]
    Closed
}