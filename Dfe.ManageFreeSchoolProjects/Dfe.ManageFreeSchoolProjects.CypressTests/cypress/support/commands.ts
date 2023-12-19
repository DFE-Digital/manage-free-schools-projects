import "cypress-localstorage-commands";
import "cypress-axe";
import { AuthenticationInterceptor } from "../auth/authenticationInterceptor";
import { Logger } from "../common/logger";

Cypress.Commands.add("getByTestId", (id) => {
    cy.get(`[data-testid="${id}"]`);
});

Cypress.Commands.add("containsByTestId", (id) => {
    cy.get(`[data-testid*="${id}"]`);
});

Cypress.Commands.add("getById", (id) => {
    cy.get(`[id="${id}"]`);
});

Cypress.Commands.add("getByClass", (className) => {
    cy.get(`[class="${className}"]`);
});

Cypress.Commands.add("getByName", (name) => {
    cy.get(`[name="${name}"]`);
});

Cypress.Commands.add("getByRole", (role) => {
    cy.get(`[role="${role}"]`);
});

Cypress.Commands.add("getByLabelFor", (labelFor) => {
    cy.get(`[for="${labelFor}"]`);
})

Cypress.Commands.add("getByRadioOption", (radioText: string) => {
    cy.contains(radioText)
    .invoke('attr', 'for')
    .then((id) => {
        cy.get('#' + id);
    });
})

Cypress.Commands.add("assertChildList", (selector: string, values: string[]) => {
    cy.getByTestId(selector)
        .children()
        .should("have.length", values.length)
        .each((el, i) => {
            expect(el.text()).to.equal(values[i]);
        });
});

Cypress.Commands.add("login", (params) => {
    cy.clearCookies();
    cy.clearLocalStorage();

    // Intercept all browser requests and add our special auth header
    // Means we don't have to use azure to authenticate
    new AuthenticationInterceptor().register(params);

    cy.visit("/");
});

Cypress.Commands.add("executeAccessibilityTests", () => {
    Logger.log("Executing the command");
    const continueOnFail = false;

    // Ensure that the axe dependency is available in the browser
    Logger.log("Inject Axe");
    cy.injectAxe();

    Logger.log("Checking accessibility");
    cy.checkA11y(undefined, {
        // These will be fixed one by one
        rules: {
            region: { enabled: false },
        },
    }, undefined, continueOnFail);

    Logger.log("Command finished");
});
