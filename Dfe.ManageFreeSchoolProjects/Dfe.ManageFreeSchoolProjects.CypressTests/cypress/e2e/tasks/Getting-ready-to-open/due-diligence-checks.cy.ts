import { ProjectDetailsRequest } from "cypress/api/domain";
import projectApi from "cypress/api/projectApi";
import { RequestBuilder } from "cypress/api/requestBuilder";
import { Logger } from "cypress/common/logger";
import summaryPage from "cypress/pages/task-summary-base";
import taskListPage from "cypress/pages/taskListPage";
import dueDiligenceChecksEditPage from "../../../pages/tasks/Getting-ready-to-open/edit-due-diligence-checks-cy";
import schoolDetailsPage from "../../../pages/schoolDetailsPage";


describe("Testing the due diligence checks task", () => {

    let project: ProjectDetailsRequest;

    beforeEach(() => {
        cy.login();

        project = RequestBuilder.createProjectDetails();

        projectApi
            .post({
                projects: [project],
            })
            .then(() => {
                cy.visit(`/projects/${project.projectId}/tasks`);
            });
    });

    it("Should be able input due diligence checks", () => {
        Logger.log("Select due diligence checks");

        taskListPage.isTaskStatusIsNotStarted("DueDiligenceChecks")
            .selectDueDiligenceFromTaskList();

            checkSummaryPageHasEmptyValuesAndNotComplete(project.schoolName);

            Logger.log("Go back to task list")
            summaryPage.clickBack();

            taskListPage.selectDueDiligenceFromTaskList();
            summaryPage.clickChange();
            Logger.log("Due diligence checks can save null values");

            dueDiligenceChecksEditPage.clickContinue();

            checkSummaryPageHasEmptyValuesAndNotComplete(project.schoolName);

            cy.executeAccessibilityTests();

            Logger.log("Due diligence checks can be edited");
            
            summaryPage.clickChange();

            dueDiligenceChecksEditPage
            .checkReceivedChairOfTrusteesDbsCountersignedCertificate()
            .checkNonSpecialistChecksDoneOnAllTrustMembersAndTrusteesInLast2Years()
            .requestedCounterExtremismChecks("No")
            
    });
});

function checkSummaryPageHasEmptyValuesAndNotComplete(schoolName: string) {
    summaryPage
    .schoolNameIs(schoolName)
    .titleIs("Due diligence checks")
    .inOrder()
    .summaryShows("Received Chair of Trustee's DBS countersigned certificate").IsEmpty().HasChangeLink()
    .summaryShows("Non-specialist checks done on all trust members and trustees in last 2 years").IsEmpty().HasChangeLink()
    .summaryShows("Requested counter terrorism checks").IsEmpty().HasChangeLink()
    .summaryShows("Date when all check were completed").IsEmpty().HasChangeLink()
    .summaryShows("Saved the non-specialist checks spreadsheet in Workplaces folder").IsEmpty().HasChangeLink()
    .summaryShows("Deleted copy of any Chair's DBS certificate").IsEmpty().HasChangeLink()
    .summaryShows("Deleted emails containing suitability and declaration forms").IsEmpty().HasChangeLink()
    .isNotMarkedAsComplete();
}