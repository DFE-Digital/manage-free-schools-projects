import projectTable from "cypress/pages/projectTable";

describe("Testing the home page", () => {
    beforeEach(() => {
        cy.login();
        cy.visit("/");
    });

    it("Should display the users projects", () => {
        const projectId = "FS0825";

        projectTable.getRowByProjectId(projectId).then((row) => {
            row.hasProjectTitle("Salmon’s Brook Special Free School")
                .hasProjectId(projectId)
                .hasTrustName("Edmonton Academy Trust")
                .hasRegionName("London")
                .hasLocalAuthority("Enfield")
                .hasRealisticOpeningdate("07 March 2019")
                .hasStatus("Not started");
        });

        cy.excuteAccessibilityTests();
    });
});
