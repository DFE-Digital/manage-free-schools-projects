﻿using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Services.Project
{
    public interface IGetProjectTaskListSummaryService
    {
        public Task<ProjectByTaskSummaryResponse> Execute(string projectId);
    }

    public class GetProjectTaskListSummaryService : IGetProjectTaskListSummaryService
    {
        private readonly MfspApiClient _apiClient;

        public GetProjectTaskListSummaryService(MfspApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<ProjectByTaskSummaryResponse> Execute(string projectId)
        {
            var endpoint = $"/api/v1/client/projects/{projectId}/tasks/list/summary";

            var result = await _apiClient.Get<ApiSingleResponseV2<ProjectByTaskSummaryResponse>>(endpoint);

            return result.Data;
        }
    }
}
