using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks
{
    public interface IGetProjectByTaskService
    {
        public Task<GetProjectByTaskResponse> Execute(string projectId);
    }

    public class GetProjectByTaskService : IGetProjectByTaskService
    {
        private readonly MfspContext _context;

        public GetProjectByTaskService(MfspContext context)
        {
            _context = context;
        }

        public async Task<GetProjectByTaskResponse> Execute(string projectId)
        {
            var result = await
                (from kpi in _context.Kpi
                    where kpi.ProjectStatusProjectId == projectId
                    join kai in _context.Kai on kpi.Rid equals kai.Rid into kaiJoin
                    from kai in kaiJoin.DefaultIfEmpty()
                    join property in _context.Property on kpi.Rid equals property.Rid into propertyJoin
                    from property in propertyJoin.DefaultIfEmpty()
                    join riskAppraisalMeetingTask in _context.RiskAppraisalMeetingTask on kpi.Rid equals riskAppraisalMeetingTask.RID into riskAppraisalMeetingTaskJoin
                 from riskAppraisalMeetingTask in riskAppraisalMeetingTaskJoin.DefaultIfEmpty()
                    select new GetProjectByTaskResponse
                    {
                        School = new SchoolTask
                            {    
                            CurrentFreeSchoolName = kpi.ProjectStatusCurrentFreeSchoolName,
                            SchoolType = ProjectMapper.ToSchoolType(kpi.SchoolDetailsSchoolTypeMainstreamApEtc),
                            SchoolPhase = ProjectMapper.ToSchoolPhase(kpi.SchoolDetailsSchoolPhasePrimarySecondary),
                            AgeRange = kpi.SchoolDetailsAgeRange,
                            Gender = EnumParsers.ParseGender(kpi.SchoolDetailsGender),
                            Nursery = EnumParsers.ParseNursery(kpi.SchoolDetailsNursery),
                            SixthForm = EnumParsers.ParseSixthForm(kpi.SchoolDetailsSixthForm),
                            FaithStatus = EnumParsers.ParseFaithStatus(kpi.SchoolDetailsFaithStatus),
                            FaithType = EnumParsers.ParseFaithType(kpi.SchoolDetailsFaithType),
                            OtherFaithType = kpi.SchoolDetailsPleaseSpecifyOtherFaithType
                        },
                        Dates = new DatesTask()
                        {
                            DateOfEntryIntoPreopening = kpi.ProjectStatusDateOfEntryIntoPreOpening,
                            ProvisionalOpeningDateAgreedWithTrust = kpi.ProjectStatusProvisionalOpeningDateAgreedWithTrust,
                            RealisticYearOfOpening = kpi.ProjectStatusRealisticYearOfOpening,
                        },
                        Trust = new TrustTask()
                        {
                            TRN = kpi.TrustId,
                            TrustName = kpi.TrustName,
                            TrustType = kpi.TrustType,
                        },
                        RegionAndLocalAuthority = new RegionAndLocalAuthorityTask
                        {
                            Region = kpi.SchoolDetailsGeographicalRegion, 
                            LocalAuthority = kpi.LocalAuthority,
                        },
                        RiskAppraisalMeeting = new RiskAppraisalMeetingTask
                        {
                            InitialRiskAppraisalMeetingCompleted = riskAppraisalMeetingTask.MeetingCompleted,
                            ForecastDate = riskAppraisalMeetingTask.ForecastDate,
                            ActualDate = riskAppraisalMeetingTask.ActualDate,
                            CommentsOnDecisionToApprove = riskAppraisalMeetingTask.CommentOnDecision,
                            ReasonNotApplicable = riskAppraisalMeetingTask.ReasonNotApplicable
                        },
                        Constituency = new ConstituencyTask()
                        {
                            Name = kpi.SchoolDetailsConstituency,
                            MPName = kpi.SchoolDetailsConstituencyMp,
                            Party = kpi.SchoolDetailsPoliticalParty,
                        }
                    }).FirstOrDefaultAsync();
            return result;
        }
    }
}