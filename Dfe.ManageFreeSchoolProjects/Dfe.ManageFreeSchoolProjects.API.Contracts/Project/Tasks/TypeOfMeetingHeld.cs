using System.ComponentModel;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;

public enum TypeOfMeetingHeld
{
    NotSet,
    [Description("Formal meeting")]
    FormalMeeting, 
    [Description("Informal meeting")]
    InformalMeeting, 
    [Description("No meeting held")]
    NoRomHeld,
}