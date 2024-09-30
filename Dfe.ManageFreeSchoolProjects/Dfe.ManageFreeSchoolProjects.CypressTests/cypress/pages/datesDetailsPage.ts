class DatesDetailsPage {

    public titleIs(title: string): this {
        cy.getByTestId("title").should("contains.text", title)
        return this;
    }

    public schoolNameIs(school: string) {
        cy.getByTestId("school-name").should("contains.text", school);
        return this;
    }

    public clickContinue(): this {
        cy.getByClass("govuk-button").click();
        return this;
    }

    public addCancelledDate(day: string, month: string, year: string): this {
        cy.enterDate("project-cancelled-date", day, month, year);
        return this;
    }
    
    public withEntryIntoPreOpening(day: string, month: string, year: string): this {
        cy.enterDate("entry-into-pre-opening", day, month, year);
        return this;
    }

    public withProvisionalOpeningDateAgreedWithTrust(day: string, month: string, year: string): this {
        cy.enterDate("provisional-opening-date-agreed-with-trust", day, month, year);
        return this;
    }

    withRealisticYearOfOpeningStartDate(year: string): this {
        cy.getById("realistic-year-of-opening-startyear").typeFast(year)
        return this;
    }

    withRealisticYearOfOpeningEndDate(year: string): this {
        cy.getById("realistic-year-of-opening-endyear").typeFast(year)
        return this;
    }
}

const datesDetailsPage = new DatesDetailsPage();

export default datesDetailsPage;