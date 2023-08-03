describe("Testing the index page", () =>
{
    it("Should show a list of projects", () =>
    {
        cy.login();
        cy.visit("/");
    });
});