import { ProjectDetailsRequest } from "cypress/api/domain";
import projectApi from "cypress/api/projectApi";
import { RequestBuilder } from "cypress/api/requestBuilder";
import { Logger } from "cypress/common/logger";
import editCapacityWhenFullPage from "cypress/pages/pupil-numbers/editCapacityWhenFullPage";
import pupilNumbersSummaryComponent from "cypress/pages/pupil-numbers/pupilNumbersSummaryComponent";
import viewPupilNumbersPage from "cypress/pages/pupil-numbers/viewPupilNumbersPage";
import validationComponent from "cypress/pages/validationComponent";

describe("Testing the setting of pupil numbers", () => {
    let project: ProjectDetailsRequest;
    const numberOutsideRangeValidationMessage = "{0} must be greater than 0";

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

    it("Should be able to edit capacity when full", () => {
        pupilNumbersSummaryComponent.viewDetails();

        Logger.log("Checking pupil numbers");
        viewPupilNumbersPage
            .hasNurseryCapacity("0")
            .hasReceptionToYear6Capacity("0")
            .hasYear7ToYear11Capacity("0")
            .hasYear12ToYear14Capacity("0")
            .hasSpecialEducationalNeedsCapacity("0")
            .hasAlternativeProvisionCapacity("0")
            .hasTotalCapacity("0");

        cy.executeAccessibilityTests();

        viewPupilNumbersPage.editCapacity();

        editCapacityWhenFullPage
            .withNurseryCapacity("asd")
            .saveAndContinue();

        validationComponent
            .hasValidationError("The value 'asd' is not valid for Nursery");

        editCapacityWhenFullPage
            .withNurseryCapacity("-1")
            .withReceptionToYear6Capacity("-1")
            .withYear7ToYear11Capacity("-1")
            .withYear12ToYear14Capacity("-1")
            .withSpecialEducationalNeedsCapacity("-1")
            .withAlternativeProvisionCapacity("-1")
            .saveAndContinue();

        validationComponent
            .hasValidationError(numberOutsideRangeValidationMessage.replace("{0}", "Nursery"))
            .hasValidationError(numberOutsideRangeValidationMessage.replace("{0}", "Reception to year 6"))
            .hasValidationError(numberOutsideRangeValidationMessage.replace("{0}", "Year 7 to year 11"))
            .hasValidationError(numberOutsideRangeValidationMessage.replace("{0}", "Year 12 to year 14"))
            .hasValidationError(numberOutsideRangeValidationMessage.replace("{0}", "Special educational needs"))
            .hasValidationError(numberOutsideRangeValidationMessage.replace("{0}", "Alternative provision"));

        cy.executeAccessibilityTests();

        editCapacityWhenFullPage
            .withNurseryCapacity("10")
            .withReceptionToYear6Capacity("20")
            .withYear7ToYear11Capacity("30")
            .withYear12ToYear14Capacity("40")
            .withSpecialEducationalNeedsCapacity("50")
            .withAlternativeProvisionCapacity("60")
            .saveAndContinue();

        viewPupilNumbersPage
            .hasNurseryCapacity("10")
            .hasReceptionToYear6Capacity("20")
            .hasYear7ToYear11Capacity("30")
            .hasYear12ToYear14Capacity("40")
            .hasSpecialEducationalNeedsCapacity("50")
            .hasAlternativeProvisionCapacity("60")
            .hasTotalCapacity("210");
    });
});