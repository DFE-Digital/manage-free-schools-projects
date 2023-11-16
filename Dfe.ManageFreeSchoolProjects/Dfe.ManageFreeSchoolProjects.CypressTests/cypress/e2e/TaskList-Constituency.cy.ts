import { ProjectDetailsRequest } from "cypress/api/domain";
import projectApi from "cypress/api/projectApi";
import { RequestBuilder } from "cypress/api/requestBuilder";
import { Logger } from "cypress/common/logger";
import dataGenerator from "cypress/fixtures/dataGenerator";
import constituencyEditPage from "cypress/pages/constituencyEditPage";
import constituencySearchPage from "cypress/pages/constituencySearchPage";
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
        
        Logger.log("Select constituency");
        taskListPage.isTaskStatusIsNotStarted("Constituency")
            .selectConstituencyFromTaskList();
                
        Logger.log("Confirm empty constituency");
        ConstituencySummaryPage
            .inOrder().schoolNameIs(project.schoolName)
            .titleIs("Constituency")
            .summaryShows("Name").IsEmpty().HasChangeLink()
            .summaryShows("MP").IsEmpty().HasNoChangeLink()
            .summaryShows("Political party").IsEmpty().HasNoChangeLink()
            .isNotMarkedAsComplete();

        Logger.log("Go back to task list");
        ConstituencySummaryPage.clickBack();

        Logger.log("Confirm not started and open constituency");
        taskListPage.isTaskStatusIsNotStarted("Constituency")
            .selectConstituencyFromTaskList();

        Logger.log("Check confirm puts project in In Progress");
        ConstituencySummaryPage.clickConfirmAndContinue();

        taskListPage.isTaskStatusInProgress("Constituency")
            .selectConstituencyFromTaskList();

        Logger.log("Check search page");

        ConstituencySummaryPage.clickChange();

        constituencySearchPage
            .schoolNameIs(project.schoolName)
            .titleIs("Search for a constituency")
            .searchHintIs("Enter a name or postcode");

        Logger.log("Check back link");
        constituencySearchPage.clickBack();

        ConstituencySummaryPage
            .inOrder().schoolNameIs(project.schoolName)
            .titleIs("Constituency")
            .summaryShows("Name").IsEmpty().HasChangeLink()
            .summaryShows("MP").IsEmpty().HasNoChangeLink()
            .summaryShows("Political party").IsEmpty().HasNoChangeLink()
            .isNotMarkedAsComplete();

        ConstituencySummaryPage.clickChange();

        Logger.log("Check validation for no input");

        constituencySearchPage
            .clickContinue()
            .errorMessage("The search constituency field is required.")
            .errorHint("The search constituency field is required.");

        Logger.log("Check validation for long string (50 chars)");

        constituencySearchPage
            .enterSearch(dataGenerator.generateAlphaNumeric(51))
            .clickContinue()
            .errorMessage("The search constituency must be 50 characters or less")
            .errorHint("The search constituency must be 50 characters or less");

        Logger.log("Perform valid search");
            
        constituencySearchPage
            .enterSearch("SW1P")
            .clickContinue()

        constituencyEditPage
            .hasResult("Battersea")
            .hasResult("Cities of London and Westminster")
            .hasResult("Hammersmith")
            .hasNone()
            .selectNone()
            .clickContinue()
    });
});