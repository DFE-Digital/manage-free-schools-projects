using Dfe.ManageFreeSchoolProjects.API.Contracts.RequestModels.Projects;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels.Project;
using Dfe.ManageFreeSchoolProjects.API.Factories.Projects;
using Dfe.ManageFreeSchoolProjects.API.UseCases;
using Dfe.ManageFreeSchoolProjects.Data.Gateways;
using Dfe.ManageFreeSchoolProjects.Data.Gateways.Projects;

namespace ConcernsCaseWork.API.UseCases.Project
{
    public interface IEditProjectCase : IUseCase<EditProjectRequest, ProjectResponse> { }

    public class EditProject : IEditProjectCase
    {
        private readonly IProjectGateway _gateway;

        public EditProject(IProjectGateway gateway)
        {
            _gateway = gateway;
        }

        public ProjectResponse Execute(EditProjectRequest request)
        {
            return ExecuteAsync(request).Result;
        }

        public async Task<ProjectResponse> ExecuteAsync(EditProjectRequest request)
        {
            var dbModel = ProjectFactory.EditDBModel(request);
            var editedprojects = await _gateway.EditProject(dbModel);

            return ProjectFactory.CreateResponse(editedprojects);
        }
    }
}
