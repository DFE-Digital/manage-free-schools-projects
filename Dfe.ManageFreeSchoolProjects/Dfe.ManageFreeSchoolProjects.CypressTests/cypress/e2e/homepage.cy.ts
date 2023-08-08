import { Logger } from "cypress/common/logger";
import createProjectPage from "cypress/pages/createProjectPage";
import homePage from "cypress/pages/homePage";
import projectTable from "cypress/pages/projectTable";
import validationComponent from "cypress/pages/validationComponent";
import { v4 } from "uuid";

describe("Testing the home page", () => {
    beforeEach(() => {
        cy.login();
        cy.visit("/project/create");
    });

    it("Should be able to create a project and view it in the project list", () => {
        Logger.log("Creating a new project");
        const projectId: string = v4().substring(0, 8);
        const schoolName = `${projectId} school`;

        createProjectPage.createProject();

        validationComponent
            .hasValidationError("The Project ID field is required")
            .hasValidationError("The School name field is required")
            .hasValidationError("The Application number field is required")
            .hasValidationError("The Application wave field is required");

        createProjectPage
            .withProjectId(projectId)
            .withSchoolName(`${projectId} school`)
            .withApplicationNumber("1")
            .withApplicationWave("1");

        // cy.excuteAccessibilityTests();

        createProjectPage.createProject();

        projectTable.getRowByProjectId(projectId).then((row) => {
            row.hasProjectId(projectId).hasProjectTitle(schoolName);
        });

        // cy.excuteAccessibilityTests();
    });
});
