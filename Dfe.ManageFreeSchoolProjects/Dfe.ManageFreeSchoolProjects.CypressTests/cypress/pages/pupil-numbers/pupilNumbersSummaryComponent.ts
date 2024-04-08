class PupilNumbersSummaryComponent {
    public viewDetails(): this {
        cy.getByTestId("change-pupil-numbers").click();

        return this;
    }
}

const pupilNumbersSummaryComponent = new PupilNumbersSummaryComponent();

export default pupilNumbersSummaryComponent;