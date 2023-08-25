using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.Exceptions;
using Dfe.ManageFreeSchoolProjects.Data;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.ProjectTask
{
    public interface IUpdateProjectTaskService
    {
        public Task Execute(string projectId, UpdateProjectTasksRequest request);
    }

    public class UpdateProjectTaskService : IUpdateProjectTaskService
    {
        public readonly MfspContext _context;

        public UpdateProjectTaskService(MfspContext context)
        {
            _context = context;
        }

        public async Task Execute(string projectId, UpdateProjectTasksRequest request)
        {
            var dbKpi = await _context.Kpi.FirstOrDefaultAsync(p => p.ProjectStatusProjectId == projectId);

            if (dbKpi == null) 
            {
                throw new NotFoundException($"Project {projectId} not found");
            }

            var dbKai = new Kai()
            {
                Rid = dbKpi.Rid,
            };

            var dbProperty = new Property()
            {
                Rid = dbKpi.Rid,
                Tos = "123"
            };

            var dbTrust = new Trust()
            {
                Rid = dbKpi.Rid
            };

            var dbConstruction = new Construction()
            {
                Rid = dbKpi.Rid
            };

            _context.Kai.Add(dbKai);
            _context.Property.Add(dbProperty);
            _context.Trust.Add(dbTrust);
            _context.Construction.Add(dbConstruction);

            // Updates here
            ApplySchoolTaskUpdates(request.Tasks.School, dbKpi, dbKai);
            ApplyConstructionTaskUpdates(request.Tasks.Construction, dbProperty, dbTrust, dbConstruction);

            await _context.SaveChangesAsync();
        }

        private void ApplySchoolTaskUpdates(
            SchoolTaskRequest task, 
            Kpi dbKpi, 
            Kai dbKai)
        {
            if (task == null)
            {
                return;
            }

            dbKpi.SchoolDetailsSchoolTypeMainstreamApEtc = task.SchoolType;
            dbKpi.SchoolDetailsSchoolPhasePrimarySecondary = task.SchoolPhase;
            dbKpi.SchoolDetailsAgeRange = task.AgeRange = task.AgeRange;
            dbKpi.SchoolDetailsNursery = task.Nursery;
            dbKpi.SchoolDetailsSixthForm = task.SixthForm;

            dbKai.ApplicationDetailsCompanyName = task.CompanyName;
            dbKai.ApplicationDetailsNumberOfCompanyMembers = task.NumberOfCompanyMembers;
            dbKai.ApplicationDetailsProposedChairOfTrustees = task.ProposedChairOfTrustees;  
        }

        private void ApplyConstructionTaskUpdates(
            ConstructionTaskRequest task,
            Property dbProperty,
            Trust dbTrust,
            Construction dbConstruction) 
        {
            if (task == null)
            {
                return;
            }

            dbProperty.SiteNameOfSite = task.NameOfSite;
            dbProperty.SiteAddressOfSite = task.AddressOfSite;
            dbProperty.SitePostcodeOfSite = task.PostcodeOfSite;
            dbProperty.SiteBuildingType = task.BuildingType;

            dbTrust.TrustRef = task.TrustRef;
            dbTrust.LeadSponsor = task.TrustLeadSponsor;
            dbTrust.TrustsTrustName = task.TrustName;

            dbConstruction.SiteDetailsAreaOfNewBuildM2 = task.SiteMinArea;
            dbConstruction.SiteDetailsTypeOfWorks = task.TypeofWorksLocation;
        }
    }
}
