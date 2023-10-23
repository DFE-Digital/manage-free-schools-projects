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
                join trust in _context.Trust on kpi.Rid equals trust.Rid into trustJoin
                from trust in trustJoin.DefaultIfEmpty()
                join construction in _context.Construction on kpi.Rid equals construction.Rid into constructionJoin
                from construction in constructionJoin.DefaultIfEmpty()
                select new GetProjectByTaskResponse()
                {
                    School = new SchoolTask()
                    {
                        CurrentFreeSchoolName = kpi.ProjectStatusCurrentFreeSchoolName,
                        SchoolType = kpi.SchoolDetailsSchoolTypeMainstreamApEtc,
                        // SchoolPhase = kpi.SchoolDetailsSchoolPhasePrimarySecondary,
                        AgeRange = kpi.SchoolDetailsAgeRange,
                        Nursery = kpi.SchoolDetailsNursery,
                        SixthForm = kpi.SchoolDetailsSixthForm,
                    },
                    Construction = new ConstructionTask()
                    {
                        NameOfSite = property.SiteNameOfSite,
                        AddressOfSite = property.SiteAddressOfSite,
                        PostcodeOfSite = property.SitePostcodeOfSite,
                        BuildingType = property.SiteBuildingType,
                        TrustRef = trust.TrustRef,
                        TrustLeadSponsor = trust.LeadSponsor,
                        TrustName = trust.TrustsTrustName,
                        SiteMinArea = construction.SiteDetailsAreaOfNewBuildM2,
                        TypeofWorksLocation = construction.SiteDetailsTypeOfWorks,
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
