using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.PreFundingAgreementCheckpointMeeting
{
    public class UpdatePreFundingAgreementCheckpointMeetingTaskService : IUpdateTaskService
    {
        private readonly MfspContext _context;

        public UpdatePreFundingAgreementCheckpointMeetingTaskService(MfspContext context)
        {
            _context = context;
        }

        public async Task Update(UpdateTaskServiceParameters parameters)
        {
            var task = parameters.Request.PreFundingAgreementCheckpointMeetingTask;
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

            db.FsgPreOpeningMilestonesPfacmActualDateOfCompletion =
                parameters.Request.PreFundingAgreementCheckpointMeetingTask.DateOfTheMeeting;

            db.PFACMTypeOfMeetingHeld = parameters.Request.PreFundingAgreementCheckpointMeetingTask.TypeOfMeetingHeld.ToString();

            db.PFACMCommissionedAnExternalExpertToAttendMeetingsIfApplicable =
                parameters.Request.PreFundingAgreementCheckpointMeetingTask.CommissionedExternalExpert;

            db.PFACMSavedMeetingNoteInWorkplacesFolder = parameters.Request.PreFundingAgreementCheckpointMeetingTask
                .SavedMeetingNoteInWorkplacesFolder;

            db.PFACMSentAnEmailToTheTrust = parameters.Request.PreFundingAgreementCheckpointMeetingTask
                .SentAnEmailToTheTrust;

            db.PFACMWhyAMeetingWasNotHeld = parameters.Request.PreFundingAgreementCheckpointMeetingTask.WhyMeetingWasNotHeld;
        }
    }
}