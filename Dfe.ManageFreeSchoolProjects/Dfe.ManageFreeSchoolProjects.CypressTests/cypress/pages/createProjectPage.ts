class CreateProjectPage {
    public withMethod(value: string): this {
        cy.getByTestId(value).check();

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

    public withRegion(value: string): this {
        cy.getByTestId(value).check();

        return this;
    }

    public hasRegion(value: string): this {
        cy.getByTestId("region").should("contain.text", value);

        return this;
    }

    public withLocalAuthority(value: string): this {
        cy.getByTestId(value).check();

        return this;
    }

    public hasLocalAuthority(value: string): this {
        cy.getByTestId("local-authority").should("contain.text", value);

        return this;
    }

    public continue(): this {
        cy.getByTestId("continue").click();

        return this;
    }
}

const createProjectPage = new CreateProjectPage();

export default createProjectPage;
