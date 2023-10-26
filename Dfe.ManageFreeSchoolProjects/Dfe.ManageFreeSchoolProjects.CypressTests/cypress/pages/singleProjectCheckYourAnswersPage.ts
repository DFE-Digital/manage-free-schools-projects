class SingleProjectCheckYourAnswersPage {
    public checkElementsVisible(): this {
        cy.contains("Back");

        cy.get("h1").contains("Creating a new free school project");
        cy.get("h1").contains("Check your answers");

        cy.get("dt").eq(0).contains("Temporary Project ID");
        cy.get("a[href='/project/create/projectid']").contains("Change");

        cy.get("dt").eq(1).contains("Current free school name");
        cy.get("dd").eq(2).contains("St Dunstan's Abbey, (Plymouth)");
        cy.get("a[href='/project/create/school']").contains("Change");

        cy.get("dt").eq(2).contains("Region");
        cy.get("dd").eq(4).contains("South West");
        cy.get("a[href='create-new-project-regions']").contains("Change");

        cy.get("dt").eq(3).contains("Local authority");
        cy.get("dd").eq(6).contains("Bedford");
        cy.get("a[href='create-new-project-local-authority']").contains("Change");

        cy.get("dt").eq(4).contains("Provisional opening date agreed with trust");
    
        cy.get(".govuk-summary-list__actions").eq(4).contains("Change");
        
        cy.get("dt").eq(5).contains("Number of forms of entry");
        cy.get("dd").eq(10).contains("1");
        cy.get(".govuk-summary-list__actions").eq(5).contains("Change");
        
        cy.get("dt").eq(6).contains("School type");
        cy.get("dd").eq(12).contains("AP");
        cy.get(".govuk-summary-list__actions").eq(6).contains("Change");

        cy.get("h2").contains("Now create your project");

        cy.contains("By selecting the Create project button, you are confirming that you have approval to create projects.");
        
        cy.get(".govuk-button").contains("Create project");
        
        return this;
    }

    public submitAnswersAndGenerateProject(): this {
        cy.get(".govuk-button").contains("Create project").click();
        return this;
    }
}

const singleProjectCheckYourAnswersPage = new SingleProjectCheckYourAnswersPage();

export default singleProjectCheckYourAnswersPage;