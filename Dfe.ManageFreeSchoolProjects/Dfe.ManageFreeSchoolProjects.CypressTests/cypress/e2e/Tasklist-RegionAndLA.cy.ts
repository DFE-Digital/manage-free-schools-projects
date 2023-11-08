import { ProjectDetailsRequest } from "cypress/api/domain";
import projectApi from "cypress/api/projectApi";
import { RequestBuilder } from "cypress/api/requestBuilder";
import { Logger } from "cypress/common/logger";
import trustDetailsPage from "cypress/pages/trustDetailsPage";
import trustSummaryPage from "cypress/pages/trustSummaryPage";
import projectOverviewPage from "cypress/pages/projectOverviewPage";
import taskListPage from "cypress/pages/taskListPage";
import confirmTrustPage from "cypress/pages/confirmTrustPage";
import schoolSummaryPage from "cypress/pages/schoolSummaryPage";
import schoolDetailsPage from "cypress/pages/schoolDetailsPage";
import regionAndLocalAuthoritySummaryPage from "cypress/pages/regionAndLocalAuthoritySummaryPage";
import regionAndLocalAuthorityDetailsPage from "cypress/pages/regionAndLocalAuthorityDetailsPage";

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

    it("Should successfully set Tasklist-Region And LA information", () => {

        Logger.log("Clicking on Task list tab");
        projectOverviewPage.selectTaskListTab();

        cy.executeAccessibilityTests();

        Logger.log("Selecting School link from Tasklist");
        taskListPage.selectRegionAndLAFromTaskList();

        cy.executeAccessibilityTests();

        Logger.log("Checking Region and LA Summary page elements present");
        regionAndLocalAuthoritySummaryPage.verifyRegionAndLASummaryElementsVisible(project.schoolName);

        Logger.log("Selecting first Change link from first 'Region' line");
        regionAndLocalAuthoritySummaryPage.selectChangeRegionToGoToRegionDetails();

        cy.executeAccessibilityTests();

        regionAndLocalAuthorityDetailsPage.checkElementsVisible(project.schoolName);



        Logger.log("Test that submitting a blank form on Region page results in a validation error prompting us to make a selection");
        regionAndLocalAuthorityDetailsPage.selectContinue();

        cy.executeAccessibilityTests();

        regionAndLocalAuthorityDetailsPage.verifyValidationMessage();

        schoolDetailsPage.selectSaveAndContinue();

        schoolSummaryPage.verifySchoolSummaryCompleteElementsVisible(project.schoolName);

        cy.executeAccessibilityTests();

        schoolSummaryPage.selectMarkItemAsComplete();

        schoolSummaryPage.selectConfirmAndContinue();

        taskListPage.verifySchoolMarkedAsComplete();

    });
});
