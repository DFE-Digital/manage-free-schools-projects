using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.Exceptions;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Risk;
using Dfe.ManageFreeSchoolProjects.Data;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.ProjectOverview
{
    public interface IGetProjectOverviewService
    {
        public Task<ProjectOverviewResponse> Execute(string projectId);
    }

    public class GetProjectOverviewService : IGetProjectOverviewService
    {
        private readonly MfspContext _context;

        public GetProjectOverviewService(MfspContext context)
        {
            _context = context;
        }

        public async Task<ProjectOverviewResponse> Execute(string projectId)
        {
            var project = await _context.Kpi.FirstOrDefaultAsync(k => k.ProjectStatusProjectId == projectId);

            if (project == null) 
            {
                throw new NotFoundException($"Project {projectId} not found");
            }

            var risk = await GetRisk(project.Rid);

            return new ProjectOverviewResponse()
            {
                ProjectStatus = new ProjectStatusResponse()
                {
                    ProjectStatus = project.ProjectStatusProjectStatus,
                    CurrentFreeSchoolName = project.ProjectStatusCurrentFreeSchoolName,
                    FreeSchoolsApplicationNumber = project.ProjectStatusFreeSchoolsApplicationNumber,
                    ProjectId = project.ProjectStatusProjectId,
                    Urn = project.ProjectStatusUrnWhenGivenOne,
                    ApplicationWave = project.ProjectStatusFreeSchoolApplicationWave,
                    RealisticYearOfOpening = project.ProjectStatusRealisticYearOfOpening,
                    DateOfEntryIntoPreopening = project.ProjectStatusDateOfEntryIntoPreOpening,
                    ProvisionalOpeningDateAgreedWithTrust = project.ProjectStatusProvisionalOpeningDateAgreedWithTrust,
                    ActualOpeningDate = FormatDate(project.ProjectStatusActualOpeningDate),
                    OpeningAcademicYear = project.ProjectStatusTrustsPreferredYearOfOpening
                },
                SchoolDetails = new SchoolDetailsResponse()
                {
                    LocalAuthority = project.LocalAuthority,
                    Region = project.SchoolDetailsGeographicalRegion,
                    Constituency = project.SchoolDetailsConstituency,
                    ConstituencyMp = project.SchoolDetailsConstituencyMp,
                    NumberOfEntryForms = project.SchoolDetailsNumberOfFormsOfEntry,
                    SchoolType = project.SchoolDetailsSchoolTypeMainstreamApEtc,
                    SchoolPhase = project.SchoolDetailsSchoolPhasePrimarySecondary,
                    AgeRange = project.SchoolDetailsAgeRange,
                    Gender = project.SchoolDetailsGender,
                    Nursery = project.SchoolDetailsNursery,
                    SixthForm = project.SchoolDetailsSixthForm,
                    IndependentConverter = project.SchoolDetailsIndependentConverter,
                    SpecialistResourceProvision = project.SchoolDetailsSpecialistResourceProvision,
                    FaithStatus = project.SchoolDetailsFaithStatus,
                    FaithType = project.SchoolDetailsFaithType,
                    TrustId = project.SchoolDetailsTrustId,
                    TrustName = project.SchoolDetailsTrustName,
                    TrustType = project.SchoolDetailsTrustType
                },
                Risk = risk
            };
        }

        private static string FormatDate(DateTime? date)
        {
            if (!date.HasValue)
            {
                return null;
            }

            return date.Value.Date.ToString();
        }

        private async Task<ProjectRiskOverviewResponse> GetRisk(string rid)
        {
            var rag = await _context.Rag
                .Select(e => new 
                {
                    e.Rid,
                    RiskRating = e.RagRatingsOverallRagRating,
                    Summary = e.RagRatingsOverallRagSummary,
                    Date = EF.Property<DateTime>(e, "PeriodStart")
                }).FirstOrDefaultAsync(r => r.Rid == rid);

            if (rag == null) 
            {
                return new ProjectRiskOverviewResponse();
            }

            var result = new ProjectRiskOverviewResponse()
            {
                Date = rag.Date,
                RiskRating = ProjectRiskMapper.ToRiskRating(rag.RiskRating),
                Summary = rag.Summary
            };

            return result;
        }
    }
}
