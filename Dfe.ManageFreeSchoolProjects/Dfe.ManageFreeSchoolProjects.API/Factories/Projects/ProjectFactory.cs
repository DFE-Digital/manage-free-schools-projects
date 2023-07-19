using Dfe.ManageFreeSchoolProjects.API.Contracts.RequestModels.Projects;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels.Project;
using Dfe.ManageFreeSchoolProjects.Data.Enums;
using Dfe.ManageFreeSchoolProjects.Data.Models;
using Dfe.ManageFreeSchoolProjects.Data.Models.Projects;

namespace Dfe.ManageFreeSchoolProjects.API.Factories.Projects
{
    public class ProjectFactory
	{
		public static Project CreateDBModel(CreateProjectRequest createProjectRequest)
		{
			return new Project
			{
				ProjectId = createProjectRequest.ProjectId,
				SchoolName = createProjectRequest.SchoolName,
                ApplicationNumber = createProjectRequest.ApplicationNumber,
                ApplicationWave = createProjectRequest.ApplicationWave,
                CreatedAt = DateTime.Now,
				CreatedBy = createProjectRequest.CreatedBy			
			};
		}      

        public static ProjectResponse CreateResponse(Project model)
		{
			return new ProjectResponse
			{
				Id = model.Id,
				ProjectId = model.ProjectId,
				SchoolName= model.SchoolName,
				ApplicationNumber = model.ApplicationNumber,
				ApplicationWave = model.ApplicationWave,
				CreatedAt = model.CreatedAt,
				CreatedBy = model.CreatedBy,
				UpdatedAt= model.UpdatedAt,
			};
		}
	}
}
