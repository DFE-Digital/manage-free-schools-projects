class SingleProjectRegionPage {
    public checkElementsVisible(): this {
        //cy.contains("Back");

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

    public selectContinue(): this {
        cy.getByTestId("continue").click();

        return this;
    }

    public verifyValidationMessage(): this {
        cy.getById("region-error-link").contains("The region field is required");
        cy.getById("region-error").contains("The region field is required");

        return this;
    }

    public selectEastMidlands(): this {
        cy.getByTestId("EastMidlands").click();
        cy.getByTestId("EastMidlands").should("be.checked");

        cy.getByTestId("London").should("not.be.checked");

        cy.getByTestId("EastOfEngland").should("not.be.checked");

        cy.getByTestId("NorthEast").should("not.be.checked");

        cy.getByTestId("NorthWest").should("not.be.checked");

        cy.getByTestId("SouthEast").should("not.be.checked");

        cy.getByTestId("SouthWest").should("not.be.checked");

        cy.getByTestId("WestMidlands").should("not.be.checked");

        cy.getByTestId("YorkshireAndHumber").should("not.be.checked");

        return this;
    }

    public selectLondon(): this {
        cy.getByTestId("London").click();
        cy.getByTestId("London").should("be.checked");

        cy.getByTestId("EastMidlands").should("not.be.checked");

        cy.getByTestId("NorthEast").should("not.be.checked");

        cy.getByTestId("NorthWest").should("not.be.checked");

        cy.getByTestId("SouthEast").should("not.be.checked");

        cy.getByTestId("SouthWest").should("not.be.checked");

        cy.getByTestId("WestMidlands").should("not.be.checked");

        cy.getByTestId("YorkshireAndHumber").should("not.be.checked");

        return this;
    }

    public selectEastOfEngland(): this {
        cy.getByTestId("EastOfEngland").click();
        cy.getByTestId("EastOfEngland").should("be.checked");

        cy.getByTestId("London").should("not.be.checked");

        cy.getByTestId("EastMidlands").should("not.be.checked");

        cy.getByTestId("NorthEast").should("not.be.checked");

        cy.getByTestId("NorthWest").should("not.be.checked");

        cy.getByTestId("SouthEast").should("not.be.checked");

        cy.getByTestId("SouthWest").should("not.be.checked");

        cy.getByTestId("WestMidlands").should("not.be.checked");

        cy.getByTestId("YorkshireAndHumber").should("not.be.checked");

        return this;
    }

    public selectNorthEast(): this {
        cy.getByTestId("NorthEast").click();
        cy.getByTestId("NorthEast").should("be.checked");

        cy.getByTestId("EastOfEngland").should("not.be.checked");

        cy.getByTestId("EastMidlands").should("not.be.checked");

        cy.getByTestId("London").should("not.be.checked");

        cy.getByTestId("NorthWest").should("not.be.checked");

        cy.getByTestId("SouthEast").should("not.be.checked");

        cy.getByTestId("SouthWest").should("not.be.checked");

        cy.getByTestId("WestMidlands").should("not.be.checked");

        cy.getByTestId("YorkshireAndHumber").should("not.be.checked");

        return this;
    }

    public selectNorthWest(): this {
        cy.getByTestId("NorthWest").click();
        cy.getByTestId("NorthWest").should("be.checked");

        cy.getByTestId("EastOfEngland").should("not.be.checked");

        cy.getByTestId("EastMidlands").should("not.be.checked");

        cy.getByTestId("London").should("not.be.checked");

        cy.getByTestId("NorthEast").should("not.be.checked");

        cy.getByTestId("SouthEast").should("not.be.checked");

        cy.getByTestId("SouthWest").should("not.be.checked");

        cy.getByTestId("WestMidlands").should("not.be.checked");

        cy.getByTestId("YorkshireAndHumber").should("not.be.checked");

        return this;
    }

    public selectSouthEast(): this {
        cy.getByTestId("SouthEast").click();
        cy.getByTestId("SouthEast").should("be.checked");

        cy.getByTestId("EastOfEngland").should("not.be.checked");

        cy.getByTestId("EastMidlands").should("not.be.checked");

        cy.getByTestId("London").should("not.be.checked");

        cy.getByTestId("NorthEast").should("not.be.checked");

        cy.getByTestId("NorthWest").should("not.be.checked");

        cy.getByTestId("SouthWest").should("not.be.checked");

        cy.getByTestId("WestMidlands").should("not.be.checked");

        cy.getByTestId("YorkshireAndHumber").should("not.be.checked");

        return this;
    }

    public selectSouthWest(): this {
        cy.getByTestId("SouthWest").click();
        cy.getByTestId("SouthWest").should("be.checked");

        cy.getByTestId("EastOfEngland").should("not.be.checked");

        cy.getByTestId("EastMidlands").should("not.be.checked");

        cy.getByTestId("London").should("not.be.checked");

        cy.getByTestId("NorthEast").should("not.be.checked");

        cy.getByTestId("NorthWest").should("not.be.checked");

        cy.getByTestId("SouthEast").should("not.be.checked");

        cy.getByTestId("WestMidlands").should("not.be.checked");

        cy.getByTestId("YorkshireAndHumber").should("not.be.checked");

        return this;
    }

    public selectWestMidlands(): this {
        cy.getByTestId("WestMidlands").click();
        cy.getByTestId("WestMidlands").should("be.checked");

        cy.getByTestId("EastOfEngland").should("not.be.checked");

        cy.getByTestId("EastMidlands").should("not.be.checked");

        cy.getByTestId("London").should("not.be.checked");

        cy.getByTestId("NorthEast").should("not.be.checked");

        cy.getByTestId("NorthWest").should("not.be.checked");

        cy.getByTestId("SouthEast").should("not.be.checked");

        cy.getByTestId("SouthWest").should("not.be.checked");

        cy.getByTestId("YorkshireAndHumber").should("not.be.checked");

        return this;
    }

    public selectYorkshireAndHumber(): this {
        cy.getByTestId("YorkshireAndHumber").click();
        cy.getByTestId("YorkshireAndHumber").should("be.checked");

        cy.getByTestId("EastOfEngland").should("not.be.checked");

        cy.getByTestId("EastMidlands").should("not.be.checked");

        cy.getByTestId("London").should("not.be.checked");

        cy.getByTestId("NorthEast").should("not.be.checked");

        cy.getByTestId("NorthWest").should("not.be.checked");

        cy.getByTestId("SouthEast").should("not.be.checked");

        cy.getByTestId("SouthWest").should("not.be.checked");

        cy.getByTestId("WestMidlands").should("not.be.checked");

        return this;
    }



    
}

const singleProjectRegionPage = new SingleProjectRegionPage();

export default singleProjectRegionPage;