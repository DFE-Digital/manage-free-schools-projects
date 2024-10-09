using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.ReadinessToOpenMeeting;

public class UpdateROMService(MfspContext context) : IUpdateTaskService
{
    public async Task Update(UpdateTaskServiceParameters parameters)
    {
        var task = parameters.Request.ReadinessToOpenMeetingTask;
        var dbKpi = parameters.Kpi;

        if (task is null)
            return;
        
        var db = await context.Milestones.FirstOrDefaultAsync(r => r.Rid == dbKpi.Rid);

        if (db == null)
        {
            db = new Data.Entities.Existing.Milestones { Rid = dbKpi.Rid };
            context.Add(db);
        }

        db.AROMIsExpectedToHappen = parameters.Request.ReadinessToOpenMeetingTask.AROMIsExpectedToHappen;

        db.FsgPreOpeningMilestonesRomForecastDate = parameters.Request.ReadinessToOpenMeetingTask.ExpectedDateOfTheMeeting;

        db.FsgPreOpeningMilestonesRomActualDateOfCompletion =
            parameters.Request.ReadinessToOpenMeetingTask.DateOfTheMeeting;

        db.ROMTypeOfMeetingHeld = parameters.Request.ReadinessToOpenMeetingTask.TypeOfMeetingHeld.ToString();
        
        db.ROMPrincipalDesignateHasProvidedChecklist =
            parameters.Request.ReadinessToOpenMeetingTask.PrincipalDesignateHasProvidedTheChecklist;

        db.ROMCommissionedAnExternalExpertToAttendMeetingsIfApplicable = parameters.Request.ReadinessToOpenMeetingTask
            .CommissionedAnExternalExpertToAttendAnyMeetingsIfApplicable;

        db.ROMSavedTheInternalROMReportInWorkplacesFolder = parameters.Request.ReadinessToOpenMeetingTask
            .SavedTheInternalRomReportToWorkplacesFolder;

        db.ROMSavedTheExternalROMReportInWorkplacesFolder = parameters.Request.ReadinessToOpenMeetingTask
            .SavedTheExternalRomReportToWorkplacesFolder;

        db.ROMWhyAMeetingWasNotHeld = parameters.Request.ReadinessToOpenMeetingTask.WhyMeetingWasNotHeld;
    }
}