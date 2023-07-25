using Dfe.ManageFreeSchoolProjects.API.Contracts.RequestModels.Projects;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels.Project;
using Dfe.ManageFreeSchoolProjects.API.Factories.Projects;
using Dfe.ManageFreeSchoolProjects.API.UseCases;
using Dfe.ManageFreeSchoolProjects.Data.Gateways;
using Dfe.ManageFreeSchoolProjects.Data.Gateways.Projects;

namespace ConcernsCaseWork.API.UseCases.Project
{
	public interface IGetProjectByIdCase : IUseCase<GetProjectRequest, ProjectResponse> { }

    public class GetProjectById : IGetProjectByIdCase
	{
		private readonly IProjectGateway _gateway;

		public GetProjectById(IProjectGateway gateway)
		{
			_gateway = gateway;
		}

		public ProjectResponse Execute(GetProjectRequest request)
		{

            var project = _gateway.GetProjectById(request.ProjectId);

            return ProjectFactory.CreateResponse(project);

		}

	}
}
