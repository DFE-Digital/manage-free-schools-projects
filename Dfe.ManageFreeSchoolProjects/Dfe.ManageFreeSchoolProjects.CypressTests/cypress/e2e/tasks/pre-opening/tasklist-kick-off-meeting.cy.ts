import { ProjectDetailsRequest } from "cypress/api/domain";
import projectApi from "cypress/api/projectApi";
import { RequestBuilder } from "cypress/api/requestBuilder";
import dataGenerator from "cypress/fixtures/dataGenerator";
import summaryPage from "cypress/pages/task-summary-base";
import taskListPage from "cypress/pages/taskListPage";
import articlesOfAssociationEditPage from "cypress/pages/tasks/pre-opening/edit-articles-of-association.cy";

describe("Testing kick off meeting Task", () => {

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

    it("Should successfully set kick off meeting", () => {

        cy.log("Select kick off meeting");
        taskListPage.isTaskStatusIsNotStarted("KickOffMeeting")
            .selectKickOffMeetingFromTaskList();

        cy.log("Confirm empty kick off meeting");

        summaryPage
            .schoolNameIs(project.schoolName)
            .titleIs("Kick-off meeting")
            .inOrder()
        
    })
})
