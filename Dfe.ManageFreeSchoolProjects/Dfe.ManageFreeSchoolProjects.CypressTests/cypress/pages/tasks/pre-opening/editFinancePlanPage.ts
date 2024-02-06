class EditFinancePlanPage {
    public titleIs(title: string): this {
        cy.getByTestId("title").should("contains.text", title)
        return this;
    }

    public schoolNameIs(school: string) {
        cy.getByTestId("school-name").should("contains.text", school);
        return this;
    }

    clickContinue(): this {
        cy.getByTestId("continue").click();
        return this;
    }

    public checkFinancePlanAgreed(): this {
        cy.getById("finance-plan-agreed").click();
        return this;
    }

    public withDateAgreed(day: string, month: string, year: string): this {

        this.enterDate("date-agreed", day, month, year);

        return this;
    }

    public checkPlanSavedInWorkplacesFolder(): this {
        cy.getById("plan-saved-in-workplaces-folder").click();
        return this;
    }

    public withLocalAuthorityAgreedToUnderwritePupilNumbers(value: string): this {
        cy.getByTestId(`local-authority-agreed-${value}`).click();
        return this;
    }

    public withComments(value: string): this {
        cy.getByTestId("comments").clear().type(value);
        return this;
    }

    public withCommentsExceedingMaxLength(): this {
        cy.getByTestId("comments").clear().invoke("val", "a".repeat(1000));
        return this;
    }

    public checkTrustWillOptInToRpa(): this {
        cy.getById(`trust-opt-into-rpa`).click();
        return this;
    }

    private enterDate(idPrefix: string, day: string, month: string, year: string) {
        cy.getById(`${idPrefix}-day`).clear();
        cy.getById(`${idPrefix}-month`).clear();
        cy.getById(`${idPrefix}-year`).clear();

        if (day.length > 0) {
            cy.getById(`${idPrefix}-day`).type(day);
        }

        if (month.length > 0) {
            cy.getById(`${idPrefix}-month`).type(month);
        }

        if (year.length > 0) {
            cy.getById(`${idPrefix}-year`).type(year);
        }
    }
}

const editFinancePlanPage = new EditFinancePlanPage();

export default editFinancePlanPage;