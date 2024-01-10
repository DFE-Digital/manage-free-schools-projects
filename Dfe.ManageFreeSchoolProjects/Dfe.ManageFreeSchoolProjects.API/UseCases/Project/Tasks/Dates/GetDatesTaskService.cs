using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.Dates
{
    public class GetDatesTaskService : IGetTaskService
    {
        public async Task<GetProjectByTaskResponse> Get(GetTaskServiceParameters parameters)
        {
            var query = parameters.BaseQuery;

            var result = await query.Select(kpi => new GetProjectByTaskResponse()
            {
                Dates =
                {
                    DateOfEntryIntoPreopening = kpi.ProjectStatusDateOfEntryIntoPreOpening,
                    ProvisionalOpeningDateAgreedWithTrust = kpi.ProjectStatusProvisionalOpeningDateAgreedWithTrust,
                    RealisticYearOfOpening = kpi.ProjectStatusRealisticYearOfOpening,
                }
            }).FirstOrDefaultAsync();

            return result;
        }
    }
}
