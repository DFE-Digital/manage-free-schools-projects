using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.Data;
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
                return null;
            }

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
                    DateOfEntryIntoPreopening = FormatDate(project.ProjectStatusDateOfEntryIntoPreOpening),
                    ProvisionalOpeningDateAgreedWithTrust = FormatDate(project.ProjectStatusProvisionalOpeningDateAgreedWithTrust),
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
                }
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
    }
}
