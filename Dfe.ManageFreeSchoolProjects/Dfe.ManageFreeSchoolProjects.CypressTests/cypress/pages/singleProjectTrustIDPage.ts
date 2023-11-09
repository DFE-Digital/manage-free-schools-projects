import dataGenerator from "cypress/fixtures/dataGenerator";

const validTrustId = "";

class SingleProjectTrustIdPage {
    public checkElementsVisible(): this {
        cy.contains("Back");

        cy.get("h1").contains("Creating a new free school project");
        cy.get("h1").contains("Search for a trust by TRN (trust reference number)");

        cy.getById("create-new-project-trn-hint").contains("For example, TR00036");

        cy.getById("trn");

        cy.getByTestId("continue");
        
        return this;
    }

    public selectContinue(): this {
        cy.getByTestId("continue").click();

        return this;
    }

    public verifyEmptyValidationMessage(): this {
        cy.getById("trn-error-link").contains("Enter the TRN");
        cy.getById("trn-error").contains("Enter the TRN");

        return this;
    }

    public UserEntersAndSubmitsInvalidChars(): this {
        cy.getByTestId("trn").type(",\"(){}<>,!@£$%^&*+-");
        cy.getByTestId("continue").click();

        return this;
    }

    public verifyValidTRNFormatMessage(): this {
        cy.getById("trn-error-link").contains("The TRN must be in the format TRXXXXX");
        cy.getById("trn-error").contains("The TRN must be in the format TRXXXXX");

        return this;
    }

    public UserEntersAndSubmitsSpaces(): this {
        cy.getByTestId("trn").clear();
        cy.getByTestId("trn").type("T R N 1 2 3 4 5");
        cy.getByTestId("continue").click();

        return this;
    }

    public UserEntersMoreThanSixDigitsAfterTRNPrefixChars(): this {
        cy.getByTestId("trn").clear();
        cy.getByTestId("trn").type("TRN123456");
        cy.getByTestId("continue").click();

        return this;
    }

    public verifyTRNTooLongMessage(): this {
        cy.getById("trn-error-link").contains("TRN must be 7 characters or less");
        cy.getById("trn-error").contains("TRN must be 7 characters or less");

        return this;
    }

    public UserAttemptsSQLInjection(): this {
        cy.getByTestId("trn").clear();
        cy.getByTestId("trn").type("' OR 1=1");
        cy.getByTestId("continue").click();

        return this;
    }

    public UserAttemptsJavaScriptAttack(): this {
        cy.getByTestId("trn").clear();
        cy.getByTestId("trn").type("<script>window.alert('Hello World!')</script>");
        cy.getByTestId("continue").click();

        return this;
    }

    public UserEntersValidTrustReferenceNumber(validTrustId :string): this {

        cy.getByTestId("trn").clear();
        cy.getByTestId("trn").type(validTrustId);
        cy.getByTestId("continue").click();

        return this;
    }





}

const singleProjectTrustIdPage = new SingleProjectTrustIdPage();

export default singleProjectTrustIdPage;