import { BulkProjectTable, BulkEditRow, ProjectDetailsRequest } from "cypress/api/domain";
import bulkEditProjectPage from "cypress/pages/bulkEditProjectPage";
import { v4 } from "uuid";
import { stringify } from "csv-stringify/sync";
import { utils, write } from "xlsx/xlsx";
import { RequestBuilder } from "cypress/api/requestBuilder";
import projectApi from "cypress/api/projectApi";


describe("Bulk editing project", () => {

    let project: ProjectDetailsRequest;
    let now: Date;

    beforeEach(() => {
        cy.login();

        now = new Date();

        project = RequestBuilder.createProjectDetails();

        projectApi
            .post({
                projects: [project],
            })
            .then(() => {
                cy.visit(`/bulk-edit-file-upload`);
            });
    });

    it("Should validate when no file is selected", () => {
        bulkEditProjectPage
          .continue()
          .errorMessage('Select a file');
    }); 

    it("Should validate an uploaded CSV file", () => {
        const completeRow: BulkEditRow = {
            projectId: v4(),
        };

        const emptyRow: BulkEditRow = {
            projectId: v4(),
        };

        const csv = createCsv([completeRow, emptyRow]);

        bulkEditProjectPage.upload(csv, "file.csv").continue();

    });

    it("Should validate an uploaded Excel file", () => {
        const completeRow: BulkEditRow = {
            projectId: v4(),
            localAuthority: v4(),
            actualOpeningDate: v4(),
            status: "Pre-opening",
        };

        const emptyRow: BulkEditRow = {
            projectId: v4(),
        };

        const buffer = createExcel([completeRow, emptyRow]);

        bulkEditProjectPage.upload(buffer, "file.xlsx").continue();
    });

    function createTable(rows: Array<BulkEditRow>) {
        const result: BulkProjectTable<BulkEditRow> = {
            headers: [
                "Project ID",
                "Project status",
                "Actual opening date",
                "Local authority"                
            ],
            rows: rows,
        };

        return result;
    }

    function createCsv(rows: Array<BulkEditRow>): Buffer {
        const table = createTable(rows);

        const result = stringify(table.rows, {
            columns: table.headers,
            header: true,
        });

        return Buffer.from(result);
    }

    function createExcel(rows: Array<BulkEditRow>): Buffer {
        const table = createTable(rows);

        const worksheet = utils.json_to_sheet(table.rows, {
            header: table.headers,
        });

        const workbook = utils.book_new();
        utils.book_append_sheet(workbook, worksheet);

        const buffer = write(workbook, {
            type: "buffer",
            bookType: "xlsx",
            compression: true,
        });

        return buffer;
    }
});
