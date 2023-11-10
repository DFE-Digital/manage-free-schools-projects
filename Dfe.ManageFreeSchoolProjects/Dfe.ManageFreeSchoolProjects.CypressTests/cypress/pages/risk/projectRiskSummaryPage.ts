class ProjectRiskSummaryPage {
    public hasOverallRiskRating(values: string[]): this {

        this.checkRagRating(`overall-risk-rating`, values);

        return this;
    }

    public hasOverallRiskSummary(value: string): this {

        cy.getByTestId(`overall-risk-summary`).should(`contain.text`, value);

        return this;
    }

    public changeOverallRisk(): this {
        cy.getByTestId("change-governance-and-suitability-risk").click();

        return this;
    }

    public hasGovernanceAndSuitabilityRiskRating(values: string[]): this {

        this.checkRagRating(`governance-and-suitability-risk-rating`, values);

        return this;
    }

    public hasGovernanceAndSuitabilityRiskSummary(value: string): this {

        cy.getByTestId(`governance-and-suitability-risk-summary`).should(`contain.text`, value);

        return this;
    }

    public hasEducationRiskRating(values: string[]): this {

        this.checkRagRating(`education-risk-rating`, values);

        return this;
    }

    public hasEducationRiskSummary(value: string): this {

        cy.getByTestId(`education-risk-summary`).should(`contain.text`, value);

        return this;
    }

    public hasFinanceRiskRating(values: string[]): this {

        this.checkRagRating(`finance-risk-rating`, values);

        return this;
    }

    public hasFinanceRiskSummary(value: string): this {

        cy.getByTestId(`finance-risk-summary`).should(`contain.text`, value);

        return this;
    }

    public hasRiskAppraisalFormSharePointLink(value: string): this {

        cy.getByTestId(`risk-appraisal-form-sharepoint-link`).should(`contain.text`, value);

        return this;
    }

    public addRiskEntry(): this {
        cy.getByTestId("add-risk-entry").click();

        return this;
    }

    public createRiskEntry(): this {
        cy.getByTestId("create-risk-entry").click();

        return this;
    }

    private checkRagRating(selector: string, values: string[]): void {
        cy.getByTestId(selector)
            .children()
            .should("have.length", values.length)
            .each((el, i) => {
                expect(el.text()).to.equal(values[i]);
            });
    }
}

const projectRiskSummaryPage = new ProjectRiskSummaryPage();

export default projectRiskSummaryPage;