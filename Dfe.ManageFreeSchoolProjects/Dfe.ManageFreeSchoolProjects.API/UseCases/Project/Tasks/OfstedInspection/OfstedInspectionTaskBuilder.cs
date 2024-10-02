﻿using Dfe.ManageFreeSchoolProjects.API.Contracts.Common;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.OfstedInspection
{
    public static class OfstedInspectionTaskBuilder
    {
        public static OfstedInspectionTask Build(Milestones milestones)
        {
            if (milestones == null)
            {
                return new OfstedInspectionTask();
            }

            return new OfstedInspectionTask()
            {
                ProcessDetailsProvided = milestones.FsgPreOpeningMilestonesProcessDetailsProvided,
                InspectionBlockDecided = milestones.FsgPreOpeningMilestonesInspectionBlockDecided,
                OfstedAndTrustLiaisonDetailsConfirmed = milestones.FsgPreOpeningMilestonesOfstedAndTrustLiaisonDetailsConfirmed,
                BlockAndContentDetailsToOpenersSpreadSheet = milestones.FsgPreOpeningMilestonesBlockAndContentDetailsToOpenersSpreadSheet,
                SharedOutcomeWithTrust = milestones.FsgPreOpeningMilestonesSharedOutcomeWithTrust,
                ProposedToOpenOnGias = milestones.FsgPreOpeningMilestonesProposedToOpenOnGias,
                SavedToWorkplaces = milestones.FsgPreOpeningMilestonesDocumentsAndG6SavedToWorkplaces,
                InspectionConditionsMet = InspectionConditionsMet(milestones.FsgPreOpeningMilestonesInspectionConditionsMet),
            };

        }

        private static YesNoNotApplicable InspectionConditionsMet(string condition)
        {
            return condition switch
            {
                "Yes" => YesNoNotApplicable.Yes,
                "No" => YesNoNotApplicable.No,
                "Not applicable" => YesNoNotApplicable.NotApplicable
            };
        }
    }
}
