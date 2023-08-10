class ValidationComponent {
    public hasValidationError(message: string): this {
        cy.task("log", `Has Validation error ${message}`);

        cy.getByTestId("error-summary").should("contain.text", message);

        return this;
    }
}

const validationComponent = new ValidationComponent();
export default validationComponent;
