class HomePage {
    public createProject(): this {
        cy.getByTestId("create-project-button").click();

        return this;
    }

    public deleteProject(): this {
        cy.getByTestId("delete-project-button").click();

        return this;
    }
}

const homePage = new HomePage();

export default homePage;
