import { ProjectRecordCreator } from "cypress/constants/cypressConstants";

class HomePage {

    public createNewProjects(): this {
        cy.contains("Create new projects").click();
        cy.login({role: ProjectRecordCreator})
        return this;
    }

    public createProject(): this {
        cy.getByTestId("create-project-button").click();

        return this;
    }

    public deleteProject(): this {
        cy.getByTestId("delete-project-button").click();

        return this;
    }

    public withProjectFilter(project: string): this {
        cy.getByTestId("search-by-project").clear().type(project);

        return this;
    }

    public withRegionFilter(region: string): this {
        cy.getByTestId(`${region}-option`).check();

        return this;
    }

    public withLocalAuthorityFilter(localAuthority: string): this {
        cy.getByTestId(`${localAuthority}-option`).check();

        return this;
    }

    public applyFilters(): this {
        cy.getByTestId("apply-filters").click();

        return this;
    }
}

const homePage = new HomePage();

export default homePage;
