
class EditProjectManagedByPage {

    withProjectManagedByName(value: string): this {
        cy.getByTestId("edit-project-managed-by-name").clear().type(value)
        return this;
    }

    withNullProjectManagedByName(): this {
        cy.getByTestId("edit-project-managed-by-name").clear()
        return this;
    }

    withProjectManagedByEmail(value: string): this {
        cy.getByTestId("edit-project-managed-by-email").clear().type(value)
        return this;
    }

    withNullProjectManagedByEmail(): this {
        cy.getByTestId("edit-project-managed-by-email").clear()
        return this;
    }

    errorForProjectManagedByName(error: string): this {
        cy.getById('project-managed-by-name-error').contains(error)
        return this
    }
    errorForProjectManagedByEmail(error: string): this {
        cy.getById('project-managed-by-email-error').contains(error)
        return this
    }

    public hasTitle(value: string): this {
        cy.getByTestId(`edit-project-managed-by-title`).should("contain.text", value);

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

const editProjectManagedByPage = new EditProjectManagedByPage();

export default editProjectManagedByPage;