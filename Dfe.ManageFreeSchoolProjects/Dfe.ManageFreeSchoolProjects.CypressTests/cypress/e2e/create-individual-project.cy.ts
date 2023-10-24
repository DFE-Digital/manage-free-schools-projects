import { Logger } from "cypress/common/logger";
import { ProjectRecordCreator } from "cypress/constants/cypressConstants";
import singleProjectConfirmationPage from "cypress/pages/singleProjectConfirmationPage";
import createProjectPage from "cypress/pages/createProjectPage";
import homePage from "cypress/pages/homePage";
import singleProjectCheckYourAnswersPage from "cypress/pages/singleProjectCheckYourAnswersPage";
import singleProjectCurrentFreeSchoolNamePage from "cypress/pages/singleProjectCurrentFreeSchoolNamePage";
import singleProjectLocalAuthorityPage from "cypress/pages/singleProjectLocalAuthorityPage";
import singleProjectRegionPage from "cypress/pages/singleProjectRegionPage";
import singleProjectTemporaryProjectIdPage from "cypress/pages/singleProjectTemporaryProjectIdPage";
import validationComponent from "cypress/pages/validationComponent";
import whichProjectMethodPage from "cypress/pages/whichProjectMethodPage";
import { v4 } from "uuid";


describe("Creating an individual project - NEGATIVE ROLE TESTS - USER DOES NOT GET GREEN CREATE NEW PROJECT BUTTON", () => {
    beforeEach(() => {
        
        cy.login({role: "POTATO"});
        cy.visit(Cypress.env('url'));
        
    });

    it("Should NOT allow a NON-projectrecordcreator user to access certain URLs", () => {
        // Define the URLs that should trigger a failure for the "POTATO" user
        const unauthorizedUrls = ["/project/create/method", "/project/create/school", "/project/create/region", "/project/create/localauthority", "/project/create/checkyouranswers", "/project/create/confirmation"];

        // Verify that the "NON-Projectrecordcreator" user cannot access unauthorized URLs
        cy.location().should((loc) => {
            const currentUrl = loc.href;
            if (unauthorizedUrls.some(url => currentUrl.includes(url))) {
                // Fail the test if the user is on an unauthorized URL
                throw new Error("Test failed because the 'NON-Projectrecordcreator' user is on an unauthorized URL");
            }
        });
    });

    it("Should NOT allow a NON-projectrecordcreator user to create a new project using the form", () => {
        // VERIFY THIS NON-PROJECTRECORDCREATOR USER DOES NOT GET A GREEN CREATE NEW PROJECT BUTTON
            cy.contains('Create new projects').should('not.exist');
            
            cy.excuteAccessibilityTests();
            cy.checkA11y();
    });

});



describe("Creating an individual project - Create new project button should display for projectrecordcreator role", () => {
    beforeEach(() => {
        
        cy.login({role: ProjectRecordCreator});
        cy.visit('/');
        
    });

    it("Should display Create new projects button for projectRecordCreator role", () => {
        const school = `${v4()} school`;

        cy.contains('Create new projects').should('be.visible');

        cy.excuteAccessibilityTests();
        cy.checkA11y();

    });
});

