using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.PupilNumbers;
using Dfe.ManageFreeSchoolProjects.API.Extensions;
using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.PupilNumbers
{
    public interface IGetPupilNumbersService
    {
        public Task<GetPupilNumbersResponse> Execute(string projectId);
    }

    public class GetPupilNumbersService : IGetPupilNumbersService
    {
        private readonly MfspContext _context;

        public GetPupilNumbersService(MfspContext context)
        {
            _context = context;
        }

        public async Task<GetPupilNumbersResponse> Execute(string projectId)
        {
            var kpi = await _context.Kpi.FirstOrDefaultAsync(kpi => kpi.ProjectStatusProjectId == projectId);

            var result = await _context.Po
                .Where(po => po.Rid == kpi.Rid)
                .Select(po => new GetPupilNumbersResponse
                {
                    CapacityWhenFull = new CapacityWhenFullResponse
                    {
                        Nursery = po.PupilNumbersAndCapacityNurseryUnder5s.ToInt(),
                        ReceptionToYear6 = po.PupilNumbersAndCapacityYrY6Capacity.ToInt(),
                        Year7ToYear11 = po.PupilNumbersAndCapacityY7Y11Capacity.ToInt(),
                        Year12ToYear14 = po.PupilNumbersAndCapacityY12Y14Post16Capacity.ToInt(),
                        SpecialistEducationNeeds = po.PupilNumbersAndCapacitySpecialistResourceProvisionSpecial.ToInt(),
                        AlternativeProvision = po.PupilNumbersAndCapacitySpecialistResourceProvisionAp.ToInt(),
                        TotalCapacity = po.PupilNumbersAndCapacityTotalOfCapacityTotals.ToInt()
                    }
                })
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
