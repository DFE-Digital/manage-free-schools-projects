import { ProjectDetailsRequest } from "cypress/api/domain";
import projectApi from "cypress/api/projectApi";
import { RequestBuilder } from "cypress/api/requestBuilder";
import { Logger } from "cypress/common/logger";
import summaryPage from "cypress/pages/task-summary-base";
import taskListPage from "cypress/pages/taskListPage";
import editPrincipleDesignatePage from "../../../pages/tasks/pre-opening/edit-principle-designate.cy";

describe("Testing principle designate task", () => {
    let project: ProjectDetailsRequest;

    beforeEach(() => {
        cy.login();

        project = RequestBuilder.createProjectDetailsNonPresumption();

        projectApi
            .post({
                projects: [project],
            })
            .then(() => {
                cy.visit(`/projects/${project.projectId}/tasks`);
            });
    });

    it("Should be able to set a principle designate", () => {
        
        Logger.log("Select finance plan");
        taskListPage.isTaskStatusIsNotStarted("PrincipleDesignate")
            .selectPrincipleDesignateFromTaskList();

        cy.log("Confirm empty principle designate");
        summaryPage
            .schoolNameIs(project.schoolName)
            .titleIs("Principle designate")
            .inOrder()
            .summaryShows("Trust has appointed a principle designate").HasValue("No").HasChangeLink()
            .summaryShows("Commissioned an external expert").IsEmpty().HasChangeLink()
            .isNotMarkedAsComplete();

        cy.log("Go back to task list");
        summaryPage.clickBack();

        cy.log("Confirm not started and open principle designate");
        taskListPage.isTaskStatusIsNotStarted("PrincipleDesignate")
            .selectPrincipleDesignateFromTaskList();

        cy.log("Check confirm puts project in In Progress");
        summaryPage.clickConfirmAndContinue();

        taskListPage.isTaskStatusInProgress("PrincipleDesignate")
            .selectPrincipleDesignateFromTaskList();
        
        
        summaryPage.clickChange();

        cy.log("Check validation for principle designate");
        
        editPrincipleDesignatePage
            .checkYesForPrincipleDesignate()
            .clickContinue()
            .errorForPrincipleDesignateAppointedDate().showsError("Enter the actual date a principle designate was appointed")
            .withPrincipleDesignateAppointedDate("24","","")
            .clickContinue()
            .errorForPrincipleDesignateAppointedDate().showsError("Trust appointed principle designate date must include a month and year")
            .withPrincipleDesignateAppointedDate("24","4","")
            .clickContinue()
            .errorForPrincipleDesignateAppointedDate().showsError("Trust appointed principle designate date must include a year")
            .withPrincipleDesignateAppointedDate("24","4","2049")
            .checkYesForExternalExpert()
            .clickContinue()

        summaryPage
            .schoolNameIs(project.schoolName)
            .titleIs("Principle designate")
            .inOrder()
            .summaryShows("Trust has appointed a principle designate").HasValue("Yes").HasChangeLink()
            .summaryShows("Date that the trust appointed principal designate").HasValue("24 April 2049").HasChangeLink()
            .summaryShows("Commissioned an external expert").HasValue("Yes").HasChangeLink()
            .isNotMarkedAsComplete();

        cy.log("can edit principle designate");

        summaryPage.clickChange();


        editPrincipleDesignatePage
            .checkNoForPrincipleDesignate()
            .checkNoForExternalExpert()
            .clickContinue()

        summaryPage
            .schoolNameIs(project.schoolName)
            .titleIs("Principle designate")
            .inOrder()
            .summaryShows("Trust has appointed a principle designate").HasValue("No").HasChangeLink()
            .summaryShows("Commissioned an external expert").HasValue("No").HasChangeLink()
            .isNotMarkedAsComplete()
            .MarkAsComplete()
            .clickConfirmAndContinue()

        cy.log("principle designate is marked as complete");

        taskListPage.isTaskStatusIsCompleted("PrincipleDesignate")
    });
});