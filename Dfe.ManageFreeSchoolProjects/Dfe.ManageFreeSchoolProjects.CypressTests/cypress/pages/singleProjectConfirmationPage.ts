class SingleProjectConfirmationPage {
    public checkElementsVisible(): this {

        cy.get("h1").contains("Free school project created");

        cy.contains("Temporary project ID");

        cy.contains("We have sent a notification email to team leaders.");
        
        cy.get("h2").contains("What happens next");

        cy.contains("Go back to the projects listing page and see the newly created projects.");

        return this;
    }
}

const singleProjectConfirmationPage = new SingleProjectConfirmationPage();

export default singleProjectConfirmationPage;