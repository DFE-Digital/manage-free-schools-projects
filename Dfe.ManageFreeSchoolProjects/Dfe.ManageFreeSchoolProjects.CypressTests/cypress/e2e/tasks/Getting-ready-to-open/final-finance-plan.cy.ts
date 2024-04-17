import { ProjectDetailsRequest } from "cypress/api/domain";
import projectApi from "cypress/api/projectApi";
import { RequestBuilder } from "cypress/api/requestBuilder";
import summaryPage from "cypress/pages/task-summary-base";
import taskListPage from "cypress/pages/taskListPage";
import finalFinancePlanEditPage from "../../../pages/tasks/Getting-ready-to-open/edit-final-finance-plan.cy";

describe("Testing Final finance plan Task", () => {

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

    it("Should successfully set Final fiance plan task", () => {

        cy.log("Select Final finance plan");
        taskListPage.isTaskStatusIsNotStarted("FinalFinancePlan")
            .selectFinalFinancePlanFromTaskList();

        cy.log("Confirm empty Final finance plan");

        summaryPage
            .schoolNameIs(project.schoolName)
            .titleIs("Final finance plan")
            .inOrder()
            .summaryShows("Confirmed that the trust has provided the final finance plan").IsEmpty().HasChangeLink()
            .summaryShows("Date the Grade 6 signed-off the final plan").IsEmpty().HasChangeLink()
            .summaryShows("Sent the final plan to the Revenue Funding mailbox").IsEmpty().HasChangeLink()
            .summaryShows("Saved final plan in Workplaces folder").IsEmpty().HasChangeLink()
            .isNotMarkedAsComplete();


        cy.executeAccessibilityTests();
        cy.log("Go back to task list");
        summaryPage.clickBack();

        cy.log("Confirm not started and open Final finance plan");
        taskListPage.isTaskStatusIsNotStarted("FinalFinancePlan")
            .selectFinalFinancePlanFromTaskList();

        cy.log("Check confirm puts project in In Progress");
        summaryPage.clickConfirmAndContinue();

        taskListPage.isTaskStatusInProgress("FinalFinancePlan")
            .selectFinalFinancePlanFromTaskList();

        cy.log("Check edit page");

        summaryPage.clickChange();

        cy.executeAccessibilityTests({ "aria-allowed-attr": { enabled: false } });
        
        cy.log("Date the Grade 6 signed-off the final plan Validation")

        finalFinancePlanEditPage
            .withGrade6SignedOffFinalPlanDate("2","ds","2050")
            .clickContinue()
            .errorGrade6SignedOffFinalPlanDate().showsError("Enter a date in the correct format")
            .withGrade6SignedOffFinalPlanDate("2","2","2090")
            .clickContinue()
            .errorGrade6SignedOffFinalPlanDate().showsError("Year must be between 2000 and 2050")
            .withGrade6SignedOffFinalPlanDate("2","2","1999")
            .clickContinue()
            .errorGrade6SignedOffFinalPlanDate().showsError("Year must be between 2000 and 2050")

        cy.log("Date the Grade 6 signed-off the final plan Validation")

        finalFinancePlanEditPage
            .checkConfirmedTrustHasProvidedFinalPlan()
            .withGrade6SignedOffFinalPlanDate("2","2","2050")
            .checkSentFinalPlanToRevenueFundingMailbox()
            .checkSavedFinalPlanInWorkplacesFolder()
            .clickContinue()

        summaryPage
            .schoolNameIs(project.schoolName)
            .titleIs("Final finance plan")
            .inOrder()
            .summaryShows("Confirmed that the trust has provided the final finance plan").HasValue("Yes").HasChangeLink()
            .summaryShows("Date the Grade 6 signed-off the final plan").HasValue("2 February 2050").HasChangeLink()
            .summaryShows("Sent the final plan to the Revenue Funding mailbox").HasValue("Yes").HasChangeLink()
            .summaryShows("Saved final plan in Workplaces folder").HasValue("Yes").HasChangeLink()

        cy.log("Unselect checkboxes")
       
        summaryPage.clickChange();
        finalFinancePlanEditPage
            .uncheckConfirmedTrustHasProvidedFinalPlan()
            .uncheckSentFinalPlanToRevenueFundingMailbox()
            .uncheckSavedFinalPlanInWorkplacesFolder()
            .clickContinue()
            

        summaryPage
            .schoolNameIs(project.schoolName)
            .titleIs("Final finance plan")
            .inOrder()
            .summaryShows("Confirmed that the trust has provided the final finance plan").IsEmpty().HasChangeLink()
            .summaryShows("Date the Grade 6 signed-off the final plan").HasValue("2 February 2050").HasChangeLink()
            .summaryShows("Sent the final plan to the Revenue Funding mailbox").IsEmpty().HasChangeLink()
            .summaryShows("Saved final plan in Workplaces folder").IsEmpty().HasChangeLink()
            .isNotMarkedAsComplete()
            .MarkAsComplete()
            .clickConfirmAndContinue()

        taskListPage
            .selectFinalFinancePlanFromTaskList()
        
        summaryPage.isMarkedAsComplete()
    })
    
    
    
})