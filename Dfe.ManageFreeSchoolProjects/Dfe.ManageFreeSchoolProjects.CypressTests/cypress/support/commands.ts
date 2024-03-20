import "cypress-localstorage-commands";
import "cypress-axe";
import { AuthenticationInterceptor } from "../auth/authenticationInterceptor";
import { Logger } from "../common/logger";
import { RuleObject } from "axe-core";

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

Cypress.Commands.add("executeAccessibilityTests", (ruleOverride?: RuleObject) => {
    Logger.log("Executing the command");
    const continueOnFail = false;

    let ruleConfiguration: RuleObject = {
        region: { enabled: false }
    };

    if (ruleOverride) {
        ruleConfiguration = { ...ruleConfiguration, ...ruleOverride };
    }

    // Ensure that the axe dependency is available in the browser
    Logger.log("Inject Axe");
    cy.injectAxe();

    Logger.log("Checking accessibility");
    cy.checkA11y(undefined, {
        rules: ruleConfiguration,
    }, undefined, continueOnFail);
});

Cypress.Commands.add("enterDate", (idPrefix: string, day: string, month: string, year: string) => {
    cy.getById(`${idPrefix}-day`).clear();
    cy.getById(`${idPrefix}-month`).clear();
    cy.getById(`${idPrefix}-year`).clear();

    if (day.length > 0) {
        cy.getById(`${idPrefix}-day`).type(day);
    }

    if (month.length > 0) {
        cy.getById(`${idPrefix}-month`).type(month);
    }

    if (year.length > 0) {
        cy.getById(`${idPrefix}-year`).type(year);
    }
});

Cypress.Commands.add("hasAddress", (id: string, line1: string, line2: string, city: string) => {
    if (line1 === "Empty") {
        cy.getByTestId(id).should("contain.text", "Empty");

        return;
    }

    cy.getByTestId(id).find("[data-testid='address-line1']").should("contain.text", line1);
    cy.getByTestId(id).find("[data-testid='address-line2']").should("contain.text", line2);
    cy.getByTestId(id).find("[data-testid='address-line3']").should("contain.text", city);
});
