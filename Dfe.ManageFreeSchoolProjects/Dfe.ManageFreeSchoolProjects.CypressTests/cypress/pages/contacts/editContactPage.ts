
class EditContactPage {

    withContactName(value: string): this {
        cy.getByTestId("edit-contact-name").clear().type(value)
        return this;
    }

    withNullContactName(): this {
        cy.getByTestId("edit-contact-name").clear()
        return this;
    }

    withContactEmail(value: string): this {
        cy.getByTestId("edit-contact-email").clear().type(value)
        return this;
    }

    withNullContactEmail(): this {
        cy.getByTestId("edit-contact-email").clear()
        return this;
    }

    errorForContactName(error: string): this {
        cy.getById('contact-name-error').contains(error)
        return this
    }
    errorForContactEmail(error: string): this {
        cy.getById('contact-email-error').contains(error)
        return this
    }

    public hasTitle(value: string): this {
        cy.getByTestId(`edit-contact-title`).should("contain.text", value);

        return this;
    }
    public hasSchoolName(value: string): this {

        cy.getByTestId(`school-name`).should("contain.text", value);

        return this;
    }

    clickContinue() : this {
        cy.getByTestId("continue").click();
        return this;
    }
}

const editContactPage = new EditContactPage();

export default editContactPage;