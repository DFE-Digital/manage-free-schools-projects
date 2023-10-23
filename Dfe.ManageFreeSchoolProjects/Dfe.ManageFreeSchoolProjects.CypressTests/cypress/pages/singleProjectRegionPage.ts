class SingleProjectRegionPage {
    public checkElementsVisible(): this {
        cy.contains("Back");

        cy.get("h1").contains("Creating a new free school project");
        cy.get("h1").contains("What is the region of the school?");

        cy.getByTestId("EastMidlands").should("not.be.checked");
        cy.contains("East Midlands");

        cy.getByTestId("EastOfEngland").should("not.be.checked");
        cy.contains("East Of England");

        cy.getByTestId("London").should("not.be.checked");
        cy.contains("London");

        cy.getByTestId("NorthEast").should("not.be.checked");
        cy.contains("North East");

        cy.getByTestId("NorthWest").should("not.be.checked");
        cy.contains("North West");

        cy.getByTestId("SouthEast").should("not.be.checked");
        cy.contains("South East");

        cy.getByTestId("SouthWest").should("not.be.checked");
        cy.contains("South West");

        cy.getByTestId("WestMidlands").should("not.be.checked");
        cy.contains("West Midlands");

        cy.getByTestId("YorkshireAndHumber").should("not.be.checked");
        cy.contains("Yorkshire and the Humber");

        cy.getByTestId("continue");
        
        return this;
    }
}

const singleProjectRegionPage = new SingleProjectRegionPage();

export default singleProjectRegionPage;