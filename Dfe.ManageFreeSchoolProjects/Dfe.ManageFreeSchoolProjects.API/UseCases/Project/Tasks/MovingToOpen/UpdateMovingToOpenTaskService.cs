using Dfe.ManageFreeSchoolProjects.Data;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.MovingToOpen
{
    public class UpdateMovingToOpenTaskService : IUpdateTaskService
    {
        
        private readonly MfspContext _context;

        public UpdateMovingToOpenTaskService(MfspContext context)
        {
            _context = context;
        }
        
        public async Task Update(UpdateTaskServiceParameters parameters)
        {
            var task = parameters.Request.MovingToOpen;
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

            db.FsgPreOpeningMilestoneMovingToOpenProjectBriefToSfso = task.ProjectBriefToSfso;
            db.FsgPreOpeningMilestoneMovingToOpenProjectBriefToEducationEstates = task.ProjectBriefToEducationEstates;
            db.FsgPreOpeningMilestoneMovingToOpenProjectBriefToNewDeliveryOfficer =
                task.ProjectBriefToNewDeliveryOfficer;
            db.FsgPreOpeningMilestoneMovingToOpenSentEmailsToRelevantContacts = task.SentEmailsToRelevantContacts;
            db.FsgPreOpeningMilestoneMovingToOpenSentEmailsToSchoolsPrinciple = task.SentEmailsToSchoolsPrinciple;
            db.FsgPreOpeningMilestoneMovingToOpenSavedToWorkplacesFolderAnnexB = task.SavedToWorkplacesFolderAnnexB;
            db.FsgPreOpeningMilestoneMovingToOpenSavedToWorkplacesFolderAnnexE = task.SavedToWorkplacesFolderAnnexE;
            db.FsgPreOpeningMilestoneMovingToOpenSavedToWorkplacesFolderProjectBrief =
                task.SavedToWorkplacesFolderProjectBrief;
            dbKpi.ProjectStatusActualOpeningDate = task.ActualOpeningDate;

        }
    }
}