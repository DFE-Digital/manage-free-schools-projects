using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.RequestModels.Projects;
using Dfe.ManageFreeSchoolProjects.API.Extensions;
using Dfe.ManageFreeSchoolProjects.Data;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project
{
    public interface ICreateProjectService
    {
        Task<CreateProjectResponse> Execute(CreateProjectRequest createProjectRequest);
    }

    public class CreateProject : ICreateProjectService
    {
        private readonly MfspContext _context;

        public CreateProject(MfspContext context)
        {
            _context = context;
        }

        public async Task<CreateProjectResponse> Execute(CreateProjectRequest createProjectRequest)
        {
            var result = new CreateProjectResponse();
            var checkedProjects = new List<Kpi>();
            var checkedProjectsPO = new List<Po>();

            var duplicatesFound = false;

            foreach (ProjectDetails proj in createProjectRequest.Projects)
            {
                var existingProject = await _context.Kpi
                    .FirstOrDefaultAsync(k => k.ProjectStatusProjectId == proj.ProjectId);

                var projectCreateState = ProjectCreateState.New;

                if (existingProject != null)
                {
                    duplicatesFound = true;
                    projectCreateState = ProjectCreateState.Exists;
                }

                result.Projects.Add(new ProjectResponseDetails
                {
                    ProjectId = proj.ProjectId,
                    ProjectCreateState = projectCreateState
                });

                var rid = Guid.NewGuid().ToString().Substring(0, 10);
                var trust = await GetTrust(proj.TRN);

                var kpi = new Kpi
                {
                    Rid = rid,
                    ProjectStatusProjectId = proj.ProjectId,
                    ProjectStatusCurrentFreeSchoolName = proj.SchoolName,
                    ProjectStatusFreeSchoolApplicationWave = "",
                    ProjectStatusFreeSchoolsApplicationNumber = "",
                    AprilIndicator = "",
                    Wave = "",
                    UpperStatus = "",
                    FsType = "",
                    FsType1 = "",
                    MatUnitProjects = "",
                    SponsorUnitProjects = "",
                    SchoolDetailsGeographicalRegion = proj.Region,
                    SchoolDetailsLocalAuthority = proj.LocalAuthorityCode,
                    LocalAuthority = proj.LocalAuthority,
                    SchoolDetailsSchoolTypeMainstreamApEtc = ProjectMapper.ToSchoolType(proj.SchoolType),
                    SchoolDetailsSchoolPhasePrimarySecondary = ProjectMapper.ToSchoolPhase(proj.SchoolPhase),
                    TrustId = trust.TrustRef,
                    TrustName = trust.TrustsTrustName,
                    TrustType = trust.TrustsTrustType,
                    SchoolDetailsTrustId = trust.TrustsTrustRef,
                    SchoolDetailsTrustName = trust.TrustsTrustName,
                    SchoolDetailsTrustType = trust.TrustsTrustType,
                    SchoolDetailsSixthForm = proj.SixthForm.ToString(),
                    SchoolDetailsNursery = proj.Nursery.ToString(),
                    SchoolDetailsFaithStatus = proj.FaithStatus.ToString(),
                    SchoolDetailsFaithType = proj.FaithType.ToDescription(),
                    SchoolDetailsPleaseSpecifyOtherFaithType = proj.OtherFaithType,
                    SchoolDetailsNumberOfFormsOfEntry = proj.FormsOfEntry
                };

                checkedProjects.Add(kpi);

                checkedProjectsPO.Add(new Po()
                {
                    Rid = rid,
                    PupilNumbersAndCapacityYrY6Capacity = proj.YRY6Capacity.ToString(),
                    PupilNumbersAndCapacityY7Y11Capacity = proj.Y7Y11Capacity.ToString(),
                    PupilNumbersAndCapacityYrY11Pre16Capacity = (proj.YRY6Capacity + proj.Y7Y11Capacity).ToString(),
                    PupilNumbersAndCapacityY12Y14Post16Capacity = proj.Y12Y14Capacity.ToString(),
                    PupilNumbersAndCapacityTotalOfCapacityTotals = (proj.YRY6Capacity + proj.Y7Y11Capacity + proj.Y12Y14Capacity).ToString()
                });
            }

            if (duplicatesFound)
            {
                return result;
            }

            foreach (Kpi proj in checkedProjects)
            {
                _context.Add(proj);
                _context.AddRange(CreateTasks(proj.Rid));
                _context.Add(new Data.Entities.RiskAppraisalMeetingTask() { RID = proj.Rid });

                var po = checkedProjectsPO.Find(p => p.Rid == proj.Rid);
                _context.Add(po);
            }

            await _context.SaveChangesAsync();

            return result;
        }

        private async Task<Trust> GetTrust(string trustRef)
        {
            var result = await _context.Trust.FirstOrDefaultAsync(e => e.TrustRef == trustRef);
            return result;
        }

        private static IEnumerable<Data.Entities.Existing.Tasks> CreateTasks(string kpiRid)
        {
            yield return new() { Rid = kpiRid, TaskName = TaskName.School, Status = Status.NotStarted };
            yield return new() { Rid = kpiRid, TaskName = TaskName.Dates, Status = Status.NotStarted };
            yield return new() { Rid = kpiRid, TaskName = TaskName.Trust, Status = Status.NotStarted };
            yield return new() { Rid = kpiRid, TaskName = TaskName.RiskAppraisalMeeting, Status = Status.NotStarted };
            yield return new() { Rid = kpiRid, TaskName = TaskName.Constituency, Status = Status.NotStarted };
        }
    }
}