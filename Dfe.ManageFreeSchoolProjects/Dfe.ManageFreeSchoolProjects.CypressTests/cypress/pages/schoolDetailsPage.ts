import singleProjectCurrentFreeSchoolNamePage from "./singleProjectCurrentFreeSchoolNamePage";

const invalidTRNString = "POTATO";
const invalidTRNStringWithSpaces = "P O T A T O";
const invalidTRNNumbersString = "1234567";
const nonExistentTrustId = "TR09999";

const SQLInjectionAttempt = "' OR 1=1";
const crossSiteScriptingAttempt = "<script>window.alert('Hello World!')</script>";

class SchoolDetailsPage {

    public verifySchoolDetailsElementsVisible(schoolName: string): this {
        cy.getByClass("govuk-back-link").contains("Back");
        cy.getByClass("govuk-caption-l").contains(schoolName);
        cy.getByClass("govuk-heading-xl").contains("Edit school");

        cy.get("label[for='current-free-school-name'").contains("Current free school name");
        cy.getByTestId("current-free-school-name").should("have.value", schoolName);

        cy.get("label[for='school-type'").eq(0).contains("School type");
        
        cy.getByTestId("AlternativeProvision").should("not.be.checked");
        cy.get("label[for='school-type-1']").contains("Alternative provision");

        cy.getByTestId("FurtherEducation").should("not.be.checked");
        cy.get("label[for='school-type-2']").contains("Further education");

        cy.getByTestId("Mainstream").should("not.be.checked");
        cy.get("label[for='school-type-3']").contains("Mainstream");

        cy.getByTestId("Special").should("not.be.checked");
        cy.get("label[for='school-type-4']").contains("Special");

        cy.getByTestId("StudioSchool").should("not.be.checked");
        cy.get("label[for='school-type-5']").contains("Studio school");

        cy.getByTestId("UniversityTechnicalCollege").should("not.be.checked");
        cy.get("label[for='school-type-6']").contains("University technical college");


        cy.get("label[for='school-phase'").contains("School phase");
        
        cy.getByTestId("Primary").should("not.be.checked");
        cy.get("label[for='school-phase-1']").contains("Primary");

        cy.getByTestId("Secondary").should("not.be.checked");
        cy.get("label[for='school-phase-2']").contains("Secondary");

        cy.getByTestId("SixteenToNineteen").should("not.be.checked");
        cy.get("label[for='school-phase-3']").contains("16 to 19");

        cy.getByTestId("AllThrough").should("not.be.checked");
        cy.get("label[for='school-phase-4']").contains("All-through");



        cy.get("label[for='age-range'").contains("Age range");

        cy.get("label[for='age-range-from'").contains("From");
        cy.getByTestId("age-range-from").should('have.value', '');

        cy.get("label[for='age-range-to'").contains("To");
        cy.getByTestId("age-range-to").should('have.value', '');



        cy.get("label[for='gender'").contains("Gender");
        
        cy.getByTestId("BoysOnly").should("not.be.checked");
        cy.get("label[for='gender-1']").contains("Boys only");

        cy.getByTestId("GirlsOnly").should("not.be.checked");
        cy.get("label[for='gender-2']").contains("Girls only");

        cy.getByTestId("Mixed").should("not.be.checked");
        cy.get("label[for='gender-3']").contains("Mixed");




        cy.get("label[for='nursery'").contains("Nursery");
        
        cy.getById("nursery-1").should("not.be.checked");
        cy.get("label[for='nursery-1']").contains("No");

        cy.getById("nursery-2").should("not.be.checked");
        cy.get("label[for='nursery-2']").contains("Yes");



        cy.get("label[for='school-type'").eq(1).contains("Sixth form");
        
        cy.getById("sixth-form-1").should("not.be.checked");
        cy.get("label[for='sixth-form-1']").contains("No");

        cy.getById("sixth-form-2").should("not.be.checked");
        cy.get("label[for='sixth-form-2']").contains("Yes");




        cy.get("label[for='faith-status'").contains("Faith status");
        
        cy.getByTestId("Designation").should("not.be.checked");
        cy.get("label[for='faith-status-1']").contains("None");

        cy.getByTestId("Ethos").should("not.be.checked");
        cy.get("label[for='faith-status-2']").contains("Designation");

        cy.getByTestId("None").should("not.be.checked");
        cy.get("label[for='faith-status-3']").contains("Ethos");

        cy.getByClass("govuk-button").should("be.visible").contains("Save and continue");
        
        return this;
    }

    public clearSchoolNameField(): this {
        cy.getByTestId("current-free-school-name").clear();

        return this;
    }

    public enterSchoolNameField(schoolName: string): this {
        cy.getByTestId("current-free-school-name").type(schoolName);
1
        return this;
    }

    public selectMainstream(): this {
        cy.getByTestId("Mainstream").click();
        cy.getByTestId("Mainstream").should("be.checked");

        return this;
    }

    public selectSecondary(): this {
        cy.getByTestId("Secondary").click();
        cy.getByTestId("Secondary").should("be.checked");

        return this;
    }

    public enterAgeRangeFromEleven(): this {
        cy.getByTestId("age-range-from").type("11");

        return this;
    }

