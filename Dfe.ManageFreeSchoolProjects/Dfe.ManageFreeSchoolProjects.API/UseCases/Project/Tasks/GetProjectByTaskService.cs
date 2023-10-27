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
            var result = await
                (from kpi in _context.Kpi
                where kpi.ProjectStatusProjectId == projectId
                join kai in _context.Kai on kpi.Rid equals kai.Rid into kaiJoin
                from kai in kaiJoin.DefaultIfEmpty()
                join property in _context.Property on kpi.Rid equals property.Rid into propertyJoin
                from property in propertyJoin.DefaultIfEmpty()
                select new GetProjectByTaskResponse()
                {
                    School = new SchoolTask()
                    {
                        CurrentFreeSchoolName = kpi.ProjectStatusCurrentFreeSchoolName,
                        SchoolType = kpi.SchoolDetailsSchoolTypeMainstreamApEtc,
                        SchoolPhase = kpi.SchoolDetailsSchoolPhasePrimarySecondary,
                        AgeRange = kpi.SchoolDetailsAgeRange,
                        Nursery = kpi.SchoolDetailsNursery,
                        SixthForm = kpi.SchoolDetailsSixthForm,
                        CompanyName = kai.ApplicationDetailsCompanyName,
                        NumberOfCompanyMembers = kai.ApplicationDetailsNumberOfCompanyMembers,
                        ProposedChairOfTrustees = kai.ApplicationDetailsProposedChairOfTrustees
                    },
                    Dates = new DatesTask()
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
