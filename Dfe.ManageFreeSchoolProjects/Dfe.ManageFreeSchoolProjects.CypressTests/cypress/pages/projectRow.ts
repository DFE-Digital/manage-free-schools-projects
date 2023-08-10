export class ProjectRow {
    constructor(private element: Element) {}

    public hasProjectId(value: string): this {
        this.containsText("project-id", value);

        return this;
    }

    public hasProjectTitle(value: string): this {
        this.containsText("project-title", value);

        return this;
    }

    private containsText(id: string, value: string) {
        cy.wrap(this.element).within(() => {
            cy.getByTestId(id).should("contain.text", value);
        });
    }
}
