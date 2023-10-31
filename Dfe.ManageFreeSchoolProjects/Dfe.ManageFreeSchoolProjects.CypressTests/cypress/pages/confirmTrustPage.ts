class ConfirmTrustPage {
    public verifyConfirmTrustElementsVisible(schoolName: string, validTrustId: string): this {
        cy.getByClass("govuk-back-link").contains("Back");
        cy.getByClass("govuk-heading-xl").getByClass("govuk-caption-l").contains(schoolName);
        cy.getByClass("govuk-heading-xl").contains("Confirm the trust");

        cy.getByClass("govuk-summary-list__key").eq(0).contains("Trust ID:");
        cy.getByClass("govuk-summary-list__value").eq(0).contains(validTrustId);

        cy.getByClass("govuk-summary-list__key").eq(1).contains("Trust name:");
        cy.getByClass("govuk-summary-list__value").eq(1).contains("King's Group Academies");

        cy.getByClass("govuk-summary-list__key").eq(2).contains("Trust type:");
        cy.getByClass("govuk-summary-list__value").eq(2).contains("MAT");

        cy.getByClass("govuk-button").should("be.visible").contains("Save and continue");

        return this;
    }

    public selectSaveAndContinue(): this {
        cy.getByClass("govuk-button").click();

        return this;
    }
}

const confirmTrustPage = new ConfirmTrustPage();

export default confirmTrustPage;