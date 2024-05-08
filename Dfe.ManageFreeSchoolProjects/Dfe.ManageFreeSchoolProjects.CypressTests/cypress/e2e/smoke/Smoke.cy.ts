import { Logger } from "cypress/common/logger";
import { ProjectRecordCreator } from "cypress/constants/cypressConstants";

describe("Pioneer smoke test", () => {
    beforeEach(() => {
        Logger.log(Cypress.env('CypressTestSecret')+"bef")
        Logger.log(Cypress.env('url')+"bef")
        cy.login({role: "POTATO"});
        cy.visit(Cypress.env('url'));
        Logger.log(Cypress.env('CypressTestSecret')+"aft")
        Logger.log(Cypress.env('url')+"aft")
    });
    
    describe("Inital smoke tests to verify pipelines are running ok", () => {
        beforeEach(() => {
            Logger.log(Cypress.env('CypressTestSecret')+"second bef")
            Logger.log(Cypress.env('url')+"second bef")
            cy.login({role: ProjectRecordCreator});
            cy.visit('/');
        });

        it("Should run in test enviroment only", { tags:['smoke']},() =>  {
            Logger.log("this log should only show in test environment");
            cy.contains('Create new projects').should('be.visible');
            cy.executeAccessibilityTests();
        });
    });

})