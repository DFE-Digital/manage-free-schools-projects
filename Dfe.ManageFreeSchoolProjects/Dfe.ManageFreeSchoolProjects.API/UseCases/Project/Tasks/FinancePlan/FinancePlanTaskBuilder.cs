using Dfe.ManageFreeSchoolProjects.API.Contracts.Common;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Extensions;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.FinancePlan
{
    public static class FinancePlanTaskBuilder
    {
        public static FinancePlanTask Build(Milestones milestones, Po po)
        {
            if (milestones == null || po == null)
            {
                return new FinancePlanTask();
            }

            return new FinancePlanTask()
            {
                FinancePlanAgreed = ConvertYesNo(milestones.FsgPreOpeningMilestonesBefpApplicable),
                DateAgreed = milestones.FsgPreOpeningMilestonesBefpActualDateOfCompletion,
                PlanSavedInWorkplacesFolder = milestones.FinancePlanSavedInWorkplacesFolder,
                LocalAuthorityAgreedPupilNumbers = milestones.LAAgreedPupilNumbers,
                TrustWillOptIntoRpa = ConvertYesNo(po.FinancialPlanningOptInToRpa),
                RpaStartDate = po.FinancialPlanningStartDateOfRpa,
                RpaCoverType = po.FinancialPlanningTypeOfRpaCover,

                UnderwrittenPlacesPrimaryYear1 = po.UnderwrittenPlacesPrimaryYear1.ToInt(),
                UnderwrittenPlacesPrimaryYear2 = po.UnderwrittenPlacesPrimaryYear2.ToInt(),
                UnderwrittenPlacesPrimaryYear3 = po.UnderwrittenPlacesPrimaryYear3.ToInt(),
                UnderwrittenPlacesPrimaryYear4 = po.UnderwrittenPlacesPrimaryYear4.ToInt(),
                UnderwrittenPlacesPrimaryYear5 = po.UnderwrittenPlacesPrimaryYear5.ToInt(),
                UnderwrittenPlacesPrimaryYear6 = po.UnderwrittenPlacesPrimaryYear6.ToInt(),
                UnderwrittenPlacesPrimaryYear7 = po.UnderwrittenPlacesPrimaryYear7.ToInt(),

                UnderwrittenPlacesSecondaryYear1 = po.UnderwrittenPlacesSecondaryYear1.ToInt(),
                UnderwrittenPlacesSecondaryYear2 = po.UnderwrittenPlacesSecondaryYear2.ToInt(),
                UnderwrittenPlacesSecondaryYear3 = po.UnderwrittenPlacesSecondaryYear3.ToInt(),
                UnderwrittenPlacesSecondaryYear4 = po.UnderwrittenPlacesSecondaryYear4.ToInt(),
                UnderwrittenPlacesSecondaryYear5 = po.UnderwrittenPlacesSecondaryYear5.ToInt(),

                UnderwrittenPlacesSixteenToNineteenYear1 = po.UnderwrittenPlacesSixteenToNineteenYear1.ToInt(),
                UnderwrittenPlacesSixteenToNineteenYear2 = po.UnderwrittenPlacesSixteenToNineteenYear2.ToInt(),
                UnderwrittenPlacesSixteenToNineteenYear3 = po.UnderwrittenPlacesSixteenToNineteenYear3.ToInt(),

                ConfirmationFromLocalAuthoritySavedInWorkplacesFolder = po.ConfirmationFromLocalAuthoritySavedInWorkplacesFolder,
                CommentsAboutUnderwrittenPlaces = po.CommentsAboutUnderwrittenPlaces



            };
        }

        private static YesNo? ConvertYesNo(string value)
        {
            return Enum.TryParse<YesNo>(value, true, out var result) ? result : null;
        }
    }
}
