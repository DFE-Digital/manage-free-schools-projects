import summaryPage from "./task-summary-base";

class RegionAndLocalAuthoritySummaryPage {

    public verifyRegionAndLASummaryElementsVisible(schoolName: string): this {
        cy.getByClass("govuk-back-link").contains("Back");
        cy.getByClass("govuk-caption-l").contains(schoolName);
        cy.getByClass("govuk-heading-xl").contains("Region and local authority");

        summaryPage.inOrder()
            .summaryShows("Region").IsEmpty().HasChangeLink()
            .summaryShows("Local authority").IsEmpty().HasChangeLink();

        cy.getById("mark-as-completed").should("not.be.checked");
        cy.contains("Mark this section as complete, you can still make changes later");

        cy.getByClass("govuk-button").should("be.visible").contains("Confirm and continue");

        return this;
    }

    public verifyRegionAndLASummaryCompleteElementsVisible(schoolName: string): this {
        cy.getByClass("govuk-back-link").contains("Back");
        cy.getByClass("govuk-caption-l").contains(schoolName);
        cy.getByClass("govuk-heading-xl").contains("Region and local authority");


        summaryPage.inOrder()
            .summaryShows("Region").HasValue("South West").HasChangeLink()
            .summaryShows("Local authority").HasValue("Plymouth").HasChangeLink();

        cy.getById("mark-as-completed").should("not.be.checked");
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
        cy.getById("mark-as-completed").click();
        return this;
    }

    public selectConfirmAndContinue(): this {
        cy.contains("Confirm and continue").click();
        return this;
    }
}

const regionAndLocalAuthoritySummaryPage = new RegionAndLocalAuthoritySummaryPage();

export default regionAndLocalAuthoritySummaryPage;