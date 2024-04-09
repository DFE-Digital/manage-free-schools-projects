import { ProjectDetailsRequest } from "cypress/api/domain";
import projectApi from "cypress/api/projectApi";
import { RequestBuilder } from "cypress/api/requestBuilder";
import { Logger } from "cypress/common/logger";
import editCapacityWhenFullPage from "cypress/pages/pupil-numbers/editCapacityWhenFullPage";
import editPost16PublishedAdmissionNumberPage from "cypress/pages/pupil-numbers/editPost16PublishedAdmissionNumberPage";
import editPre16PublishedAdmissionNumberPage from "cypress/pages/pupil-numbers/editPre16PublishedAdmissionNumberPage";
import pupilNumbersSummaryComponent from "cypress/pages/pupil-numbers/pupilNumbersSummaryComponent";
import viewCapacityWhenFullPage from "cypress/pages/pupil-numbers/viewCapacityWhenFullPage";
import viewPost16PublishedAdmissionNumberPage from "cypress/pages/pupil-numbers/viewPost16PublishedAdmissionNumberPage";
import viewPre16PublishedAdmissionNumber from "cypress/pages/pupil-numbers/viewPre16PublishedAdmissionNumberPage";
import viewPupilNumbersPage from "cypress/pages/pupil-numbers/viewPupilNumbersPage";
import validationComponent from "cypress/pages/validationComponent";

describe("Testing the setting of pupil numbers", () => {
    let project: ProjectDetailsRequest;
    const numberOutsideRangeValidationMessage = "{0} must be 0 or more";
    const isNumberValidationMessage = "{0} must be a number, like 30";

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

        viewPupilNumbersPage
            .hasSchoolName(project.schoolName)

        Logger.log("Check all values are 0 initially");
        viewCapacityWhenFullPage
            .hasNursery("0")
            .hasReceptionToYear6("0")
            .hasYear7ToYear11("0")
            .hasYear12ToYear14("0")
            .hasSpecialEducationalNeeds("0")
            .hasAlternativeProvision("0")
            .hasTotal("0");

        cy.executeAccessibilityTests();

        Logger.log("Validate fields");
        viewPupilNumbersPage.editCapacity();

        editCapacityWhenFullPage
            .hasSchoolName(project.schoolName)
            .withNurseryCapacity("asd")
            .saveAndContinue();

        validationComponent
            .hasValidationError(isNumberValidationMessage.replace("{0}", "Nursery"));

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

        Logger.log("Set valid values");
        editCapacityWhenFullPage
            .hasSchoolName(project.schoolName)
            .withNurseryCapacity("10")
            .withReceptionToYear6Capacity("20")
            .withYear7ToYear11Capacity("30")
            .withYear12ToYear14Capacity("40")
            .withSpecialEducationalNeedsCapacity("50")
            .withAlternativeProvisionCapacity("60")
            .saveAndContinue();

        viewCapacityWhenFullPage
            .hasNursery("10")
            .hasReceptionToYear6("20")
            .hasYear7ToYear11("30")
            .hasYear12ToYear14("40")
            .hasSpecialEducationalNeeds("50")
            .hasAlternativeProvision("60")
            .hasTotal("210");
    });

    it("Should be able to edit the pre-16 published admission numbers", () => {
        pupilNumbersSummaryComponent.viewDetails();

        Logger.log("Check all values are 0 initially");
        viewPre16PublishedAdmissionNumber
            .hasReception("0")
            .hasYear7("0")
            .hasYear10("0")
            .hasOtherPre16("0")
            .hasTotal("0");

        viewPupilNumbersPage.editPre16PublishedAdmissionNumber();

        Logger.log("Validate fields");
        editPre16PublishedAdmissionNumberPage
            .withReception("asd")
            .saveAndContinue();

        validationComponent
            .hasValidationError(isNumberValidationMessage.replace("{0}", "Reception"));

        editPre16PublishedAdmissionNumberPage
            .withReception("-1")
            .withYear7("-1")
            .withYear10("-1")
            .withOtherPre16("-1")
            .saveAndContinue();

        validationComponent
            .hasValidationError(numberOutsideRangeValidationMessage.replace("{0}", "Reception"))
            .hasValidationError(numberOutsideRangeValidationMessage.replace("{0}", "Year 7"))
            .hasValidationError(numberOutsideRangeValidationMessage.replace("{0}", "Year 10"))
            .hasValidationError(numberOutsideRangeValidationMessage.replace("{0}", "Other pre-16"))

        cy.executeAccessibilityTests();

        editPre16PublishedAdmissionNumberPage
            .withReception("11")
            .withYear7("22")
            .withYear10("33")
            .withOtherPre16("44")
            .saveAndContinue();

        viewPre16PublishedAdmissionNumber
            .hasReception("11")
            .hasYear7("22")
            .hasYear10("33")
            .hasOtherPre16("44")
            .hasTotal("110");
    });

    it("Should be able to edit the pre-16 published admission numbers", () => {
        pupilNumbersSummaryComponent.viewDetails();

        Logger.log("Check all values are 0 initially");
        viewPost16PublishedAdmissionNumberPage
            .hasYear12("0")
            .hasOtherPost16("0")
            .hasTotal("0");

        viewPupilNumbersPage.editPost16PublishedAdmissionNumber();

        Logger.log("Validate fields");
        editPost16PublishedAdmissionNumberPage
            .withYear12("asd")
            .saveAndContinue();

        validationComponent
            .hasValidationError(isNumberValidationMessage.replace("{0}", "Year 12"));

        editPost16PublishedAdmissionNumberPage
            .withYear12("-1")
            .withOtherPost16("-1")
            .saveAndContinue();

        validationComponent
            .hasValidationError(numberOutsideRangeValidationMessage.replace("{0}", "Year 12"))
            .hasValidationError(numberOutsideRangeValidationMessage.replace("{0}", "Other post-16"));

        cy.executeAccessibilityTests();

        editPost16PublishedAdmissionNumberPage
            .withYear12("12")
            .withOtherPost16("23")
            .saveAndContinue();

        viewPost16PublishedAdmissionNumberPage
            .hasYear12("12")
            .hasOtherPost16("23")
            .hasTotal("35");

    });
});