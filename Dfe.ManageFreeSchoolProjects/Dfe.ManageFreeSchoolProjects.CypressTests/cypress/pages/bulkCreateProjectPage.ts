class BulkCreateProjectPage {
    public uploadCsv(file): this {
        cy.getByTestId("upload").selectFile({
            contents: Cypress.Buffer.from(file),
            fileName: "file.csv",
            lastModified: Date.now(),
        });
        return this;
    }

    public continue(): this {
        cy.getByTestId("continue").click();

        return this;
    }
}

const bulkCreateProjectPage = new BulkCreateProjectPage();

export default bulkCreateProjectPage;
