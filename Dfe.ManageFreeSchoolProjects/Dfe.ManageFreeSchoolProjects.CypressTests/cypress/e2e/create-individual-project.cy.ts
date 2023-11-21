import { Logger } from "cypress/common/logger";
import { ProjectRecordCreator } from "cypress/constants/cypressConstants";
import singleProjectConfirmationPage from "cypress/pages/singleProjectConfirmationPage";
import createProjectPage from "cypress/pages/createProject/createProjectPage";
import homePage from "cypress/pages/homePage";
import singleProjectCheckYourAnswersPage from "cypress/pages/singleProjectCheckYourAnswersPage";
import singleProjectCurrentFreeSchoolNamePage from "cypress/pages/singleProjectCurrentFreeSchoolNamePage";
import singleProjectLocalAuthorityPage from "cypress/pages/singleProjectLocalAuthorityPage";
import singleProjectRegionPage from "cypress/pages/singleProjectRegionPage";
import singleProjectTemporaryProjectIdPage from "cypress/pages/singleProjectTemporaryProjectIdPage";
import validationComponent from "cypress/pages/validationComponent";
import whichProjectMethodPage from "cypress/pages/whichProjectMethodPage";
import dataGenerator from "cypress/fixtures/dataGenerator";
import createProjectCheckYourAnswersPage from "cypress/pages/createProject/createProjectCheckYourAnswersPage";


describe("Creating an individual project - NEGATIVE ROLE TESTS - USER DOES NOT GET GREEN CREATE NEW PROJECT BUTTON", () => {
    beforeEach(() => {

        cy.login({ role: "POTATO" });
        cy.visit(Cypress.env('url'));

    });

    it("Should NOT allow a NON-projectrecordcreator user to access certain URLs", () => {

        Logger.log("Testing that NON-projectrecordcreator role is UNABLE to access Create individual project URLs")
        // Define the URLs that should trigger a failure for the "POTATO"/NON-projectrecordcreator user
        const unauthorizedUrls = [
            "/project/create/method",
            "/project/create/school",
            "/project/create/region",
            "/project/create/localauthority",
            "project/create/school-type",
            "/project/create/checkyouranswers",
            "/project/create/confirmation"];

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

        Logger.log("Testing that a NON-projectrecordcreator role DOES NOT have the green Create new projects CTA");
        cy.contains('Create new projects').should('not.exist');

        cy.executeAccessibilityTests();
    });

});



describe("Creating an individual project - Create new project button should display for projectrecordcreator role", () => {
    beforeEach(() => {

        cy.login({ role: ProjectRecordCreator });
        cy.visit('/');

    });

    it("Should display Create new projects button for projectRecordCreator role", () => {
        Logger.log("Testing that a projectrecordcreator role DOES have the green Create new projects CTA");
        cy.contains('Create new projects').should('be.visible');


        cy.executeAccessibilityTests();


    });
});

