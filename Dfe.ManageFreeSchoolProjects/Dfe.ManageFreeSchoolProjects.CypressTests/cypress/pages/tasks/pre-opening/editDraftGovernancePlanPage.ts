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

    public checkPlanReceivedFromTrust(): this {
        cy.getById("plan-received-from-trust").check();
        return this;
    }

    public withDatePlanReceived(day: string, month: string, year: string): this {
        cy.enterDate("date-plan-received", day, month, year);

        return this;
    }

    public checkPlanAssessedUsingTemplate(): this {
        cy.getById("plan-assessed-using-template").check();
        return this;
    }

    public checkPlanAndAssessmentSharedWithExpert(): this {
        cy.getById("plan-and-assessment-shared-with-expert").check();
        return this;
    }

    public checkPlanAndAssessmentSharedWithEsfa(): this {
        cy.getById("plan-and-assessment-shared-with-esfa").check();
        return this;
    }

    public checkFedBackToTrustOnPlan(): this {
        cy.getById("fed-back-to-trust-on-plan").check();
        return this;
    }

    public checkDocumentsSavedInWorkplacesFolder(): this {
        cy.getById("documents-saved-in-workplaces-folder").check();
        return this;
    }

    public withComments(value: string): this {
        cy.getById("comments").clear().type(value);

        return this;
    }

    public withCommentsExceedingMaxLength(): this {
        cy.getById("comments").clear().invoke("text", "a".repeat(1000));

        return this;
    }
}

const editDraftGovernancePlanPage = new EditDraftGovernancePlanPage();

export default editDraftGovernancePlanPage;