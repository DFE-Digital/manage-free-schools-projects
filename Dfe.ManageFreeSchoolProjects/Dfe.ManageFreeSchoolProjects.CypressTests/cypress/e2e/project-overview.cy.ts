import { ProjectDetailsRequest } from "cypress/api/domain";
import projectApi from "cypress/api/projectApi";
import { RequestBuilder } from "cypress/api/requestBuilder";
import { Logger } from "cypress/common/logger";
import projectOverviewPage from "cypress/pages/projectOverviewPage";

describe("Testing project overview", () => {
    let project: ProjectDetailsRequest;

    beforeEach(() => {
        cy.login();

        project = RequestBuilder.createProjectDetails();

        projectApi
            .post({
                projects: [project],
            })
            .then(() => {
                cy.visit(`/projects/${project.projectId}/overview`);
            });
    });

    it("Should display the configured information", () => {
        Logger.log("Project status");
        projectOverviewPage
            .hasCurrentFreeSchoolName(project.schoolName)
            .hasProjectId(project.projectId);

        Logger.log("School details");
        projectOverviewPage
            .hasLocalAuthority(project.localAuthority)
            .hasRegion(project.region);
    });
});
