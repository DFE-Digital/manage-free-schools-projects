import { ProjectDetailsRequest } from "cypress/api/domain";
import projectApi from "cypress/api/projectApi";
import { RequestBuilder } from "cypress/api/requestBuilder";
import { Logger } from "cypress/common/logger";
import projectOverviewPage from "../pages/projectOverviewPage";
import projectStatusPage from "../pages/project-status/projectStatusPage";
import taskListPage from "../pages/taskListPage";
import contactsPage from "../pages/contacts/contactsPage";

describe("Testing that we can change the project status", () => {
    let project: ProjectDetailsRequest;
    let now: Date;

    describe("Change project status", () => {

        it("Change status for central route project", () => {

            cy.login();

            now = new Date();
    
            project = RequestBuilder.createProjectDetailsCentralRoute();
    
            projectApi
                .post({
                    projects: [project],
                })
                .then(() => {
                    cy.visit(`/projects/${project.projectId}/overview`);
                });

            projectOverviewPage
                 .clickChangeProjectStatus();

            projectStatusPage
                 .selectApplicationCompetitionStage()
                 .clickSaveAndContine()

            projectOverviewPage
                .hasProjectStatus("Application competition stage")
                .clickChangeProjectStatus();

            projectStatusPage
                .selectApplicationStage()
                .clickSaveAndContine();

            projectOverviewPage
                .hasProjectStatus("Application stage")
                .clickChangeProjectStatus();

            projectStatusPage
                .selectOpenNotIncludedFigures()
                .clickSaveAndContine();

            projectOverviewPage
                .hasProjectStatus("Open - not included in figures")
                .clickChangeProjectStatus();

            projectStatusPage
                .selectPreOpeningNotIncludedFigures()
                .clickSaveAndContine();

            projectOverviewPage
                .hasProjectStatus("Pre-opening - not included in figures")
                .clickChangeProjectStatus();

            projectStatusPage
                .selectRejected()
                .clickSaveAndContine();

            projectOverviewPage
                .hasProjectStatus("Rejected")
                .clickChangeProjectStatus();

            projectStatusPage
                .selectWithdrawnInApplication()
                .clickSaveAndContine()
                .errorForWithdrawnInApplicationDate("Enter a date in the correct format")
                .addWithdrawnInApplicationYear("1", "1", "error")
                .clickSaveAndContine()
                .errorForWithdrawnInApplicationDate("Enter a date in the correct format")
                .addWithdrawnInApplicationYear("1", "1", "1999")
                .clickSaveAndContine()
                .errorForWithdrawnInApplicationDate("Year must be between 2000 and 2050")
                .clickSaveAndContine()
                .addWithdrawnInApplicationYear("1", "1", "2051")
                .errorForWithdrawnInApplicationDate("Year must be between 2000 and 2050")
                .addWithdrawnInApplicationYear("1", "1", "2045")
                .clickSaveAndContine();

            Logger.log("user is sent back to projects overview page");

            projectOverviewPage
                .hasWithdrawnDate("1 January 2045");
        });

        it.skip("Change status for Presumption project", () => {

            projectOverviewPage
              .hasProjectStatus("Pre-opening")
              .clickChangeProjectStatus();

            projectStatusPage
              .preOpeningIsChecked()
              .clickBackLink();

            Logger.log("verify user is back on projectList Page");
            
            projectOverviewPage
                .hasTrustId(project.TRN)
                .selectTaskListTab();
            
            taskListPage
                .hasProjectStatus("Pre-opening")
                .clickChangeProjectStatus();

            projectStatusPage
                .preOpeningIsChecked()
                .clickBackLink();

            Logger.log("verify user is back on tasklist page");
            
            taskListPage
                .onTasklistTab()
                .selectContactTab();
            
            contactsPage
                .hasProjectStatus("Pre-opening")
                .clickChangeProjectStatus();

            projectStatusPage
                .preOpeningIsChecked()
                .clickBackLink();
            
            Logger.log("verify user is back on contacts page");
            
            contactsPage
                .onContactsTab()
                .selectAboutTheProjectTab();

            Logger.log("change status to open");

            projectOverviewPage
                .hasProjectStatus("Pre-opening")
                .clickChangeProjectStatus();
            
            projectStatusPage
                .preOpeningIsChecked()
                .selectOpen()
                .clickSaveAndContine();

            projectOverviewPage
                .hasProjectStatus("Open")
                .hasTrustId(project.TRN)
                .selectTaskListTab();

            Logger.log("change status to cancelled");

            taskListPage
                .hasProjectStatus("Open")
                .clickChangeProjectStatus();

            projectStatusPage
                .openIsChecked()
                .selectCancelled()
                .clickSaveAndContine()
                .errorForCancelledDate("Enter a date in the correct format")
                .addCancelledYear("1", "1", "error")
                .clickSaveAndContine()
                .errorForCancelledDate("Enter a date in the correct format")
                .addCancelledYear("1", "1", "1999")
                .clickSaveAndContine()
                .errorForCancelledDate("Year must be between 2000 and 2050")
                .clickSaveAndContine()
                .addCancelledYear("1", "1", "2051")
                .errorForCancelledDate("Year must be between 2000 and 2050")
                .addCancelledYear("1", "1", "2050")
                .clickSaveAndContine();

            Logger.log("user is sent back to tasklist");
            
            taskListPage
                .onTasklistTab()
                .hasProjectStatus("Cancelled")
                .selectAboutTheProjectTab();


            Logger.log("cancelled date is shown on project overview");
            
            projectOverviewPage
                .hasCancelledDate("1 January 2050")
                .selectContactsTab();

            Logger.log("change status to closed");
            
            contactsPage
                .hasProjectStatus("Cancelled")
                .clickChangeProjectStatus();

            projectStatusPage
                .cancelledIsChecked()
                .cancelledYearHasValue("1", "1", "2050")
                .selectClosed()
                .clickSaveAndContine()
                .errorForClosedDate("Enter a date in the correct format")
                .addClosedYear("1", "1", "error")
                .clickSaveAndContine()
                .errorForClosedDate("Enter a date in the correct format")
                .addClosedYear("1", "1", "1999")
                .clickSaveAndContine()
                .errorForClosedDate("Year must be between 2000 and 2050")
                .clickSaveAndContine()
                .addClosedYear("1", "1", "2051")
                .errorForClosedDate("Year must be between 2000 and 2050")
                .addClosedYear("1", "1", "2048")
                .clickSaveAndContine();

            Logger.log("user is sent back to contacts page");
           
            contactsPage
                .onContactsTab()
                .hasProjectStatus("Closed")
                .selectAboutTheProjectTab();

            Logger.log("closed date is shown on project overview");

            projectOverviewPage
                .hasClosedDate("1 January 2048");

            Logger.log("change status to withdrawn");

            projectOverviewPage
                .clickChangeProjectStatus();

            projectStatusPage
                .closedIsChecked()
                .closedYearHasValue("1", "1", "2048")
                .selectWithdrawn()
                .clickSaveAndContine()
                .errorForWithdrawnDate("Enter a date in the correct format")
                .addWithdrawnYear("1", "1", "error")
                .clickSaveAndContine()
                .errorForWithdrawnDate("Enter a date in the correct format")
                .addWithdrawnYear("1", "1", "1999")
                .clickSaveAndContine()
                .errorForWithdrawnDate("Year must be between 2000 and 2050")
                .clickSaveAndContine()
                .addWithdrawnYear("1", "1", "2051")
                .errorForWithdrawnDate("Year must be between 2000 and 2050")
                .addWithdrawnYear("1", "1", "2047")
                .clickSaveAndContine();

            Logger.log("user is sent back to projects overview page");

            projectOverviewPage
                .hasWithdrawnDate("1 January 2047");
        });
    });
});