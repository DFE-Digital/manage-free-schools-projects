import BasePage from "./BasePage";

class TrustSummaryPage extends BasePage{

    public verifyTrustSummaryElementsVisible(schoolName: string): this {
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

    public verifyTrustSummaryCompleteElementsVisible(schoolName: string, validTrustId: string): this {
        cy.getByClass("govuk-back-link").contains("Back");
        cy.getByClass("govuk-heading-xl").contains("Trust");

        cy.getByClass("govuk-summary-list__key").eq(0).contains("TRN (trust reference number)");
        cy.getByClass("govuk-summary-list__value").eq(0).contains(validTrustId);
        cy.getByClass("govuk-link").eq(2).contains("Change");

        cy.getByClass("govuk-summary-list__key").eq(1).contains("Trust name");
        cy.getByClass("govuk-summary-list__value").eq(1).contains("King's Group Academies");

        cy.getByClass("govuk-summary-list__key").eq(2).contains("Trust type");
        cy.getByClass("govuk-summary-list__value").eq(2).contains("MAT");

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