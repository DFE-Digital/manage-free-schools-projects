export class ConstituencySummaryPage {
    private summaryCounter = -1;

    public inOrder(): this
    {
        this.summaryCounter = -1
        return this;
    }

    public titleIs(title: string): this {
        cy.getByTestId("title").should("contains.text", title)
        return this;
    }

    public schoolNameIs(school: string): this {
        cy.getByTestId("school-name").should("contains.text", school);
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

    public HasValue(value): this {
        cy.get(".govuk-summary-list__value").eq(this.summaryCounter).should("contains.text", value);
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
 
    public MarkAsComplete() {
        cy.getByTestId("mark-as-complete").click();
        return this;
    }
 
    public clickBack() {
        cy.get(".govuk-back-link").click();
    }

    public clickConfirmAndContinue() {
        cy.getByTestId("confirm").click();
    }

    public clickChange() {
        cy.contains("Change").click();
    }
}

const constituencySummaryPage = new ConstituencySummaryPage();
export default constituencySummaryPage;
