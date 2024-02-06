import { ProjectDetailsRequest } from "cypress/api/domain";
import projectApi from "cypress/api/projectApi";
import { RequestBuilder } from "cypress/api/requestBuilder";
import { Logger } from "cypress/common/logger";
import summaryPage from "cypress/pages/task-summary-base";
import taskListPage from "cypress/pages/taskListPage";
import editFinancePlanPage from "cypress/pages/tasks/pre-opening/editFinancePlanPage";
import validationComponent from "cypress/pages/validationComponent";

describe("Testing finance plan task", () => {
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

    it("Should be able to set a finance plan", () => {

        Logger.log("Select finance plan");
        taskListPage.isTaskStatusIsNotStarted("FinancePlan")
            .selectFinancePlanFromTaskList();

        Logger.log("Go back to task list");
        summaryPage.clickBack();

        taskListPage.selectFinancePlanFromTaskList();

        Logger.log("Confirm empty finance plan");
        summaryPage
            .schoolNameIs(project.schoolName)
            .titleIs("Finance plan")
            .inOrder()
            .summaryShows("Finance plan agreed").IsEmpty().HasChangeLink()
            .summaryShows("Date agreed").IsEmpty().HasChangeLink()
            .summaryShows("Plan saved in Workspaces folder").IsEmpty().HasChangeLink()
            .summaryShows("Local authority agreed to underwrite pupil numbers").IsEmpty().HasChangeLink()
            .summaryShows("Comments").IsEmpty().HasChangeLink()
            .summaryShows("Trust will opt-in to RPA (risk protection arrangement)").IsEmpty().HasChangeLink()
            .isNotMarkedAsComplete();

        cy.executeAccessibilityTests();

        summaryPage.clickChange();

        Logger.log("Check all the fields are optional");
        editFinancePlanPage
            .titleIs("Edit finance plan")
            .schoolNameIs(project.schoolName)
            .clickContinue();

        Logger.log("Add new values");
        summaryPage.clickChange();

        Logger.log("Testing validation");
        editFinancePlanPage
            .withDateAgreed("33", "", "")
            .withCommentsExceedingMaxLength()
            .clickContinue();

        validationComponent
            .hasValidationError("Date agreed must include a month and year")
            .hasValidationError("The comments must be 999 characters or less");

        editFinancePlanPage
            .schoolNameIs(project.schoolName)
            .checkFinancePlanAgreed()
            .withDateAgreed("10", "08", "2024")
            .checkPlanSavedInWorkplacesFolder()
            .withLocalAuthorityAgreedToUnderwritePupilNumbers("Yes")
            .withComments("Some comments")
            .checkTrustWillOptInToRpa()
            .clickContinue();

        summaryPage
            .inOrder()
            .summaryShows("Finance plan agreed").HasValue("Yes")
            .summaryShows("Date agreed").HasValue("10 August 2024")
            .summaryShows("Plan saved in Workspaces folder").HasValue("Yes")
            .summaryShows("Local authority agreed to underwrite pupil numbers").HasValue("Yes")
            .summaryShows("Comments").HasValue("Some comments")
            .summaryShows("Trust will opt-in to RPA (risk protection arrangement)").HasValue("Yes");

        Logger.log("Should be able to edit the existing values");
        summaryPage.clickChange();

        editFinancePlanPage
            .checkFinancePlanAgreed()
            .withDateAgreed("12", "05", "2027")
            .checkPlanSavedInWorkplacesFolder()
            .withLocalAuthorityAgreedToUnderwritePupilNumbers("NotApplicable")
            .withComments("This is my new comments")
            .checkTrustWillOptInToRpa()
            .clickContinue();

        summaryPage
            .inOrder()
            .summaryShows("Finance plan agreed").HasValue("No")
            .summaryShows("Date agreed").HasValue("12 May 2027")
            .summaryShows("Plan saved in Workspaces folder").HasValue("No")
            .summaryShows("Local authority agreed to underwrite pupil numbers").HasValue("Not applicable")
            .summaryShows("Comments").HasValue("This is my new comments")
            .summaryShows("Trust will opt-in to RPA (risk protection arrangement)").HasValue("No");

        summaryPage.clickConfirmAndContinue();

        taskListPage.isTaskStatusInProgress("FinancePlan").selectFinancePlanFromTaskList();

        summaryPage
            .MarkAsComplete()
            .clickConfirmAndContinue();

        taskListPage.isTaskStatusIsCompleted("FinancePlan");
    });
});