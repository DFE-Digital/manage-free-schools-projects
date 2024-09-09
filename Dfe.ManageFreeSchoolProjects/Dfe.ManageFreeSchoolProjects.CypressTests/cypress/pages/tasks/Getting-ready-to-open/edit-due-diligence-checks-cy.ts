class DueDiligenceChecksEditPage { 
    private errorTracking = "";

    checkReceivedChairOfTrusteesDbsCountersignedCertificate(): this { 
        cy.getById("received-chair-of-trustees-countersigned-certificate").check()
        return this
    }

    checkNonSpecialistChecksDoneOnAllTrustMembersAndTrusteesInLast2Years() : this { 
        cy.getById("non-specialist-checks-done-on-all-trust-members-and-trustees").check()
        return this
    }


    requestedCounterExtremismChecks(option : "Yes" | "No") : this { 
        cy.getById(`requested-counter-extremism-checks-${option.toLowerCase()}`).click()
        return this
    }

    clickContinue() : this {
        cy.getByTestId("continue").click();
        return this;
    }
}

const dueDiligenceChecksEditPage = new DueDiligenceChecksEditPage();
export default dueDiligenceChecksEditPage;