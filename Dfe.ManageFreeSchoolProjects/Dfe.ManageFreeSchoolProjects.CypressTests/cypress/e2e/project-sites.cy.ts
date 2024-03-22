import { ProjectDetailsRequest } from "cypress/api/domain";
import projectApi from "cypress/api/projectApi";
import { RequestBuilder } from "cypress/api/requestBuilder";
import { Logger } from "cypress/common/logger";
import projectOverviewPage from "cypress/pages/projectOverviewPage";
import editSiteInformationPage from "cypress/pages/siteInformation/editSiteInformationPage";
import viewSiteInformationPage from "cypress/pages/siteInformation/viewSiteInformationPage";
import validationComponent from "cypress/pages/validationComponent";

describe("Testing the setting up of project sites", () => {
    let project: ProjectDetailsRequest;

    beforeEach(() => {
        cy.login();

        project = RequestBuilder.createProjectDetails();

        projectApi
            .post({
                projects: [project],
            })
            .then(() => {
                cy.visit(`/projects/${project.projectId}/overview`);
            });
    });

    it("Should be able to configure the project sites", () => {
        Logger.log("When there are no project sites should display empty");
        projectOverviewPage
            .hasTemporarySiteAddress("Empty", "", "")
            .hasTemporarySitePostcode("Empty")
            .hasPermanentSiteAddress("Empty", "", "")
            .hasPermanentSitePostcode("Empty");

        projectOverviewPage.changeSiteInformation();

        Logger.log("Test we can submit a blank site");

        viewSiteInformationPage.changePermanentSite();

        editSiteInformationPage.saveAndContinue();

        viewSiteInformationPage
            .hasSchoolName(project.schoolName)
            .hasTemporarySiteAddress("Empty", "", "")
            .hasTemporarySitePostcode("Empty")
            .hasTemporarySiteStartDateOfOccupation("Empty")
            .hasPermanentSiteAddress("Empty", "", "")
            .hasPermanentSitePostcode("Empty")
            .hasPermanentSiteStartDateOfOccupation("Empty");

        cy.executeAccessibilityTests();

        Logger.log("Add sites to the project");
        viewSiteInformationPage
            .changePermanentSite();

        editSiteInformationPage
            .withFieldsExceedingMaxLength()
            .withStartDateOfSiteOccupation("02", "", "")
            .saveAndContinue();

        validationComponent
            .hasValidationError("The Postcode must be 10 characters or less")
            .hasValidationError("The Address line 1 must be 100 characters or less")
            .hasValidationError("The Town or city must be 100 characters or less")
            .hasValidationError("The Address line 2 must be 300 characters or less")
            .hasValidationError("Start date of site occupation must include a month and year");

        cy.executeAccessibilityTests();

        editSiteInformationPage
            .hasSchoolName(project.schoolName)
            .withAddressLine1("Permanent test house")
            .withAddressLine2("Permanent test street")
            .withTownOrCity("Permanent test town")
            .withPostcode("TE1 1ST")
            .withStartDateOfSiteOccupation("01", "05", "2025")
            .saveAndContinue();

        viewSiteInformationPage
            .changeTemporarySite();

        editSiteInformationPage
            .withAddressLine1("Temporary test house")
            .withAddressLine2("Temporary test street")
            .withTownOrCity("Temporary test town")
            .withPostcode("TE1 2ND")
            .withStartDateOfSiteOccupation("22", "10", "2025")
            .saveAndContinue();

        viewSiteInformationPage
            .hasPermanentSiteAddress("Permanent test house", "Permanent test street", "Permanent test town")
            .hasPermanentSitePostcode("TE1 1ST")
            .hasPermanentSiteStartDateOfOccupation("1 May 2025")
            .hasTemporarySiteAddress("Temporary test house", "Temporary test street", "Temporary test town")
            .hasTemporarySiteStartDateOfOccupation("22 October 2025")
            .hasTemporarySitePostcode("TE1 2ND");

        Logger.log("Change sites on the project");

        viewSiteInformationPage
            .changePermanentSite();

        editSiteInformationPage
            .withAddressLine1("Alternative permanent site")
            .withAddressLine2("Alternative permanent street")
            .withTownOrCity("Alternative permanent town")
            .withPostcode("TE1 4TH")
            .withStartDateOfSiteOccupation("15", "06", "2026")
            .saveAndContinue();

        viewSiteInformationPage
            .changeTemporarySite();

        editSiteInformationPage
            .withAddressLine1("Alternative temporary site")
            .withAddressLine2("Alternative temporary street")
            .withTownOrCity("Alternative temporary town")
            .withPostcode("TE1 3RD")
            .withStartDateOfSiteOccupation("12", "12", "2026")
            .saveAndContinue();

        viewSiteInformationPage
            .hasPermanentSiteAddress("Alternative permanent site", "Alternative permanent street", "Alternative permanent town")
            .hasPermanentSitePostcode("TE1 4TH")
            .hasPermanentSiteStartDateOfOccupation("15 June 2026")
            .hasTemporarySiteAddress("Alternative temporary site", "Alternative temporary street", "Alternative temporary town")
            .hasTemporarySitePostcode("TE1 3RD")
            .hasTemporarySiteStartDateOfOccupation("12 December 2026");

        Logger.log("Check the overview reflects the changes");
        viewSiteInformationPage.backToProject();

        projectOverviewPage
            .hasPermanentSiteAddress("Alternative permanent site", "Alternative permanent street", "Alternative permanent town")
            .hasPermanentSitePostcode("TE1 4TH")
            .hasTemporarySiteAddress("Alternative temporary site", "Alternative temporary street", "Alternative temporary town")
            .hasTemporarySitePostcode("TE1 3RD");
    });
});