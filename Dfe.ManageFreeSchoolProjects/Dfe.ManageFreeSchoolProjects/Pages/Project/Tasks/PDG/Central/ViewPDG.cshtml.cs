using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Dfe.ManageFreeSchoolProjects.Services.Tasks;
using Dfe.ManageFreeSchoolProjects.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Payments;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Grants;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.PDG.Central
{
    public class ViewPDGModel(ILogger<ViewPDGModel> logger, IGetProjectPaymentsService getProjectPaymentsService,
        IGetProjectByTaskService getProjectService,
        IGetTaskStatusService getTaskStatusService,
        IUpdateTaskStatusService updateTaskStatusService, 
        IGrantLettersService grantLettersService,
        IPDGPaymentInfoService paymentInfoService)
        : ViewTaskBaseModel(getProjectService, getTaskStatusService, updateTaskStatusService)
    {
        public PDGPaymentInfo PDGPaymentInfo { get; set; }

        public ProjectPayments ProjectPayments { get; set; }

        public ProjectGrantLetters PdgGrantLetters { get; set; }

        public async Task<ActionResult> OnGet()
        {
            logger.LogMethodEntered();

            await GetTask(TaskName.PDG);

            ProjectPayments = await getProjectPaymentsService.Execute(ProjectId);
            PdgGrantLetters = await grantLettersService.Get(ProjectId);
            PDGPaymentInfo = paymentInfoService.GetPDGPaymentInfo(ProjectPayments);

            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            logger.LogMethodEntered();

            await PostTask(TaskName.PDG);

            return Redirect(string.Format(RouteConstants.TaskList, ProjectId));
        }
    }
}
