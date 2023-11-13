import { ProjectDetailsRequest } from "cypress/api/domain";
import projectApi from "cypress/api/projectApi";
import { RequestBuilder } from "cypress/api/requestBuilder";
import { Logger } from "cypress/common/logger";
import editProjectRiskPage from "cypress/pages/risk/editProjectRiskPage";
import projectOverviewPage from "cypress/pages/projectOverviewPage";
import projectRiskSummaryPage from "cypress/pages/risk/projectRiskSummaryPage";
import validationComponent from "cypress/pages/validationComponent";
import projectRiskHistoryTable from "cypress/pages/risk/projectRiskHistoryTable";
import { toDisplayDate } from "cypress/support/formatDate";

describe("Testing that we can add a project risk", () => {
    let project: ProjectDetailsRequest;
    let now: Date;

    beforeEach(() => {
        cy.login();

        now = new Date();

        project = RequestBuilder.createProjectDetails();

        projectApi
            .post({
                projects: [project],
            })
            .then(() => {
                cy.visit(`/projects/${project.projectId}/overview`);
            });
    });

    describe("Adding a project risk", () => {
        it("Should be able to add project risk", () => {

            projectOverviewPage
                .hasProjectRiskDate("Empty")
                .hasProjectRiskRating(["Empty"])
                .hasProjectRiskSummary("Empty");

            Logger.log("Changing project risk")
            projectOverviewPage.changeProjectRisk();

            Logger.log("When there is no project risk, it should display empty for all fields");
            projectRiskSummaryPage
                .hasSchoolName(project.schoolName)
                .hasNoRiskDate()
                .hasOverallRiskRating(["Empty"])
                .hasOverallRiskSummary("Empty")
                .hasGovernanceAndSuitabilityRiskRating(["Empty"])
                .hasGovernanceAndSuitabilityRiskSummary("Empty")
                .hasEducationRiskRating(["Empty"])
                .hasEducationRiskSummary("Empty")
                .hasFinanceRiskRating(["Empty"])
                .hasFinanceRiskSummary("Empty")
                .hasRiskAppraisalFormSharePointLink("Empty");

            Logger.log("Add risk entry");
            projectRiskSummaryPage.addRiskEntry();

            Logger.log("Validate Governance and suitability");
            editProjectRiskPage
                .withGovernanceAndSuitabilityRiskSummaryExceeding()
                .continue();

            validationComponent
                .hasValidationError("The risk rating field is required")
                .hasValidationError("The summary must be 1000 characters or less");

            cy.excuteAccessibilityTests();

            Logger.log("Enter a valid governance and suitability risk");
            editProjectRiskPage
                .hasSchoolName(project.schoolName)
                .withGovernanceAndSuitabilityRiskRating("AmberGreen")
                .withGovernanceAndSuitabilityRiskSummary("This is my governance and suitability risk summary")
                .continue();

            Logger.log("Validate education");
            editProjectRiskPage
                .withEducationSummaryExceeding()
                .continue();

            validationComponent
                .hasValidationError("The risk rating field is required")
                .hasValidationError("The summary must be 1000 characters or less");

            cy.excuteAccessibilityTests();

            Logger.log("Enter a valid education risk");
            editProjectRiskPage
                .hasSchoolName(project.schoolName)
                .withEducationRiskRating("Red")
                .withEducationSummary("This is my education risk summary")
                .continue();

            Logger.log("Validate finance");
            editProjectRiskPage
                .withFinanceSummaryExceeding()
                .continue();

            validationComponent
                .hasValidationError("The risk rating field is required")
                .hasValidationError("The summary must be 1000 characters or less");

            cy.excuteAccessibilityTests();

            Logger.log("Enter a valid finance risk");
            editProjectRiskPage
                .hasSchoolName(project.schoolName)
                .withFinanceRiskRating("AmberRed")
                .withFinanceSummary("This is my finance risk summary")
                .continue();

            editProjectRiskPage
                .hasSchoolName(project.schoolName)
                .withRiskAppraisalFormSharePointLink("www.google.co.uk")
                .continue();

            cy.excuteAccessibilityTests();

            Logger.log("Validate overall risk");
            editProjectRiskPage
                .withOverallRiskSummaryExceeding()
                .continue();

            validationComponent
                .hasValidationError("The risk rating field is required")
                .hasValidationError("The summary must be 1000 characters or less");

            cy.excuteAccessibilityTests();

            Logger.log("Enter a valid overall risk");
            editProjectRiskPage
                .hasSchoolName(project.schoolName)
                .withOverallRiskRating("Green")
                .withOverallRiskSummary("This is my overall risk summary")
                .continue();

            projectRiskSummaryPage
                .hasOverallRiskRating(["Green"])
                .hasOverallRiskSummary("This is my overall risk summary")
                .hasGovernanceAndSuitabilityRiskRating(["Amber", "Green"])
                .hasGovernanceAndSuitabilityRiskSummary("This is my governance and suitability risk summary")
                .hasEducationRiskRating(["Red"])
                .hasEducationRiskSummary("This is my education risk summary")
                .hasFinanceRiskRating(["Amber", "Red"])
                .hasFinanceRiskSummary("This is my finance risk summary")
                .hasRiskAppraisalFormSharePointLink("www.google.co.uk");

            cy.excuteAccessibilityTests();

            Logger.log("Create the project risk");
            projectRiskSummaryPage.createRiskEntry();

            projectRiskSummaryPage
                .hasRiskDate(`${toDisplayDate(now)}`)
                .hasOverallRiskRating(["Green"])
                .hasOverallRiskSummary("This is my overall risk summary")
                .hasGovernanceAndSuitabilityRiskRating(["Amber", "Green"])
                .hasGovernanceAndSuitabilityRiskSummary("This is my governance and suitability risk summary")
                .hasEducationRiskRating(["Red"])
                .hasEducationRiskSummary("This is my education risk summary")
                .hasFinanceRiskRating(["Amber", "Red"])
                .hasFinanceRiskSummary("This is my finance risk summary")
                .hasRiskAppraisalFormSharePointLink("www.google.co.uk");

            cy.excuteAccessibilityTests();

            Logger.log("Ensure that the project risk has been updated on the overview");
            cy.visit(`/projects/${project.projectId}/overview`);

            projectOverviewPage
                .hasProjectRiskDate(toDisplayDate(now))
                .hasProjectRiskRating(["Green"])
                .hasProjectRiskSummary("This is my overall risk summary");
        });

        it("Should be able to add multiple project risks with the latest being displayed by default", () => {
            Logger.log("Changing project risk")
            projectOverviewPage.changeProjectRisk();

            Logger.log("Adding the first project risk");
            projectRiskSummaryPage.addRiskEntry();

            fillProjectRisk();
            projectRiskSummaryPage.createRiskEntry();

            Logger.log("Adding the second project risk");
            projectRiskSummaryPage.addRiskEntry();

            Logger.log("Enter a valid governance and suitability risk");
            editProjectRiskPage
                .withGovernanceAndSuitabilityRiskRating("Green")
                .withGovernanceAndSuitabilityRiskSummary("This is another governance and suitability risk summary")
                .continue();

            Logger.log("Enter a valid education risk");
            editProjectRiskPage
                .withEducationRiskRating("AmberRed")
                .withEducationSummary("This is another of my education risk summary")
                .continue();

            Logger.log("Enter a valid finance risk");
            editProjectRiskPage
                .withFinanceRiskRating("Red")
                .withFinanceSummary("This is another of my finance risk summary")
                .continue();

            editProjectRiskPage
                .withRiskAppraisalFormSharePointLink("www.yahoo.com")
                .continue();

            Logger.log("Enter a valid overall risk");
            editProjectRiskPage
                .withOverallRiskRating("AmberGreen")
                .withOverallRiskSummary("This is another of my overall risk summary")
                .continue();

            projectRiskSummaryPage.createRiskEntry();

            projectRiskSummaryPage
                .hasOverallRiskRating(["Amber", "Green"])
                .hasOverallRiskSummary("This is another of my overall risk summary")
                .hasGovernanceAndSuitabilityRiskRating(["Green"])
                .hasGovernanceAndSuitabilityRiskSummary("This is another governance and suitability risk summary")
                .hasEducationRiskRating(["Amber", "Red"])
                .hasEducationRiskSummary("This is another of my education risk summary")
                .hasFinanceRiskRating(["Red"])
                .hasFinanceRiskSummary("This is another of my finance risk summary")
                .hasRiskAppraisalFormSharePointLink("www.yahoo.com");

            Logger.log("Should show the history of the project risk")
            projectRiskHistoryTable
                .getRowByIndex(1)
                .then((firstRow) => {
                    firstRow
                        .hasDate(toDisplayDate(now))
                        .hasRiskRating(["Amber", "Green"])
                        .hasNoViewLink();

                    return projectRiskHistoryTable.getRowByIndex(2)
                })
                .then((secondRow) => {
                    secondRow
                        .hasDate(toDisplayDate(now))
                        .hasRiskRating(["Green"]);

                    Logger.log("Select the second history entry")
                    secondRow
                        .view();

                    Logger.log("Should the information for the previous risk")
                    projectRiskSummaryPage
                        .hasOverallRiskRating(["Green"])
                        .hasOverallRiskSummary("This is my overall risk summary")
                        .hasGovernanceAndSuitabilityRiskRating(["Amber", "Green"])
                        .hasGovernanceAndSuitabilityRiskSummary("This is my governance and suitability risk summary")
                        .hasEducationRiskRating(["Red"])
                        .hasEducationRiskSummary("This is my education risk summary")
                        .hasFinanceRiskRating(["Amber", "Red"])
                        .hasFinanceRiskSummary("This is my finance risk summary")
                        .hasRiskAppraisalFormSharePointLink("www.google.co.uk");

                    Logger.log("Checking the change links are updated");
                    return projectRiskHistoryTable.getRowByIndex(2);
                })
                .then((secondRow) => {
                    secondRow.hasNoViewLink();

                    return projectRiskHistoryTable.getRowByIndex(1);
                })
                .then(firstRow => {
                    firstRow.view();

                    projectRiskSummaryPage
                        .hasOverallRiskRating(["Amber", "Green"])
                        .hasOverallRiskSummary("This is another of my overall risk summary")


                    Logger.log("Ensure that the latest project risk has been updated on the overview");
                    cy.visit(`/projects/${project.projectId}/overview`);

                    projectOverviewPage
                        .hasProjectRiskDate(toDisplayDate(now))
                        .hasProjectRiskRating(["Amber", "Green"])
                        .hasProjectRiskSummary("This is another of my overall risk summary");
                });
        });

        it("Should enable the user to change their answers on the confirmation page", () => {
            Logger.log("Changing project risk")
            projectOverviewPage.changeProjectRisk();

            Logger.log("Adding the project risk");
            projectRiskSummaryPage.addRiskEntry();

            fillProjectRisk();

            Logger.log("Change governance risk")
            projectRiskSummaryPage.changeGovernanceAndSuitabilityRisk();

            editProjectRiskPage
                .hasGovernanceAndSuitabilityRiskRating("AmberGreen")
                .hasGovernanceAndSuitabilityRiskSummary("This is my governance and suitability risk summary")
                .withGovernanceAndSuitabilityRiskRating("Red")
                .withGovernanceAndSuitabilityRiskSummary("This is a red governance and suitability risk now")
                .continue();

            Logger.log("Change education risk");
            projectRiskSummaryPage.changeEducationRisk();

            editProjectRiskPage
                .hasEducationRiskRating("Red")
                .hasEducationRiskSummary("This is my education risk summary")
                .withEducationRiskRating("Green")
                .withEducationSummary("Changing education summary to green")
                .continue();

            Logger.log("Change finance risk");
            projectRiskSummaryPage.changeFinanceRisk();

            editProjectRiskPage
                .hasFinanceRiskRating("AmberRed")
                .hasFinanceRiskSummary("This is my finance risk summary")
                .withFinanceRiskRating("Red")
                .withFinanceSummary("Changing the finance summary to red")
                .continue();

            Logger.log("Change sharepoint link");
            projectRiskSummaryPage.changeRiskAppraisalFormSharePointLink();

            editProjectRiskPage
                .hasRiskAppraisalFormSharePointLink("www.google.co.uk")
                .withRiskAppraisalFormSharePointLink("www.mynewlink/risk")
                .continue();

            Logger.log("Change overall risk");
            projectRiskSummaryPage.changeOverallRisk();

            editProjectRiskPage
                .hasOverallRiskRating("Green")
                .hasOverallRiskSummary("This is my overall risk summary")
                .withOverallRiskRating("AmberGreen")
                .withOverallRiskSummary("Changing the overall risk summary to amber green")
                .continue();

            Logger.log("Ensure our changes are reflected in the confirmation");
            projectRiskSummaryPage
                .hasOverallRiskRating(["Amber", "Green"])
                .hasOverallRiskSummary("Changing the overall risk summary to amber green")
                .hasGovernanceAndSuitabilityRiskRating(["Red"])
                .hasGovernanceAndSuitabilityRiskSummary("This is a red governance and suitability risk now")
                .hasEducationRiskRating(["Green"])
                .hasEducationRiskSummary("Changing education summary to green")
                .hasFinanceRiskRating(["Red"])
                .hasFinanceRiskSummary("Changing the finance summary to red")
                .hasRiskAppraisalFormSharePointLink("www.mynewlink/risk");

            Logger.log("Ensure that when the risk is created it has the edited values");
            projectRiskSummaryPage.createRiskEntry();

            projectRiskSummaryPage
                .hasOverallRiskRating(["Amber", "Green"])
                .hasOverallRiskSummary("Changing the overall risk summary to amber green")
                .hasGovernanceAndSuitabilityRiskRating(["Red"])
                .hasGovernanceAndSuitabilityRiskSummary("This is a red governance and suitability risk now")
                .hasEducationRiskRating(["Green"])
                .hasEducationRiskSummary("Changing education summary to green")
                .hasFinanceRiskRating(["Red"])
                .hasFinanceRiskSummary("Changing the finance summary to red")
                .hasRiskAppraisalFormSharePointLink("www.mynewlink/risk");

        });
    });

    function fillProjectRisk(): void {
        Logger.log("Enter a valid governance and suitability risk");
        editProjectRiskPage
            .withGovernanceAndSuitabilityRiskRating("AmberGreen")
            .withGovernanceAndSuitabilityRiskSummary("This is my governance and suitability risk summary")
            .continue();

        Logger.log("Enter a valid education risk");
        editProjectRiskPage
            .withEducationRiskRating("Red")
            .withEducationSummary("This is my education risk summary")
            .continue();

        Logger.log("Enter a valid finance risk");
        editProjectRiskPage
            .withFinanceRiskRating("AmberRed")
            .withFinanceSummary("This is my finance risk summary")
            .continue();

        editProjectRiskPage
            .withRiskAppraisalFormSharePointLink("www.google.co.uk")
            .continue();

        Logger.log("Enter a valid overall risk");
        editProjectRiskPage
            .withOverallRiskRating("Green")
            .withOverallRiskSummary("This is my overall risk summary")
            .continue();
    }
});