class ContactsPage {

    public hasProjectAssignedToName(value: string): this {
        cy.getByTestId(`project-assigned-to-name`).should(`contain.text`, value);
        return this;
    }

    public hasProjectAssignedToEmail(value: string): this {
        cy.getByTestId(`project-assigned-to-email`).should(`contain.text`, value);
        return this;
    }

    public hasSchoolChairName(value: string): this {
        cy.getByTestId(`school-chair-name`).should(`contain.text`, value);
        return this;
    }
    public hasSchoolChairEmail(value: string): this {
        cy.getByTestId(`school-chair-email`).should(`contain.text`, value);
        return this;
    }

    public hasTitle(value: string): this {
        cy.getByTestId(`contacts-summary-title`).should("contain.text", value);

        return this;
    }
    public hasSchoolName(value: string): this {

        cy.getByTestId(`school-name`).should("contain.text", value);

        return this;
    }

    public goToEditSchoolChair(): this {
        cy.getByTestId("edit-school-chair").click();

        return this;
    }

    public goToEditProjectAssignedTo(): this {
        cy.getByTestId("edit-project-assigned-to").click();

        return this;
    }

    public goToProjectsOverviewPage(): this {
        cy.getByTestId("back-button").click();

        return this;
    }
}

const contactsPage = new ContactsPage();

export default contactsPage;