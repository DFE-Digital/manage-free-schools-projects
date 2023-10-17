import { Logger } from "cypress/common/logger";
import { ProjectRecordCreator } from "cypress/constants/cypressConstants";
import createProjectPage from "cypress/pages/createProjectPage";
import validationComponent from "cypress/pages/validationComponent";
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



describe("Creating an individual project - POSITIVE ROLE TESTS", () => {
    beforeEach(() => {
        
        cy.login({role: ProjectRecordCreator});
        cy.visit(Cypress.env("url"));
        
    });

    it("Should allow a projectRecordCreator user to create a new project using the form", () => {
        const school = `${v4()} school`;

        cy.contains('Create new projects').should('be.visible');  

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

    });
});

