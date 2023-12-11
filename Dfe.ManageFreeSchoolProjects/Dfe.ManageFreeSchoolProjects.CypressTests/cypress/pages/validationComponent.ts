class ValidationComponent {
    public hasValidationError(message: string): this {
        cy.getByTestId("error-summary").should("contain.text", message);

        return this;
    }

    public hasLinkedValidationError(): this {
        cy.getByTestId("error-summary-link")
          .find("a")
          .invoke('attr', "href")
          .then((href: string | undefined) => {
            cy.get(href as string).should('exist');
        });

        return this;
    }
}

const validationComponent = new ValidationComponent();
export default validationComponent;
