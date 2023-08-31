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
        public Task<List<CreateProjectResponse>> Execute(List<CreateProjectRequest> createProjectsRequest);
    }

    public class CreateProjectService : ICreateProjectService
    {
        private readonly MfspApiClient _apiClient;

        public CreateProjectService(MfspApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<List<CreateProjectResponse>> Execute(List<CreateProjectRequest> createProjectsRequest)
        {
            return await _apiClient.Post<List<CreateProjectRequest>, List<CreateProjectResponse>>($"/api/v1/client/project/create/", createProjectsRequest);
        }
    }
}

