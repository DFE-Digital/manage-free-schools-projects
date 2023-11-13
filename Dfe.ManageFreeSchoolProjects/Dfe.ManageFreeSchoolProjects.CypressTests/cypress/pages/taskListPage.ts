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

    public verifyRegionAndLAMarkedAsComplete(): this {
        cy.getByClass("app-task-list__item").eq(3).contains("Completed");
        return this;
    }


}

const taskListPage = new TaskListPage();

export default taskListPage;