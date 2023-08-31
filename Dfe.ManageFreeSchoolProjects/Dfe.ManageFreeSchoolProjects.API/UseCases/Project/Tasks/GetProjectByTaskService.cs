using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks
{
    public interface IGetProjectByTaskService
    {
        public Task<GetProjectByTaskResponse> Execute(string projectId);
    }

    public class GetProjectByTaskService : IGetProjectByTaskService
    {
        private readonly MfspContext _context;

        public GetProjectByTaskService(MfspContext context)
        {
            _context = context;
        }

        public async Task<GetProjectByTaskResponse> Execute(string projectId)
        {
            var result = await _context.Kpi
                .Where(e => e.ProjectStatusProjectId == projectId)
                .Select((row) =>
                new GetProjectByTaskResponse()
                {
                    School = new SchoolTask()
                    {
                        SchoolType = row.SchoolDetailsSchoolTypeMainstreamApEtc,
                        SchoolPhase = row.SchoolDetailsSchoolPhasePrimarySecondary,
                        AgeRange = row.SchoolDetailsAgeRange,
                        Nursery = row.SchoolDetailsNursery,
                        SixthForm = row.SchoolDetailsSixthForm
                    }
                })
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
