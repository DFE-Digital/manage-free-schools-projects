using Azure.Core;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Exceptions;
using Dfe.ManageFreeSchoolProjects.Data;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.ReferenceNumbers
{
    public class UpdateReferenceNumbersTaskService : IUpdateTaskService
    {
        public async Task Update(UpdateTaskServiceParameters parameters)
        {
            var task = parameters.Request.ReferenceNumbers;
            var dbKpi = parameters.Kpi;

            if (task == null)
            {
                return;
            }

            dbKpi.ProjectStatusProjectId = task.ProjectId;
            dbKpi.ProjectStatusUrnWhenGivenOne = task.Urn;
        }
    }
}
