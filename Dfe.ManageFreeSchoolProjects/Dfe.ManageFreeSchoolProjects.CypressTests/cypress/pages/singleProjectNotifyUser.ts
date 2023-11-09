

const validTrustId = "";
const validEmail = "";

class SingleProjectNotifyUserPage {
    public checkElementsVisible(): this {
        cy.contains("Back");

        cy.get("h1").contains("Creating a new free school project");
        cy.get("h1").contains("Who do you want to notify about this project?");

        cy.get("p").contains("This is so they can assign it to someone to manage.");

        cy.get("label[for='email']").contains("Email address");

        cy.getById("email");

        cy.getByTestId("continue");
        
        return this;
    }

    public selectContinue(): this {
        cy.getByTestId("continue").click();

        return this;
    }

    public verifyEmptyValidationMessage(): this {
        cy.getById("email-error-link").contains("You must enter an email address");
        cy.getById("email-error").contains("You must enter an email address");

        return this;
    }

    public UserEntersAndSubmitsInvalidEmailFormat(): this {
        cy.getByTestId("email").type("POTATO");
        cy.getByTestId("continue").click();

        return this;
    }

    public verifyValidEmailFormatMessage(): this {
        cy.getById("email-error-link").contains("Please enter a valid email address");
        cy.getById("email-error").contains("Please enter a valid email address");

        return this;
    }

    public UserEntersAnInvalidEmailWithSpaces(): this {
        cy.getByTestId("email").clear();
        cy.getByTestId("email").type("a n i n v a l i d @ e m a i l a d d r e s s w i t h s p a c e s . c o m");
        cy.getByTestId("continue").click();

        return this;
    }

    public UserEntersEmailMoreThanOneHundredChars(): this {
        cy.getByTestId("email").clear();
        cy.getByTestId("email").type("areallyunnecessarilylongemailaddressthatshouldhopefullycheckthatavalidationmessageoccurs@foranema.com");
        cy.getByTestId("continue").click();

        return this;
    }

    public verifyEmailTooLongMessage(): this {
        cy.getById("email-error-link").contains("The email address must be 100 chars or less");
        cy.getById("email-error").contains("The email address must be 100 chars or less");

        return this;
    }

    public UserAttemptsSQLInjection(): this {
        cy.getByTestId("email").clear();
        cy.getByTestId("email").type("' OR 1=1");
        cy.getByTestId("continue").click();

        return this;
    }

    public UserAttemptsJavaScriptAttack(): this {
        cy.getByTestId("email").clear();
        cy.getByTestId("email").type("<script>window.alert('Hello World!')</script>");
        cy.getByTestId("continue").click();

        return this;
    }

    public UserEntersValidEmailAddress(validEmail :string): this {

        cy.getByTestId("email").clear();
        cy.getByTestId("email").type(validEmail);
        cy.getByTestId("continue").click();

        return this;
    }





}

const singleProjectNotifyUserPage = new SingleProjectNotifyUserPage();

export default singleProjectNotifyUserPage;