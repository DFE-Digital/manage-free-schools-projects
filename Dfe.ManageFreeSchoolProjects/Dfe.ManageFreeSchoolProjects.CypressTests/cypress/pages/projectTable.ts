import { ProjectRow } from "./projectRow";

class ProjectTable {
    public getRowByProjectId(projectId: string): Cypress.Chainable<ProjectRow> {
        return cy.getByTestId(`row-${projectId}`).then((el) => {
            return new ProjectRow(el);
        });
    }
}

const projectTable = new ProjectTable();

export default projectTable;
