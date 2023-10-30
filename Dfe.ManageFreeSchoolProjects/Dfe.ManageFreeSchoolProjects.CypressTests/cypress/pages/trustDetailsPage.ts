const invalidTRNString = "POTATO";
const invalidTRNStringWithSpaces = "P O T A T O";
const invalidTRNNumbersString = "1234567";
const nonExistentTrustId = 'TR09999';
const validTrustId = 'TR03446';
class TrustDetailsPage {
    
    
    selectSaveAndContinueButton(): this {
        cy.getByClass("govuk-button").click();
        return this;
    }

    verifyValidationMessagesWhenNoDataSet(): this {
        cy.getById("trn-error").should("be.visible").contains("Enter the TRN");

        return this;
    }

    enterInvalidTRNStringInTRNPage(): this {
        cy.getById("trn").click();
        cy.getById("trn").type(invalidTRNString);

        return this;

    }

    enterInvalidTRNStringWithSpacesInTRNPage(): this {
        cy.getById("trn").clear();
        cy.getById("trn").click();
        cy.getById("trn").type(invalidTRNStringWithSpaces);

        return this;

    }

    enterInvalidTRNNumbersStringInTRNPage(): this {
        cy.getById("trn").clear();
        cy.getById("trn").click();
        cy.getById("trn").type(invalidTRNNumbersString);

        return this;

    }

    enterNonExistentTrustIdInTRNPage(): this {
        cy.getById("trn").clear();
        cy.getById("trn").click();
        cy.getById("trn").type(nonExistentTrustId);

        return this;

    }


    verifyValidationMessagesWhenInvalidTRNFormatEntered(): this {
        cy.getById("trn-error").should("be.visible").contains("The TRN must be in the format TRXXXXX");

        return this;
    }

    verifyValidationMessagesWhenTRNTooLongEntered(): this {
        cy.getById("trn-error").should("be.visible").contains("The TRN (trust reference number) must be 7 characters or less");

        return this;
    }

    verifyValidationMessagesWhenNonExistentTRNEntered(): this {
        cy.getById("trn-error").should("be.visible").contains("Trust ID not found");

        return this;
    }


   
}

const trustDetailsPage = new TrustDetailsPage();

export default trustDetailsPage;