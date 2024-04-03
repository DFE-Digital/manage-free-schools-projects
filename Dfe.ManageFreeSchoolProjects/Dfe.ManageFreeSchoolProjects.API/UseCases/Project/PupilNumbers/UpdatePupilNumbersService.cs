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
        private readonly IEnumerable<IUpdatePupilNumbersSectionService> _updatePupilNumbersSectionServices;

        public UpdatePupilNumbersService(
            MfspContext context,
            IEnumerable<IUpdatePupilNumbersSectionService> updatePupilNumbersSectionServices)
        {
            _context = context;
            _updatePupilNumbersSectionServices = updatePupilNumbersSectionServices;
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

            foreach (var updatePupilNumbersSectionService in _updatePupilNumbersSectionServices)
            {
                updatePupilNumbersSectionService.Execute(po, request);
            }

            await _context.SaveChangesAsync();
        }
    }
}
