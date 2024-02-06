class TaskListPage {

    public selectDatesFromTaskList(): this {
        cy.getByTestId("dates-task").click()
        return this;
    }

    public selectSchoolFromTaskList(): this {
        cy.getByTestId("school-task").click()
        return this;
    }

    public selectRegionAndLAFromTaskList(): this {
        cy.getByTestId("region-la-task").click()
        return this;
    }

    public selectTrustFromTaskList(): this {
        cy.getByTestId("trust-task").click()
        return this;
    }

    public selectConstituencyFromTaskList(): this {
        cy.getByTestId("constituency-task").click()
        return this;
    }

    public selectRiskAppraisalMeetingFromTaskList(): this {
        cy.getByTestId("risk-appraisal-meeting-task").click()
        return this;
    }

    public selectArticlesOfAssociationFromTaskList(): this {
        cy.getByTestId("articles-of-association-task").click()
        return this;
    }

    public selectFinancePlanFromTaskList(): this {
        cy.getByTestId("finance-plan-task").click()
        return this;
    }

    public isTaskStatusIsNotStarted(taskName: string): this {
        cy.get(`[data-testid="task-${taskName}-status"]`).should("contains.text", "Not started");
        return this;
    }

    public isTaskStatusInProgress(taskName: string): this {
        cy.get(`[data-testid="task-${taskName}-status"]`).should("contains.text", "In progress");
        return this;
    }

    public isTaskStatusIsCompleted(taskName: string): this {
        cy.get(`[data-testid="task-${taskName}-status"]`).contains("Completed");
        return this;
    }
}

const taskListPage = new TaskListPage();

export default taskListPage;