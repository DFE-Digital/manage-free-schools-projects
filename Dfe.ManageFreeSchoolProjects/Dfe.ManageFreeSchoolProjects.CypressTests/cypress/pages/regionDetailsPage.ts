class RegionDetailsPage {
    public checkElementsVisible(schoolName :string): this {
        cy.contains("Back");

        cy.get("h1").contains(schoolName);
        cy.get("h1").contains("Edit region");

        cy.getByTestId("East Midlands").should("not.be.checked");
        cy.contains("East Midlands");

        cy.getByTestId("East of England").should("not.be.checked");
        cy.contains("East of England");

        cy.getByTestId("London").should("not.be.checked");
        cy.contains("London");

        cy.getByTestId("North East").should("not.be.checked");
        cy.contains("North East");

        cy.getByTestId("North West").should("not.be.checked");
        cy.contains("North West");

        cy.getByTestId("South East").should("not.be.checked");
        cy.contains("South East");

        cy.getByTestId("South West").should("not.be.checked");
        cy.contains("South West");

        cy.getByTestId("West Midlands").should("not.be.checked");
        cy.contains("West Midlands");

        cy.getByTestId("Yorkshire and the Humber").should("not.be.checked");
        cy.contains("Yorkshire and the Humber");

        cy.contains("Save and continue");
        
        return this;
    }

    public selectContinue(): this {
        cy.contains("Save and continue").click();

        return this;
    }

    public verifyValidationMessage(): this {
        cy.getById("region-error-link").contains("Select the region of the free school");
        cy.getById("region-error").contains("Select the region of the free school");

        return this;
    }

    public selectEastMidlands(): this {
        cy.getByTestId("East Midlands").click();
        cy.getByTestId("East Midlands").should("be.checked");

        cy.getByTestId("London").should("not.be.checked");

        cy.getByTestId("East of England").should("not.be.checked");

        cy.getByTestId("North East").should("not.be.checked");

        cy.getByTestId("North West").should("not.be.checked");

        cy.getByTestId("South East").should("not.be.checked");

        cy.getByTestId("South West").should("not.be.checked");

        cy.getByTestId("West Midlands").should("not.be.checked");

        cy.getByTestId("Yorkshire and the Humber").should("not.be.checked");

        return this;
    }

    public selectLondon(): this {
        cy.getByTestId("London").click();
        cy.getByTestId("London").should("be.checked");

        cy.getByTestId("East Midlands").should("not.be.checked");

        cy.getByTestId("North East").should("not.be.checked");

        cy.getByTestId("North West").should("not.be.checked");

        cy.getByTestId("South East").should("not.be.checked");

        cy.getByTestId("South West").should("not.be.checked");

        cy.getByTestId("West Midlands").should("not.be.checked");

        cy.getByTestId("Yorkshire and the Humber").should("not.be.checked");

        return this;
    }

    public selectEastOfEngland(): this {
        cy.getByTestId("East of England").click();
        cy.getByTestId("East of England").should("be.checked");

        cy.getByTestId("London").should("not.be.checked");

        cy.getByTestId("East Midlands").should("not.be.checked");

        cy.getByTestId("North East").should("not.be.checked");

        cy.getByTestId("North West").should("not.be.checked");

        cy.getByTestId("South East").should("not.be.checked");

        cy.getByTestId("South West").should("not.be.checked");

        cy.getByTestId("West Midlands").should("not.be.checked");

        cy.getByTestId("Yorkshire and the Humber").should("not.be.checked");

        return this;
    }

    public selectNorthEast(): this {
        cy.getByTestId("North East").click();
        cy.getByTestId("North East").should("be.checked");

        cy.getByTestId("East of England").should("not.be.checked");

        cy.getByTestId("East Midlands").should("not.be.checked");

        cy.getByTestId("London").should("not.be.checked");

        cy.getByTestId("North West").should("not.be.checked");

        cy.getByTestId("South East").should("not.be.checked");

        cy.getByTestId("South West").should("not.be.checked");

        cy.getByTestId("West Midlands").should("not.be.checked");

        cy.getByTestId("Yorkshire and the Humber").should("not.be.checked");

        return this;
    }

    public selectNorthWest(): this {
        cy.getByTestId("North West").click();
        cy.getByTestId("North West").should("be.checked");

        cy.getByTestId("East of England").should("not.be.checked");

        cy.getByTestId("East Midlands").should("not.be.checked");

        cy.getByTestId("London").should("not.be.checked");

        cy.getByTestId("North East").should("not.be.checked");

        cy.getByTestId("South East").should("not.be.checked");

        cy.getByTestId("South West").should("not.be.checked");

        cy.getByTestId("West Midlands").should("not.be.checked");

        cy.getByTestId("Yorkshire and the Humber").should("not.be.checked");

        return this;
    }

    public selectSouthEast(): this {
        cy.getByTestId("South East").click();
        cy.getByTestId("South East").should("be.checked");

        cy.getByTestId("East of England").should("not.be.checked");

        cy.getByTestId("East Midlands").should("not.be.checked");

        cy.getByTestId("London").should("not.be.checked");

        cy.getByTestId("North East").should("not.be.checked");

        cy.getByTestId("North West").should("not.be.checked");

        cy.getByTestId("South West").should("not.be.checked");

        cy.getByTestId("West Midlands").should("not.be.checked");

        cy.getByTestId("Yorkshire and the Humber").should("not.be.checked");

        return this;
    }

    public selectSouthWest(): this {
        cy.getByTestId("South West").click();
        cy.getByTestId("South West").should("be.checked");

        cy.getByTestId("East of England").should("not.be.checked");

        cy.getByTestId("East Midlands").should("not.be.checked");

        cy.getByTestId("London").should("not.be.checked");

        cy.getByTestId("North East").should("not.be.checked");

        cy.getByTestId("North West").should("not.be.checked");

        cy.getByTestId("South East").should("not.be.checked");

        cy.getByTestId("West Midlands").should("not.be.checked");

        cy.getByTestId("Yorkshire and the Humber").should("not.be.checked");

        return this;
    }

    public selectWestMidlands(): this {
        cy.getByTestId("West Midlands").click();
        cy.getByTestId("West Midlands").should("be.checked");

        cy.getByTestId("East of England").should("not.be.checked");

        cy.getByTestId("East Midlands").should("not.be.checked");

        cy.getByTestId("London").should("not.be.checked");

        cy.getByTestId("North East").should("not.be.checked");

        cy.getByTestId("North West").should("not.be.checked");

        cy.getByTestId("South East").should("not.be.checked");

        cy.getByTestId("South West").should("not.be.checked");

        cy.getByTestId("Yorkshire and the Humber").should("not.be.checked");

        return this;
    }

    public selectYorkshireAndHumber(): this {
        cy.getByTestId("Yorkshire and the Humber").click();
        cy.getByTestId("Yorkshire and the Humber").should("be.checked");

        cy.getByTestId("East of England").should("not.be.checked");

        cy.getByTestId("East Midlands").should("not.be.checked");

        cy.getByTestId("London").should("not.be.checked");

        cy.getByTestId("North East").should("not.be.checked");

        cy.getByTestId("North West").should("not.be.checked");

        cy.getByTestId("South East").should("not.be.checked");

        cy.getByTestId("South West").should("not.be.checked");

        cy.getByTestId("West Midlands").should("not.be.checked");

        return this;
    }



    
}

const regionDetailsPage = new RegionDetailsPage();

export default regionDetailsPage;