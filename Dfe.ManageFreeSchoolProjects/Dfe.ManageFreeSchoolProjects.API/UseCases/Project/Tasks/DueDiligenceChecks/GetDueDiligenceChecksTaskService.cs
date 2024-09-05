using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.DueDiligenceChecks;

public class GetDueDiligenceChecksTaskService(MfspContext context) : IGetTaskService
{
    public async Task<GetProjectByTaskResponse> Get(GetTaskServiceParameters parameters)
    {
        var result = await (from kpi in parameters.BaseQuery
            join milestones in context.Milestones on kpi.Rid equals milestones.Rid into joinedMilestones
            from milestones in joinedMilestones.DefaultIfEmpty()
            select new GetProjectByTaskResponse
            {
                DueDiligenceChecks = new Contracts.Project.Tasks.DueDiligenceChecks 
                {
                    SavedNonSpecialistChecksSpreadsheetInWorkplaces = milestones.FsgPreOpeningSavedNonSpecialistChecksSpreadsheetInWorkplaces,
                    RequestedCounterExtremismChecks = milestones.FsgPreOpeningRequestedCounterExtremismChecks,
                    DateWhenAllChecksWereCompleted = milestones.FsgPreOpeningMilestonesDbscActualDateOfCompletion,
                    ReceivedChairOfTrusteesCountersignedCertificate = milestones.FsgPreOpeningReceivedChairOfTrusteesCountersignedCertificate,
                    DeletedAnyCopiesOfChairsDBSCertificate = milestones.FsgPreOpeningDeletedAnyCopiesOfChairsDBSCertificate,
                    NonSpecialistChecksDoneOnAllTrustMembersAndTrustees = milestones.FsgPreOpeningNonSpecialistChecksDoneOnAllTrustMembersAndTrustees,
                    DeletedEmailsContainingSuitabilityAndDeclarationForms = milestones.FsgPreOpeningDeletedEmailsContainingSuitabilityAndDeclarationForms
                }
            }).FirstOrDefaultAsync();

        return result ?? new GetProjectByTaskResponse { DueDiligenceChecks = new Contracts.Project.Tasks.DueDiligenceChecks() };
    }
}