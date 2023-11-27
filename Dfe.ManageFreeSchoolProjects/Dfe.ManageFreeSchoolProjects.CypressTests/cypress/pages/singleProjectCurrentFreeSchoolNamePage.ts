import { specialCharsTestString } from "cypress/constants/stringTestConstants";
import BasePage from "./BasePage";

class SingleProjectCurrentFreeSchoolNamePage extends BasePage {
    public checkElementsVisible(e2eTestSchool): this {
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
        cy.getById("school-error-link").contains("Enter the current free school name.");
        cy.getById("school-error").contains("Enter the current free school name.");

        return this;
    }

    public UserEntersAndSubmitsInvalidChars(): this {
        cy.getByTestId("school").clear();
        cy.getByTestId("school").type(specialCharsTestString);
        cy.getByTestId("continue").click();

        return this;
    }

    public verifyInvalidCharsValidationMessage(): this {
        cy.getById("school-error-link").contains("School name must not include special characters other than , ( ) '");
        cy.getById("school-error").contains("School name must not include special characters other than , ( ) '");

        return this;
    }

    public UserEntersMoreThanOneHundredChars(): this {
        cy.getByTestId("school").clear();
        cy.getByTestId("school").type("12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901");
        cy.getByTestId("continue").click();

        return this;
    }

    public verifyMoreThanHundredCharsValidationMessage(): this {
        cy.getById("school-error-link").contains("The school name must be 100 characters or less");
        cy.getById("school-error").contains("The school name must be 100 characters or less");

        return this;
    }

    public UserAttemptsSQLInjection(): this {
        cy.getByTestId("school").clear();
        cy.getByTestId("school").type("' OR 1=1");
        cy.getByTestId("continue").click();

        return this;
    }

    public UserAttemptsJavaScriptAttack(): this {
        cy.getByTestId("school").clear();
        cy.getByTestId("school").type("<script>window.alert('Hello World!')</script>");
        cy.getByTestId("continue").click();

        return this;
    }

    public userEntersValidSchool(e2eTestSchool :string): this {

        let theE2eTestSchool = "St Dunstan's Abbey, (Plymouth)";
        cy.getByTestId("school").clear();
        cy.getByTestId("school").type(theE2eTestSchool);
        cy.getByTestId("continue").click();

        return this;
    }


}

const singleProjectCurrentFreeSchoolNamePage = new SingleProjectCurrentFreeSchoolNamePage();

export default singleProjectCurrentFreeSchoolNamePage;