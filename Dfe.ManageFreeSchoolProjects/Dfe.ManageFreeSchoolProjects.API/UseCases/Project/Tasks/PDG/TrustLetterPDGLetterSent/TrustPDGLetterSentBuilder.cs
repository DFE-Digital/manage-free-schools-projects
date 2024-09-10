using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks.PDG;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.PDG.TrustLetterPDGLetterSent
{
    public static class TrustPDGLetterSentBuilder
    {
        public static TrustPDGLetterSentTask Build(Po po)
        {
            if (po == null)
            {
                return new TrustPDGLetterSentTask();
            }

            return new TrustPDGLetterSentTask()
            {
                TrustSignedPDGLetterDate = po.ProjectDevelopmentGrantFundingPdgGrantLetterDate,
                PDGLetterSavedInWorkplaces = GetPDGLetterSavedInWorkplaces(po),
            };
        }

        public static bool? GetPDGLetterSavedInWorkplaces(Po po)
        {
            if (po == null)
            {
                return null;
            }

            if(po.PdgGrantLetterLinkSavedToWorkplaces != null)
            {
                return po.PdgGrantLetterLinkSavedToWorkplaces;
            }

            return po.ProjectDevelopmentGrantFundingPdgGrantLetterLink != null &&
                                            po.ProjectDevelopmentGrantFunding1stPdgGrantVariationLink != null &&
                                            po.ProjectDevelopmentGrantFunding2ndPdgGrantVariationLink != null &&
                                            po.ProjectDevelopmentGrantFunding3rdPdgGrantVariationLink != null &&
                                            po.ProjectDevelopmentGrantFunding4thPdgGrantVariationLink != null;
        }
    }
}
