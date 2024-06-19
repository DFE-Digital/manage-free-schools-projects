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

    public addCancelledYear(value: string): this {
        cy.getById("cancelled-year").clear().type(value);
        return this;
    }

    public cancelledYearHasValue(value: string): this {
        cy.getById("cancelled-year").should("have.value",value);
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

    public addClosedYear(value: string): this {
        cy.getById("closed-year").clear().type(value);
        return this;
    }

    public closedYearHasValue(value: string): this {
        cy.getById("closed-year").should("have.value", value);
        return this;
    }

    public selectWithdrawn(): this {
        cy.getById("project-status-withdrawn").click();
        return this;
    }

    public addWithdrawnYear(value: string): this {
        cy.getById("withdrawn-year").clear().type(value);
        return this;
    }

    public withdrawnYearHasValue(value: string): this {
        cy.getById("withdrawn-year").should("have.value", value);
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
        cy.getById('cancelled-year-error-error-link').contains(error)
        return this
    }

    errorForCancelledDateCount(error: string): this {
        cy.getById('cancelled-year-count-error-error-link').contains(error)
        return this
    }

    errorForClosedDate(error: string): this {
        cy.getById('closed-year-error-error-link').contains(error)
        return this
    }

    errorForClosedDateCount(error: string): this {
        cy.getById('closed-year-count-error-error-link').contains(error)
        return this
    }

    errorForWithdrawnDate(error: string): this {
        cy.getById('withdrawn-year-error-error-link').contains(error)
        return this
    }

    errorForWithdrawnDateCount(error: string): this {
        cy.getById('withdrawn-year-count-error-error-link').contains(error)
        return this
    }
}

const projectStatusPage = new ProjectStatusPage();

export default projectStatusPage;