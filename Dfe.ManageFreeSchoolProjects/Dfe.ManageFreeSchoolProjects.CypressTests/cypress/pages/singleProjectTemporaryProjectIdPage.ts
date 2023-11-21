

//let temporaryProjectId = "";

import { specialCharsTestString } from "cypress/constants/stringTestConstants";

class SingleProjectTemporaryProjectIdPage {
    public checkElementsVisible(): this {
        cy.contains("Back");

        cy.get("h1").contains("Creating a new free school project");
        cy.get("h1").contains("What is the temporary project ID?");

        cy.getById("create-new-project-id-hint").contains("For example, W3456");

        cy.getById("projectid");

        cy.getByTestId("continue");
        
        return this;
    }

    public selectContinue(): this {
        cy.getByTestId("continue").click();

        return this;
    }

    public verifyEmptyValidationMessage(): this {
        cy.getById("projectid-error-link").contains("The temporary project id field is required");
        cy.getById("projectid-error").contains("The temporary project id field is required");

        return this;
    }

    public UserEntersAndSubmitsInvalidChars(): this {
        cy.getByTestId("projectid").type(specialCharsTestString);
        cy.getByTestId("continue").click();

        return this;
    }

    public verifyInvalidCharsValidationMessage(): this {
        cy.getById("projectid-error-link").contains("Temporary project ID must only include numbers and letters");
        cy.getById("projectid-error").contains("Temporary project ID must only include numbers and letters");

        return this;
    }

    public UserEntersAndSubmitsSpaces(): this {
        cy.getByTestId("projectid").clear();
        cy.getByTestId("projectid").type("W 3 4 5 6 ");
        cy.getByTestId("continue").click();

        return this;
    }

    public verifySpacesValidationMessage(): this {
        cy.getById("projectid-error-link").contains("Temporary project ID must not include spaces");
        cy.getById("projectid-error").contains("Temporary project ID must not include spaces");

        return this;
    }

    public UserEntersMoreThanTwentyFiveChars(): this {
        cy.getByTestId("projectid").clear();
        cy.getByTestId("projectid").type("AB345678901234567890123456");
        cy.getByTestId("continue").click();

        return this;
    }

    public verifyMoreThanTwentyFiveCharsValidationMessage(): this {
        cy.getById("projectid-error-link").contains("The temporary project id must be 25 characters or less");
        cy.getById("projectid-error").contains("The temporary project id must be 25 characters or less");

        return this;
    }

    public UserAttemptsSQLInjection(): this {
        cy.getByTestId("projectid").clear();
        cy.getByTestId("projectid").type("' OR 1=1");
        cy.getByTestId("continue").click();

        return this;
    }

    public UserAttemptsJavaScriptAttack(): this {
        cy.getByTestId("projectid").clear();
        cy.getByTestId("projectid").type("<script>window.alert('Hello World!')</script>");
        cy.getByTestId("continue").click();

        return this;
    }

    public UserEntersValidTempId(temporaryProjectId): this {

        cy.getByTestId("projectid").clear();
        cy.getByTestId("projectid").type(temporaryProjectId);
        cy.getByTestId("continue").click();

        return this;
    }





}

const singleProjectTemporaryProjectIdPage = new SingleProjectTemporaryProjectIdPage();

export default singleProjectTemporaryProjectIdPage;