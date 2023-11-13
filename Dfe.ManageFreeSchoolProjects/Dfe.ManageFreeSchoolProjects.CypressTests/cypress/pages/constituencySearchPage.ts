import validationComponent from "./validationComponent";

class ConstituencySearchPage {
    public schoolNameIs(school: string): this {
        cy.get(".govuk-label--xl").get(".govuk-caption-l").should("contains.text", school);
        return this;
    }

    public titleIs(title: string): this {
        cy.get(".govuk-label--xl").should("contains.text", title)
        return this;
    }

    public searchHintIs(hint: string): this {
        cy.get("#search-constituency-hint").should("contains.text", hint);
        return this;
    }

    public errorMessage(error: string): this {
        validationComponent.hasValidationError(error);
        return this;
    }

    public errorHint(error: string): this {
        cy.get("#search-constituency-error").should("contains.text", error);
        return this;
    }

    public enterSearch(searchTerm: string): this {
        cy.get("#search-constituency").type(searchTerm);
        return this;
    }

    public clickBack(): this {
        cy.get(".govuk-back-link").click();
        return this;
    }

    public clickContinue(): this {
        cy.getByTestId("continue").click();
        return this;
    }
}

const constituencySearchPage = new ConstituencySearchPage();
export default constituencySearchPage;
