import { ProjectDetailsRequest } from "cypress/api/domain";
import projectApi from "cypress/api/projectApi";
import { RequestBuilder } from "cypress/api/requestBuilder";
import { Logger } from "cypress/common/logger";
import summaryPage from "cypress/pages/task-summary-base";
import taskListPage from "cypress/pages/taskListPage";

describe("Testing the admissions arragements task", () => {

    let project: ProjectDetailsRequest;

    beforeEach(() => {
        cy.login();

        project = RequestBuilder.createProjectDetails();

        projectApi
            .post({
                projects: [project],
            })
            .then(() => {
                cy.visit(`/projects/${project.projectId}/tasks`);
            });
    });

    it("Should be able to set admissions arragements", () => {
        Logger.log("Select admissions arragements");
        taskListPage.isTaskStatusIsNotStarted("AdmissionsArrangements")
            .selectAdmissionsArrangementsFromTaskList();

        Logger.log("Go back to task list");
        summaryPage.clickBack();

        taskListPage.selectAdmissionsArrangementsFromTaskList();

        Logger.log("Confirm empty admissions arrangements");
        summaryPage
            .schoolNameIs(project.schoolName)
            .titleIs("Admissions arrangements");

        cy.executeAccessibilityTests();

        Logger.log("Should update the task status");
        summaryPage.clickConfirmAndContinue();

        taskListPage.isTaskStatusInProgress("AdmissionsArrangements");

        taskListPage.selectAdmissionsArrangementsFromTaskList();

        summaryPage
            .MarkAsComplete()
            .clickConfirmAndContinue();

        taskListPage.isTaskStatusIsCompleted("AdmissionsArrangements");
    });
});