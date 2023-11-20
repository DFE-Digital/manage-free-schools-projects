using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.RequestModels.Projects;
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
            CreateProjectResponse result = new CreateProjectResponse();
            List<Kpi> checkedProjects = new List<Kpi>();

            bool duplicatesFound = false;

            foreach (ProjectDetails proj in createProjectRequest.Projects)
            {
                var existingProject = await _context.Kpi
                    .FirstOrDefaultAsync(k => k.ProjectStatusProjectId == proj.ProjectId);

                ProjectCreateState projectCreateState = ProjectCreateState.New;

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

                var trust = await GetTrust(proj.TRN);

                checkedProjects.Add(new Kpi()
                {
                    Rid = Guid.NewGuid().ToString().Substring(0, 10),
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
                    TrustId = trust.TrustRef,
                    TrustName = trust.TrustsTrustName,
                    TrustType = trust.TrustsTrustType,
                    SchoolDetailsTrustId = trust.TrustsTrustRef,
                    SchoolDetailsTrustName = trust.TrustsTrustName,
                    SchoolDetailsTrustType = trust.TrustsTrustType,
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
            }
            
            await _context.SaveChangesAsync();

            return result;
        }

        private async Task<Trust> GetTrust(string trustRef)
        {
            var result = await _context.Trust.FirstOrDefaultAsync(e => e.TrustRef == trustRef);

            return result;
        }

        private List<Data.Entities.Existing.Tasks> CreateTasks(string kpiRid)
        {
            return new List<Data.Entities.Existing.Tasks>()
            {
                new() { Rid = kpiRid, TaskName = TaskName.School, Status = Status.NotStarted  },
                new() { Rid = kpiRid, TaskName = TaskName.Dates, Status = Status.NotStarted },
                new() { Rid = kpiRid, TaskName = TaskName.RiskAppraisal, Status = Status.NotStarted },
                new() { Rid = kpiRid, TaskName = TaskName.Trust, Status = Status.NotStarted },
                new() { Rid = kpiRid, TaskName = TaskName.Constituency, Status = Status.NotStarted },
            };
        }
    }
}