describe("Creating an individual project - Test Create new individual project journey for projectRecordCreator role", () => {
    beforeEach(() => {
        
        cy.login({role: ProjectRecordCreator});
        cy.visit('/');
        
    });

    it("Should navigate to project/create/method page", () => {
        const school = `${v4()} school`;

        cy.excuteAccessibilityTests();
        cy.checkA11y();

        homePage.createNewProjects();

        whichProjectMethodPage.checkElementsVisible();

        cy.excuteAccessibilityTests();
        cy.checkA11y();

        // TEST WE CANNOT PROCEED WITHOUT SELECTING AN OPTION
        whichProjectMethodPage.selectContinue();

        cy.excuteAccessibilityTests();
        cy.checkA11y();

        // VERIFY WE GET CORRECT VALIDATION RESPONSE
        whichProjectMethodPage.verifyValidationMessage();

        // TEST WE CANNOT SELECT MORE THAN ONE OPTION
        whichProjectMethodPage.selectIndividualProject();
        whichProjectMethodPage.selectBulkUploadProject();
        
        
        // CLICK CONTINUE WITH INDIVIDUAL PROJECT SELECTED
        whichProjectMethodPage.selectIndividualProject();

        cy.excuteAccessibilityTests();
        cy.checkA11y();

        whichProjectMethodPage.selectContinue();

        cy.excuteAccessibilityTests();
        cy.checkA11y();

        singleProjectTemporaryProjectIdPage.checkElementsVisible();

        // TEST THAT SUBMITTING A BLANK TEMPORARY ID FAILS
        singleProjectTemporaryProjectIdPage.selectContinue();

        cy.excuteAccessibilityTests();
        cy.checkA11y();

        singleProjectTemporaryProjectIdPage.verifyEmptyValidationMessage();


        // TEST THAT SUBMITTING INVALID CHARS IN TEMPORARY ID FAILS
        singleProjectTemporaryProjectIdPage.UserEntersAndSubmitsInvalidChars();

        cy.excuteAccessibilityTests();
        cy.checkA11y();

        singleProjectTemporaryProjectIdPage.verifyInvalidCharsValidationMessage();

        // TEST THAT SUBMITTING SPACES IN TEMPORARY ID FAILS
        singleProjectTemporaryProjectIdPage.UserEntersAndSubmitsSpaces();

        cy.excuteAccessibilityTests();
        cy.checkA11y();

        singleProjectTemporaryProjectIdPage.verifySpacesValidationMessage();

        // TEST THAT ATTEMPTING TO SUBMIT A VALID FORMAT BUT > 25 CHARS FAILS
        singleProjectTemporaryProjectIdPage.UserEntersMoreThanTwentyFiveChars();

        cy.excuteAccessibilityTests();
        cy.checkA11y();

        singleProjectTemporaryProjectIdPage.verifyMoreThanTwentFiveCharsValidationMessage();

        // TEST THAT AN SQL INJECTION ATTACK FAILS
        singleProjectTemporaryProjectIdPage.UserAttemptsSQLInjection();

        cy.excuteAccessibilityTests();
        cy.checkA11y();

        singleProjectTemporaryProjectIdPage.verifyInvalidCharsValidationMessage();

        // TEST THAT A JAVASCRIPT ATTACK FAILS
        singleProjectTemporaryProjectIdPage.UserAttemptsJavaScriptAttack();

        cy.excuteAccessibilityTests();
        cy.checkA11y();

        singleProjectTemporaryProjectIdPage.verifyInvalidCharsValidationMessage();

        // TEST THAT A VALID FORMAT 25 CHARS OR LESS LETS US PROCEED TO THE FREE SCHOOL NAME SECTION
        singleProjectTemporaryProjectIdPage.UserEntersValidTempId();
        singleProjectTemporaryProjectIdPage.selectContinue();

        //-------------------------------------------------------------
        // FREE SCHOOL NAME PAGE
        //--------------------------------------------------------------

        cy.excuteAccessibilityTests();
        cy.checkA11y();

        singleProjectCurrentFreeSchoolNamePage.checkElementsVisible();

        // TEST THAT SUBMITTING A BLANK SCHOOL NAME FAILS
        singleProjectCurrentFreeSchoolNamePage.selectContinue();

        cy.excuteAccessibilityTests();
        cy.checkA11y();

        singleProjectCurrentFreeSchoolNamePage.verifyEmptyValidationMessage();

        // TEST THAT SUBMITTING UNALLOWED SPECIAL CHARS IN SCHOOL NAME FAILS
        singleProjectCurrentFreeSchoolNamePage.UserEntersAndSubmitsInvalidChars();

        cy.excuteAccessibilityTests();
        cy.checkA11y();

        singleProjectCurrentFreeSchoolNamePage.verifyInvalidCharsValidationMessage();

        // TEST THAT ATTEMPTING TO SUBMIT A VALID FORMAT BUT > 80? 100? CHARS FAILS
        singleProjectCurrentFreeSchoolNamePage.UserEntersMoreThanEightyChars();

        cy.excuteAccessibilityTests();
        cy.checkA11y();

        singleProjectCurrentFreeSchoolNamePage.verifyMoreThanEightyCharsValidationMessage();

        // TEST THAT AN SQL INJECTION ATTACK FAILS
        singleProjectCurrentFreeSchoolNamePage.UserAttemptsSQLInjection();

        cy.excuteAccessibilityTests();
        cy.checkA11y();

        singleProjectCurrentFreeSchoolNamePage.verifyInvalidCharsValidationMessage();

        // TEST THAT A JAVASCRIPT ATTACK FAILS
        singleProjectCurrentFreeSchoolNamePage.UserAttemptsJavaScriptAttack();

        cy.excuteAccessibilityTests();
        cy.checkA11y();

        singleProjectCurrentFreeSchoolNamePage.verifyInvalidCharsValidationMessage();

        // TEST THAT A VALID FORMAT 80 CHARS? 100 CHARS? WITH ALL LEGIT SPECIAL CHARS OR LESS PASSES AND LETS US PROCEED TO THE REGION PAGE
        singleProjectCurrentFreeSchoolNamePage.userEntersValidSchool();


        //------------------------------------------------------------------------------------------------------------------------
        //REGION PAGE
        //------------------------------------------------------------------------------------------------------------------------

        cy.excuteAccessibilityTests();
        cy.checkA11y();

        singleProjectRegionPage.checkElementsVisible();

        // TEST THAT A USER IS UNABLE TO PROCEED WITHOUT MAKING A SELECTION
        singleProjectRegionPage.selectContinue();

        cy.excuteAccessibilityTests();
        cy.checkA11y();

        singleProjectRegionPage.verifyValidationMessage();

        // TEST THAT A USER IS UNABLE TO HAVE >1 RADIO BUTTON CHECKED AT ONE TIME
        singleProjectRegionPage.selectEastMidlands();
        singleProjectRegionPage.selectEastOfEngland();
        singleProjectRegionPage.selectLondon();
        singleProjectRegionPage.selectNorthEast();
        singleProjectRegionPage.selectNorthWest();
        singleProjectRegionPage.selectSouthEast();
        singleProjectRegionPage.selectWestMidlands();
        singleProjectRegionPage.selectYorkshireAndHumber();


        // TEST THAT A USER CAN MAKE A VALID SELECTION AND PROCEED TO LOCAL AUTHORITY PAGE
        singleProjectRegionPage.selectSouthWest();

        cy.excuteAccessibilityTests();
        cy.checkA11y();

        singleProjectRegionPage.selectContinue();

        //------------------------------------------------------------------------------------------------------------------------
        //LOCAL AUTHORITY PAGE
        //------------------------------------------------------------------------------------------------------------------------

        cy.excuteAccessibilityTests();
        cy.checkA11y();

        singleProjectLocalAuthorityPage.checkElementsVisible();

        // TEST THAT A USER IS UNABLE TO PROCEED WITHOUT MAKING A SELECTION
        singleProjectLocalAuthorityPage.selectContinue();

        cy.excuteAccessibilityTests();
        cy.checkA11y();
        
        singleProjectLocalAuthorityPage.verifyValidationMessage();

        // TEST THAT A USER IS UNABLE TO HAVE >1 RADIO BUTTON CHECKED AT ONE TIME
        singleProjectLocalAuthorityPage.selectCambridgeshire();
        singleProjectLocalAuthorityPage.selectCentralBedfordshire();
        singleProjectLocalAuthorityPage.selectEssex();
        singleProjectLocalAuthorityPage.selectHertfordshire();

        // TEST THAT A USER CAN MAKE A VALID SELECTION AND PROCEED TO CHECK YOUR ANSWERS PAGE
        singleProjectLocalAuthorityPage.selectBedford();

        cy.excuteAccessibilityTests();
        cy.checkA11y();

        singleProjectLocalAuthorityPage.selectContinue();

        //--------------------------------------------------------------------------------------------------------------------------
        //CHECK YOUR ANSWERS PAGE
        //--------------------------------------------------------------------------------------------------------------------------

        cy.excuteAccessibilityTests();
        cy.checkA11y();

        singleProjectCheckYourAnswersPage.checkElementsVisible();

        singleProjectCheckYourAnswersPage.submitAnswersAndGenerateProject();

        //--------------------------------------------------------------------------------------------------------------------------
        //PROJECT CREATED CONFIRMATION PAGE
        //--------------------------------------------------------------------------------------------------------------------------

        cy.excuteAccessibilityTests();
        cy.checkA11y();

        singleProjectConfirmationPage.checkElementsVisible();


        //--------------------------------------------------------------------------------------------------------------------------
        // MIKE'S LEGACY CODE - LEAVE THIS FOR NOW!
        //--------------------------------------------------------------------------------------------------------------------------
/*
        Logger.log("Selecting method");
        createProjectPage.continue();
        validationComponent.hasValidationError("The method field is required");
        createProjectPage.withMethod("Individual").continue();

        Logger.log("Setting school name");
        createProjectPage.continue();
        validationComponent.hasValidationError(
            "The free school name field is required",
        );
        createProjectPage.withSchoolExceedingLimit().continue();
        validationComponent.hasValidationError(
            "The free school name must be 80 characters or less",
        );
        createProjectPage.withSchool(school).continue();

        Logger.log("Selecting region");
        createProjectPage.continue();
        validationComponent.hasValidationError("The region field is required");
        createProjectPage.withRegion("SouthEast").continue();

        Logger.log("Selecting local authority");
        createProjectPage.continue();
        validationComponent.hasValidationError(
            "The local authority field is required",
        );
        createProjectPage.withLocalAuthority("Essex").continue();

        Logger.log("Checking the information on the confirmation page");
        createProjectPage
            .hasSchool(school)
            .hasRegion("South East")
            .hasLocalAuthority("Essex");
*/
    });
});

