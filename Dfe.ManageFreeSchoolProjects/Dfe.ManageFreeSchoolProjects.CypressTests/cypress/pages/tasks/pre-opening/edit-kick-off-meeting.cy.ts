class KickOffMeetingEditPage {
    private errorTracking = "";
    
    titleIs(title: string): this {
        cy.getByTestId("title").should("contains.text", title)
        return this;
    }

    schoolNameIs(school: string) {
        cy.getByTestId("school-name").should("contains.text", school);
        return this;
    }

   
    private setDate(key: string, day: string, month: string, year: string) {
        cy.get('#' + `${key}-day`).clear().type(day);
        cy.get('#' + `${key}-month`).clear().type(month);
        cy.get('#' + `${key}-year`).clear().type(year);
    }

    checkFundingArrangementsAgreed(): this {
        cy.getById("funding-arrangements-agreed").check()
        return this
    }
    withComments(comment: string): this {
        cy.getById("funding-arrangements-details-agreed").clear().type(comment)
        return this;
    }

    withRealisticYearOfOpeningStartDate(year: string): this {
        cy.getById("realistic-year-of-opening-startyear").clear().type(year)
        return this;
    }

    withRealisticYearOfOpeningEndDate(year: string): this {
        cy.getById("realistic-year-of-opening-endyear").clear().type(year)
        return this;
    }
    

    withProvisionalOpeningDate(day: string, month: string, year: string): this {
        const key = "provisional-opening-date";
        this.setDate(key, day, month, year);
        return this
    }

   
    
    withSharepointLink(value: string): this {
        cy.getByTestId("sharepoint-link").clear().type(value)
        return this;
    }

    errorForComments(): this
    {
        this.errorTracking = "funding-arrangements-details-agreed";
        return this;       
    }

    withFundingArrangementsAgreed(setting: "Yes" | "No"): this {
        const control = "Funding arrangement details agreed between local authority and sponsor";
        cy.contains(control)
            .parent()
            .contains(setting)
            .invoke('attr', 'for')
            .then((id) => {
                cy.get('#' + id).click();
            });
        return this;
    }
    
    errorForRealisticStartDate(error: string): this {
        cy.getById('realistic-year-of-opening-error').contains(error)
        return this
    }

    errorForSharepointLink(): this
    {
        this.errorTracking = "sharepoint-link";
        return this;       
    }

    errorForProvisionalOpeningDate(): this
    {
        this.errorTracking = "provisional-opening-date";
        return this;       
    }
    
    showsError(error: string)
    {
        cy.get(`#${this.errorTracking}-error-link`)
            .should("contain.text", error);
        
        cy.get(`#${this.errorTracking}-error-link`)
        .invoke('attr', 'href')
        .then((href) => {
            cy.get(href as string).should("exist");
        });
    
        cy.get(`#${this.errorTracking}-error`)
            .should("contain.text", error);
        return this;
    }

    clickContinue() : this {
        cy.getByTestId("continue").click();
        return this;
    }
}

const kickOffMeetingEditPage = new KickOffMeetingEditPage();
export default kickOffMeetingEditPage;
