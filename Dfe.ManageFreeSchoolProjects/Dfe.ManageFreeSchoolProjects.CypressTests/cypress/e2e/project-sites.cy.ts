import { ProjectDetailsRequest } from "cypress/api/domain";
import projectApi from "cypress/api/projectApi";
import { RequestBuilder } from "cypress/api/requestBuilder";
import { Logger } from "cypress/common/logger";
import projectOverviewPage from "cypress/pages/projectOverviewPage";
import viewSiteInformationPage from "cypress/pages/siteInformation/viewSiteInformationPage";

describe("Testing the setting up of project sites - presumption route", () => {
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

    it("Should be able to open the project sites", () => {
        Logger.log("Should be able to see all field names");
        projectOverviewPage
            .selectSiteInformationTab();
        
        viewSiteInformationPage
            .hasProjectTitleHeader(project.schoolName)
            .hasProjectStatus("Pre-opening")
            .hasTemporaryAddress()
            .hasTemporaryPostcode()
            .hasTemporarySiteRiskRating()
            .hasTemporaryPlanningDecision()
            .hasTemporaryKeyDatesHeading()
            .hasTemporaryKeyDates()
            .hasTemporaryForecastDates()
            .hasTemporaryActualDates()
            .hasTemporaryHeadsOfTermsAgreed()
            .hasTemporaryContractorAppointed()
            .hasTemporaryPlanningDecisionDate()
            .hasTemporaryAccomodationFirstReadyForOccupation()

            .hasPermanentAddress()
            .hasPermanentPostcode()
            .hasPermanentSiteRiskRating()
            .hasPermanentPlanningDecision()
            .hasPermanentKeyDatesHeading()
            .hasPermanentKeyDates()
            .hasPermanentForecastDates()
            .hasPermanentActualDates()
            .hasPermanentHeadsOfTermsAgreed()
            .hasPermanentContractorAppointed()
            .hasPermanentPlanningDecisionDate()
            .hasPermanentAccomodationFirstReadyForOccupation()

            .hasCapitalRating()
            .hasRiskSummary();

        cy.executeAccessibilityTests();
    });

});

describe("Testing the setting up of project sites - central route", () => {
    let project: ProjectDetailsRequest;
    beforeEach(() => {
        cy.login();

        project = RequestBuilder.createProjectDetailsCentralRoute();

        projectApi
            .post({
                projects: [project],
            })
            .then(() => {
                cy.visit(`/projects/${project.projectId}/site-information`);
            });
    });

    it("Should be able to view site information for a central route project", () => {
        Logger.log("Should be able to see all field names");
        viewSiteInformationPage
            .hasProjectTitleHeader(project.schoolName)
            .hasProjectStatus("Pre-opening")
            .hasTemporaryAddress()
            .hasTemporaryPostcode()
            .hasTemporarySiteRiskRating()
            .hasTemporaryPlanningDecision()
            .hasTemporaryKeyDatesHeading()
            .hasTemporaryKeyDates()
            .hasTemporaryForecastDates()
            .hasTemporaryActualDates()
            .hasTemporaryHeadsOfTermsAgreed()
            .hasTemporaryContractorAppointed()
            .hasTemporaryPlanningDecisionDate()
            .hasTemporaryAccomodationFirstReadyForOccupation()

            .hasPermanentAddress()
            .hasPermanentPostcode()
            .hasPermanentSiteRiskRating()
            .hasPermanentPlanningDecision()
            .hasPermanentKeyDatesHeading()
            .hasPermanentKeyDates()
            .hasPermanentForecastDates()
            .hasPermanentActualDates()
            .hasPermanentHeadsOfTermsAgreed()
            .hasPermanentContractorAppointed()
            .hasPermanentPlanningDecisionDate()
            .hasPermanentAccomodationFirstReadyForOccupation()

            .hasCapitalRating()
            .hasRiskSummary();
        
        cy.executeAccessibilityTests();

    });

});