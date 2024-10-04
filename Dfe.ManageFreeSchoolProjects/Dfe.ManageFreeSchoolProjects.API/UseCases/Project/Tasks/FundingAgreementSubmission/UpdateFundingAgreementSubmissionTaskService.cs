
using Dfe.ManageFreeSchoolProjects.Data;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.FundingAgreementSubmission
{
    public class UpdateFundingAgreementSubmissionTaskService : IUpdateTaskService
    {
        private readonly MfspContext _context;

        public UpdateFundingAgreementSubmissionTaskService(MfspContext context)
        {
            _context = context;
        }

        public async Task Update(UpdateTaskServiceParameters parameters)
        {
            var task = parameters.Request.FundingAgreementSubmission;
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

            milestone.FsgPreOpeningMilestonesMfadDraftedFaSubmission = task.DraftedFundingAgreementSubmission;
            milestone.FsgPreOpeningMilestonesMfadRegionalDirectorSignedOffFaSubmission = task.RegionalDirectorSignedOffFundingAgreementSubmission;
            milestone.FsgPreOpeningMilestonesMfadMinisterSignedOffFaSubmission = task.MinisterSignedOffFundingAgreementSubmission;
            milestone.FsgPreOpeningMilestonesMfadIncludedSignedOffImpactAssessmentFaSubmission = task.IncludedSignedOffImpactAssessment;
            milestone.FsgPreOpeningMilestonesMfadSavedFaSubmissionInWorkplacesFolder = task.SavedFundingAgreementSubmissionInWorkplacesFolder;

            await _context.SaveChangesAsync();
        }
    }
}
