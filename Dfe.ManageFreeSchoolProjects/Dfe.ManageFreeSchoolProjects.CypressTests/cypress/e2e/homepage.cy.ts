import dashboardApi from "cypress/api/dashboardApi";
import { ProjectDetailsRequest } from "cypress/api/domain";
import projectApi from "cypress/api/projectApi";
import { RequestBuilder } from "cypress/api/requestBuilder";
import { Logger } from "cypress/common/logger";
import homePage from "cypress/pages/homePage";
import paginationComponent from "cypress/pages/paginationComponent";
import projectTable from "cypress/pages/projectTable";

describe("Testing the home page", () => {
    beforeEach(() => {
        cy.login();
        cy.visit("/");
    });

    describe("Filtering by project", () => {
        let firstProject: ProjectDetailsRequest;
        let secondProject: ProjectDetailsRequest;
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
        let firstProject: ProjectDetailsRequest;

        beforeEach(() => {
            firstProject = RequestBuilder.createProjectDetails();
            firstProject.region = `North West`;

            projectApi.post({
                projects: [firstProject],
            });
        });

        it("Should be able to filter projects by region", () => {
            homePage.withRegionFilter("North West").applyFilters();

            projectTable.allRowsHaveRegion("North West");
        });
    });

    describe("Filtering by region and Local authority", () => {
        let firstProject: ProjectDetailsRequest;

        beforeEach(() => {
            firstProject = RequestBuilder.createProjectDetails();
            firstProject.region = `East Of England`;
            firstProject.localAuthority = "Bedford";

            const secondProject = RequestBuilder.createProjectDetails();
            firstProject.region = "East Of England";

            projectApi.post({
                projects: [firstProject, secondProject],
            });
        });

        it("Should be able to filter projects by local authority", () => {
            homePage
                .withRegionFilter("East Of England")
                .withLocalAuthorityFilter("Bedford")
                .applyFilters();

            projectTable.allRowsHaveLocalAuthority("Bedford");
        });
    });

    describe("Pagination on the dashboard with filters for project", () => {
        const paginationPrefix = "Pagination";
        const region = "South East";

        beforeEach(() => {
            dashboardApi
                .get({ project: paginationPrefix, regions: [region] })
                .then((response) => {
                    console.log(response);
                    const currentNumberOfProjects = response.paging.recordCount;
                    const projectsToCreate = 15 - currentNumberOfProjects;

                    const projects: Array<ProjectDetailsRequest> = [];

                    for (let idx = 0; idx < projectsToCreate; idx++) {
                        const project = RequestBuilder.createProjectDetails();
                        project.schoolName = `${project.schoolName.substring(
                            0,
                            15,
                        )} ${paginationPrefix}`;
                        project.region = region;
                        projects.push(project);
                    }

                    projectApi.post({
                        projects: projects,
                    });

                    cy.reload();
                });
        });

        it("Should paginate the cases based on my filter criteria", () => {
            homePage
                .withProjectFilter(paginationPrefix)
                .withRegionFilter(region)
                .applyFilters();

            let pageOneProjects: Array<string> = [];
            let pageTwoProjects: Array<string> = [];

            projectTable
                .getProjectIds()
                .then((projectIds: Array<string>) => {
                    pageOneProjects = projectIds;

                    Logger.log("Ensure we have 5 projects on page one");
                    expect(pageOneProjects.length).to.eq(5);

                    Logger.log(
                        "Moving to the second page using the direct link",
                    );
                    paginationComponent.goToPage("2");
                    return projectTable.getProjectIds();
                })
                .then((projectIds: Array<string>) => {
                    pageTwoProjects = projectIds;

                    Logger.log("Ensure we have 5 projects on page 2");
                    expect(pageTwoProjects.length).to.equal(5);

                    Logger.log(
                        "Ensure that the projects on page one and two are different",
                    );
                    hasNoSimilarElements(pageOneProjects, pageTwoProjects);

                    Logger.log("Move to the previous page, which is page 1");
                    paginationComponent.previous();
                    return projectTable.getProjectIds();
                })
                .then((projectIds: Array<string>) => {
                    Logger.log(
                        "On moving to page one, we should get the exact same projects",
                    );
                    expect(projectIds).to.deep.equal(pageOneProjects);

                    Logger.log("Move to the next page, which is page 2");
                    paginationComponent.next();
                    return projectTable.getProjectIds();
                })
                .then((projectIds: Array<string>) => {
                    Logger.log(
                        "On moving to page two, we should get the exact same cases",
                    );
                    expect(projectIds).to.deep.equal(pageTwoProjects);
                });
        });
    });

    function hasNoSimilarElements(first: Array<string>, second: Array<string>) {
        const firstSet = new Set(first);

        const match = second.some((e) => firstSet.has(e));

        expect(match).to.be.false;
    }
});
