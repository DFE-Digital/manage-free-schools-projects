using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.DueDiligenceChecks;

public class UpdateDueDiligenceChecksTaskService(MfspContext context) : IUpdateTaskService
{
    public async Task Update(UpdateTaskServiceParameters parameters)
    {
        var task = parameters.Request.DueDiligenceChecks;
        var dbKpi = parameters.Kpi;

        if (task is null)
            return;

        var db = await context.Milestones.FirstOrDefaultAsync(r => r.Rid == dbKpi.Rid);

        if (db == null)
        {
            db = new Data.Entities.Existing.Milestones { Rid = dbKpi.Rid };
            context.Add(db);
        }

        db.FsgPreOpeningReceivedChairOfTrusteesCountersignedCertificate =
            task.ReceivedChairOfTrusteesCountersignedCertificate;

        db.FsgPreOpeningNonSpecialistChecksDoneOnAllTrustMembersAndTrustees = task.NonSpecialistChecksDoneOnAllTrustMembersAndTrustees;

        db.FsgPreOpeningRequestedCounterExtremismChecks = task.RequestedCounterExtremismChecks;

        db.FsgPreOpeningMilestonesDbscActualDateOfCompletion = task.DateWhenAllChecksWereCompleted;

        db.FsgPreOpeningSavedNonSpecialistChecksSpreadsheetInWorkplaces =
            task.SavedNonSpecialistChecksSpreadsheetInWorkplaces;

        db.FsgPreOpeningDeletedAnyCopiesOfChairsDBSCertificate = task.DeletedAnyCopiesOfChairsDBSCertificate;

        db.FsgPreOpeningDeletedEmailsContainingSuitabilityAndDeclarationForms =
            task.DeletedEmailsContainingSuitabilityAndDeclarationForms;
    }
}