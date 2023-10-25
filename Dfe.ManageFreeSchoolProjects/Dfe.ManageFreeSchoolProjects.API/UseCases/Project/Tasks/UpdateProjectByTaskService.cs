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
            try
            {
                var dbKpi = await _context.Kpi.FirstOrDefaultAsync(p => p.ProjectStatusProjectId == projectId);

                if (dbKpi == null)
                {
                    throw new NotFoundException($"Project {projectId} not found");
                }

                var dbProperty = await GetProperty(dbKpi.Rid);
                var dbTrust = await GetTrust(dbKpi.Rid);
                var dbConstruction = await GetConstruction(dbKpi.Rid);

                // Updates here
                ApplySchoolTaskUpdates(request.School, dbKpi);
                ApplyConstructionTaskUpdates(request.Construction, dbProperty, dbTrust, dbConstruction);
                ApplyDatesTaskUpdates(request.Dates, dbKpi);

                await UpdateTaskStatus(dbKpi.Rid, Status.InProgress, request);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static void ApplySchoolTaskUpdates(
            SchoolTask task,
            Kpi dbKpi)
        {
            if (task == null)
            {
                return;
            }

            dbKpi.ProjectStatusCurrentFreeSchoolName = task.CurrentFreeSchoolName;
            dbKpi.SchoolDetailsSchoolTypeMainstreamApEtc = task.SchoolType.MapSchoolType();
            dbKpi.SchoolDetailsSchoolPhasePrimarySecondary = task.SchoolPhase.MapSchoolPhase();
            dbKpi.SchoolDetailsAgeRange = task.AgeRange;
            dbKpi.SchoolDetailsNursery = task.Nursery.ToString();
            dbKpi.SchoolDetailsSixthForm = task.SixthForm.ToString();
            dbKpi.SchoolDetailsFaithStatus = task.FaithStatus.ToString();
            dbKpi.SchoolDetailsFaithType = task.FaithType.ToString();
            dbKpi.SchoolDetailsPleaseSpecifyOtherFaithType = task.OtherFaithType;
        }

        private static void ApplyConstructionTaskUpdates(
            ConstructionTask task,
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

        private static void ApplyDatesTaskUpdates(DatesTask task, Kpi dbKpi)
        {
            if (task == null)
            {
                return;
            }

            dbKpi.ProjectStatusDateOfEntryIntoPreOpening = task.DateOfEntryIntoPreopening;
            dbKpi.ProjectStatusRealisticYearOfOpening = task.RealisticYearOfOpening;
            dbKpi.ProjectStatusProvisionalOpeningDateAgreedWithTrust = task.ProvisionalOpeningDateAgreedWithTrust;
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

        private async Task<Trust> GetTrust(string id)
        {
            var result = await _context.Trust.FirstOrDefaultAsync(e => e.Rid == id);

            if (result == null)
            {
                result = new Trust()
                {
                    Rid = id
                };

                _context.Trust.Add(result);
            }

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