describe.skip("Creating an individual project - Test Create new individual project journey for projectRecordCreator role", () => {
    beforeEach(() => {

        cy.login({ role: ProjectRecordCreator });
        cy.visit('/');

    });

    it("Should navigate to project/create/method page", () => {

        const temporaryProjectId = dataGenerator.generateTemporaryId();
        const e2eTestSchool = "St Dunstan's Abbey, (Plymouth)";

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


        cy.executeAccessibilityTests();



        whichProjectMethodPage.selectContinue();


        cy.executeAccessibilityTests();


        singleProjectTemporaryProjectIdPage.checkElementsVisible();


        Logger.log("TESTING THAT SUBMITTING A BLANK TEMPORARY ID FAILS");
        singleProjectTemporaryProjectIdPage.selectContinue();


        cy.executeAccessibilityTests();


        singleProjectTemporaryProjectIdPage.verifyEmptyValidationMessage();


        Logger.log("TEST THAT SUBMITTING INVALID CHARS IN TEMPORARY ID FAILS");
        singleProjectTemporaryProjectIdPage.UserEntersAndSubmitsInvalidChars();


        cy.executeAccessibilityTests();


        singleProjectTemporaryProjectIdPage.verifyInvalidCharsValidationMessage();


        Logger.log("TESTING THAT SUBMITTING INVALID SPACES IN TEMPORARY ID FAILS");
        singleProjectTemporaryProjectIdPage.UserEntersAndSubmitsSpaces();


        cy.executeAccessibilityTests();


        singleProjectTemporaryProjectIdPage.verifySpacesValidationMessage();


        Logger.log("TESTING THAT ATTEMPTING TO SUBMIT A VALID FORMAT BUT > 25 CHARS TEMPORARY ID FAILS");
        singleProjectTemporaryProjectIdPage.UserEntersMoreThanTwentyFiveChars();


        cy.executeAccessibilityTests();


        singleProjectTemporaryProjectIdPage.verifyMoreThanTwentyFiveCharsValidationMessage();


        Logger.log("TESTING THAT AN SQL INJECTION ATTACK IN TEMPORARY ID FIELD FAILS");
        singleProjectTemporaryProjectIdPage.UserAttemptsSQLInjection();


        cy.executeAccessibilityTests();


        singleProjectTemporaryProjectIdPage.verifySpacesValidationMessage();


        Logger.log("TESTING THAT A JAVASCRIPT ATTACK IN TEMPORARY ID FIELD FAILS");
        singleProjectTemporaryProjectIdPage.UserAttemptsJavaScriptAttack();


        cy.executeAccessibilityTests();


        singleProjectTemporaryProjectIdPage.verifyMoreThanTwentyFiveCharsValidationMessage();


        Logger.log("TESTING THAT A VALID FORMAT OF TEMPORARY ID OF 25 CHARS OR LESS PASSES");
        singleProjectTemporaryProjectIdPage.UserEntersValidTempId(temporaryProjectId);
        singleProjectTemporaryProjectIdPage.selectContinue();

        //-------------------------------------------------------------
        // FREE SCHOOL NAME PAGE
        //--------------------------------------------------------------


        cy.executeAccessibilityTests();


        singleProjectCurrentFreeSchoolNamePage.checkElementsVisible(e2eTestSchool);


        Logger.log("TESTING THAT SUBMITTING A BLANK SCHOOL NAME FAILS");
        singleProjectCurrentFreeSchoolNamePage.selectContinue();

        cy.executeAccessibilityTests();


        singleProjectCurrentFreeSchoolNamePage.verifyEmptyValidationMessage();

        Logger.log("TESTING THAT SUBMITTING INVALID CHARS IN SCHOOL NAME FAILS");
        singleProjectCurrentFreeSchoolNamePage.UserEntersAndSubmitsInvalidChars();

        Logger.log("Checking accessibility of singleProjectSchoolName page for a projectrecordcreator when invalid chars validation message occurs");
        cy.executeAccessibilityTests();


        singleProjectCurrentFreeSchoolNamePage.verifyInvalidCharsValidationMessage();





        Logger.log("TESTING THAT ATTEMPTING TO SUBMIT A VALID FORMAT BUT > 100 CHARS SCHOOL NAME FAILS");
        singleProjectCurrentFreeSchoolNamePage.UserEntersMoreThanOneHundredChars();


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
        singleProjectCurrentFreeSchoolNamePage.userEntersValidSchool(e2eTestSchool);


        //------------------------------------------------------------------------------------------------------------------------
        //REGION PAGE
        //------------------------------------------------------------------------------------------------------------------------

        cy.executeAccessibilityTests();


        singleProjectRegionPage.checkElementsVisible();


        Logger.log("TESTING THAT A USER IS UNABLE TO PROCEED ON SINGLEPROJECTREGIONPAGE WITHOUT MAKING A SELECTION");
        singleProjectRegionPage.selectContinue();

        cy.executeAccessibilityTests();


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


        cy.executeAccessibilityTests();


        singleProjectRegionPage.selectContinue();

        //------------------------------------------------------------------------------------------------------------------------
        //LOCAL AUTHORITY PAGE
        //------------------------------------------------------------------------------------------------------------------------

        cy.executeAccessibilityTests();


        singleProjectLocalAuthorityPage.checkElementsVisible();

        Logger.log("TESTING THAT A USER IS UNABLE TO PROCEED ON SINGLEPROJECTLOCALAUTHORITYPAGE WITHOUT MAKING A SELECTION");
        singleProjectLocalAuthorityPage.selectContinue();


        cy.executeAccessibilityTests();


        singleProjectLocalAuthorityPage.verifyValidationMessage();

        Logger.log("TESTING THAT A USER IS UNABLE TO HAVE >1 RADIO BUTTON CHECKED AT ONE TIME ON SINGLEPROJECTLOCALAUTHORITY PAGE");
        singleProjectLocalAuthorityPage.selectIslesOfScilly()
            .selectBathAndNorthEastSomerset()
            .selectBristol()
            .selectNorthSomerset()
            .selectSouthGloucestershire()
            .selectPoole()
            .selectDorset()
            .selectBournemouthChristchurchAndPoole()
            .selectWiltshire()
            .selectSwindon()
            .selectDevon()
            .selectPlymouth()
            .selectTorbay()
            .selectCornwall()
            .selectGloucestershire()
            .selectSomerset();



        Logger.log("TESTING THAT A USER CAN MAKE A VALID SELECTION IN PLYMOUTH AND PROCEED TO CHECK YOUR ANSWERS PAGE");
        singleProjectLocalAuthorityPage.selectPlymouth();


        singleProjectLocalAuthorityPage.selectContinue();

        //--------------------------------------------------------------------------------------------------------------------------
        // SCHOOL TYPE
        // -------------------------------------------------------------------------------------------------------------------------
        Logger.log("Selecting school type");
        createProjectPage.continue();

        validationComponent.hasValidationError("Select school type");

        cy.executeAccessibilityTests();

        createProjectPage
            .withSchoolType("Studio school")
            .continue();

        createProjectCheckYourAnswersPage
            .hasSchoolType("Studio school")

        //--------------------------------------------------------------------------------------------------------------------------
        //CHECK YOUR ANSWERS PAGE
        //--------------------------------------------------------------------------------------------------------------------------

        Logger.log("Checking accessibility of singleProjectCreateCheckYourAnswers page for a projectrecordcreator");
        cy.executeAccessibilityTests();


        singleProjectCheckYourAnswersPage.checkElementsVisible();

        // -------------------------------------------------------------------------------------------------------------------------
        // CHANGE ANSWERS
        // -------------------------------------------------------------------------------------------------------------------------
        Logger.log("Change the school type value");
        createProjectCheckYourAnswersPage.changeSchoolType();

        createProjectPage
            .hasSchoolType("Studio school")
            .withSchoolType("Mainstream")
            .continue();

        createProjectCheckYourAnswersPage.hasSchoolType("Mainstream");

        //--------------------------------------------------------------------------------------------------------------------------
        //PROJECT CREATED CONFIRMATION PAGE
        //--------------------------------------------------------------------------------------------------------------------------
        singleProjectCheckYourAnswersPage.submitAnswersAndGenerateProject();

        Logger.log("Checking accessibility of singleProjectCreateConfirmation page for a projectrecordcreator");
        cy.executeAccessibilityTests();

        singleProjectConfirmationPage.checkElementsVisible(temporaryProjectId);
    });
});

