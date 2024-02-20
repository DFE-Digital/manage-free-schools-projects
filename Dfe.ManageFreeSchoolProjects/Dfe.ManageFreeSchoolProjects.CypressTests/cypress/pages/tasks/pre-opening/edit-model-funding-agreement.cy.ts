class ModelFundingAgreementEditPage {
    private errorTracking = "";

    private setDate(key: string, day: string, month: string, year: string) {
        cy.get('#' + `${key}-day`).clear().type(day);
        cy.get('#' + `${key}-month`).clear().type(month);
        cy.get('#' + `${key}-year`).clear().type(year);
    }

    errorForAgreedDate(): this {
        this.errorTracking = "date-trust-agrees-with-model-fa";
        return this;
    }

    withAgreedDate(day: string, month: string, year: string): this {
        const key = "date-trust-agrees-with-model-fa";
        this.setDate(key, day, month, year);
        return this
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


const modelFundingAgreementEditPage = new ModelFundingAgreementEditPage();
export default modelFundingAgreementEditPage;
