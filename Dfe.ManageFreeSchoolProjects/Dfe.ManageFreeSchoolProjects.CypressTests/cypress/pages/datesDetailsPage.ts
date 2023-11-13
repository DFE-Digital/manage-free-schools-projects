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

        cy.getByName("opening-academic-years-startyear").clear();

        cy.getByName("opening-academic-years-endyear").clear();

        return this;
    }

    verifyValidationMessagesWhenNoDataSet(schoolName: string): this {
        cy.getByClass("govuk-heading-xl").getByClass("govuk-caption-l").contains(schoolName);

        cy.getById("entry-into-pre-opening-error").should("be.visible").contains("Enter a date for the entry into pre-opening");

        cy.getById("provisional-opening-date-agreed-with-trust-error").should("be.visible").contains("Enter a date for the provisional opening date agreed with trust");

        cy.getById("opening-academic-years-error").should("be.visible").contains("Start date should be in the format: 20XX");

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

        cy.getByName("opening-academic-years-startyear").click()
        cy.getByName("opening-academic-years-startyear").type("2026");

        cy.getByName("opening-academic-years-endyear").click()
        cy.getByName("opening-academic-years-endyear").type("2027");

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

        cy.getByName("opening-academic-years-startyear").click()
        cy.getByName("opening-academic-years-startyear").type("2023");

        cy.getByName("opening-academic-years-endyear").click()
        cy.getByName("opening-academic-years-endyear").type("2024");

        return this;

    }

    verifyValidationMessagesWhenInvalidDateFormatEntered(schoolName: string): this {
        cy.getByClass("govuk-heading-xl").getByClass("govuk-caption-l").contains(schoolName);

        cy.getById("entry-into-pre-opening-error").should("be.visible").contains("Enter a date in the correct format");

        cy.getById("provisional-opening-date-agreed-with-trust-error").should("be.visible").contains("Enter a date in the correct format")
        return this;
    }

    verifyValidationMessagesWhenInvalidDateEntered(schoolName: string): this {
        cy.getByClass("govuk-heading-xl").getByClass("govuk-caption-l").contains(schoolName);
        cy.getById("entry-into-pre-opening-error").should("be.visible").contains("Day must be between 1 and 28");

        cy.getById("provisional-opening-date-agreed-with-trust-error").should("be.visible").contains("Day must be between 1 and 28");

        return this;
    }

    enterInvalidAcademicYearStartDateInEditDatesPage(): this {
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

        cy.getByName("opening-academic-years-startyear").click()
        cy.getByName("opening-academic-years-startyear").type("POTATO");

        cy.getByName("opening-academic-years-endyear").click()
        cy.getByName("opening-academic-years-endyear").type("2026");


        return this;
    }

    enterInvalidAcademicYearEndDateInEditDatesPage(): this {
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

        cy.getByName("opening-academic-years-startyear").click()
        cy.getByName("opening-academic-years-startyear").type("2025");

        cy.getByName("opening-academic-years-endyear").click()
        cy.getByName("opening-academic-years-endyear").type("POTATO");


        return this;
    }

    verifyInvalidAcademicStartYearDate(): this {
        cy.getById('opening-academic-years-error').contains('Start date should be in the format: 20XX') 
        return this
    }

    verifyInvalidAcademicEndYearDate(): this {
        cy.getById('opening-academic-years-error').contains('End date should be in the format: 20XX') 
        return this
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

        cy.getByName("opening-academic-years-startyear").click()
        cy.getByName("opening-academic-years-startyear").type("2025");

        cy.getByName("opening-academic-years-endyear").click()
        cy.getByName("opening-academic-years-endyear").type("2026");


        return this;
    }
   
}

const datesDetailsPage = new DatesDetailsPage();

export default datesDetailsPage;