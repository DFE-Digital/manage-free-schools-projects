import { ProjectDetailsRequest } from "cypress/api/domain";
import projectApi from "cypress/api/projectApi";
import { RequestBuilder } from "cypress/api/requestBuilder";
import { Logger } from "cypress/common/logger";
import dataGenerator from "cypress/fixtures/dataGenerator";
import projectOverviewPage from "../pages/projectOverviewPage";
import projectStatusPage from "../pages/project-status/projectStatusPage";
import taskListPage from "../pages/taskListPage";
import contactsPage from "../pages/contacts/contactsPage";

describe("Testing that we can change the project status", () => {
    let project: ProjectDetailsRequest;
    let now: Date;

    beforeEach(() => {
        cy.login();

        now = new Date();

        project = RequestBuilder.createProjectDetails();

        projectApi
            .post({
                projects: [project],
            })
            .then(() => {
                cy.visit(`/projects/${project.projectId}/overview`);
            });
    });

    describe("Change project status", () => {
        it("Should be able to change assigned to", () => {

            projectOverviewPage
              .hasProjectStatus("Pre-opening")
              .clickChangeProjectStatus()
            
            projectStatusPage
              .preOpeningIsChecked()
              .clickBackLink()

            Logger.log("verify user is back on projectList Page");
            
            projectOverviewPage
                .hasTrustId(project.TRN)
                .selectTaskListTab()
            
            taskListPage
                .hasProjectStatus("Pre-opening")
                .clickChangeProjectStatus();

            projectStatusPage
                .preOpeningIsChecked()
                .clickBackLink()

            Logger.log("verify user is back on tasklist page");
            
            taskListPage
                .onTasklistTab()
                .selectContactTab()
            
            contactsPage
                .hasProjectStatus("Pre-opening")
                .clickChangeProjectStatus();

            projectStatusPage
                .preOpeningIsChecked()
                .clickBackLink()
            
            Logger.log("verify user is back on contacts page");
            
            contactsPage
                .onContactsTab()
                .selectAboutTheProjectTab()

            Logger.log("change status to open");

            projectOverviewPage
                .hasProjectStatus("Pre-opening")
                .clickChangeProjectStatus()
            
            projectStatusPage
                .preOpeningIsChecked()
                .selectOpen()
                .clickSaveAndContine()

            projectOverviewPage
                .hasProjectStatus("Open")
                .hasTrustId(project.TRN)
                .selectTaskListTab()

            Logger.log("change status to cancelled");

            taskListPage
                .hasProjectStatus("Open")
                .clickChangeProjectStatus();

            projectStatusPage
                .openIsChecked()
                .selectCancelled()
                .clickSaveAndContine()
                .errorForCancelledDate("Enter a year in the correct format")
                .addCancelledYear("error")
                .clickSaveAndContine()
                .errorForCancelledDate("Enter a year in the correct format")
                .addCancelledYear("1999")
                .clickSaveAndContine()
                .errorForCancelledDate("Enter a year between 2000 and 2050")
                .clickSaveAndContine()
                .addCancelledYear("2051")
                .errorForCancelledDate("Enter a year between 2000 and 2050")
                .addCancelledYear("2050")
                .clickSaveAndContine()

            Logger.log("user is sent back to tasklist");
            
            taskListPage
                .onTasklistTab()
                .hasProjectStatus("Cancelled")
                .selectAboutTheProjectTab()


            Logger.log("cancelled date is shown on project overview");
            
            projectOverviewPage
                .hasCancelledDate("2050")
                .selectContactsTab()

            Logger.log("change status to closed");
            
            contactsPage
                .hasProjectStatus("Cancelled")
                .clickChangeProjectStatus();

            projectStatusPage
                .cancelledIsChecked()
                .cancelledYearHasValue("2050")
                .selectClosed()
                .clickSaveAndContine()
                .errorForClosedDate("Enter a year in the correct format")
                .addClosedYear("error")
                .clickSaveAndContine()
                .errorForClosedDate("Enter a year in the correct format")
                .addClosedYear("1999")
                .clickSaveAndContine()
                .errorForClosedDate("Enter a year between 2000 and 2050")
                .clickSaveAndContine()
                .addClosedYear("2051")
                .errorForClosedDate("Enter a year between 2000 and 2050")
                .addClosedYear("2050")
                .clickSaveAndContine()

            Logger.log("user is sent back to contacts page");
           
            contactsPage
                .onContactsTab()
                .hasProjectStatus("Closed")
                .selectAboutTheProjectTab();

            Logger.log("closed date is shown on project overview");

            projectOverviewPage
                .hasClosedDate("2050")

            Logger.log("change status to withdrawn");

            projectOverviewPage
                .clickChangeProjectStatus()

            projectStatusPage
                .closedIsChecked()
                .closedYearHasValue("2050")
                .selectWithdrawn()
                .clickSaveAndContine()
                .errorForWithdrawnDate("Enter a year in the correct format")
                .addWithdrawnYear("error")
                .clickSaveAndContine()
                .errorForWithdrawnDate("Enter a year in the correct format")
                .addWithdrawnYear("1999")
                .clickSaveAndContine()
                .errorForWithdrawnDate("Enter a year between 2000 and 2050")
                .clickSaveAndContine()
                .addWithdrawnYear("2051")
                .errorForWithdrawnDate("Enter a year between 2000 and 2050")
                .addWithdrawnYear("2050")
                .clickSaveAndContine()

            Logger.log("user is sent back to projects overview page");

            projectOverviewPage
                .hasWithdrawnDate("2050")

            Logger.log("change status to withdrawn in application");

            projectOverviewPage
                .clickChangeProjectStatus()

            projectStatusPage
                .withdrawnIsChecked()
                .withdrawnYearHasValue("2050")
                .selectWithdrawnInApplication()
                .clickSaveAndContine()
                .errorForWithdrawnInApplicationDate("Enter a year in the correct format")
                .addWithdrawnInApplicationYear("error")
                .clickSaveAndContine()
                .errorForWithdrawnInApplicationDate("Enter a year in the correct format")
                .addWithdrawnInApplicationYear("1999")
                .clickSaveAndContine()
                .errorForWithdrawnInApplicationDate("Enter a year between 2000 and 2050")
                .clickSaveAndContine()
                .addWithdrawnInApplicationYear("2051")
                .errorForWithdrawnInApplicationDate("Enter a year between 2000 and 2050")
                .addWithdrawnInApplicationYear("2050")
                .clickSaveAndContine()

            Logger.log("user is sent back to projects overview page");

            projectOverviewPage
                .hasWithdrawnDate("2050")
        })
    })
})