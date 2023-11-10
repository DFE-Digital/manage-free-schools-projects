class EditProjectRiskPage {
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

    public withRiskAppraisalFormSharePointLink(value: string): this {
        cy.getByTestId("sharepoint-link").clear().type(value);

        return this;
    }

    public withOverallRiskRating(value: string): this {

        cy.getByTestId(`risk-rating-${value}`).check();

        return this;
    }

    public withOverallSummary(value: string): this {

        cy.getByTestId(`overall-risk-summary`).clear().type(value);

        return this;
    }

    public withOverallSummaryExceeding(): this {
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