    public enterAgeRangeToSixteen(): this {
        cy.getByTestId("age-range-to").type("16");

        return this;
    }

    public selectGenderMixed(): this {
        cy.getByTestId("Mixed").click();
        cy.getByTestId("Mixed").should("be.checked");

        return this;
    }

    public selectNurseryNo(): this {
        cy.getById("nursery-1").click();
        cy.getById("nursery-1").should("be.checked");

        return this;
    }

    public selectSixthFormYes(): this {
        cy.getById("sixth-form-2").click();
       // cy.getById("sixth-form-2").should("be.selected");

        return this;
    }

    public selectFaithStatusNone(): this {
        cy.getByTestId("None").click();
        cy.getByTestId("None").should("be.selected");

        return this
    }

    public selectFaithDesignation(): this {
        cy.getByTestId("Designation").click();
      //  cy.getByTestId("Designation").should("be.selected");

        return this;
    }

    public selectFaithEthos(): this {
        cy.getByTestId("Ethos").click();
        cy.getByTestId("Ethos").should("be.selected");

        return this;
    }

    public verifyFaithTypeErrorSummaryAndErrorVisible(): this {
        cy.getById("error-summary-title").contains("There is a problem");

        cy.getById("faith-type-error-link").contains("Faith type is required");

        return this;
    }





    public selectSaveAndContinue(): this {
        cy.getByClass("govuk-button").click();
        
        return this;
    }

    public verifyValidationSummaryAndErrorsVisible(): this {
        cy.getById("error-summary-title").contains("There is a problem");

        cy.getById("gender-error-link").contains("The Gender field is required");
        cy.getById("nursery-error-link").contains("The Nursery field is required");
        cy.getById("sixth-form-error-link").contains("The Sixth form field is required");
        cy.getById("school-type-error-link").contains("The School type field is required");
        cy.getById("age-range-to-error-link").contains("The Age range to field is required");
        cy.getById("faith-status-error-link").contains("The Faith status field is required");
        cy.getById("school-phase-error-link").contains("The School phase field is required");
        cy.getById("age-range-from-error-link").contains("The Age range from field is required");
        cy.getById("current-free-school-name-error-link").contains("The Current free school name field is required");



        cy.getById("current-free-school-name-error").contains("The Current free school name field is required");
        
        cy.getById("school-type-error").contains("The School type field is required");

        cy.getById("school-phase-error").contains("The School phase field is required");

        cy.getById("age-range-from-error").contains("The Age range from field is required");

        cy.getById("age-range-to-error").contains("The Age range to field is required");

        cy.getById("gender-error").contains("The Gender field is required");
        
        cy.getById("nursery-error").contains("The Nursery field is required");

        cy.getById("sixth-form-error").contains("The Sixth form field is required");

        cy.getById("faith-status-error").contains("The Faith status field is required");

        return this;
    }

    public verifyValidationMessagesWhenNoDataSet(): this {
        cy.getById("trn-error").should("be.visible").contains("Enter the TRN");

        return this;
    }

    public enterInvalidTRNStringInTRNPage(): this {
        cy.getById("trn").click();
        cy.getById("trn").type(invalidTRNString);

        return this;

    }

    public enterInvalidTRNStringWithSpacesInTRNPage(): this {
        cy.getById("trn").clear();
        cy.getById("trn").click();
        cy.getById("trn").type(invalidTRNStringWithSpaces);

        return this;

    }

    public enterInvalidTRNNumbersStringInTRNPage(): this {
        cy.getById("trn").clear();
        cy.getById("trn").click();
        cy.getById("trn").type(invalidTRNNumbersString);

        return this;

    }

    public enterNonExistentTrustIdInTRNPage(): this {
        cy.getById("trn").clear();
        cy.getById("trn").click();
        cy.getById("trn").type(nonExistentTrustId);

        return this;

    }

    public enterSQLInjectionAttemptInTRNPage(): this {
        cy.getById("trn").clear();
        cy.getById("trn").click();
        cy.getById("trn").type(SQLInjectionAttempt);

        return this;

    }

    public enterCrossSiteScriptingAttemptInTRNPage(): this {
        cy.getById("trn").clear();
        cy.getById("trn").click();
        cy.getById("trn").type(crossSiteScriptingAttempt);

        return this;

    }

    public enterValidTrustId(validTrustId: string): this {
        cy.getById("trn").clear();
        cy.getById("trn").click();
        cy.getById("trn").type(validTrustId);

        return this;
    }


    public verifyValidationMessagesWhenInvalidTRNFormatEntered(): this {
        cy.getById("trn-error").should("be.visible").contains("The TRN must be in the format TRXXXXX");

        return this;
    }

    public verifyValidationMessagesWhenTRNTooLongEntered(): this {
        cy.getById("trn-error").should("be.visible").contains("The TRN (trust reference number) must be 7 characters or less");

        return this;
    }

    public verifyValidationMessagesWhenNonExistentTRNEntered(): this {
        cy.getById("trn-error").should("be.visible").contains("Trust ID not found");

        return this;
    }


   
}

const schoolDetailsPage = new SchoolDetailsPage();

export default schoolDetailsPage;