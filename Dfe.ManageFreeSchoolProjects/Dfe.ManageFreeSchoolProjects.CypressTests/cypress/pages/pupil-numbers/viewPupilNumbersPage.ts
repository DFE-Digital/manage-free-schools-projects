class ViewPupilNumbersPage {

    public hasSchoolName(value: string) {
        cy.getByTestId("school-name").should("contain.text", value);

        return this;
    }

    public hasNurseryCapacity(value: string): this {
        cy.getByTestId("nursery-capacity").should("contain.text", value);

        return this;
    }

    public hasReceptionToYear6Capacity(value: string): this {
        cy.getByTestId("reception-to-year6-capacity").should("contain.text", value);

        return this;
    }

    public hasYear7ToYear11Capacity(value: string): this {
        cy.getByTestId("year7-to-year11-capacity").should("contain.text", value);

        return this;
    }

    public hasYear12ToYear14Capacity(value: string): this {
        cy.getByTestId("year12-to-year14-capacity").should("contain.text", value);

        return this;
    }

    public hasSpecialEducationalNeedsCapacity(value: string): this {
        cy.getByTestId("special-educational-needs-capacity").should("contain.text", value);

        return this;
    }

    public hasAlternativeProvisionCapacity(value: string): this {
        cy.getByTestId("alternative-provision-capacity").should("contain.text", value);

        return this;
    }

    public hasTotalCapacity(value: string): this {
        cy.getByTestId("total-capacity").should("contain.text", value);

        return this;
    }

    public editCapacity(): this {
        cy.getByTestId("edit-capacity").click();

        return this;
    }
}

const viewPupilNumbersPage = new ViewPupilNumbersPage();

export default viewPupilNumbersPage;