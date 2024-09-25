using System.ComponentModel;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;

public enum TypeOfMeetingHeld
{
    NotSet,
    [Description("Formal meeting")]
    FormalMeeting,
    [Description("Formal checkpoint meeting")]
    FormalCheckpointMeeting,
    [Description("Informal meeting")]
    InformalMeeting,
    [Description("Internal review / case conference")]
    InternalReviewMeeting,
    [Description("No meeting held")]
    NoMeetingHeld,
}