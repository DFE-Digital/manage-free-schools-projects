using System;
using ConcernsCaseWork.Service.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.RequestModels.Projects;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;

namespace Dfe.ManageFreeSchoolProjects.Services.Project
{
    public interface ICreateProjectService
    {
        public Task<CreateProjectResponse> Execute(CreateProjectRequest createProjectRequest);
    }

    public class CreateProjectService : ICreateProjectService
    {
        private readonly MfspApiClient _apiClient;

        public CreateProjectService(MfspApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<CreateProjectResponse> Execute(CreateProjectRequest createProjectRequest)
        {
            return await _apiClient.Post<CreateProjectRequest, CreateProjectResponse>($"/api/v1/client/projects/create/", createProjectRequest);
        }
    }
}

