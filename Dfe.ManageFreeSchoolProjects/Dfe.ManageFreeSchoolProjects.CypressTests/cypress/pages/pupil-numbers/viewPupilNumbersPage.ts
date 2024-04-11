class ViewPupilNumbersPage {

    public hasSchoolName(value: string) {
        cy.getByTestId("school-name").should("contain.text", value);

        return this;
    }

    public editCapacity(): this {
        cy.getByTestId("edit-capacity").click();

        return this;
    }

    public editPre16PublishedAdmissionNumber(): this {
        cy.getByTestId("edit-pre16-pan").click();

        return this;

    }

    public editPost16PublishedAdmissionNumber(): this {
        cy.getByTestId("edit-post16-pan").click();

        return this;

    }

    public editRecruitmentAndViability(): this {
        cy.getByTestId("edit-recruitment-and-viability").click();

        return this;
    }

    public editPre16CapacityBuildup(): this {
        cy.getByTestId("edit-pre16-capacity-buildup").click();

        return this;
    }
}

const viewPupilNumbersPage = new ViewPupilNumbersPage();

export default viewPupilNumbersPage;