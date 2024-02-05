import { ProjectDetailsRequest } from "cypress/api/domain";
import projectApi from "cypress/api/projectApi";
import { RequestBuilder } from "cypress/api/requestBuilder";
import summaryPage from "cypress/pages/task-summary-base";
import taskListPage from "cypress/pages/taskListPage";

describe("Testing finance plan task", () => {
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

    it("Should be able to set a finance plan", () => {

        cy.log("Go back to task list");
        summaryPage.clickBack();

        cy.log("Select articles of association");
        taskListPage.isTaskStatusIsNotStarted("FinancePlan")
            .selectArticlesOfAssociationFromTaskList();

        cy.log("Confirm empty articles of association");
        summaryPage
            .schoolNameIs(project.schoolName)
            .titleIs("Finance plan")
            .inOrder()
            .summaryShows("Finance plan agreed").IsEmpty().HasChangeLink()
            .summaryShows("Date agreed").IsEmpty().HasChangeLink()
            .summaryShows("Plan saved in Workspaces folder").IsEmpty().HasChangeLink()
            .summaryShows("Local authority agreed to underwrite pupil numbers").IsEmpty().HasChangeLink()
            .summaryShows("Comments").IsEmpty().HasChangeLink()
            .summaryShows("Trust with opt into RPA (risk protection arrangement)").IsEmpty().HasChangeLink()
            .isNotMarkedAsComplete();

        cy.executeAccessibilityTests();
    });
});