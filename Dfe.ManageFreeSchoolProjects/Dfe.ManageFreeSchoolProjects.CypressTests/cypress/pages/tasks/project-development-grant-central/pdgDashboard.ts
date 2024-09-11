class PdgDashboard {
    selectChangeTotalGrant() {
        cy.getByTestId("change-total-grant").click();
    }

    selectChangePaymentSchedule() {
        cy.getByTestId("change-payment-schedule").click();
    }
    
    selectChangeTrustLetter() {
        cy.getByTestId("change-trust-letter").click();
    }

    selectChangeGrantLetter() {
        cy.getByTestId("change-grant-letters").click();
    }

    selectChangeWriteOff() {
        cy.getByTestId("change-write-off").click();
    }

    selectChangeStopPayments() {
        cy.getByTestId("change-stop-payments").click();
    }

    selectChangeRefunds() {
        cy.getByTestId("change-refunds").click();
    }

}

const pdgDashboard = new PdgDashboard();
export default pdgDashboard;
