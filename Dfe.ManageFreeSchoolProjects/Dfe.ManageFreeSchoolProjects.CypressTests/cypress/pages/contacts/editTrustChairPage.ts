
class EditTrustChairPage {

    withTrustChairName(value: string): this {
        cy.getByTestId("edit-trust-chair-name").clear().type(value)
        return this;
    }
    withTrustChairEmail(value: string): this {
        cy.getByTestId("edit-trust-chair-email").clear().type(value)
        return this;
    }
    errorForTrustChairName(error: string): this {
        cy.getById('trust-chair-name-error').contains(error)
        return this
    }
    errorForTrustChairEmail(error: string): this {
        cy.getById('trust-chair-email-error').contains(error)
        return this
    }

    clickContinue() : this {
        cy.getByTestId("continue").click();
        return this;
    }
}

const editTrustChairPage = new EditTrustChairPage();

export default editTrustChairPage;