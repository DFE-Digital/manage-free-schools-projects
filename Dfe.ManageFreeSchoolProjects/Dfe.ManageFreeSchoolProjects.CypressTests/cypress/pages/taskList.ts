class TaskListPage {

    public selectDatesFromTaskList(): this {
        cy.contains("Dates").click()
        return this;
    }
}

const taskListPage = new TaskListPage();

export default taskListPage;