using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Exceptions;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.ApplicationsEvidence;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.PrincipleDesignate;
using Dfe.ManageFreeSchoolProjects.Data;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Tasks;

public interface IGetTasksService
{ 
    Task<ProjectByTaskSummaryResponse> Execute(string projectId);
}

public class GetAllTasksStatusService : IGetTasksService
{
    private readonly MfspContext _context;

    private static int _hiddenCompletedTasks;
    private static int _tasksCount;
    
    public GetAllTasksStatusService(MfspContext context)
    {
        _context = context;
    }

    public async Task<ProjectByTaskSummaryResponse> Execute(string projectId)
    {
        var dbKpi = await _context.Kpi.FirstOrDefaultAsync(x => x.ProjectStatusProjectId == projectId);

        if (dbKpi == null)
        {
            throw new NotFoundException($"Project with id {projectId} not found");
        }

        var dbTasks = await _context.Tasks.Where(x => x.Rid == dbKpi.Rid).ToListAsync();

        var projectTasks = dbTasks.Select(task =>
        {
            return new TaskSummaryResponse
            {
                Name = task.TaskName.ToString(),
                Status = task.Status.Map()
            };
        });

        ProjectByTaskSummaryResponse result = BuildProjectByTaskSummaryResponse(dbKpi, projectTasks);

        return result;
    }

    private static ProjectByTaskSummaryResponse BuildProjectByTaskSummaryResponse(Kpi dbKpi, IEnumerable<TaskSummaryResponse> projectTasks)
    {
        _tasksCount = 0;
        _hiddenCompletedTasks = 0;
        var result = new ProjectByTaskSummaryResponse
        {
            SchoolName = dbKpi.ProjectStatusCurrentFreeSchoolName,
            School = SafeRetrieveTaskSummary(projectTasks, "School"),
            ReferenceNumbers = SafeRetrieveTaskSummary(projectTasks, "ReferenceNumbers"),
            Dates = SafeRetrieveTaskSummary(projectTasks, "Dates"),
            Trust = SafeRetrieveTaskSummary(projectTasks, "Trust"),
            RegionAndLocalAuthority = SafeRetrieveTaskSummary(projectTasks, "RegionAndLocalAuthority"),
            RiskAppraisalMeeting = SafeRetrieveTaskSummary(projectTasks, "RiskAppraisalMeeting"),
            Constituency = SafeRetrieveTaskSummary(projectTasks, "Constituency"),
            FundingAgreement = SafeRetrieveTaskSummary(projectTasks, "FundingAgreement"),
            StatutoryConsultation = SafeRetrieveTaskSummary(projectTasks, "StatutoryConsultation"),
            ArticlesOfAssociation = SafeRetrieveTaskSummary(projectTasks, "ArticlesOfAssociation"),
            FinancePlan = SafeRetrieveTaskSummary(projectTasks, "FinancePlan"),
            KickOffMeeting = SafeRetrieveTaskSummary(projectTasks, "KickOffMeeting"),
            Gias = SafeRetrieveTaskSummary(projectTasks, "Gias"),
            DraftGovernancePlan = SafeRetrieveTaskSummary(projectTasks, TaskName.DraftGovernancePlan.ToString()),
            EducationBrief = SafeRetrieveTaskSummary(projectTasks, "EducationBrief"),
            EqualitiesAssessment = SafeRetrieveTaskSummary(projectTasks, "EqualitiesAssessment"),
            PupilNumbersChecks = SafeRetrieveTaskSummary(projectTasks, "PupilNumbersChecks"),
            AdmissionsArrangements = SafeRetrieveTaskSummary(projectTasks, "AdmissionsArrangements"),
            ImpactAssessment = SafeRetrieveTaskSummary(projectTasks, "ImpactAssessment"),
            EvidenceOfAcceptedOffers = SafeRetrieveTaskSummary(projectTasks, "EvidenceOfAcceptedOffers"),
            OfstedInspection = SafeRetrieveTaskSummary(projectTasks, "OfstedInspection"),
            FundingAgreementHealthCheck = SafeRetrieveTaskSummary(projectTasks, "FundingAgreementHealthCheck"),
            FundingAgreementSubmission = SafeRetrieveTaskSummary(projectTasks, "FundingAgreementSubmission"),
            PDG = SafeRetrieveTaskSummary(projectTasks, "PDG"),
            FinalFinancePlan = SafeRetrieveTaskSummary(projectTasks, TaskName.FinalFinancePlan.ToString()),
            CommissionedExternalExpert = SafeRetrieveTaskSummary(projectTasks, TaskName.CommissionedExternalExpert.ToString()),
            MovingToOpen = SafeRetrieveTaskSummary(projectTasks, TaskName.MovingToOpen.ToString())
        };

        var applicationsEvidenceTask = SafeRetrieveTaskSummary(projectTasks, TaskName.ApplicationsEvidence.ToString());
        var principleDesignateTask = SafeRetrieveTaskSummary(projectTasks, TaskName.PrincipleDesignate.ToString());
        
        result.ApplicationsEvidence = BuildApplicationsEvidenceTask(applicationsEvidenceTask, dbKpi);
        
        result.PrincipleDesignate = BuildPrincipleDesignateTask(principleDesignateTask, dbKpi);
        
        result.TaskCount = _tasksCount;
        
        RemoveHiddenCompletedTaskStatus(result.ApplicationsEvidence);
        RemoveHiddenCompletedTaskStatus(result.PrincipleDesignate);
        
        result.CompletedTasks = projectTasks.Count(x => x.Status == ProjectTaskStatus.Completed) - _hiddenCompletedTasks;
        
        
        return result;
    }

    private static TaskSummaryResponse SafeRetrieveTaskSummary(IEnumerable<TaskSummaryResponse> projectTasks, string taskName)
    {
        _tasksCount++;
        return projectTasks.FirstOrDefault(x => x.Name == taskName, new TaskSummaryResponse { Name = taskName, Status = ProjectTaskStatus.NotStarted });
    }

    private static TaskSummaryResponse BuildApplicationsEvidenceTask(TaskSummaryResponse taskSummaryResponse, Kpi kpi)
    {
        var parameters = new ApplicationsEvidenceTaskSummaryBuilderParameters()
        {
            SchoolType = ProjectMapper.ToSchoolType(kpi.SchoolDetailsSchoolTypeMainstreamApEtc),
            TaskSummary = taskSummaryResponse
        };
        
        var result = new ApplicationsEvidenceTaskSummaryBuilder().Build(parameters);
        
        _tasksCount -= taskSummaryResponse.IsHidden ? 1 : 0;
        
        return result;
    }
    
    private static TaskSummaryResponse BuildPrincipleDesignateTask(TaskSummaryResponse taskSummaryResponse, Kpi kpi)
    {
        var parameters = new PrincipleDesignateTaskSummaryBuilderParameters()
        {
            ApplicationWave = kpi.ProjectStatusFreeSchoolApplicationWave,
            TaskSummary = taskSummaryResponse
        };
        
        var result = new PrincipleDesignateTaskSummaryBuilder().Build(parameters);
        
        _tasksCount -= taskSummaryResponse.IsHidden ? 1 : 0;
        
        return result;
    }

    private static void RemoveHiddenCompletedTaskStatus(TaskSummaryResponse taskSummaryResponse)
    {
        if (taskSummaryResponse.IsHidden && taskSummaryResponse.Status == ProjectTaskStatus.Completed)
        {
            _hiddenCompletedTasks++;
        }
    }
}

