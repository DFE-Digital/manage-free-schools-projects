class WhichProjectMethodPage {
    public checkElementsVisible(): this {
        cy.getByClass("govuk-back-link").contains("Back");

        cy.getByClass("govuk-fieldset__heading").contains("Which method do you want to use?");

        cy.getByTestId("Individual").should('not.be.checked');
        cy.getByLabelFor("method").contains("Creating an individual project");

        cy.getByTestId("Bulk").should('not.be.checked');
        cy.getByLabelFor("method-2").contains("Bulk upload");

        cy.getByTestId("continue").should("be.visible").contains("Continue");

        cy.getById("subsection-title").contains("Guidance");

        cy.contains("Creating individual new free school project");
        cy.contains("Bulk upload of new free achool projects");

        return this;
    }

    public selectIndividualProject(): this {
        cy.getByTestId("Individual").click();
        cy.getByTestId("Individual").should("be.checked");
        cy.getByTestId("Bulk").should("not.be.checked");

        return this;
    }

    public selectBulkUpload(): this {
        cy.getByTestId("Bulk").click();
        cy.getByTestId("Bulk").should("be.checked");
        cy.getByTestId("Individual").should("not.be.checked");

        return this;
    }

    public selectContinue(): this {
        cy.getByTestId("continue").click();

        return this;
    }
}

const whichProjectMethodPage = new WhichProjectMethodPage();

export default whichProjectMethodPage;