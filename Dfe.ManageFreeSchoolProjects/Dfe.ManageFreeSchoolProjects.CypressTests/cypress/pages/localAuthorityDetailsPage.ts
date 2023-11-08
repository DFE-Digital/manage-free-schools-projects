class LocalAuthorityDetailsPage {
    public checkElementsVisible(schoolName :string): this {
        cy.contains("Back");

        cy.get("h1").contains(schoolName);
        cy.get("h1").contains("Edit local authority");

        cy.getByTestId("Bedford").should("not.be.checked");
        cy.contains("Bedford");

        cy.getByTestId("Cambridgeshire").should("not.be.checked");
        cy.contains("Cambridgeshire");

        cy.getByTestId("CentralBedfordshire").should("not.be.checked");
        cy.contains("Central Bedfordshire");

        cy.getByTestId("Essex").should("not.be.checked");
        cy.contains("Essex");

        cy.getByTestId("Hertfordshire").should("not.be.checked");
        cy.contains("Hertfordshire");

        cy.getByTestId("continue");
        
        return this;
    }

    public selectContinue(): this {
        cy.getByTestId("continue").click();

        return this;
    }

    public verifyValidationMessage(): this {
        cy.getById("local-authority-error-link").contains("The local authority field is required");
        cy.getById("local-authority-error").contains("The local authority field is required");

        return this;
    }

    public selectBedford(): this {
        cy.getByTestId("Bedford").click();
        cy.getByTestId("Bedford").should("be.checked");

        cy.getByTestId("Cambridgeshire").should("not.be.checked");
        cy.getByTestId("CentralBedfordshire").should("not.be.checked");
        cy.getByTestId("Essex").should("not.be.checked");
        cy.getByTestId("Hertfordshire").should("not.be.checked");
    
        return this;
    }

    public selectCambridgeshire(): this {
        cy.getByTestId("Cambridgeshire").click();
        cy.getByTestId("Cambridgeshire").should("be.checked");

        cy.getByTestId("Bedford").should("not.be.checked");
        cy.getByTestId("CentralBedfordshire").should("not.be.checked");
        cy.getByTestId("Essex").should("not.be.checked");
        cy.getByTestId("Hertfordshire").should("not.be.checked");
    
        return this;
    }

    public selectCentralBedfordshire(): this {
        cy.getByTestId("CentralBedfordshire").click();
        cy.getByTestId("CentralBedfordshire").should("be.checked");

        cy.getByTestId("Bedford").should("not.be.checked");
        cy.getByTestId("Cambridgeshire").should("not.be.checked");
        cy.getByTestId("Essex").should("not.be.checked");
        cy.getByTestId("Hertfordshire").should("not.be.checked");
    
        return this;
    }

    public selectEssex(): this {
        cy.getByTestId("Essex").click();
        cy.getByTestId("Essex").should("be.checked");

        cy.getByTestId("Bedford").should("not.be.checked");
        cy.getByTestId("Cambridgeshire").should("not.be.checked");
        cy.getByTestId("CentralBedfordshire").should("not.be.checked");
        cy.getByTestId("Hertfordshire").should("not.be.checked");
    
        return this;
    }

    public selectHertfordshire(): this {
        cy.getByTestId("Hertfordshire").click();
        cy.getByTestId("Hertfordshire").should("be.checked");

        cy.getByTestId("Bedford").should("not.be.checked");
        cy.getByTestId("Cambridgeshire").should("not.be.checked");
        cy.getByTestId("CentralBedfordshire").should("not.be.checked");
        cy.getByTestId("Essex").should("not.be.checked");
    
        return this;
    }



    
}

const localAuthorityDetailsPage = new LocalAuthorityDetailsPage();

export default localAuthorityDetailsPage;