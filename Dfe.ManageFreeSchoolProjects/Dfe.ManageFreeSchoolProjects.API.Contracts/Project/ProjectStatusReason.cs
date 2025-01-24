using System.ComponentModel;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project
{
    public enum ProjectCancelledReason
    {
        NotSet,

        [Description("Education quality")]
        EducationQuality,

        [Description("Governance")]
        Governance,

        [Description("Site and planning issues")]
        SiteAndPlanningIssues,

        [Description("Pupil numbers")]
        PupilNumbers,

    }

    public enum ProjectWithdrawnReason
    {
        NotSet,

        [Description("Education quality")]
        EducationQuality,

        [Description("Governance")]
        Governance,

        [Description("Site and planning issues")]
        SiteAndPlanningIssues,

        [Description("Pupil numbers")]
        PupilNumbers,

    }
}
