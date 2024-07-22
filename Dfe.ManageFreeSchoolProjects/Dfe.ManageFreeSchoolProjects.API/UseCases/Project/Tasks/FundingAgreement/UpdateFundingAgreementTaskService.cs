using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.FundingAgreement
{
    public class UpdateFundingAgreementTaskService : IUpdateTaskService
    {
        private readonly MfspContext _context;

        public UpdateFundingAgreementTaskService(MfspContext context)
        {
            _context = context;
        }

        public async Task Update(UpdateTaskServiceParameters parameters)
        {
            var task = parameters.Request.FundingAgreement;
            var dbKpi = parameters.Kpi;
            
            if (task is null)
            {
                return;
            }

            var db = await _context.Milestones.FirstOrDefaultAsync(r => r.Rid == dbKpi.Rid);

            if (db == null)
            {
                db = new Data.Entities.Existing.Milestones();
                db.Rid = dbKpi.Rid;
                _context.Add(db);
            }

            db.FsgPreOpeningMilestonesMfadTailoredAModelFundingAgreement = task.TailoredTheFundingAgreement;
            db.FsgPreOpeningMilestonesMfadSharedFaWithTheTrust = task.SharedFAWithTheTrust;
            db.FsgPreOpeningMilestonesMfadTrustAgreesWithModelFa = task.TrustHasSignedTheFA;
            db.FsgPreOpeningMilestonesMfadActualDateOfCompletion = task.DateTheTrustSignedFA;
            db.FsgPreOpeningMilestonesFaForecastDate = task.ExpectedDateFAIsSignedOnSecretaryOfStatesBehalf;
            db.FsgPreOpeningMilestonesFaActualDateOfCompletion = task.DateFAWasSigned;
            db.FsgPreOpeningMilestonesMfadSavedFaDocumentsInWorkspacesFolder =  task.SavedFADocumentsInWorkplacesFolder;
            
        }
    }
}