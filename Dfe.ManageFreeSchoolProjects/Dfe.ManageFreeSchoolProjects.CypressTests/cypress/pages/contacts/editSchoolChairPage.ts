
class EditSchoolChairPage {

    withSchoolChairName(value: string): this {
        cy.getByTestId("edit-school-chair-name").clear().type(value)
        return this;
    }
    withSchoolChairEmail(value: string): this {
        cy.getByTestId("edit-school-chair-email").clear().type(value)
        return this;
    }
    errorForSchoolChairName(error: string): this {
        cy.getById('school-chair-name-error').contains(error)
        return this
    }
    errorForSchoolChairEmail(error: string): this {
        cy.getById('school-chair-email-error').contains(error)
        return this
    }

    clickContinue() : this {
        cy.getByTestId("continue").click();
        return this;
    }
}

const editSchoolChairPage = new EditSchoolChairPage();

export default editSchoolChairPage;