class SingleProjectCheckYourAnswersPage {
    public checkElementsVisible(): this {

        return this;
    }

    public submitAnswersAndGenerateProject(): this {
        cy.contains("Create project").eq(1).click();
        return this;
    }
}

const singleProjectCheckYourAnswersPage = new SingleProjectCheckYourAnswersPage();

export default singleProjectCheckYourAnswersPage;