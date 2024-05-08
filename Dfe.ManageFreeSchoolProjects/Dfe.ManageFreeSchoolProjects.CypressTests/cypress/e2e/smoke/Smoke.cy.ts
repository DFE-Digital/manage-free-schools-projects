import { Logger } from "cypress/common/logger";
import { ProjectRecordCreator } from "cypress/constants/cypressConstants";

describe("Pioneer smoke test", () => {
    beforeEach(() => {
        //cy.login({role: "POTATO"});
        //cy.visit(Cypress.env('url'));
    });
    
    describe("Inital smoke tests to verify pipelines are running ok", () => {
        beforeEach(() => {
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