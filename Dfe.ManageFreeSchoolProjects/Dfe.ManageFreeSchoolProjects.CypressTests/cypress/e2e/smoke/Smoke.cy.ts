import { Logger } from "cypress/common/logger";
import {EnvApiKey, EnvAuthKey, EnvUrl, ProjectRecordCreator} from "cypress/constants/cypressConstants";

describe("Pioneer smoke test", () => {
    beforeEach(() => {
        
        Logger.log("this is the authkey >> " +Cypress.env(EnvAuthKey) + " <<<")
        Logger.log("this is the base URL >> "+Cypress.env(EnvUrl) + " <<<")
        Logger.log("this is the API key >> "+Cypress.env(EnvApiKey) + " <<<")
        cy.login()
        cy.visit('/');
    });

        it("Should run in test enviroment only", { tags:['smoke']},() =>  {
            Logger.log("this log should only show in test environment");
            cy.contains('Create new projects').should('be.visible');
            cy.executeAccessibilityTests();
        });

})