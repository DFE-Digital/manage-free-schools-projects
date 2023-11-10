export class ProjectRiskHistoryRow {
    constructor(private element: Element) {

    }

    public hasDate(value: string): this {
        this.containsText("project-risk-history-date", value);

        return this;
    }

    public hasRiskRating(values: string[]): this {

        cy.wrap(this.element)
            .within(() => {
                cy.getByTestId("project-risk-history-rating")
                    .children()
                    .should("have.length", values.length)
                    .each((el, i) => {
                        expect(el.text()).to.equal(values[i]);
                    });
            });

        return this;
    }

    public view() {
        cy.wrap(this.element).within(() => {
            this.getViewLink().click();
        })
    }

    public hasNoViewLink() {
        cy.wrap(this.element)
            .within(() => {
                this.getViewLink().should("not.exist");
            });
    }

    private getViewLink() {
        return cy.getByTestId("project-risk-view-link");
    }

    private containsText(id: string, value: string) {
        cy.wrap(this.element)
            .within(() => {
                cy.getByTestId(id).should("contain.text", value);
            });
    }

}
