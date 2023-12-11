import validationComponent from "../validationComponent";

class CreateProjectPage {

    public errorMessage(error: string): this {
        validationComponent.hasValidationError(error);
        validationComponent.hasLinkedValidationError();
        return this;
    }

    public selectOption(method: string): this {
        cy.getByRadioOption(method).check();
        return this;
    }

    public titleIs(title: string) {
        cy.getByTestId("title-heading").should("contain.text", title);
        return this;
    }

    public enterProjectId(value: string) {
        cy.getByTestId("projectid").clear().type(value);
        return this;
    }

    public checkProjectId(value: string) {
        cy.getByTestId("projectid").should("have.value", value);
        return this;
    }

    public projectIDErrorMessage(error: string): this {
        cy.getById("projectid-error").should("contain.text", error);
        return this;
    }


    public withSchool(value: string): this {
        cy.getByTestId("school").clear().type(value);
        return this;
    }

    public hasSchool(value: string): this {
        cy.getByTestId("school").should("contain.text", value);

        return this;
    }

    public withSchoolExceedingLimit(): this {
        cy.getByTestId("school").clear().invoke("val", "abcde".repeat(20));

        return this;
    }

    public withRegion(region: string): this {
        cy.getByTestId(region).check();

        return this;
    }

    public hasRegion(value: string): this {
        cy.getByTestId("region").should("contain.text", value);

        return this;
    }

    public withLocalAuthority(localAuthority: string): this {
        cy.getByTestId(localAuthority).check();

        return this;
    }

    public hasLocalAuthority(value: string): this {
        cy.getByTestId("local-authority").should("contain.text", value);

        return this;
    }

    public hasSchoolType(value: string): this {
        cy.getByTestId(value).should("be.checked");

        return this;
    }

    public withSchoolType(value: string): this {
        cy.getByTestId(value).check();

        return this;
    }

    public continue(): this {
        cy.getByTestId("continue").click();
        return this;
    }

    public back(): this {
        cy.getByTestId("back").click();
        return this;
    }
}

const createProjectPage = new CreateProjectPage();

export default createProjectPage;
