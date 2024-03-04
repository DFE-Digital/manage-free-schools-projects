using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.AdmissionsArrangements
{
    public class GetAdmissionsArrangementsTaskService : IGetTaskService
    {
        private readonly MfspContext _context;

        public GetAdmissionsArrangementsTaskService(MfspContext context)
        {
            _context = context;
        }

        public Task<GetProjectByTaskResponse> Get(GetTaskServiceParameters parameters)
        {
            return Task.FromResult(new GetProjectByTaskResponse() { AdmissionsArrangements = new() });
        }
    }
}
