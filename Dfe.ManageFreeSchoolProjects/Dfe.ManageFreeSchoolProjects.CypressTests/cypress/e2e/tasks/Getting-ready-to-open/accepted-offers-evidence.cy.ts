import { ProjectDetailsRequest } from "cypress/api/domain";
import projectApi from "cypress/api/projectApi";
import { RequestBuilder } from "cypress/api/requestBuilder";
import { Logger } from "cypress/common/logger";
import summaryPage from "cypress/pages/task-summary-base";
import taskListPage from "cypress/pages/taskListPage";
import acceptedOffersEvidenceEditPage from "../../../pages/tasks/Getting-ready-to-open/edit-accepted-offers-evidence-cy";

describe("Testing the impact assessment task", () => {

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

    it("Should be able to set impact assessment", () => {
        Logger.log("Select Accepted offers evidence");
        taskListPage.isTaskStatusIsNotStarted("EvidenceOfAcceptedOffers")
            .selectAcceptedOffersEvidenceFromTaskList();

        summaryPage
            .schoolNameIs(project.schoolName)
            .titleIs("Accepted offers evidence")
            .inOrder()
            .summaryShows("Seen evidence of accepted offers").IsEmpty().HasChangeLink()
            .summaryShows("Comments").IsEmpty().HasChangeLink()
            .summaryShows("Saved email from the trust in Workplaces folder confirming accepted offers").IsEmpty().HasChangeLink()
            .isNotMarkedAsComplete();

        Logger.log("Go back to task list");
        summaryPage.clickBack();

        taskListPage.selectAcceptedOffersEvidenceFromTaskList();
        summaryPage.clickChange();

        Logger.log("Accepted offers evidence can save null values");

        acceptedOffersEvidenceEditPage
        .clickContinue()

        summaryPage
            .schoolNameIs(project.schoolName)
            .titleIs("Impact assessment")
            .inOrder()
            .summaryShows("Seen evidence of accepted offers").IsEmpty().HasChangeLink()
            .summaryShows("Comments").IsEmpty().HasChangeLink()
            .summaryShows("Saved email from the trust in Workplaces folder confirming accepted offers").IsEmpty().HasChangeLink()
            .isNotMarkedAsComplete()
            .clickChange();

        cy.executeAccessibilityTests();

        Logger.log("Impact assessment can be edited");

        acceptedOffersEvidenceEditPage
            .checkImpactAssessmentDone()
            .checkSavedToWorkplaces()
            .clickContinue()

        Logger.log("Should update the task status");

        summaryPage
            .schoolNameIs(project.schoolName)
            .titleIs("Impact assessment")
            .inOrder()
            .summaryShows("Conducted the impact assessment using the Assess the Impact of Opening a New Free School tool").HasValue("Yes").HasChangeLink()
            .summaryShows("Saved the signed-off impact assessment in Workplaces folder").HasValue("Yes").HasChangeLink()
            .isNotMarkedAsComplete()
            .clickConfirmAndContinue()

        taskListPage.isTaskStatusInProgress("ImpactAssessment");

        taskListPage.selectImpactAssessmentFromTaskList();
        summaryPage.clickChange();

        acceptedOffersEvidenceEditPage
            .uncheckImpactAssessmentDone()
            .uncheckSavedToWorkplaces()
            .clickContinue()

        summaryPage
            .schoolNameIs(project.schoolName)
            .titleIs("Impact assessment")
            .inOrder()
            .summaryShows("Conducted the impact assessment using the Assess the Impact of Opening a New Free School tool").IsEmpty().HasChangeLink()
            .summaryShows("Saved the signed-off impact assessment in Workplaces folder").IsEmpty().HasChangeLink()
            .isNotMarkedAsComplete()
            .MarkAsComplete()
            .clickConfirmAndContinue()
        
        taskListPage.isTaskStatusIsCompleted("ImpactAssessment");
    });
});