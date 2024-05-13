class ContactsSummaryPage {

    public hasProjectManagedByName(value: string): this {
        cy.getByTestId(`project-managed-by-name`).should(`contain.text`, value);
        return this;
    }

    public hasProjectManagedByEmail(value: string): this {
        cy.getByTestId(`project-managed-by-email`).should(`contain.text`, value);
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

    public goToEditProjectManagedBy(): this {
        cy.getByTestId("edit-project-managed-by").click();

        return this;
    }

    public goToProjectsOverviewPage(): this {
        cy.getByTestId("back-button").click();

        return this;
    }
}

const contactsSummaryPage = new ContactsSummaryPage();

export default contactsSummaryPage;