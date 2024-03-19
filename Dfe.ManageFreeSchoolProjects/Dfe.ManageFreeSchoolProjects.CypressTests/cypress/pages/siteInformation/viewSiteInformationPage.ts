export class ViewSiteInformationPage {
    public hasTemporarySiteAddress(line1: string, line2: string = ""): this {

        cy.enterAddress("temporary-site-address", line1, line2);

        return this;
    }

    public hasTemporarySitePostcode(postcode: string): this {
        cy.getByTestId("temporary-site-postcode").should("contain.text", postcode);

        return this;
    }

    public hasPermanentSiteAddress(line1: string, line2: string = ""): this {
        cy.enterAddress("permanent-site-address", line1, line2);

        return this;
    }

    public hasPermanentSitePostcode(postcode: string): this {
        cy.getByTestId("permanent-site-postcode").should("contain.text", postcode);

        return this;
    }

    public changeTemporarySite(): this {
        cy.getByTestId("change-temporary-site").click();

        return this;
    }

    public changePermanentSite(): this {
        cy.getByTestId("change-permanent-site").click();

        return this;
    }

    public backToProject(): this {
        cy.get(".govuk-back-link").click();

        return this;
    }
}

const viewSiteInformationPage = new ViewSiteInformationPage();

export default viewSiteInformationPage;