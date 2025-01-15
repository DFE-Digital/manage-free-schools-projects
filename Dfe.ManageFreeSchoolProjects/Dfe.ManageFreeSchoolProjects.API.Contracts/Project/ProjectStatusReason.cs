using System.ComponentModel;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project
{
    public enum ProjectCancelledReason
    {
        NotSet,

        [Description("Educational")]
        Educational,

        [Description("Governance")]
        Governance,

        [Description("Planning")]
        Planning,

        [Description("Procurement / Construction")]
        ProcurementConstruction,

        [Description("Property")]
        Property,

        [Description("Pupil numbers / viability")]
        PupilNumbersViability,

        [Description("Trust not content with site option")]
        TrustNotContentWithSiteOption,

        [Description("Trust not willing to open in temporary accommodation")]
        TrustNotWillingToOpenInTemporaryAccommodation


    }

    public enum ProjectWithdrawnReason
    {
        NotSet,

        [Description("Educational")]
        Educational,

        [Description("Governance")]
        Governance,

        [Description("Planning")]
        Planning,

        [Description("Procurement / Construction")]
        ProcurementConstruction,

        [Description("Property")]
        Property,

        [Description("Pupil numbers / viability")]
        PupilNumbersViability,

        [Description("Trust not content with site option")]
        TrustNotContentWithSiteOption,

        [Description("Trust not willing to open in temporary accommodation")]
        TrustNotWillingToOpenInTemporaryAccommodation


    }
}
