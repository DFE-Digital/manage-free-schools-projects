class ProjectStatusPage {
    private errorTracking = "";
    public preOpeningIsChecked(): this {
        cy.getById("project-status-pre-opening").should("be.checked");
        return this;
    }

    public selectPreOpening(): this {
        cy.getById("project-status-pre-opening").click();
        return this;
    }

    public openIsChecked(): this {
        cy.getById("project-status-open").should("be.checked");
        return this;
    }

    public selectOpen(): this {
        cy.getById("project-status-open").click();
        return this;
    }

    public cancelledIsChecked(): this {
        cy.getById("project-status-cancelled").should("be.checked");
        return this;
    }

    public selectCancelled(): this {
        cy.getById("project-status-cancelled").click();
        return this;
    }

    public addCancelledYear(day: string, month: string, year: string): this {
        cy.enterDate("year-cancelled", day, month, year);
        return this;
    }

    public cancelledYearHasValue(day: string, month: string, year: string): this {
        cy.checkDate("year-cancelled", day, month, year);
        return this;
    }

    public closedIsChecked(): this {
        cy.getById("project-status-closed").should("be.checked");
        return this;
    }

    public selectClosed(): this {
        cy.getById("project-status-closed").click();
        return this;
    }

    public addClosedYear(day: string, month: string, year: string): this {
        cy.enterDate("year-closed", day, month, year);
        return this;
    }

    public closedYearHasValue(day: string, month: string, year: string): this {
        cy.checkDate("year-closed", day, month, year);
        return this;
    }

    public selectWithdrawn(): this {
        cy.getById("project-status-withdrawn").click();
        return this;
    }

    public addWithdrawnYear(day: string, month: string, year: string): this {
        cy.enterDate("year-withdrawn", day, month, year);
        return this;
    }

    public withdrawnYearHasValue(day: string, month: string, year: string): this {
        cy.checkDate("year-withdrawn", day, month, year);
        return this;
    }

    public withdrawnIsChecked(): this {
        cy.getById("project-status-withdrawn").should("be.checked");
        return this;
    }

    public selectWithdrawnInApplication(): this {
        cy.getById("project-status-withdrawn-application").click();
        return this;
    }

    public addWithdrawnInApplicationYear(day: string, month: string, year: string): this {
        cy.enterDate("year-withdrawn-application", day, month, year);
        return this;
    }

    public withdrawnInApplicationYearHasValue(value: string): this {
        cy.getById("year-withdrawn-application").should("have.value", value);
        return this;
    }
    
    public clickSaveAndContine(): this {
        cy.getByTestId("save-and-continue").click();
        return this;
    }

    public clickBackLink(): this {
        cy.getById("back-link").click();
        return this;
    }

    errorForCancelledDate(error: string): this {
        cy.getById('year-cancelled-error-link').contains(error)
        return this
    }

    errorForClosedDate(error: string): this {
        cy.getById('year-closed-error-link').contains(error)
        return this
    }

    errorForWithdrawnDate(error: string): this {
        cy.getById('year-withdrawn-error-link').contains(error)
        return this
    }

    
    errorForWithdrawnInApplicationDate(error: string): this {
        cy.getById('year-withdrawn-application-error-link').contains(error)
        return this
    }
}

const projectStatusPage = new ProjectStatusPage();

export default projectStatusPage;