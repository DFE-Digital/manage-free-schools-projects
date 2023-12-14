import { ProjectDetailsRequest } from "cypress/api/domain";
import projectApi from "cypress/api/projectApi";
import { RequestBuilder } from "cypress/api/requestBuilder";
import { Logger } from "cypress/common/logger";
import datesDetailsPage from "cypress/pages/datesDetailsPage";
import projectOverviewPage from "cypress/pages/projectOverviewPage";
import summaryPage from "cypress/pages/task-summary-base";
import taskListPage from "cypress/pages/taskListPage";

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
                    .selectDatesFromTaskList();
        
        Logger.log("Confirm empty dates");
        summaryPage
            .schoolNameIs(project.schoolName)
            .titleIs("Dates")
            .inOrder()
            .summaryShows("Entry into pre-opening").IsEmpty().HasChangeLink()
            .summaryShows("Provisional opening date agreed with trust").IsEmpty().HasChangeLink()
            .summaryShows("Opening academic year").IsEmpty().HasChangeLink()
            .isNotMarkedAsComplete();

        cy.executeAccessibilityTests();

        Logger.log("Go back to task list");
        summaryPage.clickBack();

        cy.executeAccessibilityTests();

        Logger.log("Confirm not started and open Dates");
        taskListPage.isTaskStatusIsNotStarted("Dates")
            .selectDatesFromTaskList();
        
        cy.executeAccessibilityTests();

        Logger.log("Check confirm puts project in In Progress");
        summaryPage.clickConfirmAndContinue();
        
        cy.executeAccessibilityTests();

        taskListPage.isTaskStatusInProgress("Dates")
            .selectDatesFromTaskList();
        
        cy.executeAccessibilityTests();


        summaryPage.clickChange();

        cy.executeAccessibilityTests();

        datesDetailsPage.selectSaveAndContinueButton();

        Logger.log("Check we get the correct validation messages coming back when no data entered");
        datesDetailsPage.verifyValidationMessagesWhenNoDataSet(project.schoolName);

        Logger.log("Attempting to Reload page and clear controls");
        cy.reload();
        datesDetailsPage.clearTextInControls();

        Logger.log("Entering in exceptional date format to check correct validation messages");
        datesDetailsPage.enterInvalidDateFormatInEditDatesPage();

        Logger.log("Submitting invalid date formats");
        datesDetailsPage.selectSaveAndContinueButton();

        cy.executeAccessibilityTests();

        Logger.log("Verify we get correct validation messages for exceptional date formats");
        datesDetailsPage.verifyValidationMessagesWhenInvalidDateFormatEntered(project.schoolName);

        Logger.log("Attempting to Reload page and clear controls");
        cy.reload();
        datesDetailsPage.clearTextInControls();

        Logger.log("Attempting to add invalid dates in correct format");
        datesDetailsPage.enterInvalidDateInEditDatesPage(); 

        Logger.log("Submitting invalid dates");
        datesDetailsPage.selectSaveAndContinueButton();

        cy.executeAccessibilityTests();

        Logger.log("Verify we get correct validation messages for exceptional days in dates");
        datesDetailsPage.verifyValidationMessagesWhenInvalidDateEntered(project.schoolName);

        Logger.log("Attempting to Reload page and clear controls");
        cy.reload();
        datesDetailsPage.clearTextInControls();

        Logger.log("Verify we get correct validation messages for exceptional year in academic year START field");
        datesDetailsPage.enterInvalidAcademicYearStartDateInEditDatesPage();
        datesDetailsPage.selectSaveAndContinueButton();
        datesDetailsPage.verifyInvalidAcademicStartYearDate();

        Logger.log("Attempting to Reload page and clear controls");
        cy.reload();
        datesDetailsPage.clearTextInControls();

        Logger.log("Verify we get correct validation messages for exceptional year in academic yearTo field");
        datesDetailsPage.enterInvalidAcademicYearEndDateInEditDatesPage();
        datesDetailsPage.selectSaveAndContinueButton();
        datesDetailsPage.verifyInvalidAcademicEndYearDate();

        Logger.log("Attempting to Reload page and clear controls");
        cy.reload();
        datesDetailsPage.clearTextInControls();

        Logger.log("Attempting to add valid dates in correct format");
        datesDetailsPage.enterValidDatesInEditDatesPage();

        Logger.log("Submitting valid dates");
        datesDetailsPage.selectSaveAndContinueButton();

        cy.executeAccessibilityTests();

        Logger.log("Confirm Dates Summary Page Complete");
        summaryPage
            .schoolNameIs(project.schoolName)
            .titleIs("Dates")
            .inOrder()
            .summaryShows("Entry into pre-opening").HasValue("28/2/2025").HasChangeLink()
            .summaryShows("Provisional opening date agreed with trust").HasValue("28/2/2025").HasChangeLink()
            .summaryShows("Opening academic year").HasValue("2025/26").HasChangeLink()
            .isNotMarkedAsComplete();

        cy.executeAccessibilityTests();
      

       summaryPage
            .MarkAsComplete()
            .clickConfirmAndContinue();

        taskListPage.isTaskStatusIsCompleted("Dates");

    });
});
