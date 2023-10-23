class SingleProjectCurrentFreeSchoolNamePage {
    public checkElementsVisible(): this {
        cy.contains("Back");

        cy.get("h1").contains("Creating a new free school project");
        cy.get("h1").contains("What is the current free school name?");

        cy.getById("create-new-project-school-name-hint").contains("You can still edit it anytime");

        cy.getById("school");

        cy.getByTestId("continue");
        
        return this;
    }

    public selectContinue(): this {
        cy.getByTestId("continue").click();

        return this;
    }

    public verifyEmptyValidationMessage(): this {
        cy.getById("school-error-link").contains("The free school name field is required");
        cy.getById("school-error").contains("The free school name field is required");

        return this;
    }

    public UserEntersAndSubmitsInvalidChars(): this {
        cy.getByTestId("school").type(",\"(){}<>,!@£$%^&*+-");
        cy.getByTestId("continue").click();

        return this;
    }

    public verifyInvalidCharsValidationMessage(): this {
        cy.getById("school-error-link").contains("The free school name must only include valid characters");
        cy.getById("school-error").contains("The free school name must only include valid characters");

        return this;
    }

    public UserEntersMoreThanEightyChars(): this {
        cy.getByTestId("school").type("123456789012345678901234567890123456789012345678901234567890123456789012345678901");
        cy.getByTestId("continue").click();

        return this;
    }

    public verifyMoreThanEightyCharsValidationMessage(): this {
        cy.getById("school-error-link").contains("The free school name must be 80 characters or less");
        cy.getById("school-error").contains("The free school name must be 80 characters or less");

        return this;
    }

    public UserAttemptsSQLInjection(): this {
        cy.getByTestId("school").type("' OR 1=1");
        cy.getByTestId("continue").click();

        return this;
    }

    public UserAttemptsJavaScriptAttack(): this {
        cy.getByTestId("school").type("<script>window.alert('Hello World!')</script>");
        cy.getByTestId("continue").click();

        return this;
    }

    public UserEntersValidSchool(): this {

        cy.getByTestId("school").type("Plymstock School");
        cy.getByTestId("continue").click();

        return this;
    }


}

const singleProjectCurrentFreeSchoolNamePage = new SingleProjectCurrentFreeSchoolNamePage();

export default singleProjectCurrentFreeSchoolNamePage;