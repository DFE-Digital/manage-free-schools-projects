class EditDraftGovernancePlanPage {
    public titleIs(title: string): this {
        cy.getByTestId("title").should("contains.text", title)
        return this;
    }

    public schoolNameIs(school: string) {
        cy.getByTestId("school-name").should("contains.text", school);
        return this;
    }

    public clickContinue(): this {
        cy.getByTestId("continue").click();
        return this;
    }

    public withForecastDate(day: string, month: string, year: string): this {
        cy.enterDate("forecast-date", day, month, year);

        return this;
    }

    public withActualDate(day: string, month: string, year: string): this {
        cy.enterDate("actual-date", day, month, year);

        return this;
    }

    public withCommentsOnDecisionToApprove(value: string): this {
        cy.getById("comments").clear().type(value);

        return this;
    }

    public withCommentsOnDecisionToApproveExceedingMaxLength(): this {
        cy.getById("comments").clear().invoke("text", "a".repeat(1000));

        return this;
    }

    public withSharepointLink(value: string): this {
        cy.getById("sharepoint-link").clear().type(value);

        return this;
    }

    public withSharepointLinkExceedingMaxLength(): this {
        cy.getById("sharepoint-link").clear().invoke("val", "a".repeat(501));

        return this;
    }
}

const editDraftGovernancePlanPage = new EditDraftGovernancePlanPage();

export default editDraftGovernancePlanPage;