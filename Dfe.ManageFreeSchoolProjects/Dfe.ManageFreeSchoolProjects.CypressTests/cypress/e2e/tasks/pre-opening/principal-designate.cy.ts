import { ProjectDetailsRequest } from "cypress/api/domain";
import projectApi from "cypress/api/projectApi";
import { RequestBuilder } from "cypress/api/requestBuilder";
import { Logger } from "cypress/common/logger";
import summaryPage from "cypress/pages/task-summary-base";
import taskListPage from "cypress/pages/taskListPage";
import editPrincipalDesignatePage from "../../../pages/tasks/pre-opening/edit-principal-designate.cy";

describe("Testing Principal designate task", () => {
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

    it("Should be able to set a Principal designate", () => {
        
        Logger.log("Select finance plan");
        taskListPage.isTaskStatusIsNotStarted("PrincipalDesignate")
            .selectPrincipleDesignateFromTaskList();

        cy.log("Confirm empty Principal designate");
        summaryPage
            .schoolNameIs(project.schoolName)
            .titleIs("Principal designate")
            .inOrder()
            .summaryShows("Trust has appointed a principal designate").IsEmpty().HasChangeLink()
            .summaryShows("Commissioned an external expert").IsEmpty().HasChangeLink()
            .isNotMarkedAsComplete();

        cy.log("Go back to task list");
        summaryPage.clickBack();

        cy.log("Confirm not started and open principal designate");
        taskListPage.isTaskStatusIsNotStarted("PrincipalDesignate")
            .selectPrincipleDesignateFromTaskList();

        cy.log("Check confirm puts project in In Progress");
        summaryPage.clickConfirmAndContinue();

        taskListPage.isTaskStatusInProgress("PrincipalDesignate")
            .selectPrincipleDesignateFromTaskList();
        
        
        summaryPage.clickChange();

        cy.log("Check empty values accepted");

        editPrincipalDesignatePage
            .clickContinue()

        summaryPage
            .schoolNameIs(project.schoolName)
            .titleIs("Principal designate")
            .inOrder()
            .summaryShows("Trust has appointed a principal designate").IsEmpty().HasChangeLink()
            .summaryShows("Commissioned an external expert").IsEmpty().HasChangeLink()
            .isNotMarkedAsComplete()
            .clickChange()
        
        cy.log("Check validation for principal designate");
        
        editPrincipalDesignatePage
            .checkYesForPrincipleDesignate()
            .clickContinue()
            .errorForPrincipleDesignateAppointedDate().showsError("Enter the actual date a principal designate was appointed")
            .withPrincipleDesignateAppointedDate("24","","")
            .clickContinue()
            .errorForPrincipleDesignateAppointedDate().showsError("Trust appointed principal designate date must include a month and year")
            .withPrincipleDesignateAppointedDate("24","4","")
            .clickContinue()
            .errorForPrincipleDesignateAppointedDate().showsError("Trust appointed principal designate date must include a year")
            .withPrincipleDesignateAppointedDate("24","4","2049")
            .checkYesForExternalExpert()
            .clickContinue()

        summaryPage
            .schoolNameIs(project.schoolName)
            .titleIs("Principal designate")
            .inOrder()
            .summaryShows("Trust has appointed a principal designate").HasValue("Yes").HasChangeLink()
            .summaryShows("Date that the trust appointed principal designate").HasValue("24 April 2049").HasChangeLink()
            .summaryShows("Commissioned an external expert").HasValue("Yes").HasChangeLink()
            .isNotMarkedAsComplete();

        cy.log("can edit principal designate");

        summaryPage.clickChange();


        editPrincipalDesignatePage
            .checkNoForPrincipleDesignate()
            .checkNoForExternalExpert()
            .clickContinue()

        summaryPage
            .schoolNameIs(project.schoolName)
            .titleIs("Principal designate")
            .inOrder()
            .summaryShows("Trust has appointed a principal designate").HasValue("No").HasChangeLink()
            .summaryShows("Commissioned an external expert").HasValue("No").HasChangeLink()
            .isNotMarkedAsComplete()
            .MarkAsComplete()
            .clickConfirmAndContinue()

        cy.log("principal designate is marked as complete");

        taskListPage.isTaskStatusIsCompleted("PrincipalDesignate")
    });
});