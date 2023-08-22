import { Logger } from "cypress/common/logger";
import projectOverviewPage from "cypress/pages/projectOverviewPage";

describe("Testing project overview", () => {
    beforeEach(() => {
        cy.login();

        cy.visit("/projects/FS0297/overview");
    });

    it("Should display the configured information", () => {
        Logger.log("Project status");
        projectOverviewPage
            .hasCurrentFreeSchoolName("The Aspire Academy")
            .hasProjectStatus("Open")
            .hasApplicationNumber("AP009W4")
            .hasProjectId("FS0297")
            .hasUrn("141034")
            .hasApplicationWave("FS Wave 4")
            .hasRealisticYearOfOpening("2014/15")
            .hasDateOfEntryIntoPreopening("22/05/2013 00:00:00")
            .hasProvisionalOpeningDateAgreedWithTrust("01/09/2014 00:00:00")
            .hasActualOpeningDate("01/09/2014 00:00:00")
            .hasOpeningAcademicYear("Empty");

        Logger.log("School details");
        projectOverviewPage
            .hasLocalAuthority("Worcestershire")
            .hasRegion("West Midlands")
            .hasConstituency("Worcester")
            .hasNumberOfFormsOfEntry("Empty")
            .hasSchoolType("FS - AP")
            .hasSchoolPhase("Secondary")
            .hasAgeRange("11-16")
            .hasGender("Mixed")
            .hasNursery("No")
            .hasSixthForm("No")
            .hasIndependentConverter("No")
            .hasSpecialistResourceProvision("Empty")
            .hasFaithStatus("None")
            .hasFaithType("None")
            .hasTrustId("2946")
            .hasTrustName("Empty")
            .hasTrustType("Empty");
    });
});
