import { ProjectDetailsRequest } from "cypress/api/domain";
import projectApi from "cypress/api/projectApi";
import { RequestBuilder } from "cypress/api/requestBuilder";
import { Logger } from "cypress/common/logger";
import trustDetailsPage from "cypress/pages/trustDetailsPage";
import trustSummaryPage from "cypress/pages/trustSummaryPage";
import projectOverviewPage from "cypress/pages/projectOverviewPage";
import taskListPage from "cypress/pages/taskListPage";
import confirmTrustPage from "cypress/pages/confirmTrustPage";
import schoolSummaryPage from "cypress/pages/schoolSummaryPage";
import schoolDetailsPage from "cypress/pages/schoolDetailsPage";

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

    it("Should successfully set Tasklist-school information", () => {
        const validTrustId = "TR03446";

        Logger.log("Clicking on Task list tab");
        projectOverviewPage.selectTaskListTab();

        cy.excuteAccessibilityTests();

        Logger.log("Selecting School link from Tasklist");
        taskListPage.selectSchoolFromTaskList();

        cy.excuteAccessibilityTests();

        Logger.log("Checking School Summary page elements present");
        schoolSummaryPage.verifySchoolSummaryElementsVisible(project.schoolName);

        Logger.log("Selecting first Change link from first 'Current free school name' line");
        schoolSummaryPage.selectChangeCurrrentFreeSchoolNameToGoToSchoolDetails();

        cy.excuteAccessibilityTests

        schoolDetailsPage.verifySchoolDetailsElementsVisible(project.schoolName);



        Logger.log("Test that submitting a blank form results in all validation errors displaying (except faith type)");
        schoolDetailsPage.clearSchoolNameField();

        schoolDetailsPage.selectSaveAndContinue();

        cy.excuteAccessibilityTests

        schoolDetailsPage.verifyValidationSummaryAndErrorsVisible();



        Logger.log("Test that submitting a form with all fields complete apart from Faith type when Ethos or Designation selected results in error summary and error");
        schoolDetailsPage.enterSchoolNameField(project.schoolName);

        schoolDetailsPage.selectMainstream();

        schoolDetailsPage.selectSecondary();

        schoolDetailsPage.enterAgeRangeFromEleven();

        schoolDetailsPage.enterAgeRangeToSixteen();

        schoolDetailsPage.selectGenderMixed();

        schoolDetailsPage.selectNurseryNo();

        schoolDetailsPage.selectSixthFormYes();

        schoolDetailsPage.selectFaithDesignation();

        schoolDetailsPage.selectSaveAndContinue();

        // UNCOMMENT THIS AFTER ARIA-ID BUG FIX
        cy.excuteAccessibilityTests();

/*
        Logger.log("Attempting to save 'Search for a trust by TRN' with no values");
        trustDetailsPage.selectSaveAndContinue()
        
        cy.excuteAccessibilityTests();

        Logger.log("Check we get the correct validation messages coming back when no data entered");
        trustDetailsPage.verifyValidationMessagesWhenNoDataSet();

        Logger.log("Attempting to save 'Search for a trust by TRN' with an invalid string e.g. 'POTATO'");
        trustDetailsPage.enterInvalidTRNStringInTRNPage();

        trustDetailsPage.selectSaveAndContinue();

        cy.excuteAccessibilityTests();

        trustDetailsPage.verifyValidationMessagesWhenInvalidTRNFormatEntered();

        Logger.log("Attempting to save 'Search for a trust by TRN' with an invalid string with spaces e.g. 'P O T A T O'");
        trustDetailsPage.enterInvalidTRNStringWithSpacesInTRNPage();

        trustDetailsPage.selectSaveAndContinue();

        cy.excuteAccessibilityTests();

        trustDetailsPage.verifyValidationMessagesWhenTRNTooLongEntered();

        Logger.log("Attempting to save 'Search for a trust by TRN' with an invalid numerical string e.g. '1234567'");
        trustDetailsPage.enterInvalidTRNNumbersStringInTRNPage();

        trustDetailsPage.selectSaveAndContinue();

        cy.excuteAccessibilityTests();

        trustDetailsPage.verifyValidationMessagesWhenInvalidTRNFormatEntered();

        Logger.log("Attempting to save 'Search for a trust by TRN' with a TRN that doesn't exist");
        trustDetailsPage.enterNonExistentTrustIdInTRNPage();

        trustDetailsPage.selectSaveAndContinue();

        cy.excuteAccessibilityTests();

        trustDetailsPage.verifyValidationMessagesWhenNonExistentTRNEntered();



        Logger.log("Attempting to save 'Search for a trust by TRN' with SQL injection attempt");
        trustDetailsPage.enterSQLInjectionAttemptInTRNPage();

        trustDetailsPage.selectSaveAndContinue();

        cy.excuteAccessibilityTests();

        trustDetailsPage.verifyValidationMessagesWhenTRNTooLongEntered();



        Logger.log("Attempting to save 'Search for a trust by TRN' with Cross-site Scripting Attack");
        trustDetailsPage.enterCrossSiteScriptingAttemptInTRNPage();

        trustDetailsPage.selectSaveAndContinue();

        cy.excuteAccessibilityTests();

        trustDetailsPage.verifyValidationMessagesWhenTRNTooLongEntered();



        Logger.log("Attempting to save 'Search for a trust by TRN' with valid Trust ID that exists");
        trustDetailsPage.enterValidTrustId(validTrustId);

        trustDetailsPage.selectSaveAndContinue();

        cy.excuteAccessibilityTests();

        confirmTrustPage.verifyConfirmTrustElementsVisible(project.schoolName, validTrustId);

        confirmTrustPage.selectSaveAndContinue();

        cy.excuteAccessibilityTests();

        trustSummaryPage.verifyTrustSummaryCompleteElementsVisible(project.schoolName, validTrustId);

        trustSummaryPage.selectMarkItemAsComplete();

        trustSummaryPage.selectConfirmAndContinue();

        cy.excuteAccessibilityTests();

        taskListPage.verifyTrustMarkedAsComplete();

/*
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

        taskListPage.verifyTrustMarkedAsComplete();
*/
    });
});
