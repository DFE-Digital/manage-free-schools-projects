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

        cy.excuteAccessibilityTests();

        schoolDetailsPage.verifySchoolDetailsElementsVisible(project.schoolName);



        Logger.log("Test that submitting a blank form results in all validation errors displaying (except faith type)");
        schoolDetailsPage.clearSchoolNameField();

        schoolDetailsPage.selectSaveAndContinue();

        cy.excuteAccessibilityTests();

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

        cy.excuteAccessibilityTests();

        Logger.log("Test that submitting a form with all fields complete apart from Faith type when Ethos selected results in error summary and error");

        schoolDetailsPage.selectFaithEthos();

        schoolDetailsPage.selectSaveAndContinue();

        schoolDetailsPage.verifyFaithTypeErrorSummaryAndErrorVisible();

        cy.excuteAccessibilityTests();

        Logger.log("Test that entering all numbers into schoolname field fails");

        schoolDetailsPage.clearSchoolNameField();

        schoolDetailsPage.enterNegTestAllNumbersSchoolNameField();

        schoolDetailsPage.selectFaithTypeGreekOrthodox();

        schoolDetailsPage.selectSaveAndContinue();

        cy.excuteAccessibilityTests();

        schoolDetailsPage.verifyNegTestAllNumbersOrAllInvalidSpecialCharsErrorSummaryAndError();

        Logger.log("Test that entering only disallowed special chars into schoolname field fails");

        schoolDetailsPage.clearSchoolNameField();

        schoolDetailsPage.enterNegTestAllInvalidSpecialCharsSchoolNameField();

        schoolDetailsPage.selectSaveAndContinue();

        cy.excuteAccessibilityTests();

        schoolDetailsPage.verifyNegTestAllNumbersOrAllInvalidSpecialCharsErrorSummaryAndError();

        Logger.log("Test that entering a mixture of letters and disallowed special chars into schoolname field fails");

        schoolDetailsPage.clearSchoolNameField();

        schoolDetailsPage.enterNegTestMixOfLettersAndInvalidSpecialCharsSchoolNameField();

        schoolDetailsPage.selectSaveAndContinue();

        cy.excuteAccessibilityTests();

        schoolDetailsPage.verifyNegTestMixOfLetterAndInvalidSpecialCharsErrorSummaryAndError();

        Logger.log("Test that entering an SQL injection attempt in the schoolname field fails to execute");

        schoolDetailsPage.clearSchoolNameField();

        schoolDetailsPage.enterNegTestSQLInjectionAttemptSchoolNameField();

        schoolDetailsPage.selectSaveAndContinue();

        cy.excuteAccessibilityTests();
        
        schoolDetailsPage.verifyNegTestMixOfLetterAndInvalidSpecialCharsErrorSummaryAndError();


        Logger.log("Test that entering a cross-site scripting attempt in the schoolname field fails to execute");

        schoolDetailsPage.clearSchoolNameField();

        schoolDetailsPage.enterNegTestCrossSiteScriptAttemptSchoolNameField();

        schoolDetailsPage.selectSaveAndContinue();

        cy.excuteAccessibilityTests();
        
        schoolDetailsPage.verifyNegTestMixOfLetterAndInvalidSpecialCharsErrorSummaryAndError();

        Logger.log("Test that entering more than 100 chars in the schoolname fields fails");

        schoolDetailsPage.clearSchoolNameField();

        schoolDetailsPage.enterNegTestMoreThanOneHundredCharsSchoolNameField();

        schoolDetailsPage.selectSaveAndContinue();

        cy.excuteAccessibilityTests();

        schoolDetailsPage.verifyNegTestMoreThanOneHundredCharsSchoolName();

        Logger.log("Test that entering a school name with all VALID SPECIAL CHARS in schoolname field passes");

        schoolDetailsPage.clearSchoolNameField();

        schoolDetailsPage.enterValidSpecialCharsSchoolNameField();

        schoolDetailsPage.selectSaveAndContinue();

        cy.excuteAccessibilityTests();

        schoolSummaryPage.verifySchoolSummaryValidSpecialCharsElementsVisible();


        schoolSummaryPage.selectChangeCurrrentFreeSchoolNameToGoToSchoolDetails();
        // FILL OUT SCHOOLNAME WITH project.schoolname const

        //select Other Faith Type and HAMMER OTHER FIELD







        /*
        
        Logger.log("Test that entering a schoolname containing valid special chars DOES NOT TRIGGER SCHOOLNAME VALIDATION");


        Logger.log("Test that entering all numbers into Other religion field fails");
        Logger.log("Test that entering only disallowed special chars into Other religion field fails");
        Logger.log("Test that entering a mixture of letters and disallowed special chars into Other religion field fails");
        Logger.log("Test that entering more than 100 chars in the Other religion fields fails");
        Logger.log("Test that entering an SQL injection attack in the Other religion field fails to execute");
        Logger.log("Test that entering a cross-site scripting attack in the Other religion field fails to execute");
        Logger.log("Test that entering a Other religion containing valid special chars DOES NOT TRIGGER OTHER RELIGION VALIDATION");
*/



/*
        Logger.log("Test that then selecting faith type of Other and adding a valid Other faith and submitting form passes");

        schoolDetailsPage.selectFaithTypeOther();

        cy.excuteAccessibilityTests();

        schoolDetailsPage.enterOtherJaneFaithType();

        schoolDetailsPage.selectSaveAndContinue();

        schoolSummaryPage.verifySchoolSummaryCompleteElementsVisible(project.schoolName);

        cy.excuteAccessibilityTests();

        schoolSummaryPage.selectMarkItemAsComplete();

        schoolSummaryPage.selectConfirmAndContinue();

        taskListPage.verifySchoolMarkedAsComplete();

        cy.excuteAccessibilityTests();
*/

    });
});
