import { ProjectDetailsRequest } from "cypress/api/domain";
import projectApi from "cypress/api/projectApi";
import { RequestBuilder } from "cypress/api/requestBuilder";
import { Logger } from "cypress/common/logger";
import datesDetailsPage from "cypress/pages/datesDetailsPage";
import datesSummaryPage from "cypress/pages/datesSummaryPage";
import projectOverviewPage from "cypress/pages/projectOverviewPage";
import taskListPage from "cypress/pages/taskList";

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
                cy.visit(`/projects/${project.projectId}/overview`);
            });
    });

    it("Should successfully set project dates", () => {
        Logger.log("Clicking on Task list tab");
        projectOverviewPage.selectTaskListTab();

        cy.excuteAccessibilityTests();
        cy.checkA11y();

        Logger.log("Selecting Dates link from Tasklist");
        taskListPage.selectDatesFromTaskList();

        cy.excuteAccessibilityTests();
        cy.checkA11y();

        Logger.log("Checking Dates Summary page elements present");
        datesSummaryPage.verifyDatesSummaryElementsVisible();

        Logger.log("Selecting first Change link from first 'Pre-opening' line");
        datesSummaryPage.selectChangePreopeningToGoToDatesDetails();

        cy.excuteAccessibilityTests();
        cy.checkA11y();

        Logger.log("Attempting to save Dates Details page with no values");
        datesDetailsPage.selectSaveAndContinueButton()
        
        cy.excuteAccessibilityTests();
        cy.checkA11y();

        Logger.log("Check we get the correct validation messages coming back when no data entered");
        datesDetailsPage.verifyValidationMessagesWhenNoDataSet();

        Logger.log("Attempting to Reload page and clear controls");
        cy.reload();
        datesDetailsPage.clearTextInControls();

        Logger.log("Entering in exceptional date format to check correct validation messages");
        datesDetailsPage.enterInvalidDateFormatInEditDatesPage();

        Logger.log("Submitting invalid date formats");
        datesDetailsPage.selectSaveAndContinueButton();

        cy.excuteAccessibilityTests();
        cy.checkA11y();

        Logger.log("Verify we get correct validation messages for exceptional date formats");
        datesDetailsPage.verifyValidationMessagesWhenInvalidDateFormatEntered();

        Logger.log("Attempting to Reload page and clear controls");
        cy.reload();
        datesDetailsPage.clearTextInControls();

        Logger.log("Attempting to add invalid dates in correct format");
        datesDetailsPage.enterInvalidDateInEditDatesPage(); 

        Logger.log("Submitting invalid dates");
        datesDetailsPage.selectSaveAndContinueButton();

        cy.excuteAccessibilityTests();
        cy.checkA11y();

        Logger.log("Verify we get correct validation messages for exceptional days in dates");
        datesDetailsPage.verifyValidationMessagesWhenInvalidDateEntered();

        Logger.log("Attempting to Reload page and clear controls");
        cy.reload();
        datesDetailsPage.clearTextInControls();

        Logger.log("Verify we get correct validation messages for exceptional year in academic year to field");
        datesDetailsPage.enterInvalidAcademicYearEndDateInEditDatesPage();

        cy.reload();
        datesDetailsPage.clearTextInControls();

        Logger.log("Attempting to add valid dates in correct format");
        datesDetailsPage.enterValidDatesInEditDatesPage();

        Logger.log("Submitting valid dates");
        datesDetailsPage.selectSaveAndContinueButton();

        cy.excuteAccessibilityTests();
        cy.checkA11y();

        Logger.log("Verify Dates Summary Page Complete Elements Visible");
        datesSummaryPage.verifyDatesSummaryCompleteElementsVisible();

    });
});
