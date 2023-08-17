import { CsvTable, ProjectTableRow } from "cypress/api/domain";
import bulkCreateProjectPage from "cypress/pages/bulkCreateProjectPage";
import { v4 } from "uuid";
import { stringify } from "csv-stringify/lib/sync";

describe("Creating a bulk project", () => {
    beforeEach(() => {
        cy.login();
        cy.visit("/project/create/bulk");
    });

    describe("Testing out a csv", () => {
        beforeEach(() => {});

        it("Should validate an uploaded CSV file", () => {
            const completeRow: ProjectTableRow = {
                projectId: v4(),
                projectTitle: v4(),
                trustName: v4(),
                region: v4(),
                localAuthority: v4(),
                realisticOpeningDate: v4(),
                status: v4(),
            };

            const emptyRow: ProjectTableRow = {};

            const csv = createCsv([completeRow, emptyRow]);

            bulkCreateProjectPage.uploadCsv(csv).continue();
        });
    });

    function createCsv(rows: Array<ProjectTableRow>): string {
        const data: CsvTable<ProjectTableRow> = {
            headers: [
                "projectId",
                "projectTitle",
                "trustName",
                "region",
                "localAuthority",
                "realisticOpeningDate",
                "status",
            ],
            rows: rows,
        };

        const result = stringify(data.rows, {
            columns: data.headers,
            header: true,
        });

        return result;
    }
});
