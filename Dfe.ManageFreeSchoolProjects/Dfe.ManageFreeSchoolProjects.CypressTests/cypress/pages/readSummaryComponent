class ReadSummaryComponent {
    readSummaryLine(listKey :string, listValue : string, index : number) : this {
        cy.getByClass("govuk-summary-list__key").eq(index).contains(listKey);
        cy.getByClass("govuk-summary-list__value").eq(index).contains(listValue);
        cy.getByClass("govuk-link").eq(index + 2).contains("Change");

        return this;
    }
}

const readSummaryComponent = new ReadSummaryComponent();
export default readSummaryComponent;
