using Dfe.ManageFreeSchoolProjects.Data;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.GovernancePlan
{
    public class UpdateGovernancePlanTaskService : IUpdateTaskService
    {
        private readonly MfspContext _context;

        public UpdateGovernancePlanTaskService(MfspContext context)
        {
            _context = context;
        }

        public async Task Update(UpdateTaskServiceParameters parameters)
        {
            var task = parameters.Request.GovernancePlan;
            var dbKpi = parameters.Kpi;

            if (task is null)
            {
                return;
            }

            var milestone = await _context.Milestones.FirstOrDefaultAsync(r => r.Rid == dbKpi.Rid);

            if (milestone == null)
            {
                milestone = new Milestones();
                milestone.Rid = dbKpi.Rid;
                _context.Add(milestone);
            }

            milestone.Rid = dbKpi.Rid;
            milestone.FsgPreOpeningMilestonesDgpActualDateOfCompletion = task.DatePlanReceived;
            milestone.DraftGovernancePlanDocumentsSavedInWorkplacesFolder = task.SavedDocumentsInWorkplacesFolder;
            milestone.DraftGovernancePlanAndAssessmentSharedWithEsfa = task.PlanAndAssessmentSharedWithEsfa;
            milestone.DraftGovernancePlanAndAssessmentSharedWithExpert = task.PlanAndAssessmentSharedWithExpert;
            milestone.GovernancePlanAndAssessmentSharedWithLocalAuthority = task.PlanAndAssessmentSharedWithLocalAuthority;
            milestone.DraftGovernancePlanAssessedUsingTemplate = task.PlanAssessedUsingTemplate;
            milestone.DraftGovernancePlanFedBackToTrust = task.PlanFedBackToTrust;
            milestone.DraftGovernancePlanReceivedFromTrust = task.PlanReceivedFromTrust;
            milestone.FsgPreOpeningMilestonesFgpaActualDateOfCompletion = task.DatePlanAgreed;
            milestone.FinalGovernancePlanAgreed = task.FinalGovernancePlanAgreed;
            milestone.FsgPreOpeningMilestonesMi60CommentsOnDecisionToApproveIfApplicable = task.Comments;

            await _context.SaveChangesAsync();
        }
    }
}
