using Dfe.ManageFreeSchoolProjects.API.Contracts.Common;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks
{
    public class FinancePlanTask
    {
        public YesNo? FinancePlanAgreed { get; set; }
        public DateTime? DateAgreed { get; set; }
        public YesNo? PlanSavedInWorkplacesFolder { get; set; }
        public YesNoNotApplicable? LocalAuthorityAgreedPupilNumbers { get; set; }
        public YesNo? TrustWillOptIntoRpa { get; set; }
        public DateTime? RpaStartDate { get; set; }
        public string RpaCoverType { get; set; }

        public int UnderwrittenPlacesPrimaryYear1 { get; set; }
        public int UnderwrittenPlacesPrimaryYear2 { get; set; }
        public int UnderwrittenPlacesPrimaryYear3 { get; set; }
        public int UnderwrittenPlacesPrimaryYear4 { get; set; }
        public int UnderwrittenPlacesPrimaryYear5 { get; set; }
        public int UnderwrittenPlacesPrimaryYear6 { get; set; }
        public int UnderwrittenPlacesPrimaryYear7 { get; set; }
        public int UnderwrittenPlacesSecondaryYear1 { get; set; }
        public int UnderwrittenPlacesSecondaryYear2 { get; set; }
        public int UnderwrittenPlacesSecondaryYear3 { get; set; }
        public int UnderwrittenPlacesSecondaryYear4 { get; set; }
        public int UnderwrittenPlacesSecondaryYear5 { get; set; }
        public int UnderwrittenPlacesSixteenToNineteenYear1 { get; set; }
        public int UnderwrittenPlacesSixteenToNineteenYear2 { get; set; }
        public int UnderwrittenPlacesSixteenToNineteenYear3 { get; set; }

        public bool? ConfirmationFromLocalAuthoritySavedInWorkplacesFolder { get; set; }

        public string CommentsAboutUnderwrittenPlaces { get; set; }

    }
}
