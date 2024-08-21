import { ProjectDetailsRequest } from "cypress/api/domain";
import projectApi from "cypress/api/projectApi";
import { RequestBuilder } from "cypress/api/requestBuilder";
import { Logger } from "cypress/common/logger";
import datesDetailsPage from "cypress/pages/datesDetailsPage";
import projectOverviewPage from "cypress/pages/projectOverviewPage";
import summaryPage from "cypress/pages/task-summary-base";
import taskListPage from "cypress/pages/taskListPage";
import validationComponent from "cypress/pages/validationComponent";
import projectStatusPage from "../../../pages/project-status/projectStatusPage";

describe("Testing project overview", () => {
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

    it("Should successfully set project dates", () => {
        cy.executeAccessibilityTests();

        Logger.log("Selecting Dates link from Tasklist");

        taskListPage.isTaskStatusIsNotStarted("Dates")
            .clickChangeProjectStatus()

        projectStatusPage
            .selectCancelled()
            .addCancelledYear("1", "1", "2000")
            .clickSaveAndContine()

        taskListPage.isTaskStatusIsNotStarted("Dates")
            .selectDatesFromTaskList()

        Logger.log("Confirm dates");
        summaryPage
            .schoolNameIs(project.schoolName)
            .titleIs("Dates")
            .inOrder()
            .summaryShows("Year the project was cancelled").HasValue("2000").HasChangeLink()
            .summaryShows("Entry into pre-opening").IsEmpty().HasChangeLink()
            .summaryShows("Provisional opening date agreed with trust").IsEmpty().HasChangeLink()
            .isNotMarkedAsComplete();

        cy.executeAccessibilityTests();

        summaryPage.clickChange();

        Logger.log("Check all the fields are optional");
        datesDetailsPage
            .titleIs("Edit Dates")
            .schoolNameIs(project.schoolName)
            .clickContinue();

        summaryPage.clickChange();

        Logger.log("Checking validation");
        datesDetailsPage
            .addCancelledDate("1", "1", "error")
            .withEntryIntoPreOpening("33", "", "")
            .withProvisionalOpeningDateAgreedWithTrust("44", "", "")
            .clickContinue();

        validationComponent
            .hasValidationError("Enter a year in the correct format")
            .hasValidationError("Entry into pre-opening must include a month and year")
            .hasValidationError("Provisional opening date agreed with trust must include a month and year");

        cy.executeAccessibilityTests();

        Logger.log("Add new values");
        datesDetailsPage
            .schoolNameIs(project.schoolName)
            .addCancelledDate("12", "2","2049")
            .withEntryIntoPreOpening("10", "08", "2025")
            .withProvisionalOpeningDateAgreedWithTrust("12", "09", "2026")
            .clickContinue();

        summaryPage
            .inOrder()
            .summaryShows("Year the project was cancelled").HasValue("12 February 2049").HasChangeLink()
            .summaryShows("Entry into pre-opening").HasValue("10 August 2025").HasChangeLink()
            .summaryShows("Provisional opening date agreed with trust").HasValue("12 September 2026").HasChangeLink()
            .clickChange();

        Logger.log("Change values");
        datesDetailsPage
            .withEntryIntoPreOpening("11", "08", "2026")
            .withProvisionalOpeningDateAgreedWithTrust("13", "09", "2027")
            .clickContinue();

        summaryPage
            .inOrder()
            .summaryShows("Year the project was cancelled").HasValue("2050").HasChangeLink()
            .summaryShows("Entry into pre-opening").HasValue("11 August 2026").HasChangeLink()
            .summaryShows("Provisional opening date agreed with trust").HasValue("13 September 2027").HasChangeLink()

        Logger.log("Should update the task status");
        summaryPage.clickConfirmAndContinue();

        taskListPage.isTaskStatusInProgress("Dates").selectDatesFromTaskList();

        summaryPage
            .MarkAsComplete()
            .clickConfirmAndContinue();

        taskListPage.isTaskStatusIsCompleted("Dates");

        cy.visit(`/projects/${project.projectId}/overview`);

        projectOverviewPage
            .hasDateOfEntryIntoPreopening("11 August 2026")
            .hasProvisionalOpeningDateAgreedWithTrust("13 September 2027");
    });
});
