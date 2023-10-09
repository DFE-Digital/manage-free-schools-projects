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

        Logger.log("Selecting Dates link from Tasklist")
        taskListPage.selectDatesFromTaskList();

        Logger.log("Checking Dates Summary page elements present")
        datesSummaryPage.verifyDatesSummaryElementsVisible();

        Logger.log("Selecting first Change link from first 'Pre-opening' line")
        datesSummaryPage.selectChangePreopeningToGoToDatesDetails();



    });
});
