using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project
{
    public interface IGetProjectService
    {
        public Task<GetProjectResponse> Execute(string projectId);
    }

    public class GetProjectService : IGetProjectService
    {
        private readonly MfspContext _context;

        public GetProjectService(MfspContext context)
        {
            _context = context;
        }

        public async Task<GetProjectResponse> Execute(string projectId)
        {
            var kpi = await _context.Kpi.FirstOrDefaultAsync(e => e.ProjectStatusProjectId == projectId);
            var kai = await _context.Kai.FirstOrDefaultAsync(e => e.Rid == kpi.Rid);
            var property = await _context.Property.FirstOrDefaultAsync(e => e.Rid == kpi.Rid);
            var trust = await _context.Trust.FirstOrDefaultAsync(e => e.Rid == kpi.Rid);
            var construction = await _context.Construction.FirstOrDefaultAsync(e => e.Rid == kpi.Rid);

            var result = new GetProjectResponse()
            {
                SchoolType = kpi.SchoolDetailsSchoolTypeMainstreamApEtc,
                SchoolPhase = kpi.SchoolDetailsSchoolPhasePrimarySecondary,
                AgeRange = kpi.SchoolDetailsAgeRange,
                Nursery = kpi.SchoolDetailsNursery,
                SixthForm = kpi.SchoolDetailsSixthForm,
                CompanyName = kai?.ApplicationDetailsCompanyName,
                NumberOfCompanyMembers = kai?.ApplicationDetailsNumberOfCompanyMembers,
                ProposedChairOfTrustees = kai?.ApplicationDetailsProposedChairOfTrustees,
                NameOfSite = property?.SiteNameOfSite,
                AddressOfSite = property?.SiteAddressOfSite,
                PostcodeOfSite = property?.SitePostcodeOfSite,
                BuildingType = property.SiteBuildingType,
                TrustRef = trust?.TrustRef,
                TrustLeadSponsor = trust?.LeadSponsor,
                TrustName = trust?.TrustsTrustName,
                SiteMinArea = construction?.SiteDetailsAreaOfNewBuildM2,
                TypeofWorksLocation = construction?.SiteDetailsTypeOfWorks,
            };

            return result;
        }
    }
}
