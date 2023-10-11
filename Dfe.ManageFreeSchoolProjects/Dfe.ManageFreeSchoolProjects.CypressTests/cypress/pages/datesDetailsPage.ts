const invalidDateFormatData = "POTATOES";
const invalidDateDay = "31";
const dateDay = "28";
const dateMonth = "2";
const dateYear = "2025";
class DatesDetailsPage {
    
    
    selectSaveAndContinueButton(): this {
        cy.getByClass("govuk-button").click();
        return this;
    }

    clearTextInControls(): this {
        cy.getById("entry-into-pre-opening-day").clear();

        cy.getById("entry-into-pre-opening-month").clear();

        cy.getById("entry-into-pre-opening-year").clear();
  
        cy.getById("provisional-opening-date-agreed-with-trust-day").clear();

        cy.getById("provisional-opening-date-agreed-with-trust-month").clear();

        cy.getById("provisional-opening-date-agreed-with-trust-year").clear();

        cy.getByTestId("opening-academic-year").clear();

        cy.getByTestId("opening-academic-year-to").clear();

        return this;
    }

    verifyValidationMessagesWhenNoDataSet(): this {
        cy.getById("entry-into-pre-opening-error").should("be.visible").contains("Enter a date for the entry into pre-opening");

        cy.getById("provisional-opening-date-agreed-with-trust-error").should("be.visible").contains("Enter a date for the provisional opening date agreed with trust");

        cy.getById("opening-academic-year-error").should("be.visible").contains("The Opening academic year field is required.");

        cy.getById("opening-academic-year-to-error").should("be.visible").contains("The OpeningAcademicYearTo field is required.");

        return this;
    }

    enterInvalidDateFormatInEditDatesPage(): this {
        cy.getById("entry-into-pre-opening-day").click();
        cy.getById("entry-into-pre-opening-day").type(invalidDateFormatData);

        cy.getById("entry-into-pre-opening-month").click();
        cy.getById("entry-into-pre-opening-month").type(invalidDateFormatData);

        cy.getById("entry-into-pre-opening-year").click();
        cy.getById("entry-into-pre-opening-year").type(invalidDateFormatData);
  
        cy.getById("provisional-opening-date-agreed-with-trust-day").click();
        cy.getById("provisional-opening-date-agreed-with-trust-day").type(invalidDateFormatData);

        cy.getById("provisional-opening-date-agreed-with-trust-month").click();
        cy.getById("provisional-opening-date-agreed-with-trust-month").type(invalidDateFormatData);

        cy.getById("provisional-opening-date-agreed-with-trust-year").click();
        cy.getById("provisional-opening-date-agreed-with-trust-year").type(invalidDateFormatData);

        cy.getByTestId("opening-academic-year").click()
        cy.getByTestId("opening-academic-year").type("2026");

        cy.getByTestId("opening-academic-year-to").click()
        cy.getByTestId("opening-academic-year-to").type("2027");

        return this;

    }

    enterInvalidDateInEditDatesPage(): this {
        cy.getById("entry-into-pre-opening-day").click();
        cy.getById("entry-into-pre-opening-day").type(invalidDateDay);

        cy.getById("entry-into-pre-opening-month").click();
        cy.getById("entry-into-pre-opening-month").type(dateMonth);

        cy.getById("entry-into-pre-opening-year").click();
        cy.getById("entry-into-pre-opening-year").type(dateYear);
  
        cy.getById("provisional-opening-date-agreed-with-trust-day").click();
        cy.getById("provisional-opening-date-agreed-with-trust-day").type(invalidDateDay);

        cy.getById("provisional-opening-date-agreed-with-trust-month").click();
        cy.getById("provisional-opening-date-agreed-with-trust-month").type(dateMonth);

        cy.getById("provisional-opening-date-agreed-with-trust-year").click();
        cy.getById("provisional-opening-date-agreed-with-trust-year").type(dateYear);

        cy.getByTestId("opening-academic-year").click()
        cy.getByTestId("opening-academic-year").type("2023");

        cy.getByTestId("opening-academic-year-to").click()
        cy.getByTestId("opening-academic-year-to").type("2024");

        return this;

    }

    verifyValidationMessagesWhenInvalidDateFormatEntered(): this {
        cy.getById("entry-into-pre-opening-error").should("be.visible").contains("Enter a date in the correct format");

        cy.getById("provisional-opening-date-agreed-with-trust-error").should("be.visible").contains("Enter a date in the correct format")
        return this;
    }

    verifyValidationMessagesWhenInvalidDateEntered(): this {
        cy.getById("entry-into-pre-opening-error").should("be.visible").contains("Day must be between 1 and 28");

        cy.getById("provisional-opening-date-agreed-with-trust-error").should("be.visible").contains("Day must be between 1 and 28");

        return this;
    }

    enterValidDatesInEditDatesPage(): this {
        cy.getById("entry-into-pre-opening-day").click();
        cy.getById("entry-into-pre-opening-day").type(dateDay);

        cy.getById("entry-into-pre-opening-month").click();
        cy.getById("entry-into-pre-opening-month").type(dateMonth);

        cy.getById("entry-into-pre-opening-year").click();
        cy.getById("entry-into-pre-opening-year").type(dateYear);
  
        cy.getById("provisional-opening-date-agreed-with-trust-day").click();
        cy.getById("provisional-opening-date-agreed-with-trust-day").type(dateDay);

        cy.getById("provisional-opening-date-agreed-with-trust-month").click();
        cy.getById("provisional-opening-date-agreed-with-trust-month").type(dateMonth);

        cy.getById("provisional-opening-date-agreed-with-trust-year").click();
        cy.getById("provisional-opening-date-agreed-with-trust-year").type(dateYear);

        cy.getByTestId("opening-academic-year").click()
        cy.getByTestId("opening-academic-year").type("2025");

        cy.getByTestId("opening-academic-year-to").click()
        cy.getByTestId("opening-academic-year-to").type("2026");


        return this;
    }
   
}

const datesDetailsPage = new DatesDetailsPage();

export default datesDetailsPage;