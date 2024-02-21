import { ProjectDetailsRequest } from "cypress/api/domain";
import projectApi from "cypress/api/projectApi";
import { RequestBuilder } from "cypress/api/requestBuilder";
import { Logger } from "cypress/common/logger";
import editProjectRiskPage from "cypress/pages/risk/editProjectRiskPage";
import projectRiskSummaryPage from "cypress/pages/risk/projectRiskSummaryPage";
import summaryPage from "cypress/pages/task-summary-base";
import taskListPage from "cypress/pages/taskListPage";
import editDraftGovernancePlanPage from "cypress/pages/tasks/pre-opening/editDraftGovernancePlanPage";
import validationComponent from "cypress/pages/validationComponent";

describe("Testing draft governance plan task", () => {
    let project: ProjectDetailsRequest;

    beforeEach(() => {
        cy.login();

        project = RequestBuilder.createProjectDetails();

        projectApi
            .post({
                projects: [project],
            });
    });

    it("Should not show a draft governance plan if the overall or governance risk is not red or red/amber", () => {
        cy.visit(`/projects/${project.projectId}/tasks`);

        Logger.log("This project has no risks so the task should not show");
        taskListPage.draftGovernancePlanTaskDoesNotShow();
    });

    it("Should be able to set a draft governance plan", () => {

        Logger.log("Update overall risk to red so the task shows");
        cy.visit(`/projects/${project.projectId}/risk/summary`);
        projectRiskSummaryPage
            .addRiskEntry()
            .changeOverallRisk();

        editProjectRiskPage
            .withOverallRiskRating("Red")
            .continue();

        projectRiskSummaryPage
            .markRiskAsReviewed()
            .createRiskEntry();

        cy.visit(`/projects/${project.projectId}/tasks`);

        Logger.log("Select draft governance plan");
        taskListPage.isTaskStatusIsNotStarted("DraftGovernancePlan")
            .selectDraftGovernancePlanFromTaskList();

        Logger.log("Go back to task list");
        summaryPage.clickBack();

        taskListPage.selectDraftGovernancePlanFromTaskList();

        Logger.log("Confirm empty draft governance plan");
        summaryPage
            .schoolNameIs(project.schoolName)
            .titleIs("Draft governance plan")
            .inOrder()
            .summaryShows("Forecast date").IsEmpty().HasChangeLink()
            .summaryShows("Actual date").IsEmpty().HasChangeLink()
            .summaryShows("Comments on decision to approve (if applicable)").IsEmpty().HasChangeLink()
            .summaryShows("SharePoint link").IsEmpty().HasChangeLink()
            .isNotMarkedAsComplete();

        cy.executeAccessibilityTests();

        summaryPage.clickChange();

        Logger.log("Check all the fields are optional");
        editDraftGovernancePlanPage
            .titleIs("Edit draft governance plan")
            .schoolNameIs(project.schoolName)
            .clickContinue();

        summaryPage.clickChange();

        Logger.log("Testing validation");
        editDraftGovernancePlanPage
            .withForecastDate("33", "", "")
            .withActualDate("44", "", "")
            .withSharepointLinkExceedingMaxLength()
            .withCommentsOnDecisionToApproveExceedingMaxLength()
            .clickContinue();

        validationComponent
            .hasValidationError("Forecast date must include a month and year")
            .hasValidationError("Actual date must include a month and year")
            .hasValidationError("The comments on decision to approve (if applicable) must be 999 characters or less")
            .hasValidationError("The SharePoint link must be 500 characters or less");

        cy.executeAccessibilityTests();

        editDraftGovernancePlanPage
            .withSharepointLink("aaaa")
            .clickContinue();

        validationComponent.hasValidationError("The SharePoint link must be a valid url");

        Logger.log("Add new values");
        editDraftGovernancePlanPage
            .schoolNameIs(project.schoolName)
            .withForecastDate("25", "08", "2025")
            .withActualDate("08", "09", "2025")
            .withCommentsOnDecisionToApprove("This is my comments")
            .withSharepointLink("https://www.sharepoint.com")
            .clickContinue();

        summaryPage
            .inOrder()
            .summaryShows("Forecast date").HasValue("25 August 2025").HasChangeLink()
            .summaryShows("Actual date").HasValue("8 September 2025").HasChangeLink()
            .summaryShows("Comments on decision to approve (if applicable)").HasValue("This is my comments").HasChangeLink()
            .summaryShows("SharePoint link").HasValue("https://www.sharepoint.com").HasChangeLink();

        Logger.log("Should be able to edit the existing values");
        summaryPage.clickChange();

        editDraftGovernancePlanPage
            .withForecastDate("01", "01", "2026")
            .withActualDate("16", "01", "2026")
            .withCommentsOnDecisionToApprove("This is my updated comments")
            .withSharepointLink("https://www.sharepoint.com/updated")
            .clickContinue();

        summaryPage
            .inOrder()
            .summaryShows("Forecast date").HasValue("1 January 2026").HasChangeLink()
            .summaryShows("Actual date").HasValue("16 January 2026").HasChangeLink()
            .summaryShows("Comments on decision to approve (if applicable)").HasValue("This is my updated comments").HasChangeLink()
            .summaryShows("SharePoint link").HasValue("https://www.sharepoint.com/updated").HasChangeLink();

        Logger.log("Should update the task status");
        summaryPage.clickConfirmAndContinue();

        taskListPage.isTaskStatusInProgress("DraftGovernancePlan");

        taskListPage.selectDraftGovernancePlanFromTaskList();

        summaryPage
            .MarkAsComplete()
            .clickConfirmAndContinue();

        taskListPage.isTaskStatusIsCompleted("DraftGovernancePlan");
    });
});