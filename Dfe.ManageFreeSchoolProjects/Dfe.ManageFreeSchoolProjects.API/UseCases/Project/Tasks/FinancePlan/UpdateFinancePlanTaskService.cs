
using Dfe.ManageFreeSchoolProjects.Data;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.FinancePlan
{
    public class UpdateFinancePlanTaskService : IUpdateTaskService
    {
        private readonly MfspContext _context;

        public UpdateFinancePlanTaskService(MfspContext context)
        {
            _context = context;
        }

        public async Task Update(UpdateTaskServiceParameters parameters)
        {
            var task = parameters.Request.FinancePlan;
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
            milestone.FsgPreOpeningMilestonesBefpApplicable = task.FinancePlanAgreed?.ToString();
            milestone.FsgPreOpeningMilestonesBefpActualDateOfCompletion = task.DateAgreed?.Date;
            milestone.FinancePlanSavedInWorkplacesFolder = task.PlanSavedInWorkplacesFolder;
            milestone.LAAgreedPupilNumbers = task.LocalAuthorityAgreedPupilNumbers;

            var po = await _context.Po.FirstOrDefaultAsync(r => r.Rid == dbKpi.Rid);

            if (po == null)
            {
                po = new Po();
                po.Rid = dbKpi.Rid;
                _context.Add(po);
            }

            po.FinancialPlanningOptInToRpa = task.TrustWillOptIntoRpa?.ToString();
            po.FinancialPlanningStartDateOfRpa = task.RpaStartDate;
            po.FinancialPlanningTypeOfRpaCover = task.RpaCoverType;

            po.UnderwrittenPlacesPrimaryYear1 = task.UnderwrittenPlacesPrimaryYear1.ToString();
            po.UnderwrittenPlacesPrimaryYear2 = task.UnderwrittenPlacesPrimaryYear2.ToString();
            po.UnderwrittenPlacesPrimaryYear3 = task.UnderwrittenPlacesPrimaryYear3.ToString();
            po.UnderwrittenPlacesPrimaryYear4 = task.UnderwrittenPlacesPrimaryYear4.ToString();
            po.UnderwrittenPlacesPrimaryYear5 = task.UnderwrittenPlacesPrimaryYear5.ToString();
            po.UnderwrittenPlacesPrimaryYear6 = task.UnderwrittenPlacesPrimaryYear6.ToString();
            po.UnderwrittenPlacesPrimaryYear7 = task.UnderwrittenPlacesPrimaryYear7.ToString();

            po.UnderwrittenPlacesSecondaryYear1 = task.UnderwrittenPlacesSecondaryYear1.ToString();
            po.UnderwrittenPlacesSecondaryYear2 = task.UnderwrittenPlacesSecondaryYear2.ToString();
            po.UnderwrittenPlacesSecondaryYear3 = task.UnderwrittenPlacesSecondaryYear3.ToString();
            po.UnderwrittenPlacesSecondaryYear4 = task.UnderwrittenPlacesSecondaryYear4.ToString();
            po.UnderwrittenPlacesSecondaryYear5 = task.UnderwrittenPlacesSecondaryYear5.ToString();
            
            po.UnderwrittenPlacesSixteenToNineteenYear1 = task.UnderwrittenPlacesSixteenToNineteenYear1.ToString();
            po.UnderwrittenPlacesSixteenToNineteenYear2 = task.UnderwrittenPlacesSixteenToNineteenYear2.ToString();
            po.UnderwrittenPlacesSixteenToNineteenYear3 = task.UnderwrittenPlacesSixteenToNineteenYear3.ToString();

            po.ConfirmationFromLocalAuthoritySavedInWorkplacesFolder = task.ConfirmationFromLocalAuthoritySavedInWorkplacesFolder;
            po.CommentsAboutUnderwrittenPlaces = task.CommentsAboutUnderwrittenPlaces;

            await _context.SaveChangesAsync();
        }
    }
}
