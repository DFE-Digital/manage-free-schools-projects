import { Logger } from "cypress/common/logger";
import {EnvApiKey, EnvAuthKey, EnvUrl, ProjectRecordCreator} from "cypress/constants/cypressConstants";

describe("Pioneer smoke test", () => {
    beforeEach(() => {
        
        cy.login()
        cy.visit('/');
    });

        it("Should run in test enviroment only", { tags:['smoke']},() =>  {
            Logger.log("this log should only show in test environment");
            cy.contains('Create new projects').should('be.visible');
            cy.executeAccessibilityTests();
        });

})