class EditProjectRiskPage {

    public hasGovernanceAndSuitabilityRiskRating(value: string): this {
        cy.getByTestId(`risk-rating-${value}`).should("be.checked");

        return this;
    }

    public hasGovernanceAndSuitabilityRiskSummary(value: string): this {
        cy.getByTestId(`governance-and-suitability-risk-summary`).should("have.text", value);

        return this;
    }

    public withGovernanceAndSuitabilityRiskRating(value: string): this {

        cy.getByTestId(`risk-rating-${value}`).check();

        return this;
    }

    public withGovernanceAndSuitabilityRiskSummary(value: string): this {

        cy.getByTestId(`governance-and-suitability-risk-summary`).clear().type(value);

        return this;
    }

    public withGovernanceAndSuitabilityRiskSummaryExceeding(): this {
        cy.getByTestId(`governance-and-suitability-risk-summary`).clear().invoke('text', 'a'.repeat(1001));

        return this;
    }

    public hasEducationRiskRating(value: string): this {
        cy.getByTestId(`risk-rating-${value}`).should("be.checked");

        return this;
    }

    public hasEducationRiskSummary(value: string): this {
        cy.getByTestId(`education-risk-summary`).should("have.text", value);

        return this;
    }

    public withEducationRiskRating(value: string): this {

        cy.getByTestId(`risk-rating-${value}`).check();

        return this;
    }

    public withEducationSummary(value: string): this {

        cy.getByTestId(`education-risk-summary`).clear().type(value);

        return this;
    }

    public withEducationSummaryExceeding(): this {
        cy.getByTestId(`education-risk-summary`).clear().invoke('text', 'a'.repeat(1001));

        return this;
    }

    public hasFinanceRiskRating(value: string): this {
        cy.getByTestId(`risk-rating-${value}`).should("be.checked");

        return this;
    }

    public hasFinanceRiskSummary(value: string): this {
        cy.getByTestId(`finance-risk-summary`).should("have.text", value);

        return this;
    }

    public withFinanceRiskRating(value: string): this {

        cy.getByTestId(`risk-rating-${value}`).check();

        return this;
    }

    public withFinanceSummary(value: string): this {

        cy.getByTestId(`finance-risk-summary`).clear().type(value);

        return this;
    }

    public withFinanceSummaryExceeding(): this {
        cy.getByTestId(`finance-risk-summary`).clear().invoke('text', 'a'.repeat(1001));

        return this;
    }

    public hasRiskAppraisalFormSharePointLink(value: string): this {
        cy.getByTestId("sharepoint-link").should("have.value", value);

        return this;
    }

    public withRiskAppraisalFormSharePointLink(value: string): this {
        cy.getByTestId("sharepoint-link").clear().type(value);

        return this;
    }

    public hasOverallRiskRating(value: string): this {
        cy.getByTestId(`risk-rating-${value}`).should("be.checked");

        return this;
    }

    public hasOverallRiskSummary(value: string): this {
        cy.getByTestId(`overall-risk-summary`).should("have.text", value);

        return this;
    }

    public withOverallRiskRating(value: string): this {

        cy.getByTestId(`risk-rating-${value}`).check();

        return this;
    }

    public withOverallRiskSummary(value: string): this {

        cy.getByTestId(`overall-risk-summary`).clear().type(value);

        return this;
    }

    public withOverallRiskSummaryExceeding(): this {
        cy.getByTestId(`overall-risk-summary`).clear().invoke('text', 'a'.repeat(1001));

        return this;
    }

    public continue(): this {
        cy.getByTestId('continue').click();

        return this;
    }
}

const editProjectRiskPage = new EditProjectRiskPage();

export default editProjectRiskPage;