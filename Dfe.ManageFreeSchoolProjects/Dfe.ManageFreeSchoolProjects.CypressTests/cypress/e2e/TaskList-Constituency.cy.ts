import { ProjectDetailsRequest } from "cypress/api/domain";
import projectApi from "cypress/api/projectApi";
import { RequestBuilder } from "cypress/api/requestBuilder";
import { ConstituencySummaryPage } from "cypress/pages/constituencySummaryPage";
import taskListPage from "cypress/pages/taskListPage";

describe("Testing project overview", () => {
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

    
    it("Should successfully set Tasklist-school information", () => {
        taskListPage.isTaskStatusIsNotStarted("Constituency")
            .selectConstituencyFromTaskList();
        
        ConstituencySummaryPage
            .schoolNameIs(project.schoolName)
            .titleIs("Constituency")
            .summaryShows("Name").IsEmpty().HasChangeLink()
            .summaryShows("MP").IsEmpty().HasNoChangeLink()
            .summaryShows("Political party").IsEmpty().HasNoChangeLink()
            .isNotMarkedAsComplete();

        ConstituencySummaryPage.clickBack();

        taskListPage.isTaskStatusIsNotStarted("Constituency")
            .selectConstituencyFromTaskList();

        ConstituencySummaryPage.clickConfirmAndContinue();

        taskListPage.isTaskStatusInProgress("Constituency")
            .selectConstituencyFromTaskList();

        ConstituencySummaryPage.clickChange();
    });
});