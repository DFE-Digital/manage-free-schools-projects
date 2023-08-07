export class ProjectRow {
    constructor(private element: Element) {}

    public hasProjectId(value: string): this {
        this.containsText("project-id", value);

        return this;
    }

    public hasSchoolName(value: string): this {
        this.containsText("school-name", value);

        return this;
    }

    public hasApplicationName(value: string): this {
        this.containsText("application-name", value);

        return this;
    }

    public hasApplicationWave(value: string): this {
        this.containsText("application-wave", value);

        return this;
    }

    private containsText(id: string, value: string) {
        cy.wrap(this.element).within(() => {
            cy.getByTestId(id).should("contain.text", value);
        });
    }
}
