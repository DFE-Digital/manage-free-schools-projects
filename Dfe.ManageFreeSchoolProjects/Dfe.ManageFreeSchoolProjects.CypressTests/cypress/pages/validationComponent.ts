class ValidationComponent {
    public hasValidationError(message: string): this {
        cy.getByTestId("error-summary").should("contain.text", message);

        return this;
    }
}

const validationComponent = new ValidationComponent();
export default validationComponent;
