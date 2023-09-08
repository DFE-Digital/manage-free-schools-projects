import { ProjectDetails } from "cypress/api/domain";
import projectApi from "cypress/api/projectApi";
import { RequestBuilder } from "cypress/api/requestBuilder";
import homePage from "cypress/pages/homePage";
import projectTable from "cypress/pages/projectTable";

describe("Testing the home page", () => {
    beforeEach(() => {
        cy.login();
        cy.visit("/");
    });

    describe("Filtering by project title", () => {
        let firstProject: ProjectDetails;
        let secondProject: ProjectDetails;
        let projectTitlePrefix: string;

        beforeEach(() => {
            firstProject = RequestBuilder.createProjectDetails();
            secondProject = RequestBuilder.createProjectDetails();
            projectTitlePrefix = firstProject.SchoolName.substring(0, 10);

            firstProject.SchoolName = `${projectTitlePrefix} school`;
            secondProject.SchoolName = `${projectTitlePrefix} academy`;

            projectApi.post({
                Projects: [firstProject, secondProject],
            });
        });

        it("Should be able to filter projects by project ID", () => {
            homePage.withProjectFilter(projectTitlePrefix).applyFilters();

            projectTable
                .getRowByProjectId(firstProject.ProjectId)
                .then((row) => {
                    row.hasProjectId(firstProject.ProjectId);
                    row.hasProjectTitle(firstProject.SchoolName);
                    row.hasStatus("Not started");
                });

            projectTable
                .getRowByProjectId(secondProject.ProjectId)
                .then((row) => {
                    row.hasProjectId(secondProject.ProjectId);
                    row.hasProjectTitle(secondProject.SchoolName);
                    row.hasStatus("Not started");
                });
        });
    });
});
