import { ProjectDetailsRequest } from "cypress/api/domain";
import projectApi from "cypress/api/projectApi";
import { RequestBuilder } from "cypress/api/requestBuilder";
import summaryPage from "cypress/pages/task-summary-base";
import taskListPage from "cypress/pages/taskListPage";
import riskPage from "cypress/pages/risk/projectRiskSummaryPage"

describe("!!SMOKE Testing education brief", () => {

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

    it("!!SMOKE Should successfully set education brief task", { tags:['smoke']},() => {
        cy.log("!!SMOKE education brief");
        taskListPage.isTaskStatusIsNotStarted("EducationBrief")
            .selectEducationBriefFromList();
        
        cy.log("!!SMOKE Confirm empty education brief");
        summaryPage
            .schoolNameIs(project.schoolName)
            .titleIs("Education brief")
            .inOrder()
            .summaryShows("Education plan is in the education brief").IsEmpty().HasChangeLink()
            .summaryShows("Education policies are in the education brief").IsEmpty().HasChangeLink()
            .summaryShows("Pupil assessment and tracking history are in place").IsEmpty().HasChangeLink()
            .summaryShows("Saved documents in Workplaces folder").IsEmpty().HasChangeLink()
            .isNotMarkedAsComplete();
    })
})