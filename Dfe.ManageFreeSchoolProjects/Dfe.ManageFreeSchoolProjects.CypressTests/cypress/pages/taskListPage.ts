class TaskListPage {

    public selectDatesFromTaskList(): this {
        cy.contains("Dates").click()
        return this;
    }

    public verifyDatesMarkedAsComplete(): this {
        cy.getByClass("app-task-list__item").eq(1).contains("Completed");
    }

    public selectTrustFromTaskList(): this {
        cy.contains("Trust").click()
        return this;
    }

    public verifyTrustMarkedAsComplete(): this {
        cy.getByClass("app-task-list__item").eq(2).contains("Completed");
    }
}

const taskListPage = new TaskListPage();

export default taskListPage;