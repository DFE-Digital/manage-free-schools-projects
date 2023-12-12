import readSummaryComponent from "cypress/pages/readSummaryComponent" 
import { read } from "fs";

class RegionAndLocalAuthoritySummaryPage {

    public verifyRegionAndLASummaryElementsVisible(schoolName: string): this {
        cy.getByClass("govuk-back-link").contains("Back");
        cy.getByClass("govuk-caption-l").contains(schoolName);
        cy.getByClass("govuk-heading-xl").contains("Region and local authority");

        readSummaryComponent.readSummaryLine("Region", "Empty", 0)
                            .readSummaryLine("Local authority", "Empty", 1);

        //cy.getByClass("govuk-summary-list__key").eq(0).contains("Region");
        //cy.getByClass("govuk-summary-list__value").eq(0).contains("Empty");
        //cy.getByClass("govuk-link").eq(2).contains("Change");

        // POSSIBLE HOMEWORK
        

        //cy.getByClass("govuk-summary-list__key").eq(1).contains("Local authority");
        //cy.getByClass("govuk-summary-list__value").eq(1).contains("Empty");
        //cy.getByClass("govuk-link").eq(3).contains("Change");


        cy.getById("mark-as-complete").should("not.be.checked");
        cy.contains("Mark this section as complete, you can still make changes later");

        cy.getByClass("govuk-button").should("be.visible").contains("Confirm and continue");
        
        return this;
    }

    public verifyRegionAndLASummaryCompleteElementsVisible(schoolName: string): this {
        cy.getByClass("govuk-back-link").contains("Back");
        cy.getByClass("govuk-caption-l").contains(schoolName);
        cy.getByClass("govuk-heading-xl").contains("Region and local authority");

        // CALLING OUR readSummaryLine FUNCTION POST FILLING OUR TASKLIST REGION AND LA SECTION
        readSummaryComponent.readSummaryLine("Region", "South West", 0)
                            .readSummaryLine("Local authority", "Plymouth", 1);

        cy.getById("mark-as-complete").should("not.be.checked");
        cy.contains("Mark this section as complete, you can still make changes later");

        cy.getByClass("govuk-button").should("be.visible").contains("Confirm and continue");
        
        return this;
    }

    public selectChangeRegionToGoToRegionDetails(): this {
        cy.contains("Change").eq(0).click();

        return this;
    }

    public selectChangeLAToGoToRegionDetails(): this {
        cy.contains("Change").eq(1).click();

        return this;
    }

    public selectMarkItemAsComplete(): this {
        cy.getById("mark-as-complete").click();
        return this;
    }

    public selectConfirmAndContinue(): this {
        cy.contains("Confirm and continue").click();
    }
}

const regionAndLocalAuthoritySummaryPage = new RegionAndLocalAuthoritySummaryPage();

export default regionAndLocalAuthoritySummaryPage;