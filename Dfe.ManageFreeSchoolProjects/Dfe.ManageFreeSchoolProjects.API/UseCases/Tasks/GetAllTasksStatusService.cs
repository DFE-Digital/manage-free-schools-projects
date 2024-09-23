﻿using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Exceptions;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.ApplicationsEvidence;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.FundingAgreementHealthCheck;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.FundingAgreementSubmission;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.PrincipalDesignate;
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

        var projectTasks = dbTasks.Select(task => new TaskSummaryResponse
        {
            Name = task.TaskName.ToString(),
            Status = task.Status.Map()
        });

        var result = BuildProjectByTaskSummaryResponse(dbKpi, projectTasks);

        return result;
    }

    private static ProjectByTaskSummaryResponse BuildProjectByTaskSummaryResponse(Kpi dbKpi,
        IEnumerable<TaskSummaryResponse> taskSummaryResponses)
    {
        _tasksCount = 0;
        _hiddenCompletedTasks = 0;
        
        var projectTasks = taskSummaryResponses.ToList();
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
            GovernancePlan = SafeRetrieveTaskSummary(projectTasks, TaskName.GovernancePlan.ToString()),
            EducationBrief = SafeRetrieveTaskSummary(projectTasks, "EducationBrief"),
            EqualitiesAssessment = SafeRetrieveTaskSummary(projectTasks, "EqualitiesAssessment"),
            PupilNumbersChecks = SafeRetrieveTaskSummary(projectTasks, "PupilNumbersChecks"),
            AdmissionsArrangements = SafeRetrieveTaskSummary(projectTasks, "AdmissionsArrangements"),
            ImpactAssessment = SafeRetrieveTaskSummary(projectTasks, "ImpactAssessment"),
            EvidenceOfAcceptedOffers = SafeRetrieveTaskSummary(projectTasks, "EvidenceOfAcceptedOffers"),
            OfstedInspection = SafeRetrieveTaskSummary(projectTasks, "OfstedInspection"),
            PDG = SafeRetrieveTaskSummary(projectTasks, "PDG"),
            FinalFinancePlan = SafeRetrieveTaskSummary(projectTasks, TaskName.FinalFinancePlan.ToString()),
            CommissionedExternalExpert =
                SafeRetrieveTaskSummary(projectTasks, TaskName.CommissionedExternalExpert.ToString()),
            MovingToOpen = SafeRetrieveTaskSummary(projectTasks, TaskName.MovingToOpen.ToString()),
            DueDiligenceChecks = SafeRetrieveTaskSummary(projectTasks, TaskName.DueDiligenceChecks.ToString()),
            PreFundingAgreementCheckpointMeeting = SafeRetrieveTaskSummary(projectTasks, "PreFundingAgreementCheckpointMeeting"),
            ReadinessToOpenMeeting = SafeRetrieveTaskSummary(projectTasks, TaskName.ReadinessToOpenMeeting.ToString())
        };

        var applicationsEvidenceTask = SafeRetrieveTaskSummary(projectTasks, TaskName.ApplicationsEvidence.ToString());
        var principalDesignateTask = SafeRetrieveTaskSummary(projectTasks, TaskName.PrincipalDesignate.ToString());
        var fundingAgreementHealthCheckTask = SafeRetrieveTaskSummary(projectTasks, "FundingAgreementHealthCheck");
        var fundingAgreementSubmissionTask = SafeRetrieveTaskSummary(projectTasks, "FundingAgreementSubmission");

        result.ApplicationsEvidence = BuildApplicationsEvidenceTask(applicationsEvidenceTask, dbKpi);
        result.PrincipalDesignate = BuildPrincipleDesignateTask(principalDesignateTask, dbKpi);
        result.FundingAgreementHealthCheck =
            BuildFundingAgreementHealthCheckTask(fundingAgreementHealthCheckTask, dbKpi);
        result.FundingAgreementSubmission = BuildFundingAgreementSubmissionTask(fundingAgreementSubmissionTask, dbKpi);

        result.TaskCount = _tasksCount;

        RemoveHiddenCompletedTaskStatus(result.ApplicationsEvidence);
        RemoveHiddenCompletedTaskStatus(result.PrincipalDesignate);

        result.CompletedTasks =
            projectTasks.Count(x => x.Status == ProjectTaskStatus.Completed) - _hiddenCompletedTasks;


        return result;
    }

    private static TaskSummaryResponse SafeRetrieveTaskSummary(IEnumerable<TaskSummaryResponse> projectTasks,
        string taskName)
    {
        _tasksCount++;
        return projectTasks.FirstOrDefault(x => x.Name == taskName,
            new TaskSummaryResponse { Name = taskName, Status = ProjectTaskStatus.NotStarted });
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
        var parameters = new PrincipalDesignateTaskSummaryBuilderParameters()
        {
            ApplicationWave = kpi.ProjectStatusFreeSchoolApplicationWave,
            TaskSummary = taskSummaryResponse
        };

        var result = new PrincipalDesignateTaskSummaryBuilder().Build(parameters);

        _tasksCount -= taskSummaryResponse.IsHidden ? 1 : 0;

        return result;
    }

    private static TaskSummaryResponse BuildFundingAgreementHealthCheckTask(TaskSummaryResponse taskSummaryResponse,
        Kpi kpi)
    {
        var parameters = new FundingAgreementHealthCheckTaskSummaryBuilderParameters()
        {
            ApplicationWave = kpi.ProjectStatusFreeSchoolApplicationWave,
            TaskSummary = taskSummaryResponse
        };

        var result = new FundingAgreementHealthCheckTaskSummaryBuilder().Build(parameters);

        _tasksCount -= taskSummaryResponse.IsHidden ? 1 : 0;

        return result;
    }

    private static TaskSummaryResponse BuildFundingAgreementSubmissionTask(TaskSummaryResponse taskSummaryResponse,
        Kpi kpi)
    {
        var parameters = new FundingAgreementSubmissionTaskSummaryBuilderParameters()
        {
            ApplicationWave = kpi.ProjectStatusFreeSchoolApplicationWave,
            TaskSummary = taskSummaryResponse
        };

        var result = new FundingAgreementSubmissionTaskSummaryBuilder().Build(parameters);

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