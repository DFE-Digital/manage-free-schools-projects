using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.PupilNumbers;
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
                    CapacityWhenFull = new CapacityWhenFull
                    {
                        Nursery = int.Parse(po.PupilNumbersAndCapacityNurseryUnder5s),
                        ReceptionToYear6 = int.Parse(po.PupilNumbersAndCapacityYrY6Capacity),
                        Year7ToYear11 = int.Parse(po.PupilNumbersAndCapacityY7Y11Capacity),
                        Year12ToYear14 = int.Parse(po.PupilNumbersAndCapacityY12Y14Post16Capacity),
                        SpecalistEducationNeeds = int.Parse(po.PupilNumbersAndCapacitySpecialistResourceProvisionSpecial),
                        AlternativeProvision = int.Parse(po.PupilNumbersAndCapacitySpecialistResourceProvisionAp)
                    }
                })
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
