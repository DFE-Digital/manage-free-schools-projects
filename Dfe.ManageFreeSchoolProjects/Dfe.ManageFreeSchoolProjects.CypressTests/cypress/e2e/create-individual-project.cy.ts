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
import dataGenerator from "cypress/fixtures/dataGenerator";


describe("Creating an individual project - NEGATIVE ROLE TESTS - USER DOES NOT GET GREEN CREATE NEW PROJECT BUTTON AND CANNOT ACCESS 'Create individual project' URLs", () => {
    beforeEach(() => {
        
        cy.login({role: "POTATO"});
        cy.visit(Cypress.env('url'));
        
    });

    it("Should NOT allow a NON-projectrecordcreator user to access 'Create single project' URLs", () => {

        Logger.log("Testing that NON-projectrecordcreator role is UNABLE to access Create individual project URLs")
        // Define the URLs that should trigger a failure for the "POTATO"/NON-projectrecordcreator user
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

    it("Should NOT allow a NON-projectrecordcreator user to view the green 'Create new projects' CTA on the homepage", () => {

            Logger.log("Testing that a NON-projectrecordcreator role DOES NOT have the green Create new projects CTA");
            cy.contains('Create new projects').should('not.exist');
            
            //Logger.log("Checking accessibility of the homepage");
            cy.executeAccessibilityTests();
    });

});



describe("Creating an individual project - Create new project button should display for projectrecordcreator role", () => {
    beforeEach(() => {
        
        cy.login({role: ProjectRecordCreator});
        cy.visit('/');
        
    });

    it("Should display Create new projects button for projectRecordCreator role", () => {
        Logger.log("Testing that a projectrecordcreator role DOES have the green Create new projects CTA");
        cy.contains('Create new projects').should('be.visible');

        //Logger.log("Checking accessibility of the homepage");
        cy.executeAccessibilityTests();
        

    });
});

describe("Creating an individual project - Test Create new individual project journey for projectRecordCreator role", () => {
    beforeEach(() => {
        
        cy.login({role: ProjectRecordCreator});
        cy.visit('/');
        
    });

    it("Should navigate to the project/create/method page and navigate through the 'Create individual project journey' e2e", () => {

        const temporaryProjectId = dataGenerator.generateTemporaryId();

        Logger.log("Checking accessibility of the homepage for a projectrecordcreator role");
        cy.executeAccessibilityTests();
        

        Logger.log("Clicking on Create new projects CTA as projectrecordcreator")
        homePage.createNewProjects();

        whichProjectMethodPage.checkElementsVisible();

        Logger.log("Checking accessibility of whichProjectMethodPage for a projectrecordcreator");
        cy.executeAccessibilityTests();
        

        Logger.log("TEST WE CANNOT PROCEED WITHOUT SELECTING AN OPTION");
        whichProjectMethodPage.selectContinue();

        Logger.log("Checking accessibility of whichProjectMethodPage for a projectrecordcreator when validation error occurs");
        cy.executeAccessibilityTests();
        
        whichProjectMethodPage.verifyValidationMessage();



        Logger.log("TEST WE CANNOT PROCEED WITHOUT SELECTING AN OPTION");
        whichProjectMethodPage.selectIndividualProject();
        whichProjectMethodPage.selectBulkUploadProject();



        
        Logger.log("TEST WE CAN PROCEED SELECTING CREATING AN INDIVIDUAL PROJECT OPTION");
        whichProjectMethodPage.selectIndividualProject();

        Logger.log("Checking accessibility of whichProjectMethodPage when Creating individual project selected for a projectrecordcreator");
        cy.excuteAccessibilityTests();
        //Logger.log("Checking accessibility of whichProjectMethodPage when Creating individual project selected for a projectrecordcreator");
        cy.executeAccessibilityTests();
        


        whichProjectMethodPage.selectContinue();

        //Logger.log("Checking accessibility of temporaryProjectId page when Creating individual project selected for a projectrecordcreator");
        cy.executeAccessibilityTests();
        

        singleProjectTemporaryProjectIdPage.checkElementsVisible();


        Logger.log("TESTING THAT SUBMITTING A BLANK TEMPORARY ID FAILS");
        singleProjectTemporaryProjectIdPage.selectContinue();

        //Logger.log("Checking accessibility of temporaryProjectId page for a projectrecordcreator when empty string validation message occurs");
        cy.executeAccessibilityTests();
        

        singleProjectTemporaryProjectIdPage.verifyEmptyValidationMessage();


        Logger.log("TEST THAT SUBMITTING INVALID CHARS IN TEMPORARY ID FAILS");
        singleProjectTemporaryProjectIdPage.UserEntersAndSubmitsInvalidChars();

        //Logger.log("Checking accessibility of temporaryProjectId page for a projectrecordcreator when invalid chars validation message occurs");
        cy.executeAccessibilityTests();
        

        singleProjectTemporaryProjectIdPage.verifyInvalidCharsValidationMessage();




        Logger.log("TESTING THAT SUBMITTING INVALID SPACES IN TEMPORARY ID FAILS");
        singleProjectTemporaryProjectIdPage.UserEntersAndSubmitsSpaces();

        //Logger.log("Checking accessibility of temporaryProjectId page for a projectrecordcreator when invalid spaces validation message occurs");
        cy.executeAccessibilityTests();
        

        singleProjectTemporaryProjectIdPage.verifySpacesValidationMessage();



        Logger.log("TESTING THAT ATTEMPTING TO SUBMIT A VALID FORMAT BUT > 25 CHARS TEMPORARY ID FAILS");
        singleProjectTemporaryProjectIdPage.UserEntersMoreThanTwentyFiveChars();

        //Logger.log("Checking accessibility of temporaryProjectId page for a projectrecordcreator when > 25 chars validation message occurs");
        cy.executeAccessibilityTests();
        

        singleProjectTemporaryProjectIdPage.verifyMoreThanTwentyFiveCharsValidationMessage();



        Logger.log("TESTING THAT AN SQL INJECTION ATTACK IN TEMPORARY ID FIELD FAILS");
        singleProjectTemporaryProjectIdPage.UserAttemptsSQLInjection();

        //Logger.log("Checking accessibility of temporaryProjectId page for a projectrecordcreator when invalid chars validation message occurs as part of an SQL injection attempt");
        cy.executeAccessibilityTests();
        

        singleProjectTemporaryProjectIdPage.verifySpacesValidationMessage();




        Logger.log("TESTING THAT A JAVASCRIPT ATTACK IN TEMPORARY ID FIELD FAILS");
        singleProjectTemporaryProjectIdPage.UserAttemptsJavaScriptAttack();

        //Logger.log("Checking accessibility of temporaryProjectId page for a projectrecordcreator when invalid chars validation message occurs as part of a JavaScript attack attempt");
        cy.executeAccessibilityTests();
        

        singleProjectTemporaryProjectIdPage.verifyMoreThanTwentyFiveCharsValidationMessage();



        Logger.log("TESTING THAT A VALID FORMAT OF TEMPORARY ID OF 25 CHARS OR LESS PASSES");
        singleProjectTemporaryProjectIdPage.UserEntersValidTempId(temporaryProjectId);
        singleProjectTemporaryProjectIdPage.selectContinue();

        //-------------------------------------------------------------
        // FREE SCHOOL NAME PAGE
        //--------------------------------------------------------------

        //Logger.log("Checking accessibility of singleProjectSchoolName page for a projectrecordcreator");
        cy.executeAccessibilityTests();
        

        singleProjectCurrentFreeSchoolNamePage.checkElementsVisible();




        Logger.log("TESTING THAT SUBMITTING A BLANK SCHOOL NAME FAILS");
        singleProjectCurrentFreeSchoolNamePage.selectContinue();

        //Logger.log("Checking accessibility of singleProjectSchoolName page for a projectrecordcreator when empty string validation message occurs");
        cy.executeAccessibilityTests();
        

        singleProjectCurrentFreeSchoolNamePage.verifyEmptyValidationMessage();




        Logger.log("TESTING THAT SUBMITTING INVALID CHARS IN SCHOOL NAME FAILS");
        singleProjectCurrentFreeSchoolNamePage.UserEntersAndSubmitsInvalidChars();

        Logger.log("Checking accessibility of singleProjectSchoolName page for a projectrecordcreator when invalid chars validation message occurs");
        cy.executeAccessibilityTests();
        
        // ***** UNCOMMENT THIS WHEN FINAL CREATE-PROJECT-SCHOOLNAME BUG-FIX COMES IN **************************************/
        //singleProjectCurrentFreeSchoolNamePage.verifyInvalidCharsValidationMessage();
        //*******************************************************************************************************************/



        Logger.log("TESTING THAT ATTEMPTING TO SUBMIT A VALID FORMAT BUT > 100 CHARS SCHOOL NAME FAILS");
        singleProjectCurrentFreeSchoolNamePage.UserEntersMoreThanOneHundredChars();

        //Logger.log("Checking accessibility of singleProjectSchoolName page for a projectrecordcreator when > 25 chars validation message occurs");
        cy.executeAccessibilityTests();

        singleProjectCurrentFreeSchoolNamePage.verifyMoreThanHundredCharsValidationMessage();




        Logger.log("TESTING THAT AN SQL INJECTION ATTACK IN SCHOOL NAME FIELD FAILS");
        singleProjectCurrentFreeSchoolNamePage.UserAttemptsSQLInjection();

        Logger.log("Checking accessibility of singleProjectSchoolName page for a projectrecordcreator when invalid chars validation message occurs as part of an SQL injection attempt");
        cy.executeAccessibilityTests();

        singleProjectCurrentFreeSchoolNamePage.verifyInvalidCharsValidationMessage();




        Logger.log("TESTING THAT A JAVASCRIPT ATTACK IN SCHOOL NAME FIELD FAILS");
        singleProjectCurrentFreeSchoolNamePage.UserAttemptsJavaScriptAttack();

        Logger.log("Checking accessibility of singleProjectSchoolName page for a projectrecordcreator when invalid chars validation message occurs as part of a JavaScript attack attempt");
        cy.executeAccessibilityTests();

        singleProjectCurrentFreeSchoolNamePage.verifyInvalidCharsValidationMessage();




        
        Logger.log("TESTING THAT A VALID FORMAT OF SCHOOLNAME OF 100 CHARS OR LESS PASSES");
        singleProjectCurrentFreeSchoolNamePage.userEntersValidSchool();

        //------------------------------------------------------------------------------------------------------------------------
        //REGION PAGE
        //------------------------------------------------------------------------------------------------------------------------

        //Logger.log("Checking accessibility of singleProjectRegion page for a projectrecordcreator");
        //cy.executeAccessibilityTests();
        

        singleProjectRegionPage.checkElementsVisible();

        


        Logger.log("TESTING THAT A USER IS UNABLE TO PROCEED ON SINGLEPROJECTREGIONPAGE WITHOUT MAKING A SELECTION");
        singleProjectRegionPage.selectContinue();

        //Logger.log("Checking accessibility of singleProjectRegion page for a projectrecordcreator when validation message occurs");
        //cy.executeAccessibilityTests();
        

        singleProjectRegionPage.verifyValidationMessage();




        Logger.log("TESTING THAT A USER IS UNABLE TO HAVE >1 RADIO BUTTON CHECKED AT ONE TIME ON SINGLEPROJECTREGION PAGE");
        singleProjectRegionPage.selectEastMidlands()
                               .selectEastOfEngland()
                               .selectLondon()
                               .selectNorthEast()
                               .selectNorthWest()
                               .selectSouthEast()
                               .selectWestMidlands()
                               .selectYorkshireAndHumber();




        Logger.log("TESTING THAT A USER CAN MAKE A VALID SELECTION IN SOUTH WEST AND PROCEED TO LOCAL AUTHORITY PAGE");
        singleProjectRegionPage.selectSouthWest();

        //Logger.log("Checking accessibility of singleProjectRegion page for a projectrecordcreator when South West selected");
        //cy.executeAccessibilityTests();
        

        singleProjectRegionPage.selectContinue();

        //------------------------------------------------------------------------------------------------------------------------
        //LOCAL AUTHORITY PAGE
        //------------------------------------------------------------------------------------------------------------------------

        //Logger.log("Checking accessibility of singleProjectLocalAuthority page for a projectrecordcreator");
        //cy.executeAccessibilityTests();
        

        singleProjectLocalAuthorityPage.checkElementsVisible();





        Logger.log("TESTING THAT A USER IS UNABLE TO PROCEED ON SINGLEPROJECTLOCALAUTHORITYPAGE WITHOUT MAKING A SELECTION");
        singleProjectLocalAuthorityPage.selectContinue();

        //Logger.log("Checking accessibility of singleProjectLocalAuthority page for a projectrecordcreator when validation message occurs");
        //cy.executeAccessibilityTests();
        
        
        singleProjectLocalAuthorityPage.verifyValidationMessage();





        Logger.log("TESTING THAT A USER IS UNABLE TO HAVE >1 RADIO BUTTON CHECKED AT ONE TIME ON SINGLEPROJECTLOCALAUTHORITY PAGE");
        singleProjectLocalAuthorityPage.selectCambridgeshire();
        singleProjectLocalAuthorityPage.selectCentralBedfordshire();
        singleProjectLocalAuthorityPage.selectEssex();
        singleProjectLocalAuthorityPage.selectHertfordshire();





        Logger.log("TESTING THAT A USER CAN MAKE A VALID SELECTION IN BEDFORD AND PROCEED TO CHECK YOUR ANSWERS PAGE");
        singleProjectLocalAuthorityPage.selectBedford();

        //Logger.log("Checking accessibility of singleProjectLocalAuthority page for a projectrecordcreator when Bedford selected");
        //cy.executeAccessibilityTests();
        

        singleProjectLocalAuthorityPage.selectContinue();

        //--------------------------------------------------------------------------------------------------------------------------
        //CHECK YOUR ANSWERS PAGE
        //--------------------------------------------------------------------------------------------------------------------------

        //Logger.log("Checking accessibility of singleProjectCreateCheckYourAnswers page for a projectrecordcreator");
        //cy.executeAccessibilityTests();
        

        singleProjectCheckYourAnswersPage.checkElementsVisible();


        Logger.log("TESTING THAT A USER CAN SUBMIT THE PREVIEW OF ANSWERS FROM THE 'Check Your Answers page' and Create a New Project");
        singleProjectCheckYourAnswersPage.submitAnswersAndGenerateProject();

        //--------------------------------------------------------------------------------------------------------------------------
        //PROJECT CREATED CONFIRMATION PAGE
        //--------------------------------------------------------------------------------------------------------------------------

        //Logger.log("Checking accessibility of singleProjectCreateConfirmation page for a projectrecordcreator");
        //cy.executeAccessibilityTests();
        
        Logger.log("TESTING THAT THE singleProjectConfirmationPage DISPLAYS CORRECTLY AND THE temporaryProjectId IS CORRECT");
        singleProjectConfirmationPage.checkElementsVisible(temporaryProjectId);


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

