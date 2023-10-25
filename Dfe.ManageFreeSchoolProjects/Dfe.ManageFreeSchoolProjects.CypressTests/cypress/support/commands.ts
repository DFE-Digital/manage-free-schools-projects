import "cypress-localstorage-commands";
import "cypress-axe";
import { AuthenticationInterceptor } from "../auth/authenticationInterceptor";
import { Logger } from "../common/logger";
import { RequestBuilder } from "cypress/api/requestBuilder";
import projectApi from "cypress/api/projectApi";
import { CreateProjectRequest } from "cypress/api/domain";

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

Cypress.Commands.add("login", (params) => {
    cy.clearCookies();
    cy.clearLocalStorage();

    // Intercept all browser requests and add our special auth header
    // Means we don't have to use azure to authenticate
    new AuthenticationInterceptor().register(params);

    cy.visit("/");
});

Cypress.Commands.add("excuteAccessibilityTests", () => {
    Logger.log("Executing the command");
    const continueOnFail = false;

    // Ensure that the axe dependency is available in the browser
    Logger.log("Inject Axe");
    cy.injectAxe();

    Logger.log("Checking accessibility");
    cy.checkA11y(undefined, {}, undefined, continueOnFail);

    Logger.log("Command finished");
});
