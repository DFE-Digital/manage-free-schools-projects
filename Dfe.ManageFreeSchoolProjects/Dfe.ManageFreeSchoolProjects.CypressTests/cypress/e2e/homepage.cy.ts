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
            projectTitlePrefix = firstProject.schoolName.substring(0, 10);

            firstProject.schoolName = `${projectTitlePrefix} school`;
            secondProject.schoolName = `${projectTitlePrefix} academy`;

            projectApi.post({
                projects: [firstProject, secondProject],
            });
        });

        it("Should be able to filter projects by project ID", () => {
            homePage.withProjectFilter(projectTitlePrefix).applyFilters();

            projectTable
                .getRowByProjectId(firstProject.projectId)
                .then((row) => {
                    row.hasProjectId(firstProject.projectId);
                    row.hasProjectTitle(firstProject.schoolName);
                    row.hasStatus("Not started");
                });

            projectTable
                .getRowByProjectId(secondProject.projectId)
                .then((row) => {
                    row.hasProjectId(secondProject.projectId);
                    row.hasProjectTitle(secondProject.schoolName);
                    row.hasStatus("Not started");
                });
        });
    });

    describe("Filtering by region", () => {
        let firstProject: ProjectDetails;

        beforeEach(() => {
            firstProject = RequestBuilder.createProjectDetails();
            firstProject.region = `North West`;

            projectApi.post({
                projects: [firstProject],
            });
        });

        it.only("Should be able to filter projects by region", () => {
            homePage.withRegionFilter("North West").applyFilters();

            projectTable.allRowsHaveRegion("North West");
        });
    });
});
