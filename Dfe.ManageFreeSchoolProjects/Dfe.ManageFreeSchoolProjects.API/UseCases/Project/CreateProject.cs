using System.Text.Json.Serialization;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.RequestModels.Projects;
using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project
{

    public interface ICreateProjectService
	{
		Task<ProjectResponse> Execute(CreateProjectRequest createProjectRequest);
	}

    public class CreateProject : ICreateProjectService
	{
        private readonly MfspContext _context;

        public CreateProject(MfspContext context)
        {
            _context = context;
        }

        public async Task<ProjectResponse> Execute(CreateProjectRequest createProjectRequest)
        {
            var result = _context.Kpi.Add(new ()
            {
                ProjectStatusProjectId = createProjectRequest.ProjectId,
                ProjectStatusCurrentFreeSchoolName = createProjectRequest.SchoolName,
                ProjectStatusFreeSchoolApplicationWave = createProjectRequest.ApplicationWave,
                ProjectStatusFreeSchoolsApplicationNumber = createProjectRequest.ApplicationNumber,
            });

            await _context.SaveChangesAsync();

            return result;
        }
    }
}

