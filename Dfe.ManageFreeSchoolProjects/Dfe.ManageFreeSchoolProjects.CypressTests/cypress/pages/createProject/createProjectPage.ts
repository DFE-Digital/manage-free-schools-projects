class CreateProjectPage {
    public withMethod(method: string): this {
        cy.getByTestId(method).check();

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
}

const createProjectPage = new CreateProjectPage();

export default createProjectPage;
