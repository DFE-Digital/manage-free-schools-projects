import { Logger } from "cypress/common/logger";
import { ProjectRecordCreator } from "cypress/constants/cypressConstants";
import singleProjectConfirmationPage from "cypress/pages/SingleProjectConfirmationPage";
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
        const unauthorizedUrls = ["/project/create/method", "/project/create/school"];

        // Verify that the "POTATO" user cannot access unauthorized URLs
        cy.location().should((loc) => {
            const currentUrl = loc.href;
            if (unauthorizedUrls.some(url => currentUrl.includes(url))) {
                // Fail the test if the user is on an unauthorized URL
                throw new Error("Test failed because the 'POTATO' user is on an unauthorized URL");
            }
        });
    });

    it("Should NOT allow a NON-projectrecordcreator user to create a new project using the form", () => {
        // VERIFY THIS NON-PROJECTRECORDCREATOR USER DOES NOT GET A GREEN CREATE NEW PROJECT BUTTON
            cy.contains('Create new projects').should('not.exist');    
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

    });
});

describe("Creating an individual project - Test Create new individual project journey for projectRecordCreator role", () => {
    beforeEach(() => {
        
        cy.login({role: ProjectRecordCreator});
        cy.visit('/');
        
    });

    it("Should navigate to project/create/method page", () => {
        const school = `${v4()} school`;

        homePage.createNewProjects();

        whichProjectMethodPage.checkElementsVisible();

        // TEST WE CANNOT PROCEED WITHOUT SELECTING AN OPTION

        whichProjectMethodPage.selectContinue();

        // VERIFY WE GET CORRECT VALIDATION RESPONSE
        whichProjectMethodPage.verifyValidationMessage();

        // TEST WE CANNOT SELECT MORE THAN ONE OPTION
        whichProjectMethodPage.selectIndividualProject();
        whichProjectMethodPage.selectBulkUploadProject();
        
        
        // CLICK CONTINUE WITH INDIVIDUAL PROJECT SELECTED
        whichProjectMethodPage.selectIndividualProject();
        whichProjectMethodPage.selectContinue();

        singleProjectTemporaryProjectIdPage.checkElementsVisible();

        // TEST THAT SUBMITTING A BLANK TEMPORARY ID FAILS

        // TEST THAT SUBMITTING INVALID CHARS IN TEMPORARY ID FAILS

        // TEST THAT SUBMITTING SPACES IN TEMPORARY ID FAILS

        // TEST THAT ATTEMPTING TO SUBMIT A VALID FORMAT BUT > 25 CHARS FAILS

        // TEST THAT AN SQL INJECTION ATTACK FAILS

        // TEST THAT A JAVASCRIPT ATTACK FAILS

        // TEST THAT A VALID FORMAT 25 CHARS OR LESS LETS US PROCEED TO THE FREE SCHOOL NAME SECTION

        //-------------------------------------------------------------
        // FREE SCHOOL NAME PAGE
        //--------------------------------------------------------------

        singleProjectCurrentFreeSchoolNamePage.checkElementsVisible();

        // TEST THAT SUBMITTING A BLANK SCHOOL NAME FAILS

        // TEST THAT SUBMITTING UNALLOWED SPECIAL CHARS IN SCHOOL NAME FAILS

        // TEST THAT ATTEMPTING TO SUBMIT A VALID FORMAT BUT > 80? 100? CHARS FAILS

        // TEST THAT AN SQL INJECTION ATTACK FAILS

        // TEST THAT A JAVASCRIPT ATTACK FAILS

        // TEST THAT A VALID FORMAT 80 CHARS? 100 CHARS? WITH ALL LEGIT SPECIAL CHARS OR LESS PASSES AND LETS US PROCEED TO THE REGION PAGE

        //------------------------------------------------------------------------------------------------------------------------
        //REGION PAGE
        //------------------------------------------------------------------------------------------------------------------------

        singleProjectRegionPage.checkElementsVisible();

        // TEST THAT A USER IS UNABLE TO PROCEED WITHOUT MAKING A SELECTION

        // TEST THAT A USER IS UNABLE TO HAVE >1 RADIO BUTTON CHECKED AT ONE TIME

        // TEST THAT A USER CAN MAKE A VALID SELECTION AND PROCEED TO LOCAL AUTHORITY PAGE

        //------------------------------------------------------------------------------------------------------------------------
        //LOCAL AUTHORITY PAGE
        //------------------------------------------------------------------------------------------------------------------------

        singleProjectLocalAuthorityPage.checkElementsVisible();

        // TEST THAT A USER IS UNABLE TO PROCEED WITHOUT MAKING A SELECTION

        // TEST THAT A USER IS UNABLE TO HAVE >1 RADIO BUTTON CHECKED AT ONE TIME

        // TEST THAT A USER CAN MAKE A VALID SELECTION AND PROCEED TO CHECK YOUR ANSWERS PAGE

        //--------------------------------------------------------------------------------------------------------------------------
        //CHECK YOUR ANSWERS PAGE
        //--------------------------------------------------------------------------------------------------------------------------

        singleProjectCheckYourAnswersPage.checkElementsVisible();

        singleProjectCheckYourAnswersPage.submitAnswersAndGenerateProject();

        //--------------------------------------------------------------------------------------------------------------------------
        //PROJECT CREATED CONFIRMATION PAGE
        //--------------------------------------------------------------------------------------------------------------------------

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

