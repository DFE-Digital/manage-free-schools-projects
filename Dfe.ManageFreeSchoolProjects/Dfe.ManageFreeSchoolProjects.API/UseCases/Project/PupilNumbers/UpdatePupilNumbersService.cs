using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.PupilNumbers;
using Dfe.ManageFreeSchoolProjects.Data;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.PupilNumbers
{
    public interface IUpdatePupilNumbersService
    {
        public Task Execute(string projectId, UpdatePupilNumbersRequest request);
    }

    public class UpdatePupilNumbersService : IUpdatePupilNumbersService
    {
        private readonly MfspContext _context;

        public UpdatePupilNumbersService(MfspContext context)
        {
            _context = context;
        }

        public async Task Execute(string projectId, UpdatePupilNumbersRequest request)
        {
            var kpi = await _context.Kpi.FirstOrDefaultAsync(kpi => kpi.ProjectStatusProjectId == projectId);

            var po = await _context.Po.FirstOrDefaultAsync(po => po.Rid == kpi.Rid);

            if (po == null)
            {
                po = new Po()
                {
                    Rid = kpi.Rid,
                };

                _context.Po.Add(po);
            }

            po.PupilNumbersAndCapacityNurseryUnder5s = request.CapacityWhenFull.Nursery.ToString();
            po.PupilNumbersAndCapacityYrY6Capacity = request.CapacityWhenFull.ReceptionToYear6.ToString();
            po.PupilNumbersAndCapacityY7Y11Capacity = request.CapacityWhenFull.Year7ToYear11.ToString();
            po.PupilNumbersAndCapacityY12Y14Post16Capacity = request.CapacityWhenFull.Year12ToYear14.ToString();
            po.PupilNumbersAndCapacitySpecialistResourceProvisionSpecial = request.CapacityWhenFull.SpecalistEducationNeeds.ToString();
            po.PupilNumbersAndCapacitySpecialistResourceProvisionAp = request.CapacityWhenFull.AlternativeProvision.ToString();

            await _context.SaveChangesAsync();
        }
    }
}
