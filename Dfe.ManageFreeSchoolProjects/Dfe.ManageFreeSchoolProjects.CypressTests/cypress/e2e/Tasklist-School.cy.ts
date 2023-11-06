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

        Logger.log("Clicking on Task list tab");
        projectOverviewPage.selectTaskListTab();

        cy.executeAccessibilityTests();

        Logger.log("Selecting School link from Tasklist");
        taskListPage.selectSchoolFromTaskList();

        cy.executeAccessibilityTests();

        Logger.log("Checking School Summary page elements present");
        schoolSummaryPage.verifySchoolSummaryElementsVisible(project.schoolName);

        Logger.log("Selecting first Change link from first 'Current free school name' line");
        schoolSummaryPage.selectChangeCurrrentFreeSchoolNameToGoToSchoolDetails();

        cy.executeAccessibilityTests();

        schoolDetailsPage.verifySchoolDetailsElementsVisible(project.schoolName);



        Logger.log("Test that submitting a blank form results in all validation errors displaying (except faith type)");
        schoolDetailsPage.clearSchoolNameField();

        schoolDetailsPage.selectSaveAndContinue();

        cy.executeAccessibilityTests();

        schoolDetailsPage.verifyValidationSummaryAndErrorsVisible();



        Logger.log("Test that submitting a form with all fields complete apart from Faith type when Designation selected results in error summary and error");
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

        schoolDetailsPage.verifyFaithTypeErrorSummaryAndErrorVisible();

        cy.executeAccessibilityTests();

        Logger.log("Test that submitting a form with all fields complete apart from Faith type when Ethos selected results in error summary and error");

        schoolDetailsPage.selectFaithEthos();

        schoolDetailsPage.selectSaveAndContinue();

        schoolDetailsPage.verifyFaithTypeErrorSummaryAndErrorVisible();

        cy.executeAccessibilityTests();

        schoolDetailsPage.selectFaithTypeGreekOrthodox();

        Logger.log("Test that entering only disallowed special chars into schoolname field fails");

        schoolDetailsPage.clearSchoolNameField();

        schoolDetailsPage.enterNegTestAllInvalidSpecialCharsSchoolNameField();

        schoolDetailsPage.selectSaveAndContinue();

        cy.executeAccessibilityTests();

        schoolDetailsPage.enterNegTestMixOfLettersAndInvalidSpecialCharsSchoolNameField();

        Logger.log("Test that entering a mixture of letters and disallowed special chars into schoolname field fails");

        schoolDetailsPage.clearSchoolNameField();

        schoolDetailsPage.enterNegTestMixOfLettersAndInvalidSpecialCharsSchoolNameField();

        schoolDetailsPage.selectSaveAndContinue();

        cy.executeAccessibilityTests();

        schoolDetailsPage.verifyNegTestMixOfLetterAndInvalidSpecialCharsErrorSummaryAndError();

        Logger.log("Test that entering an SQL injection attempt in the schoolname field fails to execute");

        schoolDetailsPage.clearSchoolNameField();

        schoolDetailsPage.enterNegTestSQLInjectionAttemptSchoolNameField();

        schoolDetailsPage.selectSaveAndContinue();

        cy.executeAccessibilityTests();
        
        schoolDetailsPage.verifyNegTestMixOfLetterAndInvalidSpecialCharsErrorSummaryAndError();


        Logger.log("Test that entering a cross-site scripting attempt in the schoolname field fails to execute");

        schoolDetailsPage.clearSchoolNameField();

        schoolDetailsPage.enterNegTestCrossSiteScriptAttemptSchoolNameField();

        schoolDetailsPage.selectSaveAndContinue();

        cy.executeAccessibilityTests();
        
        schoolDetailsPage.verifyNegTestMixOfLetterAndInvalidSpecialCharsErrorSummaryAndError();

        Logger.log("Test that entering more than 100 chars in the schoolname fields fails and gives correct validation");

        schoolDetailsPage.clearSchoolNameField();

        schoolDetailsPage.enterNegTestMoreThanOneHundredCharsSchoolNameField();

        schoolDetailsPage.selectSaveAndContinue();

        cy.executeAccessibilityTests();

        schoolDetailsPage.verifyNegTestMoreThanOneHundredCharsSchoolName();

        Logger.log("Test that entering a school name with all VALID SPECIAL CHARS in schoolname field passes");

        schoolDetailsPage.clearSchoolNameField();

        schoolDetailsPage.enterValidSpecialCharsSchoolNameField();

        schoolDetailsPage.selectSaveAndContinue();

        cy.executeAccessibilityTests();

        schoolSummaryPage.verifySchoolSummaryValidSpecialCharsElementsVisible();

        Logger.log("Test that selecting 'Other Religion' And Leaving 'Other religion textfield blank gives correct validation'");

        schoolSummaryPage.selectChangeCurrrentFreeSchoolNameToGoToSchoolDetails();

        schoolDetailsPage.clearSchoolNameField();

        schoolDetailsPage.enterSchoolNameField(project.schoolName);

        schoolDetailsPage.selectFaithTypeOther();

        schoolDetailsPage.selectSaveAndContinue();

        cy.executeAccessibilityTests();

        schoolDetailsPage.verifyOtherFaithTypeErrorSummaryAndErrorVisible();

        Logger.log("Test that selecting 'Other Religion' And entering all numbers in 'Other religion' textfield gives correct validation'");

        schoolDetailsPage.enterNegTestAllNumbersOtherFaithType();

        schoolDetailsPage.selectSaveAndContinue();

        cy.executeAccessibilityTests();

        schoolDetailsPage.verifyAllNumbersOrSpecialCharsOtherFaithTypeErrorSummaryAndErrorVisible();

        Logger.log("Test that selecting 'Other Religion' And entering all special chars in 'Other religion' textfield gives correct validation'");

        schoolDetailsPage.clearOtherFaithTypeField();

        schoolDetailsPage.enterNegTestAllSpecialCharsOtherFaithType();

        schoolDetailsPage.selectSaveAndContinue();

        cy.executeAccessibilityTests();

        schoolDetailsPage.verifyAllNumbersOrSpecialCharsOtherFaithTypeErrorSummaryAndErrorVisible();

        Logger.log("Test that selecting 'Other Religion' And entering SOME special chars AS WELL AS VALID CHARS in 'Other religion' textfield gives correct validation'");

        schoolDetailsPage.clearOtherFaithTypeField();

        schoolDetailsPage.enterNegTestSomeValidSomeSpecialCharsOtherFaithType();

        schoolDetailsPage.selectSaveAndContinue();

        cy.executeAccessibilityTests();

        schoolDetailsPage.verifyAllNumbersOrSpecialCharsOtherFaithTypeErrorSummaryAndErrorVisible();

        Logger.log("Test that entering an SQL injection attempt in 'Other religion' textfield gives correct validation and SQL injection attempt fails to execute'");

        schoolDetailsPage.clearOtherFaithTypeField();

        schoolDetailsPage.enterNegTestSQLInjectionAttemptOtherFaithType();

        schoolDetailsPage.selectSaveAndContinue();

        cy.executeAccessibilityTests();

        schoolDetailsPage.verifyAllNumbersOrSpecialCharsOtherFaithTypeErrorSummaryAndErrorVisible();

        Logger.log("Test that entering a Cross-site scripting attack attempt in 'Other religion' textfield gives correct validation and Cross-site scripting attack attempt fails to execute'");

        schoolDetailsPage.clearOtherFaithTypeField();

        schoolDetailsPage.enterNegTestCrossSiteScriptAttackAttemptOtherFaithType();

        schoolDetailsPage.selectSaveAndContinue();

        cy.executeAccessibilityTests();

        schoolDetailsPage.verifyAllNumbersOrSpecialCharsOtherFaithTypeErrorSummaryAndErrorVisible();

        Logger.log("Test that entering an SQL injection attempt in 'Other religion' textfield gives correct validation and SQL injection attempt fails to execute'");

        schoolDetailsPage.clearOtherFaithTypeField();

        schoolDetailsPage.enterNegTestMoreThanOneHundredCharsAttemptOtherFaithType();

        schoolDetailsPage.selectSaveAndContinue();

        cy.executeAccessibilityTests();

        Logger.log("Test that entering more than 100 chars in the 'Other religion' textfield fails and gives correct validation");

        schoolDetailsPage.verifyMoreThanOneHundredCharsOtherFaithTypeErrorSummaryAndErrorVisible();

        schoolDetailsPage.clearOtherFaithTypeField();

        schoolDetailsPage.enterOtherFaithType();

        schoolDetailsPage.selectSaveAndContinue();

        schoolSummaryPage.verifySchoolSummaryCompleteElementsVisible(project.schoolName);

        cy.executeAccessibilityTests();

        schoolSummaryPage.selectMarkItemAsComplete();

        schoolSummaryPage.selectConfirmAndContinue();

        taskListPage.verifySchoolMarkedAsComplete();

    });
});
