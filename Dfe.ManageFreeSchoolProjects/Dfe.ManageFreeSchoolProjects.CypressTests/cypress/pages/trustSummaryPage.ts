class TrustSummaryPage {

    public verifyTrustSummaryElementsVisible(): this {
        cy.getByClass("govuk-back-link").contains("Back");
        cy.getByClass("govuk-heading-xl").contains("Trust");

        cy.getByClass("govuk-summary-list__key").eq(0).contains("TRN (trust reference number)");
        cy.getByClass("govuk-summary-list__value").eq(0).contains("Empty");
        cy.getByClass("govuk-link").eq(2).contains("Change");

        cy.getByClass("govuk-summary-list__key").eq(1).contains("Trust name");
        cy.getByClass("govuk-summary-list__value").eq(1).contains("Empty");

        cy.getByClass("govuk-summary-list__key").eq(2).contains("Trust type");
        cy.getByClass("govuk-summary-list__value").eq(2).contains("Empty");

        cy.getById("mark-as-complete").should("not.be.checked");
        cy.contains("Mark this section as complete, you can still make changes later");

        cy.getByClass("govuk-button").should("be.visible").contains("Confirm and continue");
        
        return this;
    }

    public verifyTrustSummaryCompleteElementsVisible(): this {
        cy.getByClass("govuk-heading-xl").contains("Dates");

        cy.getByClass("govuk-summary-list__key").eq(0).contains("Entry into pre-opening");
        cy.getByClass("govuk-summary-list__value").eq(0).contains("28 February 2025");
        cy.getByClass("govuk-link").eq(2).contains("Change");

        cy.getByClass("govuk-summary-list__key").eq(1).contains("Provisional opening date agreed with trust");
        cy.getByClass("govuk-summary-list__value").eq(1).contains("28 February 2025");
        cy.getByClass("govuk-link").eq(3).contains("Change");

        cy.getByClass("govuk-summary-list__key").eq(2).contains("Opening academic year");
        cy.getByClass("govuk-summary-list__value").eq(2).contains("2025/26");
        cy.getByClass("govuk-link").eq(4).contains("Change");

        cy.getById("mark-as-complete").should("not.be.checked");
        cy.contains("Mark this section as complete, you can still make changes later");

        cy.getByClass("govuk-button").should("be.visible").contains("Confirm and continue");
        
        return this;
    }



    public selectChangeTRNToGoToTrustDetails(): this {
        cy.contains("Change").click();

        return this;
    }

    public selectMarkItemAsComplete(): this {
        cy.getById("mark-as-complete").click();
        return this;
    }

    public selectConfirmAndContinue(): this {
        cy.contains("Confirm and continue").click();
    }
}

const trustSummaryPage = new TrustSummaryPage();

export default trustSummaryPage;