using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.DueDiligenceChecks;

public static class DueDiligenceChecksTaskBuilder
{
    public static Contracts.Project.Tasks.DueDiligenceChecks Build(Milestones milestones)
    {
        return milestones == null
            ? new Contracts.Project.Tasks.DueDiligenceChecks()
            : new Contracts.Project.Tasks.DueDiligenceChecks
            {
                SavedNonSpecialistChecksSpreadsheetInWorkplaces =
                    milestones.FsgPreOpeningSavedNonSpecialistChecksSpreadsheetInWorkplaces,
                RequestedCounterExtremismChecks = milestones.FsgPreOpeningRequestedCounterExtremismChecks,
                DateWhenAllChecksWereCompleted = milestones.FsgPreOpeningMilestonesDbscActualDateOfCompletion,
                ReceivedChairOfTrusteesCountersignedCertificate =
                    milestones.FsgPreOpeningReceivedChairOfTrusteesCountersignedCertificate,
                DeletedAnyCopiesOfChairsDBSCertificate = milestones.FsgPreOpeningDeletedAnyCopiesOfChairsDBSCertificate,
                NonSpecialistChecksDoneOnAllTrustMembersAndTrustees =
                    milestones.FsgPreOpeningNonSpecialistChecksDoneOnAllTrustMembersAndTrustees,
                DeletedEmailsContainingSuitabilityAndDeclarationForms =
                    milestones.FsgPreOpeningDeletedEmailsContainingSuitabilityAndDeclarationForms
            };
    }
}