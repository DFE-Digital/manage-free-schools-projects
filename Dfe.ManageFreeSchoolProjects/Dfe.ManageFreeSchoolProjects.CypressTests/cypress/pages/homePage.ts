type AllowedFilters = "region" | "localAuthority" | "projectManagedBy" | "projectStatus";

class HomePage {

    public createNewProjects(): this { 
        cy.contains("Create a project").click();
        //   cy.login({role: ProjectRecordCreator})
        return this;
    }

    public createProject(): this {
        cy.getByTestId("create-project").click();

        return this;
    }

    public deleteProject(): this {
        cy.getByTestId("delete-project-button").click();

        return this;
    }

    public openFilter(): this {
        cy.getByTestId("filter-button").click();

        return this;
    }

    public withProjectFilter(project: string): this {
        cy.getByTestId("search-by-project").typeFast(project);

        return this;
    }

    public hasProjectFilter(project: string): this {
        cy.getByTestId("search-by-project")
            .should("have.value", project);

        return this;
    }

    public withRegionFilter(region: string): this {
        cy.getByTestId(`${region}-option`).check();

        return this;
    }

    public hasRegionFilter(region: string): this {
        cy.getByTestId(`${region}-option`).should("be.checked");

        return this;
    }

    public withLocalAuthorityFilter(localAuthority: string): this {
        cy.getByTestId(`${localAuthority}-option`).check();

        return this;
    }

    public hasLocalAuthorityFilter(localAuthority: string): this {
        cy.getByTestId(`${localAuthority}-option`).should("be.checked");

        return this;
    }

    public withProjectManagedByFilter(projectManagedBy: string): this {
        cy.getByTestId(`${projectManagedBy}-option`).check();

        return this;
    }

    public hasProjectManagedByFilter(projectManagedBy: string): this {
        cy.getByTestId(`${projectManagedBy}-option`).should("be.checked");

        return this;
    }

    public withProjectStatusFilter(projectStatus: string): this {
        cy.getByTestId(`${projectStatus}-option`).check();

        return this;
    }

    public hasProjectStatusFilter(projectStatus: string): this {
        cy.getByTestId(`${projectStatus}-option`).should("be.checked");

        return this;
    }

    public applyFilters(): this {
        cy.getByTestId("apply-filters").click();

        return this;
    }

    public clearFilters(): this {
        cy.getByTestId("clear-filters").click();

        return this;
    } 

    public tryViewProjectWithFilters(): this {

        const projectData: { [key: string]: string } = {};

        cy.get('tbody')
          .find('tr')
          .filter((_, row) => {
            const $row = Cypress.$(row);
    
            const hasProjectTitleOrId = $row.find('[data-testid="project-title"]').text().trim() !== "" ||
                                        $row.find('[data-testid="project-id"]').text().trim() !== "";
            const hasRegionName = $row.find('[data-testid="region-name"]').text().trim() !== "";
            const hasLocalAuthority = $row.find('[data-testid="local-authority"]').text().trim() !== "";
            const hasProjectManagedBy = $row.find('[data-testid="project-managed-by"]').text().trim() !== "";
            const hasStatus = $row.find('[data-testid="status"]').text().trim() !== "";
    
            return hasProjectTitleOrId && hasRegionName && hasLocalAuthority && hasProjectManagedBy && hasStatus;
          })
          .first()
          .then((firstRow) => {
            // Capture values into variables
            projectData.projectTitle = Cypress.$(firstRow).find('[data-testid="project-title"]').text().trim() ||
                                       Cypress.$(firstRow).find('[data-testid="project-id"]').text().trim();
            projectData.regionName = Cypress.$(firstRow).find('[data-testid="region-name"]').text().trim();
            projectData.localAuthority = Cypress.$(firstRow).find('[data-testid="local-authority"]').text().trim();
            projectData.projectManagedBy = Cypress.$(firstRow).find('[data-testid="project-managed-by"]').text().trim();
            projectData.status = Cypress.$(firstRow).find('[data-testid="status"]').text().trim();     
          }).then(() => {

            this.withProjectFilter(projectData.projectTitle);
            this.withRegionFilter(projectData.regionName);
            this.withLocalAuthorityFilter(projectData.localAuthority);
            this.withProjectManagedByFilter(projectData.projectManagedBy);
            this.withProjectStatusFilter(projectData.status);

            this.applyFilters();

            // const noProjectFound = cy.find('h1').contains('0 projects found');

            // if (noProjectFound) {
            //     this.tryViewProjectWithFilters();
            // }
          });
    
        return this;
    }

    // cy.get('.govuk-table').find('td').contains('a', 'View').first().click();

    public clickHeader(): this { 
        cy.getByClass('dfe-header__link--service').click();

        return this;
    }

    public regionsFilterInputCleared(): this {
        cy.get('#search-by-region-container input')
        .each(($input) => {
            cy.wrap($input).should('not.be.checked');
        });

        return this;
     }
     
     public selectAllRegions(): this {
        cy.get('#search-by-region-container input')
        .each(($input) => {
            cy.wrap($input).check();
        });

        return this;
     }

     public selectAllLocalAuthorities(): this {
        cy.get('#la-checkbox-container input')
        .each(($input) => {
            cy.wrap($input).check();
        });

        return this;
     }

     public withAllProjectAssignedTo(): this {
        cy.get('#search-by-pmb-container input')
        .each(($input) => {
            cy.wrap($input).check();
        });

        return this;
     }

     public allRegionsSelected(): this {
        cy.get('#search-by-region-container input')
        .each(($input) => {
            cy.wrap($input).should('be.checked');
        });

        return this;
     }

     public selectFilters(filtersToSelect: AllowedFilters[]) {
        filtersToSelect.forEach((filter) => {
            switch (filter) {
                case "region":
                    this.selectAllRegions();
                    break;
                case "localAuthority":
                    this.selectAllLocalAuthorities();
                    break;
                case "projectManagedBy":
                    this.withAllProjectAssignedTo();
                    break;
            }
        });
     }

    public downloadProjectDataExport() {
        cy.getByTestId("download-data-export").click();
    }
}

const homePage = new HomePage();

export default homePage;
