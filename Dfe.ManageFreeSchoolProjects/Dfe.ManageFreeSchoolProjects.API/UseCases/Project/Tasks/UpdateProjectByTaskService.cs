using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Exceptions;
using Dfe.ManageFreeSchoolProjects.Data;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks
{
    public interface IUpdateProjectByTaskService
    {
        public Task Execute(string projectId, UpdateProjectByTaskRequest request);
    }

    public class UpdateProjectByTaskService : IUpdateProjectByTaskService
    {
        private readonly MfspContext _context;

        public UpdateProjectByTaskService(MfspContext context)
        {
            _context = context;
        }

        public async Task Execute(string projectId, UpdateProjectByTaskRequest request)
        {
            var dbKpi = await _context.Kpi.FirstOrDefaultAsync(p => p.ProjectStatusProjectId == projectId);

            if (dbKpi == null)
            {
                throw new NotFoundException($"Project {projectId} not found");
            }

            var dbKai = await GetKai(dbKpi.Rid);
            var dbProperty = await GetProperty(dbKpi.Rid);
            var dbConstruction = await GetConstruction(dbKpi.Rid);

            // Updates here
            ApplySchoolTaskUpdates(request.School, dbKpi, dbKai);
            ApplyConstructionTaskUpdates(request.Construction, dbProperty, dbConstruction);
            ApplyDatesTaskUpdates(request.Dates, dbKpi);
            await ApplyTrustTaskUpdates(request.Trust, dbKpi);

            await UpdateTaskStatus(dbKpi.Rid, Status.InProgress, request);

            await _context.SaveChangesAsync();
        }

        private static void ApplySchoolTaskUpdates(
            SchoolTask task,
            Kpi dbKpi,
            Kai dbKai)
        {
            if (task == null)
            {
                return;
            }

            dbKpi.ProjectStatusCurrentFreeSchoolName = task.CurrentFreeSchoolName;
            dbKpi.SchoolDetailsSchoolTypeMainstreamApEtc = task.SchoolType;
            dbKpi.SchoolDetailsSchoolPhasePrimarySecondary = task.SchoolPhase;
            dbKpi.SchoolDetailsAgeRange = task.AgeRange;
            dbKpi.SchoolDetailsNursery = task.Nursery;
            dbKpi.SchoolDetailsSixthForm = task.SixthForm;

            dbKai.ApplicationDetailsCompanyName = task.CompanyName;
            dbKai.ApplicationDetailsNumberOfCompanyMembers = task.NumberOfCompanyMembers;
            dbKai.ApplicationDetailsProposedChairOfTrustees = task.ProposedChairOfTrustees;
        }

        private static void ApplyConstructionTaskUpdates(
            ConstructionTask task,
            Property dbProperty,
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

            dbConstruction.SiteDetailsAreaOfNewBuildM2 = task.SiteMinArea;
            dbConstruction.SiteDetailsTypeOfWorks = task.TypeofWorksLocation;
        }

        private static void ApplyDatesTaskUpdates(
            DatesTask task,
            Kpi dbKpi)
        {
            if (task == null)
            {
                return;
            }

            dbKpi.ProjectStatusDateOfEntryIntoPreOpening = task.DateOfEntryIntoPreopening;
            dbKpi.ProjectStatusRealisticYearOfOpening = task.RealisticYearOfOpening;
            dbKpi.ProjectStatusProvisionalOpeningDateAgreedWithTrust = task.ProvisionalOpeningDateAgreedWithTrust;
        }

        private async Task ApplyTrustTaskUpdates(
            TrustTask task,
            Kpi dbKpi)
        {
            if (task == null)
            {
                return;
            }

            var trust = await GetTrust(task.TRN);

            dbKpi.TrustId = trust.TrustRef ;
            dbKpi.TrustName = trust.TrustsTrustName;
            dbKpi.TrustType = trust.TrustsTrustType;

            dbKpi.SchoolDetailsTrustId = trust.TrustsTrustRef;
            dbKpi.SchoolDetailsTrustName = trust.TrustsTrustName;
            dbKpi.SchoolDetailsTrustType = trust.TrustsTrustType;
        }

        private async Task<Kai> GetKai(string id)
        {
            var result = await _context.Kai.FirstOrDefaultAsync(e => e.Rid == id);

            if (result == null)
            {
                result = new Kai()
                {
                    Rid = id,
                };

                _context.Kai.Add(result);
            }

            return result;
        }

        private async Task<Property> GetProperty(string id)
        {
            var result = await _context.Property.FirstOrDefaultAsync(e => e.Rid == id);

            if (result == null)
            {
                result = new Property()
                {
                    Rid = id,
                    Tos = "123"
                };

                _context.Property.Add(result);
            }

            return result;
        }

        private async Task<Trust> GetTrust(string trustRef)
        {
            var result = await _context.Trust.FirstOrDefaultAsync(e => e.TrustRef == trustRef);
            
            return result;
        }

        private async Task<Construction> GetConstruction(string id)
        {
            var result = await _context.Construction.FirstOrDefaultAsync(e => e.Rid == id);

            if (result == null)
            {
                result = new Construction()
                {
                    Rid = id
                };

                _context.Construction.Add(result);
            }

            return result;
        }

        private async Task UpdateTaskStatus(string taskRid, Status updatedStatus,
            UpdateProjectByTaskRequest updateProjectByTaskRequest)
        {
            var taskNameToUpdate = Enum.Parse<TaskName>(updateProjectByTaskRequest.TaskToUpdate);

            var task = await _context.Tasks.SingleOrDefaultAsync(x => x.Rid == taskRid
                                                                      && x.TaskName == taskNameToUpdate);
            if (task is null)
                return;

            task.Status = updatedStatus;
        }
    }
}