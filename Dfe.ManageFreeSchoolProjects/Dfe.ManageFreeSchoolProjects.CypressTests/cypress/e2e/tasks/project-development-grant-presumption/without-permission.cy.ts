import { ProjectDetailsRequest } from "cypress/api/domain";
import projectApi from "cypress/api/projectApi";
import { RequestBuilder } from "cypress/api/requestBuilder";
import summaryPage from "cypress/pages/task-summary-base";
import taskListPage from "cypress/pages/taskListPage";
import pdgDashboard from "cypress/pages/tasks/project-development-grant-presumption/dashboard";

describe("Payment Schedule Task", () => {
    let project: ProjectDetailsRequest;

    beforeEach(() => {
        cy.login();

        project = RequestBuilder.createProjectDetails();

        projectApi
            .post({
                projects: [project],
            })
            .then(() => {
                cy.visit(`/projects/${project.projectId}/tasks/`);
            });
    });

    it("Should successfully set payment schedule", () => {

        cy.log("Select Project development grant (PDG)");
        taskListPage.isTaskStatusIsNotStarted("PDG")
            .selectPDGFromTaskList();

        cy.log("Confirm empty dashboard");
        summaryPage
            .schoolNameIs(project.schoolName)
            .titleIs("Project development grant (PDG)")
            .HasNoMarkAsComplete()
            .hasNoConfirmAndContinue();

        pdgDashboard
            .changePaymentScheduleNotShown()
            .changeTrustLetterNotShown() 
            .changeStopPaymentsNotShown()
            .changeRefundsNotShown() 
            .changeWriteOffNotShown()

            summaryPage.clickBack();

        taskListPage.isTaskStatusIsNotStarted("PDG")

        cy.executeAccessibilityTests();

    });

});
