class PrincipleDesignateEditPage
{
    private errorTracking = "";


    checkYesForPrincipleDesignate(): this {
        cy.getById("trust-appointed-principle-designate-Yes").check()
        return this
    }

    checkNoForPrincipleDesignate(): this {
        cy.getById("trust-appointed-principle-designate-No").check()
        return this
    }

    checkYesForExternalExpert(): this {
        cy.getById("commissioned-external-expert-visit-yes").check()
        return this
    }

    checkNoForExternalExpert(): this {
        cy.getById("commissioned-external-expert-visit-no").check()
        return this
    }
    clickContinue(): this {
        cy.getByTestId("continue").click();
        return this;
    }

    errorForPrincipleDesignateAppointedDate(): this {
        this.errorTracking = "trust-appointed-principle-designate-date";
        return this;
    }

    withPrincipleDesignateAppointedDate(day: string, month: string, year: string): this {
        const key = "trust-appointed-principle-designate-date";
        this.setDate(key, day, month, year);
        return this
    }

    private setDate(key: string, day: string, month: string, year: string) {
        cy.get('#' + `${key}-day`).typeFast(day);
        cy.get('#' + `${key}-month`).typeFast(month);
        cy.get('#' + `${key}-year`).typeFast(year);
    }

    showsError(error: string) {
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
}

const principleDesignateEditPage = new PrincipleDesignateEditPage();
export default principleDesignateEditPage;