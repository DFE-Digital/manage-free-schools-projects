export class ConstituencySummaryPage {
    private summaryCounter = -1;

    public static schoolNameIs(school: string): ConstituencySummaryPage {
        cy.get(".govuk-heading-xl").get(".govuk-caption-l").should("contains.text", school);
        return new ConstituencySummaryPage()
    }

    public titleIs(title: string): this {
        cy.get(".govuk-heading-xl").should("contains.text", title)
        return this;
    }

    public summaryShows(key: string): this {
        this.summaryCounter++;
        cy.get(".govuk-summary-list__key").eq(this.summaryCounter).should("contains.text", key);
        return this;
    }

    public IsEmpty(): this {
        cy.get(".govuk-summary-list__value").eq(this.summaryCounter).should("contains.text", "Empty");
        return this;
    }

    public HasChangeLink(): this {
        cy.get(".govuk-summary-list__row").eq(this.summaryCounter).contains("Change");
        return this;
    }

    public HasNoChangeLink(): this {
        cy.get(".govuk-summary-list__row").eq(this.summaryCounter).contains("Change").should('not.exist');
        return this;
    }

    public isNotMarkedAsComplete() {
        cy.getByTestId("mark-as-complete").should("not.be.checked");
        return this;
    }
 
    public verifyDatesSummaryElementsVisible(schoolName: string): this {
        cy.get(".govuk-heading-xl").get(".govuk-caption-l").contains(schoolName);
        cy.get(".govuk-heading-xl").contains("Dates");

        cy.get(".govuk-summary-list__key").eq(0).contains("Entry into pre-opening");
        cy.get(".govuk-summary-list__value").eq(0).contains("Empty");
        cy.get(".govuk-link").eq(2).contains("Change");

        cy.get(".govuk-summary-list__key").eq(1).contains("Provisional opening date agreed with trust");
        cy.get(".govuk-summary-list__value").eq(1).contains("Empty");
        cy.get(".govuk-link").eq(3).contains("Change");

        cy.get(".govuk-summary-list__key").eq(2).contains("Opening academic year");
        cy.get(".govuk-summary-list__value").eq(2).contains("Empty");
        cy.get(".govuk-link").eq(4).contains("Change");

        cy.getById("mark-as-complete").should("not.be.checked");
        cy.contains("Mark this section as complete, you can still make changes later");

        cy.get(".govuk-button").should("be.visible").contains("Confirm and continue");
        
        return this;
    }

    public static clickBack() {
        cy.get(".govuk-back-link").click();
    }

    public static clickConfirmAndContinue() {
        cy.getByTestId("confirm").click();
    }

    public static clickChange() {
        cy.contains("Change").click();
    }
}