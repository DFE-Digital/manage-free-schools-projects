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

            public static Project EditDBModel(EditProjectRequest editProjectRequest)
            {
                return new Project
                {
                    ProjectId = editProjectRequest.ProjectId,
                    SchoolName = editProjectRequest.SchoolName,
                    ApplicationNumber = editProjectRequest.ApplicationNumber,
                    ApplicationWave = editProjectRequest.ApplicationWave,
                    CreatedAt = DateTime.Now,
                    CreatedBy = editProjectRequest.CreatedBy
                };
            }
        
            public static ProjectResponse EditResponse(Project model)
            {
                return new ProjectResponse
                {
                    Id = model.Id,
                    ProjectId = model.ProjectId,
                    SchoolName = model.SchoolName,
                    ApplicationNumber = model.ApplicationNumber,
                    ApplicationWave = model.ApplicationWave,
                    CreatedAt = model.CreatedAt,
                    CreatedBy = model.CreatedBy,
                    UpdatedAt = model.UpdatedAt,
                };
            }

        public static Project DeleteDBModel(DeleteProjectRequest deleteProjectRequest)
        {
            return new Project
            {
                ProjectId = deleteProjectRequest.ProjectId,
            };
        }

        public static ProjectResponse DeleteResponse(Project model)
        {
            return new ProjectResponse
            {
                Id = model.Id,
                ProjectId = model.ProjectId,
                SchoolName = model.SchoolName,
                ApplicationNumber = model.ApplicationNumber,
                ApplicationWave = model.ApplicationWave,
                CreatedAt = model.CreatedAt,
                CreatedBy = model.CreatedBy,
                UpdatedAt = model.UpdatedAt,
            };
        }
    }
}
