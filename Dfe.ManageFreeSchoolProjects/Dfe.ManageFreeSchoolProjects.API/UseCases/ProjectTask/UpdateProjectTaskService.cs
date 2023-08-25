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

            _context.Kai.Add(dbKai);

            // Updates here
            ApplySchoolTaskUpdates(request.Tasks.School, dbKpi, dbKai);

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
    }
}
