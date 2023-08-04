class CreateProjectPage {
    public withProjectId(value: string): this {
        cy.getByTestId("project-id-input").clear().type(value);

        return this;
    }

    public withSchoolName(value: string): this {
        cy.getByTestId("school-name-input").clear().type(value);

        return this;
    }

    public withApplicationNumber(value: string): this {
        cy.getByTestId("application-number-input").clear().type(value);

        return this;
    }

    public withApplicationWave(value: string): this {
        cy.getByTestId("application-wave-input").clear().type(value);

        return this;
    }

    public createProject() {
        cy.getByTestId("create-project-button").click(); 

        return this;
    }
}

const createProjectPage = new CreateProjectPage();

export default createProjectPage;
