using Dfe.ManageFreeSchoolProjects.Data;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.EducationBrief;

public class UpdateEducationBriefTaskService : IUpdateTaskService
{
    private readonly MfspContext _context;

    public UpdateEducationBriefTaskService(MfspContext context)
    {
        _context = context;
    }

    public async Task Update(UpdateTaskServiceParameters parameters)
    {
        var task = parameters.Request.EducationBrief;
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

        db.FSGPreOpeningMilestonesEPPTrustConfirmedPlansAndPoliciesInPlace = task.TrustConfirmedPlansAndPoliciesInPlace;
        db.FSGPreOpeningMilestonesEPPCommissionedEEToReviewSafeguardingPolicy = task.CommissionedEEToReviewSafeguardingPolicy;
        db.FSGPreOpeningMilestonesEPPCommissionedEEToReviewPupilAssessmentRecordingAndReportingPolicy = task.CommissionedEEToReviewPupilAssessmentRecordingAndReportingPolicy;
        db.FSGPreOpeningMilestonesEPPDateEEReviewedEducationBrief = task.DateEEReviewedEducationBrief;
        db.FSGPreOpeningMilestonesEPPSavedEESpecificationAndAdviceInWorkplaces = task.SavedEESpecificationAndAdviceInWorkplaces;
        db.FSGPreOpeningMilestonesEPPSavedCopiesOfPlansAndPoliciesInWorkplaces = task.SavedCopiesOfPlansAndPoliciesInWorkplaces;
        db.FSGPreOpeningMilestonesDateTrustProvidedEducationBrief = task.DateTrustProvidedEducationBrief;

    }
}
