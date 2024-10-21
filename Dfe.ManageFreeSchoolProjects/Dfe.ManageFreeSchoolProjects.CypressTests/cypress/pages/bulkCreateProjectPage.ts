class BulkCreateProjectPage {
    public upload(file: Buffer, filename: string): this {
        cy.get('input[name="upload"]').selectFile({
            contents: file,
            fileName: filename,
            lastModified: Date.now(),
        });
        return this;
    }

    public errorMessage(error: string): this {
        cy.get('.govuk-error-summary').should('contains.text', error)
        return this
    }
    
    public continue(): this {
        cy.contains('button','Upload').click()
        return this;
    }


}

const bulkCreateProjectPage = new BulkCreateProjectPage();

export default bulkCreateProjectPage;
