import { ProjectDetailsRequest } from "cypress/api/domain";
import projectApi from "cypress/api/projectApi";
import { RequestBuilder } from "cypress/api/requestBuilder";
import dataGenerator from "cypress/fixtures/dataGenerator";
import summaryPage from "cypress/pages/task-summary-base";
import taskListPage from "cypress/pages/taskListPage";
import articlesOfAssociationEditPage from "cypress/pages/tasks/pre-opening/edit-articles-of-association.cy";
import modelFundingAgreementEditPage from "../../../pages/tasks/pre-opening/edit-model-funding-agreement.cy";

describe("Testing Model funding agreement Task", () => {

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

    it("Should successfully set model funding agreement task", () => {

        cy.log("Select model funding agreement");
        taskListPage.isTaskStatusIsNotStarted("ModelFundingAgreement")
            .selectModelFundingAgreementFromTaskList();

        cy.log("Confirm empty model funding agreement");

        cy.log("Confirm empty model funding agreement");
        summaryPage
            .schoolNameIs(project.schoolName)
            .titleIs("Model funding agreement")
            .inOrder()
            .summaryShows("Taylored a model funding agreement (FA)").IsEmpty().HasChangeLink()
            .summaryShows("Shared FA with trust").IsEmpty().HasChangeLink()
            .summaryShows("Trust agrees with model FA").IsEmpty().HasChangeLink()
            .summaryShows("Comments").IsEmpty().HasChangeLink()
            .summaryShows("Drafted FA health check").IsEmpty().HasChangeLink()
            .summaryShows("Saved FA documents in Workplaces folder").IsEmpty().HasChangeLink()
            .isNotMarkedAsComplete();


        //cy.executeAccessibilityTests();
        cy.log("Go back to task list");
        summaryPage.clickBack();

        cy.log("Confirm not started and open model funding agreement");
        taskListPage.isTaskStatusIsNotStarted("ModelFundingAgreement")
            .selectModelFundingAgreementFromTaskList();

        cy.log("Check confirm puts project in In Progress");
        summaryPage.clickConfirmAndContinue();

        taskListPage.isTaskStatusInProgress("ModelFundingAgreement")
            .selectModelFundingAgreementFromTaskList();

        cy.log("Check search page");

        summaryPage.clickChange();

        //cy.executeAccessibilityTests();
        
        cy.log("Date Agreed Validation")

        modelFundingAgreementEditPage
            .withAgreedDate("2","ds","2050")
            .clickContinue()
            .errorForAgreedDate().showsError("dave")

        summaryPage.SummaryHasValue("SharePoint link", "https://www.gov.uk/government/organisations/department-for-education")
            .clickChange();
        
        cy.log("Date Agreed Validation")
    })
    
    
    
})