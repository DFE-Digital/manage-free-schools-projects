using Dfe.ManageFreeSchoolProjects.API.Contracts.RequestModels.Projects;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels.Project;
using Dfe.ManageFreeSchoolProjects.API.Factories.Projects;
using Dfe.ManageFreeSchoolProjects.API.UseCases;
using Dfe.ManageFreeSchoolProjects.Data.Gateways;
using Dfe.ManageFreeSchoolProjects.Data.Gateways.Projects;

namespace ConcernsCaseWork.API.UseCases.Project
{
    public class CreateProject : IUseCase<CreateProjectRequest, ProjectResponse>
	{
		private readonly IProjectGateway _gateway;

		public CreateProject(IProjectGateway gateway)
		{
			_gateway = gateway;
		}

		public ProjectResponse Execute(CreateProjectRequest request)
		{
			return ExecuteAsync(request).Result;
		}

		public async Task<ProjectResponse> ExecuteAsync(CreateProjectRequest request)
		{
			var dbModel = ProjectFactory.CreateDBModel(request);
			var createdProjects = await _gateway.CreateProject(dbModel);

			return ProjectFactory.CreateResponse(createdProjects);
		}
	}
}
