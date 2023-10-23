class SingleProjectCheckYourAnswersPage {
    public checkElementsVisible(): this {
        cy.contains("Back");

        cy.get("h1").contains("Creating a new free school project");
        cy.get("h1").contains("Check your answers");

        cy.get("dt").eq(0).contains("Temporary Project ID");
        cy.get("a[href='/project/create/projectid']").contains("Change");

        cy.get("dt").eq(1).contains("Current free school name");
        cy.get("dd").eq(1).contains("St Dunstan's Abbey, (Plymouth)");
        cy.get("a[href='create-new-project-school-name']").contains("Change");

        cy.get("dt").eq(2).contains("Region");
        cy.get("dd").eq(2).contains("South West");
        cy.get("a[href='create-new-project-regions']").contains("Change");

        cy.get("dt").eq(3).contains("Local authority");
        cy.get("dd").eq(3).contains("Bedford");
        cy.get("a[href='create-new-project-local-authority']").contains("Change");

        cy.get("dt").eq(4).contains("Provisional opening date agreed with trust");
    
        cy.get("a[href='create-new-project-school-name']").contains("Change");

        cy.contains("Change").eq(4);
        
        cy.get("dt").eq(5).contains("Number of forms of entry");
        cy.get("dd").eq(5).contains("1");
        cy.contains("Change").eq(5);
        
        cy.get("dt").eq(6).contains("School type");
        cy.get("dd").eq(6).contains("AP");
        cy.contains("Change").eq(6);

        cy.get("h2").contains("Now create your project");

        cy.get("p").eq(0).contains("By selecting the Create project button, yuo are confirming that you have approval to create projects.");
        
        cy.contains("Create project").eq(1);
        
        return this;
    }

    public submitAnswersAndGenerateProject(): this {
        cy.contains("Create project").eq(1).click();
        return this;
    }
}

const singleProjectCheckYourAnswersPage = new SingleProjectCheckYourAnswersPage();

export default singleProjectCheckYourAnswersPage;