using Dfe.ManageFreeSchoolProjects.API.Contracts.Common;
using Dfe.ManageFreeSchoolProjects.API.Extensions;
using Dfe.ManageFreeSchoolProjects.Data;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.OfstedInspection
{
    public class UpdateOfstedInspectionTaskService(MfspContext context) : IUpdateTaskService
    {
        public async Task Update(UpdateTaskServiceParameters parameters)
        {
            var task = parameters.Request.OfstedInspection;
            var dbKpi = parameters.Kpi;

            if (task is null)
                return;
            
            var db = await context.Milestones.FirstOrDefaultAsync(r => r.Rid == dbKpi.Rid);

            if (db == null)
            {
                db = new Milestones { Rid = dbKpi.Rid };
                context.Add(db);
            }

            db.FsgPreOpeningMilestonesProcessDetailsProvided = task.ProcessDetailsProvided;
            db.FsgPreOpeningMilestonesInspectionBlockDecided = task.InspectionBlockDecided;
            db.FsgPreOpeningMilestonesOfstedAndTrustLiaisonDetailsConfirmed = task.OfstedAndTrustLiaisonDetailsConfirmed;
            db.FsgPreOpeningMilestonesBlockAndContentDetailsToOpenersSpreadSheet = task.BlockAndContentDetailsToOpenersSpreadSheet;
            db.FsgPreOpeningMilestonesSharedOutcomeWithTrust = task.SharedOutcomeWithTrust;
            db.FsgPreOpeningMilestonesProposedToOpenOnGias = task.ProposedToOpenOnGias;
            db.FsgPreOpeningMilestonesDocumentsAndG6SavedToWorkplaces = task.SavedToWorkplaces;
            db.FsgPreOpeningMilestonesOprActualDateOfCompletion = task.DateInspectionsAndAnyActionsCompleted;


            switch (task.InspectionConditionsMet)
            {
                case YesNoNotApplicable.Yes or YesNoNotApplicable.No:
                    db.FsgPreOpeningMilestonesInspectionConditionsMet = task.InspectionConditionsMet.ToDescription();
                    db.FsgPreOpeningInspectionConditionsMetNotApplicable = null;
                    break;
                case YesNoNotApplicable.NotApplicable:
                    db.FsgPreOpeningInspectionConditionsMetNotApplicable = task.InspectionConditionsMet.ToDescription();
                    db.FsgPreOpeningMilestonesInspectionConditionsMet = null;
                    break;
                default:
                    db.FsgPreOpeningInspectionConditionsMetNotApplicable = null;
                    db.FsgPreOpeningMilestonesInspectionConditionsMet = null;
                    break;
            }
        }
    }
}