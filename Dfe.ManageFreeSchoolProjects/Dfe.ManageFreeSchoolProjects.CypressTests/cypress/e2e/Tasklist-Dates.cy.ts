import { ProjectDetailsRequest } from "cypress/api/domain";
import projectApi from "cypress/api/projectApi";
import { RequestBuilder } from "cypress/api/requestBuilder";
import { Logger } from "cypress/common/logger";
import datesSummaryPage from "cypress/pages/datesSummaryPage";
import projectOverviewPage from "cypress/pages/projectOverviewPage";
import taskListPage from "cypress/pages/taskList";

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
                cy.visit(`/projects/${project.projectId}/overview`);
            });
    });

    it("Should successfully set project dates", () => {
        Logger.log("Clicking on Task list tab");

        projectOverviewPage.selectTaskListTab();

        taskListPage.selectDatesFromTaskList();

        datesSummaryPage.verifyDatesSummaryElementsVisible();

        datesSummaryPage.selectChangePreopeningToGoToDatesDetails();



    });
});
