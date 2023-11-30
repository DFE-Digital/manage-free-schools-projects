import { ProjectDetailsRequest } from "cypress/api/domain";
import projectApi from "cypress/api/projectApi";
import { RequestBuilder } from "cypress/api/requestBuilder";
import { Logger } from "cypress/common/logger";
import projectOverviewPage from "cypress/pages/projectOverviewPage";
import taskListPage from "cypress/pages/taskListPage";
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
                cy.visit(`/projects/${project.projectId}/tasks`);
            });
    });

    it("Should successfully set Tasklist-school information", () => {

        const schoolWithAllValidSpecialChars = "St Dunstan's Abbey, (Plymouth)";

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
        schoolDetailsPage.clearSchoolNameField()
            .selectSaveAndContinue()
            .verifyValidationSummaryAndErrorsVisible();

        cy.executeAccessibilityTests();

        Logger.log("Test that submitting a form with all fields complete apart from Faith type when Designation selected results in error summary and error");
        schoolDetailsPage.enterSchoolNameField(project.schoolName)
            .selectMainstream()
            .selectSecondary()
            .enterAgeRangeFrom("11")
            .enterAgeRangeTo("16")
            .selectGenderMixed()
            .selectNurseryNo()
            .selectSixthFormYes()
            .selectFaithDesignation()
            .selectSaveAndContinue()
            .verifyFaithTypeErrorSummaryAndErrorVisible();

        cy.executeAccessibilityTests();

        Logger.log("Test that submitting a form with all fields complete apart from Faith type when Ethos selected results in error summary and error");

        schoolDetailsPage.selectFaithEthos()
            .selectSaveAndContinue()
            .verifyFaithTypeErrorSummaryAndErrorVisible();

        cy.executeAccessibilityTests();

        schoolDetailsPage.selectFaithTypeGreekOrthodox();

        Logger.log("Test that entering only disallowed special chars into schoolname field fails");

        schoolDetailsPage.clearSchoolNameField()
            .enterNegTestAllInvalidSpecialCharsSchoolNameField()
            .selectSaveAndContinue();

        cy.executeAccessibilityTests();

        schoolDetailsPage.enterNegTestMixOfLettersAndInvalidSpecialCharsSchoolNameField();

        Logger.log("Test that entering a mixture of letters and disallowed special chars into schoolname field fails");

        schoolDetailsPage.clearSchoolNameField()
            .enterNegTestMixOfLettersAndInvalidSpecialCharsSchoolNameField()
            .selectSaveAndContinue();

        cy.executeAccessibilityTests();

        schoolDetailsPage.verifyNegTestMixOfLetterAndInvalidSpecialCharsErrorSummaryAndError();

        Logger.log("Test that entering an SQL injection attempt in the schoolname field fails to execute");

        schoolDetailsPage.clearSchoolNameField()
            .enterNegTestSQLInjectionAttemptSchoolNameField()
            .selectSaveAndContinue();

        cy.executeAccessibilityTests();
        
        schoolDetailsPage.verifyNegTestMixOfLetterAndInvalidSpecialCharsErrorSummaryAndError();

        Logger.log("Test that entering a cross-site scripting attempt in the schoolname field fails to execute");

        schoolDetailsPage.clearSchoolNameField()
            .enterNegTestCrossSiteScriptAttemptSchoolNameField()
            .selectSaveAndContinue();

        cy.executeAccessibilityTests();
        
        schoolDetailsPage.verifyNegTestMixOfLetterAndInvalidSpecialCharsErrorSummaryAndError();

        Logger.log("Test that entering more than 100 chars in the schoolname fields fails and gives correct validation");

        schoolDetailsPage.clearSchoolNameField()
            .enterNegTestMoreThanOneHundredCharsSchoolNameField()
            .selectSaveAndContinue();

        cy.executeAccessibilityTests();

        schoolDetailsPage.verifyNegTestMoreThanOneHundredCharsSchoolName();

        Logger.log("Test that entering a school name with all VALID SPECIAL CHARS in schoolname field passes");

        schoolDetailsPage.clearSchoolNameField()
            .enterValidSpecialCharsSchoolNameField(schoolWithAllValidSpecialChars)
            .selectSaveAndContinue();

        cy.executeAccessibilityTests();

        schoolSummaryPage.verifySchoolSummaryValidSpecialCharsElementsVisible(schoolWithAllValidSpecialChars);

        Logger.log("Test that selecting 'Other Religion' And Leaving 'Other religion textfield blank gives correct validation'");

        schoolSummaryPage.selectChangeCurrrentFreeSchoolNameToGoToSchoolDetails();

        schoolDetailsPage.clearSchoolNameField()
            .enterSchoolNameField(project.schoolName)
            .selectFaithTypeOther()
            .selectSaveAndContinue();

        cy.executeAccessibilityTests();

        schoolDetailsPage.verifyOtherFaithTypeErrorSummaryAndErrorVisible();

        Logger.log("Test that selecting 'Other Religion' And entering all numbers in 'Other religion' textfield gives correct validation'");

        schoolDetailsPage.enterNegTestAllNumbersOtherFaithType()
            .selectSaveAndContinue();

        cy.executeAccessibilityTests();

        schoolDetailsPage.verifyAllNumbersOrSpecialCharsOtherFaithTypeErrorSummaryAndErrorVisible();

        Logger.log("Test that selecting 'Other Religion' And entering all special chars in 'Other religion' textfield gives correct validation'");

        schoolDetailsPage.clearOtherFaithTypeField()
            .enterNegTestAllSpecialCharsOtherFaithType()
            .selectSaveAndContinue();

        cy.executeAccessibilityTests();

        schoolDetailsPage.verifyAllNumbersOrSpecialCharsOtherFaithTypeErrorSummaryAndErrorVisible();

        Logger.log("Test that selecting 'Other Religion' And entering SOME special chars AS WELL AS VALID CHARS in 'Other religion' textfield gives correct validation'");

        schoolDetailsPage.clearOtherFaithTypeField()
            .enterNegTestSomeValidSomeSpecialCharsOtherFaithType()
            .selectSaveAndContinue();

        cy.executeAccessibilityTests();

        schoolDetailsPage.verifyAllNumbersOrSpecialCharsOtherFaithTypeErrorSummaryAndErrorVisible();

        Logger.log("Test that entering an SQL injection attempt in 'Other religion' textfield gives correct validation and SQL injection attempt fails to execute'");

        schoolDetailsPage.clearOtherFaithTypeField()
            .enterNegTestSQLInjectionAttemptOtherFaithType()
            .selectSaveAndContinue();

        cy.executeAccessibilityTests();

        schoolDetailsPage.verifyAllNumbersOrSpecialCharsOtherFaithTypeErrorSummaryAndErrorVisible();

        Logger.log("Test that entering a Cross-site scripting attack attempt in 'Other religion' textfield gives correct validation and Cross-site scripting attack attempt fails to execute'");

        schoolDetailsPage.clearOtherFaithTypeField()
            .enterNegTestCrossSiteScriptAttackAttemptOtherFaithType()
            .selectSaveAndContinue();

        cy.executeAccessibilityTests();

        schoolDetailsPage.verifyAllNumbersOrSpecialCharsOtherFaithTypeErrorSummaryAndErrorVisible();

        Logger.log("Test that entering an SQL injection attempt in 'Other religion' textfield gives correct validation and SQL injection attempt fails to execute'");

        schoolDetailsPage.clearOtherFaithTypeField()
            .enterNegTestMoreThanOneHundredCharsAttemptOtherFaithType()
            .selectSaveAndContinue();

        cy.executeAccessibilityTests();

        Logger.log("Test that entering more than 100 chars in the 'Other religion' textfield fails and gives correct validation");

        schoolDetailsPage.verifyMoreThanOneHundredCharsOtherFaithTypeErrorSummaryAndErrorVisible()
            .clearOtherFaithTypeField()
            .enterOtherFaithType()
            .selectSaveAndContinue();

        schoolSummaryPage.verifySchoolSummaryCompleteElementsVisible(project.schoolName)
            .selectMarkItemAsComplete()
            .selectConfirmAndContinue();

        taskListPage.isTaskStatusIsCompleted("School");

    });

    
    it("Should validate age range correctly", () => {

        Logger.log("Navigate to details");

        projectOverviewPage.selectTaskListTab();
        taskListPage.selectSchoolFromTaskList();
        schoolSummaryPage.selectChangeCurrrentFreeSchoolNameToGoToSchoolDetails();

        Logger.log("Set all fields valid");
        schoolDetailsPage.enterSchoolNameField(project.schoolName)
            .selectMainstream()
            .selectSecondary()
            .enterAgeRangeFrom("11")
            .enterAgeRangeTo("16")
            .selectGenderMixed()
            .selectNurseryNo()
            .selectSixthFormYes()
            .selectFaithStatusNone()
            .selectSaveAndContinue()

        schoolSummaryPage.selectChangeCurrrentFreeSchoolNameToGoToSchoolDetails();
        schoolDetailsPage
            .enterAgeRangeFrom("")
            .enterAgeRangeTo("")
            .selectSaveAndContinue()
            .errorMessage("The Age range field is required.")
            .ageRangeErrorHint("The Age range field is required.");

        schoolDetailsPage
            .enterAgeRangeFrom("A")
            .enterAgeRangeTo("")
            .selectSaveAndContinue()
            .errorMessage("Please enter a valid number")
            .ageRangeErrorHint("Please enter a valid number");

        schoolDetailsPage
            .enterAgeRangeFrom("")
            .enterAgeRangeTo("A")
            .selectSaveAndContinue()
            .errorMessage("Please enter a valid number")
            .ageRangeErrorHint("Please enter a valid number");

        schoolDetailsPage
            .enterAgeRangeFrom("1")
            .enterAgeRangeTo("")
            .selectSaveAndContinue()
            .errorMessage("Please enter a valid number")
            .ageRangeErrorHint("Please enter a valid number");

        schoolDetailsPage
            .enterAgeRangeFrom("")
            .enterAgeRangeTo("1")
            .selectSaveAndContinue()
            .errorMessage("Please enter a valid number")
            .ageRangeErrorHint("Please enter a valid number");
    
        schoolDetailsPage
            .enterAgeRangeFrom("1")
            .enterAgeRangeTo("1")
            .selectSaveAndContinue()
            .errorMessage("'Age range from' must be less than 'Age range to'")
            .ageRangeErrorHint("'Age range from' must be less than 'Age range to'");

        schoolDetailsPage
            .enterAgeRangeFrom("-1")
            .enterAgeRangeTo("10")
            .selectSaveAndContinue()
            .errorMessage("Please enter a valid number")
            .ageRangeErrorHint("Please enter a valid number");
      
        schoolDetailsPage
            .enterAgeRangeFrom("10")
            .enterAgeRangeTo("-1")
            .selectSaveAndContinue()
            .errorMessage("Please enter a valid number")
            .ageRangeErrorHint("Please enter a valid number");
        
        schoolDetailsPage
            .enterAgeRangeFrom("10")
            .enterAgeRangeTo("-1")
            .selectSaveAndContinue()
            .errorMessage("Please enter a valid number")
            .ageRangeErrorHint("Please enter a valid number");

        schoolDetailsPage
            .enterAgeRangeFrom("999")
            .enterAgeRangeTo("10")
            .selectSaveAndContinue()
            .errorMessage("'Age range from' must be 2 characters or less.")
            .ageRangeErrorHint("'Age range from' must be 2 characters or less.");

        schoolDetailsPage
            .enterAgeRangeFrom("10")
            .enterAgeRangeTo("999")
            .selectSaveAndContinue()
            .errorMessage("'Age range to' must be 2 characters or less.")
            .ageRangeErrorHint("'Age range to' must be 2 characters or less.");

    });
});
