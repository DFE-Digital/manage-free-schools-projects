using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.Data
{
    public partial class MfspContext : DbContext
    {
        public virtual DbSet<Entities.Existing.ConstructData> ConstructData { get; set; }
    }
}

namespace Dfe.ManageFreeSchoolProjects.Data.Entities.Existing
{
    public partial class ConstructData : IAuditable
    {
        public string ProjectId { get; set; }

        public DateOnly? HoTAgreedForSiteForMainSchoolBuildingActual { get; set; }

        public DateOnly? TemporaryAccommodationFirstReadyForOccupationForecast { get; set; }

        public DateOnly? TemporaryAccommodationFirstReadyForOccupationActual { get; set; }

        public DateOnly? MainSchoolBuildingFirstReadyForOccupationForecast { get; set; }

        public DateOnly? MainSchoolBuildingFirstReadyForOccupationActual { get; set; }

        public DateOnly? SiteIdentifiedForMainSchoolBuildingActual { get; set; }

        public string CapitalProjectRag { get; set; }

        public string PlanningSiteId { get; set; }

        public string IsThisTheMainPlanningRecord { get; set; }

        public string PlanningRisk { get; set; }

        public string PlanningDecision { get; set; }

        public string SiteId { get; set; }

        public string TypeOfSite { get; set; }

        public string SiteStatus { get; set; }

        public string PostcodeOfSite { get; set; }

        public DateOnly? PracticalCompletionCertificateIssuedDateA { get; set; }

        public string RegionalHead { get; set; }

        public string ProjectDirector { get; set; }

        public string ProjectManager { get; set; }

        public string TemporaryRagRating { get; set; }

        public string CapitalProjectRagRatingCommentary { get; set; }

        public string TemporaryRagRatingCommentary { get; set; }

        public DateOnly? DateOfHoTSecuredOnTemporaryAccommodationSiteIfRequired { get; set; }

        public DateOnly? LastRefreshDate { get; set; }


        public string WillTheProjectOpenInTemporaryAccommodation { get; set; }
        public string HoTsAgreedForTemporarySiteForecast { get; set; }
        public string ContractorForTemporarySiteAppointedForecast { get; set; }
        public string ContractorForTemporarySiteAppointedActual { get; set; }
        public string DateOfPlanningDecisionForTemporarySiteMainPlanningRecordForecast { get; set; }
        public string DateOfPlanningDecisionForTemporarySiteMainPlanningRecordActual { get; set; }
        public string TemporarySitePlanningDecision { get; set; }
        public string HoTsAgreedForSiteForMainSchoolBuildingForecast { get; set; }
        public string ContractorForSiteForMainSchoolBuildingAppointedForecast { get; set; }
        public string ContractorForSiteForMainSchoolBuildingAppointedActual { get; set; }
        public string DateOfPlanningDecisionForMainSiteMainPlanningRecordForecast { get; set; }
        public string DateOfPlanningDecisionForMainSiteMainPlanningRecordActual { get; set; }
        public string TemporarySiteAddress { get; set; }
        public string TemporarySitePostcode { get; set; }
        public string TemporarySitePlanningRisk { get; set; }
        public string DateTemporarySitePlanningApprovalGranted { get; set; }
        public string MainSiteAddress { get; set; }
        public string DateMainSitePlanningApprovalGranted { get; set; }

    }
}