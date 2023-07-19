using Dfe.ManageFreeSchoolProjects.API.Contracts.RequestModels.Projects;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels.Project;
using Dfe.ManageFreeSchoolProjects.API.Factories.Projects;
using Dfe.ManageFreeSchoolProjects.API.UseCases;
using Dfe.ManageFreeSchoolProjects.Data.Gateways;
using Dfe.ManageFreeSchoolProjects.Data.Gateways.Projects;

namespace ConcernsCaseWork.API.UseCases.Project
{
    public class GetAllProjects : IUseCase<GetAllProjectsRequest, ProjectResponse[]>
	{
		private readonly IProjectGateway _gateway;

		public GetAllProjects(IProjectGateway gateway)
		{
			_gateway = gateway;
		}

		public ProjectResponse[] Execute(GetAllProjectsRequest request)
		{
			return _gateway.GetProjectsByUser(request.User)
						   .Select(x => ProjectFactory.CreateResponse(x))
						   .ToArray();
		}

	}
}
