class TaskListPage {

    public selectDatesFromTaskList(): this {
        cy.contains("Dates").click()
        return this;
    }

    public verifyDatesMarkedAsComplete(): this {
        cy.getByClass("app-task-list__item").eq(1).contains("Completed");
    }

<<<<<<< HEAD
=======
    public selectSchoolFromTaskList(): this {
        cy.contains("School").click()
        return this;
    }

    public verifySchoolMarkedAsComplete(): this {
        cy.getByClass("app-task-list__item").eq(0).contains("Completed");

        return this;
    }

>>>>>>> main
    public selectTrustFromTaskList(): this {
        cy.contains("Trust").click()
        return this;
    }

    public verifyTrustMarkedAsComplete(): this {
<<<<<<< HEAD
        cy.getByClass("app-task-list__item").eq(3).contains("Completed");
=======
        cy.getByClass("app-task-list__item").eq(2).contains("Completed");
>>>>>>> main
    }
}

const taskListPage = new TaskListPage();

export default taskListPage;