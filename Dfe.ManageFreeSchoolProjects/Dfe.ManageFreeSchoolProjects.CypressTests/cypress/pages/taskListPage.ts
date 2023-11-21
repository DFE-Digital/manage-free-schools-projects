class TaskListPage {

    public selectDatesFromTaskList(): this {
        cy.contains("Dates").click()
        return this;
    }

    public verifyDatesMarkedAsComplete(): this {
        cy.getByClass("app-task-list__item").eq(1).contains("Completed");
    }

    public selectSchoolFromTaskList(): this {
        cy.contains("School").click()
        return this;
    }

    public verifySchoolMarkedAsComplete(): this {
        cy.getByClass("app-task-list__item").eq(0).contains("Completed");

        return this;
    }

    public selectRegionAndLAFromTaskList(): this {
        cy.contains("Region and local authority").click()
        return this;
    }

    public selectTrustFromTaskList(): this {
        cy.contains("Trust").click()
        return this;
    }

    public verifyTrustMarkedAsComplete(): this {
        cy.getByClass("app-task-list__item").eq(2).contains("Completed");
        return this;
    }

    public selectConstituencyFromTaskList(): this {
        cy.contains("Constituency").click()
        return this;
    }

    public selectRiskAppraisalMeetingFromTaskList(): this {
        cy.contains("Risk appraisal meeting").click()
        return this;
    }
    
    public verifyRegionAndLAMarkedAsComplete(): this {
        cy.getByClass("app-task-list__item").eq(4).contains("Completed");
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