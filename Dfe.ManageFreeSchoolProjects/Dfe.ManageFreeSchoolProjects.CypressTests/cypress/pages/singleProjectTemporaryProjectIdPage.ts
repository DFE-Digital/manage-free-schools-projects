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

    }

}

const singleProjectTemporaryProjectIdPage = new SingleProjectTemporaryProjectIdPage();

export default singleProjectTemporaryProjectIdPage;