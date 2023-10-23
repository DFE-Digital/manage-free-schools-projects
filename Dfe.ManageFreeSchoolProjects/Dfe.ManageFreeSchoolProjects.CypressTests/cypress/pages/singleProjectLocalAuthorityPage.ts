class SingleProjectLocalAuthorityPage {
    public checkElementsVisible(): this {
        cy.contains("Back");

        cy.get("h1").contains("Creating a new free school project");
        cy.get("h1").contains("What is the local authority?");

        cy.getByTestId("Bedford").should("not.be.checked");
        cy.contains("Bedford");

        cy.getByTestId("Cambridgeshire").should("not.be.checked");
        cy.contains("Cambridgeshire");

        cy.getByTestId("Central Bedfordshire").should("not.be.checked");
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
        cy.getById("local-authority-error").contains("The The local authority field is required");

        return this;
    }

    public selectBedford(): this {
        cy.getByTestId("Bedford").click();
        cy.getByTestId("Bedford").should("be.checked");

        cy.getByTestId("Cambridgeshire").should("not.be.checked");
        cy.getByTestId("CentralBedfordshire").contains("not.be.checked");
        cy.getByTestId("Essex").contains("not.be.checked");
        cy.getByTestId("Hertfordshire").contains("not.be.checked");
    
        return this;
    }

    public selectCambridgeshire(): this {
        cy.getByTestId("Cambridgeshire").click();
        cy.getByTestId("Cambridgeshire").should("be.checked");

        cy.getByTestId("Bedford").should("not.be.checked");
        cy.getByTestId("CentralBedfordshire").contains("not.be.checked");
        cy.getByTestId("Essex").contains("not.be.checked");
        cy.getByTestId("Hertfordshire").contains("not.be.checked");
    
        return this;
    }

    public selectCentralBedfordshire(): this {
        cy.getByTestId("CentralBedfordshire").click();
        cy.getByTestId("CentralBedfordshire").should("be.checked");

        cy.getByTestId("Bedford").should("not.be.checked");
        cy.getByTestId("Cambridgeshire").contains("not.be.checked");
        cy.getByTestId("Essex").contains("not.be.checked");
        cy.getByTestId("Hertfordshire").contains("not.be.checked");
    
        return this;
    }

    public selectEssex(): this {
        cy.getByTestId("Essex").click();
        cy.getByTestId("Essex").should("be.checked");

        cy.getByTestId("Bedford").should("not.be.checked");
        cy.getByTestId("Cambridgeshire").contains("not.be.checked");
        cy.getByTestId("CentralBedfordshire").contains("not.be.checked");
        cy.getByTestId("Hertfordshire").contains("not.be.checked");
    
        return this;
    }

    public selectHertfordshire(): this {
        cy.getByTestId("Hertfordshire").click();
        cy.getByTestId("Hertfordshire").should("be.checked");

        cy.getByTestId("Bedford").should("not.be.checked");
        cy.getByTestId("Cambridgeshire").contains("not.be.checked");
        cy.getByTestId("CentralBedfordshire").contains("not.be.checked");
        cy.getByTestId("Essex").contains("not.be.checked");
    
        return this;
    }




}

const singleProjectLocalAuthorityPage = new SingleProjectLocalAuthorityPage();

export default singleProjectLocalAuthorityPage;