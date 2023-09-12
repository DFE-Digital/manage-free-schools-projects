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
                .FirstOrDefaultAsync(k => k.ProjectStatusCurrentFreeSchoolName == proj.SchoolName);

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
                    ProjectStatusFreeSchoolApplicationWave = Guid.NewGuid().ToString().Substring(0, 9),
                    ProjectStatusFreeSchoolsApplicationNumber = Guid.NewGuid().ToString().Substring(0, 9),
                    AprilIndicator = Guid.NewGuid().ToString().Substring(0, 9),
                    Wave = Guid.NewGuid().ToString().Substring(0, 15),
                    UpperStatus = Guid.NewGuid().ToString().Substring(0, 10),
                    FsType = Guid.NewGuid().ToString().Substring(0, 13),
                    FsType1 = Guid.NewGuid().ToString().Substring(0, 15),
                    MatUnitProjects = Guid.NewGuid().ToString().Substring(0, 31),
                    SponsorUnitProjects = Guid.NewGuid().ToString(),
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
            }

            await _context.SaveChangesAsync();

            return result;
        }

    }
}

