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
import regionDetailsPage from "cypress/pages/regionDetailsPage";
import localAuthorityDetailsPage from "cypress/pages/localAuthorityDetailsPage";

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

        regionDetailsPage.checkElementsVisible(project.schoolName);



        Logger.log("Test that submitting a blank form on Region page results in a validation error prompting us to make a selection");
        regionDetailsPage.selectContinue();

        cy.executeAccessibilityTests();

        regionDetailsPage.verifyValidationMessage();

        Logger.log("Testing that a user is unable to have >1 radio button checked at one time on the Region Details page");

        regionDetailsPage.selectEastMidlands()
                         .selectEastOfEngland()
                         .selectLondon()
                         .selectNorthEast()
                         .selectNorthWest()
                         .selectSouthEast()
                         .selectSouthWest()
                         .selectWestMidlands()
                         .selectYorkshireAndHumber();


        Logger.log("Testing that a user can select an option e.g. 'North West' and successfully continue to Local authority Details page");

        regionDetailsPage.selectSouthWest();

        regionDetailsPage.selectContinue();

        cy.executeAccessibilityTests();

        localAuthorityDetailsPage.checkElementsVisible(project.schoolName);

        Logger.log("Testing that a user is unable to have >1 radio button checked at one time on the Local authority Details page");

        localAuthorityDetailsPage.selectIslesOfScilly()
                                 .selectBathAndNorthEastSomerset()
                                 .selectBristol()
                                 .selectNorthSomerset()
                                 .selectSouthGloucestershire()
                                 .selectPoole()
                                 .selectDorset()
                                 .selectBournemouthChristchurchAndPoole()
                                 .selectWiltshire()
                                 .selectSwindon()
                                 .selectDevon()
                                 .selectPlymouth()
                                 .selectTorbay()
                                 .selectCornwall()
                                 .selectGloucestershire()
                                 .selectSomerset();
        
        Logger.log("Testing that a user can make a selection and save the Region and Local authority data and navigate to the Region and Local authority Summary page");

        localAuthorityDetailsPage.selectPlymouth();

        localAuthorityDetailsPage.selectContinue();

        cy.executeAccessibilityTests();

      regionAndLocalAuthoritySummaryPage.verifyRegionAndLASummaryCompleteElementsVisible(project.schoolName);

      regionAndLocalAuthoritySummaryPage.selectMarkItemAsComplete();

      regionAndLocalAuthoritySummaryPage.selectConfirmAndContinue();
      
      taskListPage.verifyRegionAndLAMarkedAsComplete();

    });
});
