using Dfe.ManageFreeSchoolProjects.Data.Models.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Dfe.ManageFreeSchoolProjects.Data.Gateways.Projects
{
    public interface IProjectGateway
	{
		Task<Project> CreateProject(Project request);
		Project[] GetProjectsByUser(string user);
        Project GetProjectById(string projectId);
        Task<Project> DeleteProject(Project request);
        Task<Project> EditProject(Project request);
    }

	public class ProjectGateway : IProjectGateway
	{
		private readonly ILogger<ProjectGateway> _logger;
		private readonly ProjectsDbContext _ManageFreeSchoolProjectsDbContext;

		public ProjectGateway(ILogger<ProjectGateway> logger, ProjectsDbContext ManageFreeSchoolProjectsDbContext)
		{
			_logger = logger;
			_ManageFreeSchoolProjectsDbContext= ManageFreeSchoolProjectsDbContext;
		}

        public Project[] GetProjectsByUser(string user)
        {
            try
            {
				var projects = _ManageFreeSchoolProjectsDbContext.Projects.Where(x => x.CreatedBy == user).ToArray();
                return projects;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Failed to create Project with Id {Id}, {ex}", user, ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("An application exception has occurred whilst creating Project with Id {Id}, {ex}", user, ex);
                throw;
            }
        }

        public Project GetProjectById(string projectId)
        {
            try
            {
                var project = _ManageFreeSchoolProjectsDbContext.Projects.Where(x => x.ProjectId == projectId).ToArray();
                return project.First();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Failed to get Project with Id {Id}, {ex}", projectId, ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("An application exception has occurred whilst creating Project with Id {Id}, {ex}", projectId, ex);
                throw;
            }
        }

        public async Task<Project> CreateProject(Project request)
		{
			try
			{
				request.UpdatedAt = request.CreatedAt;
				_ManageFreeSchoolProjectsDbContext.Projects.Add(request);
				await _ManageFreeSchoolProjectsDbContext.SaveChangesAsync();
				return request;
			}
			catch (DbUpdateException ex)
			{
				_logger.LogError("Failed to create Project with Id {Id}, {ex}", request.Id, ex);
				throw;
			}
			catch (Exception ex)
			{
				_logger.LogError("An application exception has occurred whilst creating Project with Id {Id}, {ex}", request.Id, ex);
				throw;
			}
		}

        public async Task<Project> DeleteProject(Project request)
        {
            try
            {
                request.UpdatedAt = request.CreatedAt;
                _ManageFreeSchoolProjectsDbContext.Projects.Where(p => p.ProjectId == request.ProjectId).ExecuteDelete();
                await _ManageFreeSchoolProjectsDbContext.SaveChangesAsync();
                return request;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Failed to delete Project with Id {Id}, {ex}", request.Id, ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("An application exception has occurred whilst deleting Project with Id {Id}, {ex}", request.Id, ex);
                throw;
            }
        }

        public async Task<Project> EditProject(Project request)
        {
            try
            {
                request.UpdatedAt = request.CreatedAt;

                Project project = _ManageFreeSchoolProjectsDbContext.Projects.Where(p => p.ProjectId == request.ProjectId).SingleOrDefault();
                project.SchoolName = request.SchoolName;
                project.ApplicationNumber = request.ApplicationNumber;
                project.ApplicationWave = request.ApplicationWave;
                await _ManageFreeSchoolProjectsDbContext.SaveChangesAsync();
                return request;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Failed to edit Project with Id {Id}, {ex}", request.Id, ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("An application exception has occurred whilst editing Project with Id {Id}, {ex}", request.Id, ex);
                throw;
            }
        }
    }
}
