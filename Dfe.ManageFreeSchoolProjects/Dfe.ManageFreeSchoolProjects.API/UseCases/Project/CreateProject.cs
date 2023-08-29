using System.Text.Json.Serialization;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.RequestModels.Projects;
using Dfe.ManageFreeSchoolProjects.Data;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using Microsoft.EntityFrameworkCore;
using System;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project
{

    public interface ICreateProjectService
	{
		CreateProjectRequest Execute(CreateProjectRequest createProjectRequest);
	}

    public class CreateProject : ICreateProjectService
	{
        private readonly MfspContext _context;

        public CreateProject(MfspContext context)
        {
            _context = context;
        }

        public CreateProjectRequest Execute(CreateProjectRequest createProjectRequest)
        {
            var project = new Kpi()
            {
                Rid = Guid.NewGuid().ToString().Substring(0, 10),
                ProjectStatusProjectId = createProjectRequest.ProjectId,
                ProjectStatusCurrentFreeSchoolName = createProjectRequest.SchoolName,
                ProjectStatusFreeSchoolApplicationWave = createProjectRequest.ApplicationWave,
                ProjectStatusFreeSchoolsApplicationNumber = createProjectRequest.ApplicationNumber,
                AprilIndicator = Guid.NewGuid().ToString().Substring(0, 9),
                Wave = Guid.NewGuid().ToString().Substring(0, 15),
                UpperStatus = Guid.NewGuid().ToString().Substring(0, 10),
                FsType = Guid.NewGuid().ToString().Substring(0, 13),
                FsType1 = Guid.NewGuid().ToString().Substring(0, 15),
                MatUnitProjects = Guid.NewGuid().ToString().Substring(0, 31),
                SponsorUnitProjects = Guid.NewGuid().ToString()

        };

            _context.Kpi.Add(project);

            _context.SaveChanges();

            return createProjectRequest;
        }
    }
}

