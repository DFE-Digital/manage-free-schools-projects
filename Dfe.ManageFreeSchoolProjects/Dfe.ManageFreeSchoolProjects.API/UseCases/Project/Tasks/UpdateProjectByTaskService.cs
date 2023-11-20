using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Exceptions;
using Dfe.ManageFreeSchoolProjects.Data;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks
{
    public interface IUpdateProjectByTaskService
    {
        public Task Execute(string projectId, UpdateProjectByTaskRequest request);
    }

    public class UpdateProjectByTaskService : IUpdateProjectByTaskService
    {
        private readonly MfspContext _context;

        public UpdateProjectByTaskService(MfspContext context)
        {
            _context = context;
        }

        public async Task Execute(string projectId, UpdateProjectByTaskRequest request)
        {
            var dbKpi = await _context.Kpi.FirstOrDefaultAsync(p => p.ProjectStatusProjectId == projectId);

            if (dbKpi == null)
            {
                throw new NotFoundException($"Project {projectId} not found");
            }

            ApplySchoolTaskUpdates(request.School, dbKpi);
            ApplyDatesTaskUpdates(request.Dates, dbKpi);
            ApplyRegionAndLocalAuthorityTaskUpdates(request.RegionAndLocalAuthorityTask, dbKpi);
            await ApplyRiskAppraisalMeetingTaskUpdates(request.RiskAppraisalMeeting, dbKpi);
            await ApplyTrustTaskUpdates(request.Trust, dbKpi);
            await UpdateTaskStatus(dbKpi.Rid, Status.InProgress, request);
            ApplyConstituencyTaskUpdates(request.Constituency, dbKpi);

            await _context.SaveChangesAsync();
        }

        private static void ApplyRegionAndLocalAuthorityTaskUpdates(RegionAndLocalAuthorityTask regionAndLocalAuthorityTask, Kpi dbKpi)
        {
            if (regionAndLocalAuthorityTask is null)
            {
                return;
            }

            dbKpi.LocalAuthority = regionAndLocalAuthorityTask.LocalAuthority;
            dbKpi.SchoolDetailsGeographicalRegion = regionAndLocalAuthorityTask.Region;
            dbKpi.SchoolDetailsLocalAuthority = regionAndLocalAuthorityTask.LocalAuthorityCode;
        }

        private async Task ApplyRiskAppraisalMeetingTaskUpdates(RiskAppraisalMeetingTask riskAppraisalMeetingTask, Kpi dbKpi)
        {
            if (riskAppraisalMeetingTask is null)
            {
                return;
            }

            var dbRiskAppraisalMeetingTask = await _context.RiskAppraisalMeetingTask.FirstOrDefaultAsync(r => r.RID == dbKpi.Rid);

            if (dbRiskAppraisalMeetingTask == null)
            {
                dbRiskAppraisalMeetingTask = new Data.Entities.RiskAppraisalMeetingTask();
                dbRiskAppraisalMeetingTask.RID = dbKpi.Rid;
                setRiskAppraisalMeeting(riskAppraisalMeetingTask, dbRiskAppraisalMeetingTask);
                _context.Add(dbRiskAppraisalMeetingTask);
                return;
            }

            setRiskAppraisalMeeting(riskAppraisalMeetingTask, dbRiskAppraisalMeetingTask);
        }

        private static void setRiskAppraisalMeeting(RiskAppraisalMeetingTask riskAppraisalMeetingTask, Data.Entities.RiskAppraisalMeetingTask dbRiskAppraisalMeetingTask)
        {
            dbRiskAppraisalMeetingTask.MeetingCompleted = riskAppraisalMeetingTask.InitialRiskAppraisalMeetingCompleted;
            dbRiskAppraisalMeetingTask.ForecastDate = riskAppraisalMeetingTask.ForecastDate;
            dbRiskAppraisalMeetingTask.ActualDate = riskAppraisalMeetingTask.ActualDate;
            dbRiskAppraisalMeetingTask.CommentOnDecision = riskAppraisalMeetingTask.CommentsOnDecisionToApprove;
            dbRiskAppraisalMeetingTask.ReasonNotApplicable = riskAppraisalMeetingTask.ReasonNotApplicable;
        }

        private static void ApplySchoolTaskUpdates(SchoolTask task, Kpi dbKpi)
        {
            if (task == null)
            {
                return;
            }

            var faithStatus = task.FaithStatus == FaithStatus.NotSet ? string.Empty : task.FaithStatus.ToString();
            var faithType = task.FaithType == FaithType.NotSet ? string.Empty : task.FaithType.ToString();
            var gender = task.Gender == Gender.NotSet ? string.Empty : task.Gender.ToString();

            dbKpi.ProjectStatusCurrentFreeSchoolName = task.CurrentFreeSchoolName;
            dbKpi.SchoolDetailsSchoolTypeMainstreamApEtc = task.SchoolType.MapSchoolType();
            dbKpi.SchoolDetailsSchoolPhasePrimarySecondary = task.SchoolPhase.MapSchoolPhase();
            dbKpi.SchoolDetailsGender = gender;
            dbKpi.SchoolDetailsAgeRange = task.AgeRange;
            dbKpi.SchoolDetailsNursery = task.Nursery;
            dbKpi.SchoolDetailsSixthForm = task.SixthForm;

            dbKpi.SchoolDetailsFaithStatus = faithStatus;
            dbKpi.SchoolDetailsFaithType = faithType;
            dbKpi.SchoolDetailsPleaseSpecifyOtherFaithType = task.OtherFaithType;
        }

        private static void ApplyDatesTaskUpdates(DatesTask task, Kpi dbKpi)
        {
            if (task == null)
            {
                return;
            }

            dbKpi.ProjectStatusDateOfEntryIntoPreOpening = task.DateOfEntryIntoPreopening;
            dbKpi.ProjectStatusRealisticYearOfOpening = task.RealisticYearOfOpening;
            dbKpi.ProjectStatusProvisionalOpeningDateAgreedWithTrust = task.ProvisionalOpeningDateAgreedWithTrust;
        }

        private async Task ApplyTrustTaskUpdates(
            TrustTask task,
            Kpi dbKpi)
        {
            if (task == null)
            {
                return;
            }

            var trust = await GetTrust(task.TRN);

            dbKpi.TrustId = trust.TrustRef;
            dbKpi.TrustName = trust.TrustsTrustName;
            dbKpi.TrustType = trust.TrustsTrustType;

            dbKpi.SchoolDetailsTrustId = trust.TrustsTrustRef;
            dbKpi.SchoolDetailsTrustName = trust.TrustsTrustName;
            dbKpi.SchoolDetailsTrustType = trust.TrustsTrustType;
        }

        private static void ApplyConstituencyTaskUpdates(ConstituencyTask task, Kpi dbKpi)
		{
			if (task == null)
			{
				return;
			}

			dbKpi.SchoolDetailsConstituency = task.Name;
			dbKpi.SchoolDetailsConstituencyMp = task.MPName;
			dbKpi.SchoolDetailsPoliticalParty = task.Party;
		}


		private async Task<Trust> GetTrust(string trustRef)
        {
            var result = await _context.Trust.FirstOrDefaultAsync(e => e.TrustRef == trustRef);

            return result;
        }

        private async Task UpdateTaskStatus(string taskRid, Status updatedStatus, UpdateProjectByTaskRequest updateProjectByTaskRequest)
        {
            var taskNameToUpdate = Enum.Parse<TaskName>(updateProjectByTaskRequest.TaskToUpdate);

            var task = await _context.Tasks.SingleOrDefaultAsync(x => x.Rid == taskRid && x.TaskName == taskNameToUpdate);
            if (task is null)
                return;

            task.Status = updatedStatus;
        }
    }
}