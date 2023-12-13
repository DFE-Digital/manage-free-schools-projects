import { ProjectDetailsRequest } from "cypress/api/domain";
import projectApi from "cypress/api/projectApi";
import { RequestBuilder } from "cypress/api/requestBuilder";
import { Logger } from "cypress/common/logger";
import datesDetailsPage from "cypress/pages/datesDetailsPage";
import datesSummaryPage from "cypress/pages/datesSummaryPage";
import projectOverviewPage from "cypress/pages/projectOverviewPage";
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
        taskListPage.selectDatesFromTaskList();

        cy.executeAccessibilityTests();

        Logger.log("Checking Dates Summary page elements present");
        datesSummaryPage.verifyDatesSummaryElementsVisible(project.schoolName);

        Logger.log("Selecting first Change link from first 'Pre-opening' line");
        datesSummaryPage.selectChangePreopeningToGoToDatesDetails();

        cy.executeAccessibilityTests();


        Logger.log("Attempting to save Dates Details page with no values");
        datesDetailsPage.selectSaveAndContinueButton()
        
        cy.executeAccessibilityTests();

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

        Logger.log("Verify Dates Summary Page Complete Elements Visible");
        datesSummaryPage.verifyDatesSummaryCompleteElementsVisible();

        datesSummaryPage.selectMarkItemAsComplete();
        datesSummaryPage.selectConfirmAndContinue();

        taskListPage.isTaskStatusIsCompleted("Dates");

    });
});
