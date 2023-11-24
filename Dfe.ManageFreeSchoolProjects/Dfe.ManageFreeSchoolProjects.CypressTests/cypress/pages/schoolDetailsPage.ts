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

        cy.getByTestId("Mainstream").should("not.be.checked");
        cy.get("label[for='school-type-2']").contains("Mainstream");

        cy.getByTestId("Special").should("not.be.checked");
        cy.get("label[for='school-type-3']").contains("Special");

        cy.getByTestId("StudioSchool").should("not.be.checked");
        cy.get("label[for='school-type-4']").contains("Studio school");

        cy.getByTestId("UniversityTechnicalCollege").should("not.be.checked");
        cy.get("label[for='school-type-5']").contains("University technical college");


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

    public enterValidSpecialCharsSchoolNameField(schoolWithAllValidSpecialChars :string): this {
        cy.getByTestId("current-free-school-name").type(schoolWithAllValidSpecialChars);
1
        return this;
    }

    public enterNegTestAllNumbersSchoolNameField(): this {
        cy.getByTestId("current-free-school-name").type("0123456789");
1
        return this;
    }

    public enterNegTestAllInvalidSpecialCharsSchoolNameField(): this {
        cy.getByTestId("current-free-school-name").type("!\"£$%^&-+=[]{}:;@~#?/|.*" + "\\<>");
1
        return this;
    }

    public verifyNegTestAllNumbersOrAllInvalidSpecialCharsErrorSummaryAndError(): this {
        cy.getByClass("govuk-error-summary").contains("There is a problem");
        cy.getByClass("govuk-error-summary").contains("School name must not include special characters other than , ( ) '");

        cy.getByClass("govuk-error-message").contains("School name must not include special characters other than , ( ) '");

        return this;
    }

    public enterNegTestMixOfLettersAndInvalidSpecialCharsSchoolNameField(): this {
        cy.getByTestId("current-free-school-name").type("St Dunstan's Abbey, (Plymouth) !\"£$%^&-+=[]{}:;@~#?/|.*" + "\\<>");
1
        return this;
    }

    public verifyNegTestMixOfLetterAndInvalidSpecialCharsErrorSummaryAndError(): this {
        cy.getByClass("govuk-error-summary").contains("There is a problem");
        cy.getByClass("govuk-error-summary").contains("School name must not include special characters other than , ( ) '");

        cy.getByClass("govuk-error-message").contains("School name must not include special characters other than , ( ) '");

        return this;
    }


    public enterNegTestSQLInjectionAttemptSchoolNameField(): this {
        cy.getByTestId("current-free-school-name").type("' OR 1=1");
1
        return this;
    }

    public enterNegTestCrossSiteScriptAttemptSchoolNameField(): this {
        cy.getByTestId("current-free-school-name").type("<script>window.alert('Hello World')</script>");

        return this;
    }

    public enterNegTestMoreThanOneHundredCharsSchoolNameField(): this {
        cy.getByTestId("current-free-school-name").type("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvw");

        return this;
    }

    public verifyNegTestMoreThanOneHundredCharsSchoolName(): this {
        cy.getByClass("govuk-error-summary").contains("There is a problem");
        cy.getByClass("govuk-error-summary").contains("The school name must be 100 characters or less");

        cy.getByClass("govuk-error-message").contains("The school name must be 100 characters or less");

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

        return this;
    }

    public selectSixthFormYes(): this {
        cy.getById("sixth-form-2").click();

        return this;
    }

    public selectFaithStatusNone(): this {
        cy.getByTestId("None").click();

        return this
    }

    public selectFaithDesignation(): this {
        cy.getByTestId("Designation").click();

        return this;
    }

    public selectFaithEthos(): this {
        cy.getByTestId("Ethos").click();

        return this;
    }

    public selectFaithTypeGreekOrthodox(): this {
        cy.getByTestId("GreekOrthodox").click();

        return this;
    }

    public selectFaithTypeOther(): this {
        cy.getByTestId("Other").click();

        return this;
    }

    public clearOtherFaithTypeField(): this {
        cy.getByTestId("other-faith-type").clear();

        return this;
    }

    public enterOtherFaithType(): this {
        cy.getByTestId("other-faith-type").type("Jane");

        return this;
    }


    public enterNegTestAllNumbersOtherFaithType(): this {
        cy.getByTestId("other-faith-type").type("0123456789");

        return this;
    }

    public enterNegTestAllSpecialCharsOtherFaithType(): this {
        cy.getByTestId("other-faith-type").type("!@£$%^&*()',-_+=\"?/\\|`~±§");

        return this;
    }

    public enterNegTestSomeValidSomeSpecialCharsOtherFaithType(): this {
        cy.getByTestId("other-faith-type").type(",()' Catholic Jewish Muslim Hindu Buddhist Jane");

        return this;
    }

    public enterNegTestSQLInjectionAttemptOtherFaithType(): this {
        cy.getByTestId("other-faith-type").type("' OR 1=1");

        return this;
    }

    public enterNegTestCrossSiteScriptAttackAttemptOtherFaithType(): this {
        cy.getByTestId("other-faith-type").type("<script>window.alert('Hello')</script>");

        return this;
    }

    public enterNegTestMoreThanOneHundredCharsAttemptOtherFaithType(): this {
        cy.getByTestId("other-faith-type").type("ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrst");

        return this;
    }

    public verifyMoreThanOneHundredCharsOtherFaithTypeErrorSummaryAndErrorVisible(): this {
        cy.getById("error-summary-title").contains("There is a problem");

        cy.getById("other-faith-type-error-link").contains("Other faith type must be 100 characters or less");
        cy.getById("other-faith-type-error").contains("Other faith type must be 100 characters or less");

        return this;
    }

    public verifyAllNumbersOrSpecialCharsOtherFaithTypeErrorSummaryAndErrorVisible(): this {
        cy.getById("error-summary-title").contains("There is a problem");

        cy.getById("other-faith-type-error-link").contains("Other faith type must only contain letters and spaces");
        cy.getById("other-faith-type-error").contains("Other faith type must only contain letters and spaces");

        return this;
    }



    public verifyFaithTypeErrorSummaryAndErrorVisible(): this {
        cy.getById("error-summary-title").contains("There is a problem");

        cy.getById("faith-type-error-link").contains("Faith type is required");
        cy.getById("faith-type-error").contains("Faith type is required");

        return this;
    }

    public verifyOtherFaithTypeErrorSummaryAndErrorVisible(): this {
        cy.getById("error-summary-title").contains("There is a problem");

        cy.getById("other-faith-type-error-link").contains("Other faith type is required");
        cy.getById("other-faith-type-error").contains("Other faith type is required");

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
}

const schoolDetailsPage = new SchoolDetailsPage();

export default schoolDetailsPage;