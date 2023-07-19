using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Dfe.ManageFreeSchoolProjects.Data.Models;
using Dfe.ManageFreeSchoolProjects.Data.Models.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Data.Gateways.Projects
{
	public interface IProjectGateway
	{
		Task<Project> CreateProject(Project request);
		Project[] GetProjectsByUser(string user);

    }

	public class ProjectGateway : IProjectGateway
	{
		private readonly ILogger<ProjectGateway> _logger;
		private readonly ManageFreeSchoolProjectsDbContext _ManageFreeSchoolProjectsDbContext;

		public ProjectGateway(ILogger<ProjectGateway> logger, ManageFreeSchoolProjectsDbContext ManageFreeSchoolProjectsDbContext)
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
	}
}
