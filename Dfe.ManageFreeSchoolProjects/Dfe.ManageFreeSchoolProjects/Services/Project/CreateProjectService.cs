using System;
using ConcernsCaseWork.Service.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.RequestModels.Projects;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Dashboard;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Users;

namespace Dfe.ManageFreeSchoolProjects.Services.Project
{
    public interface ICreateProjectService
    {
        public Task Execute(List<CreateProjectRequest> createProjectsRequest);
    }

    public class CreateProjectService : ICreateProjectService
    {
        private readonly MfspApiClient _apiClient;

        public CreateProjectService(MfspApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task Execute(List<CreateProjectRequest> createProjectsRequest)
        {
            await _apiClient.Post<List<CreateProjectRequest>, object>($"/api/v1/client/project/create/", createProjectsRequest);
        }
    }
}

