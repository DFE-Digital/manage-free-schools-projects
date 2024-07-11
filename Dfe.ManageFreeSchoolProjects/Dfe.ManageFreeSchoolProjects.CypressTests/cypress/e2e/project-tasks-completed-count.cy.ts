import { ProjectDetailsRequest } from "cypress/api/domain";
import projectApi from "cypress/api/projectApi";
import { RequestBuilder } from "cypress/api/requestBuilder";
import { Logger } from "cypress/common/logger";
import summaryPage from "cypress/pages/task-summary-base";
import taskListPage from "cypress/pages/taskListPage";
import externalExpertVisitEditPage from "../pages/tasks/After-opening/edit-external-expert-visit.cy";
import schoolDetailsPage from "../pages/schoolDetailsPage";

describe("Testing the External expert visit task", () => {

    let project: ProjectDetailsRequest;
    let projectNonPresumption: ProjectDetailsRequest;
    let taskCountWithSchoolTypeNotSet = 27
    let taskCountWithSchoolTypeNotSetNonPresumption = 28
    let taskCountWithSchoolTypeAlternativeProvision = 26

    beforeEach(() => {
        cy.login();

        project = RequestBuilder.createProjectDetails();
        projectNonPresumption = RequestBuilder.createProjectDetailsNonPresumption();
        
        projectApi
            .post({
                projects: [projectNonPresumption],
            })
        
        
        projectApi
            .post({
                projects: [project],
            })
            .then(() => {
                cy.visit(`/projects/${project.projectId}/tasks`);
            });
    });

    it("Should be able to set external expert visit", () => {
        Logger.log("Select External expert visit");
        taskListPage.taskCompletedCountMessage(`You have completed 0 of ${taskCountWithSchoolTypeNotSet} sections.`)
        taskListPage.isTaskStatusIsNotStarted("CommissionedExternalExpert")
            .selectExternalExpertVisitFromTaskList();

        Logger.log("Mark External expert visit as completed");
        
        summaryPage
            .titleIs("External expert visit")
            .isNotMarkedAsComplete()
            .MarkAsComplete()
            .clickConfirmAndContinue();

        Logger.log("Go back to task list");

        taskListPage.taskCompletedCountMessage(`You have completed 1 of ${taskCountWithSchoolTypeNotSet} sections.`)
            .isTaskStatusIsCompleted("CommissionedExternalExpert")
            .selectApplicationsEvidenceFromTaskList();

        Logger.log("Mark Applications evidence as completed");
        
        summaryPage
            .titleIs(" Applications evidence")
            .isNotMarkedAsComplete()
            .MarkAsComplete()
            .clickConfirmAndContinue();

        Logger.log("Go back to task list");

        taskListPage.taskCompletedCountMessage(`You have completed 2 of ${taskCountWithSchoolTypeNotSet} sections.`)
            .isTaskStatusIsCompleted("CommissionedExternalExpert")
            .isTaskStatusIsCompleted("ApplicationsEvidence")
            .selectSchoolFromTaskList()

        summaryPage
            .schoolNameIs(project.schoolName)
            .titleIs("School")
            .clickChange()

        Logger.log("set school type as Alternative provision");
        
        schoolDetailsPage
            .titleIs("Edit School")
            .withSchoolType("AlternativeProvision")
            .withSchoolPhase("Secondary")
            .withAgeRange("11", "16")
            .withFormsOfEntry("3")
            .withGender("Mixed")
            .withNursery("Yes")
            .withSixthForm("No")
            .withFaithStatus("Designation")
            .withFaithType("faith-type-Jewish")
            .clickContinue();

        summaryPage
            .schoolNameIs(project.schoolName)
            .titleIs("School")
            .clickConfirmAndContinue()

        Logger.log("completed count and sections count is adjusted correctly");

        taskListPage.taskCompletedCountMessage(`You have completed 1 of ${taskCountWithSchoolTypeAlternativeProvision} sections.`)
            .isTaskStatusIsCompleted("CommissionedExternalExpert")


        Logger.log("Visit non presumtion project");
        
        cy.visit(`/projects/${projectNonPresumption.projectId}/tasks`);

        taskListPage.taskCompletedCountMessage(`You have completed 0 of ${taskCountWithSchoolTypeNotSetNonPresumption} sections.`)
    });
});