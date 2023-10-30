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

        Logger.log("Selecting Trust link from Tasklist");
        taskListPage.selectTrustFromTaskList();

        cy.excuteAccessibilityTests();

        Logger.log("Checking Trust Summary page elements present");
        trustSummaryPage.verifyTrustSummaryElementsVisible();

        Logger.log("Selecting first Change link from first 'Pre-opening' line");
        trustSummaryPage.selectChangePreopeningToGoToDatesDetails();

        cy.excuteAccessibilityTests();


        Logger.log("Attempting to save Dates Details page with no values");
        trustDetailsPage.selectSaveAndContinueButton()
        
        cy.excuteAccessibilityTests();

        Logger.log("Check we get the correct validation messages coming back when no data entered");
        trustDetailsPage.verifyValidationMessagesWhenNoDataSet();

        Logger.log("Attempting to Reload page and clear controls");
        cy.reload();
        trustDetailsPage.clearTextInControls();

        Logger.log("Entering in exceptional date format to check correct validation messages");
        trustDetailsPage.enterInvalidDateFormatInEditDatesPage();

        Logger.log("Submitting invalid date formats");
        trustDetailsPage.selectSaveAndContinueButton();

        cy.excuteAccessibilityTests();

        Logger.log("Verify we get correct validation messages for exceptional date formats");
        trustDetailsPage.verifyValidationMessagesWhenInvalidDateFormatEntered();

        Logger.log("Attempting to Reload page and clear controls");
        cy.reload();
        trustDetailsPage.clearTextInControls();

        Logger.log("Attempting to add invalid dates in correct format");
        trustDetailsPage.enterInvalidDateInEditDatesPage(); 

        Logger.log("Submitting invalid dates");
        trustDetailsPage.selectSaveAndContinueButton();

        cy.excuteAccessibilityTests();

        Logger.log("Verify we get correct validation messages for exceptional days in dates");
        trustDetailsPage.verifyValidationMessagesWhenInvalidDateEntered();

        Logger.log("Attempting to Reload page and clear controls");
        cy.reload();
        trustDetailsPage.clearTextInControls();

        Logger.log("Verify we get correct validation messages for exceptional year in academic year START field");
        trustDetailsPage.enterInvalidAcademicYearStartDateInEditDatesPage();
        trustDetailsPage.selectSaveAndContinueButton();
        trustDetailsPage.verifyInvalidAcademicStartYearDate();

        Logger.log("Attempting to Reload page and clear controls");
        cy.reload();
        trustDetailsPage.clearTextInControls();

        Logger.log("Verify we get correct validation messages for exceptional year in academic yearTo field");
        trustDetailsPage.enterInvalidAcademicYearEndDateInEditDatesPage();
        trustDetailsPage.selectSaveAndContinueButton();
        trustDetailsPage.verifyInvalidAcademicEndYearDate();

        Logger.log("Attempting to Reload page and clear controls");
        cy.reload();
        trustDetailsPage.clearTextInControls();

        Logger.log("Attempting to add valid dates in correct format");
        trustDetailsPage.enterValidDatesInEditDatesPage();

        Logger.log("Submitting valid dates");
        trustDetailsPage.selectSaveAndContinueButton();

        cy.excuteAccessibilityTests();

        Logger.log("Verify Dates Summary Page Complete Elements Visible");
        trustSummaryPage.verifyDatesSummaryCompleteElementsVisible();

        trustSummaryPage.selectMarkItemAsComplete();
        trustSummaryPage.selectConfirmAndContinue();

        taskListPage.verifyDatesMarkedAsComplete();

    });
});
