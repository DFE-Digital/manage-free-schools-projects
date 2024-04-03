class SchoolDetailsPage {

    public titleIs(title: string): this {
        cy.getByTestId("title").should("contains.text", title)
        return this;
    }

    public schoolNameIs(school: string) {
        cy.getByTestId("school-name").should("contains.text", school);
        return this;
    }

    public withSchoolName(school: string): this {
        cy.getByTestId("current-free-school-name").clear().type(school);
        return this;
    }

    public withSchoolNameExceedingMaxLength(): this {
        cy.getByTestId("current-free-school-name").invoke("val", "a".repeat(101));
        return this;
    }

    public withSchoolType(value: string): this {
        cy.getByTestId(value).check();
        return this;
    }

    public withSchoolPhase(value: string): this {
        cy.getByTestId(value).check();
        return this;
    }

    public withAgeRange(from: string, to: string): this {
        cy.getByTestId("age-range-from").clear().type(from);
        cy.getByTestId("age-range-to").clear().type(to);
        return this;
    }

    public withFormsOfEntry(value: string): this {
        cy.getByTestId("forms-of-entry").clear().type(value);
        return this;
    }

    public withFormsOfEntryExceedingLimit(): this {
        cy.getByTestId("forms-of-entry").invoke("val", "a".repeat(101));
        return this;
    }

    public withGender(value: string): this {
        cy.getByTestId(value).check();
        return this;
    }

    public withNursery(value: string): this {
        cy.getByTestId(`nursery-${value}`).check();
        return this;
    }

    public withSixthForm(value: string): this {
        cy.getByTestId(`sixth-form-${value}`).check();
        return this;
    }

    public withAlternativeProvision(value: string): this {
        cy.getByTestId(`alternative-provision-${value}`).check();
        return this;
    }

    public withSpecialEducationNeeds(value: string): this {
        cy.getByTestId(`special-education-needs-${value}`).check();
        return this;
    }

    public withFaithStatus(value: string): this {
        cy.getByTestId(value).check();
        return this;
    }

    public withFaithType(value: string): this {
        this.getFaithTypeSection();
        cy.getByTestId(value).check();
        return this;
    }

    public faithTypeSectionIsNotVisible(): this {
        this.getFaithTypeSection().should("not.be.visible");
        return this;
    }

    public withFaithTypeOtherDescription(value: string): this {
        this.getFaithTypeOtherDescription().clear().type(value);
        return this;
    }

    public withFaithTypeOtherDescriptionExceedingMaxLength(): this {
        this.getFaithTypeOtherDescription().invoke("val", "a".repeat(101));
        return this;
    }

    public faithTypeOtherDescriptionIsNotVisible() {
        this.getFaithTypeOtherDescription().should("not.be.visible");
        return this;
    }

    public clickContinue(): this {
        cy.getByTestId("continue").click();
        return this;
    }

    private getFaithTypeOtherDescription() {
        return cy.getByTestId("other-faith-type");
    }

    private getFaithTypeSection() {
        return cy.getById("faith-type-group");
    }
}

const schoolDetailsPage = new SchoolDetailsPage();

export default schoolDetailsPage;