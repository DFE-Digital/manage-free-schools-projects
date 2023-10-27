using System.Text.Json.Serialization;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.RequestModels.Projects;
using Dfe.ManageFreeSchoolProjects.Data;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using Microsoft.EntityFrameworkCore;
using System;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.Data.Entities;

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
                    LocalAuthority = proj.LocalAuthority,
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

        private List<Data.Entities.Existing.Tasks> CreateTasks(string kpiRid)
        {
            return new List<Data.Entities.Existing.Tasks>()
            {
                new() { Rid = kpiRid, TaskName = TaskName.School, Status = Status.NotStarted  },
                new() { Rid = kpiRid, TaskName = TaskName.Dates, Status = Status.NotStarted },
                new() { Rid = kpiRid, TaskName = TaskName.RiskAppraisal, Status = Status.NotStarted },
            };
        }
    }
}