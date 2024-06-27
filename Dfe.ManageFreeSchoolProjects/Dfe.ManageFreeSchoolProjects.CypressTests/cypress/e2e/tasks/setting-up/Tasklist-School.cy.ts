import { ProjectDetailsRequest } from "cypress/api/domain";
import projectApi from "cypress/api/projectApi";
import { RequestBuilder } from "cypress/api/requestBuilder";
import { Logger } from "cypress/common/logger";
import taskListPage from "cypress/pages/taskListPage";
import schoolDetailsPage from "cypress/pages/schoolDetailsPage";
import summaryPage from "cypress/pages/task-summary-base";
import validationComponent from "cypress/pages/validationComponent";
import dataGenerator from "cypress/fixtures/dataGenerator";

describe("Testing project overview", () => {
    let project: ProjectDetailsRequest;

    beforeEach(() => {
        cy.login();

        project = RequestBuilder.createProjectDetails();

        projectApi
            .post({
                projects: [project],
            })
            .then(() => {
                cy.visit(`/projects/${project.projectId}/tasks`);
            });
    });

    it("Should successfully set Tasklist-school information", () => {

        Logger.log("Select finance plan");
        taskListPage.isTaskStatusIsNotStarted("School")
            .selectSchoolFromTaskList();

        Logger.log("Go back to task list");
        summaryPage.clickBack();

        taskListPage.selectSchoolFromTaskList();

        Logger.log("Confirm empty school task");
        summaryPage
            .schoolNameIs(project.schoolName)
            .titleIs("School")
            .inOrder()
            .summaryShows("Current free school name").HasValue(project.schoolName).HasChangeLink()
            .summaryShows("School type").IsEmpty().HasChangeLink()
            .summaryShows("School phase").IsEmpty().HasChangeLink()
            .summaryShows("Age range").IsEmpty().HasChangeLink()
            .summaryShows("Forms of entry").IsEmpty().HasChangeLink()
            .summaryShows("Gender").IsEmpty().HasChangeLink()
            .summaryShows("Nursery").IsEmpty().HasChangeLink()
            .summaryShows("Sixth form").IsEmpty().HasChangeLink()
            .summaryShows("Faith status").IsEmpty().HasChangeLink()
            .summaryShows("Faith type").IsEmpty().HasChangeLink()
            .isNotMarkedAsComplete();

        cy.executeAccessibilityTests();

        summaryPage.clickChange();

        Logger.log("Checking validation");
        schoolDetailsPage
            .clickContinue();

        validationComponent
            .hasValidationError("Enter the gender field")
            .hasValidationError("Enter the nursery field")
            .hasValidationError("Enter a 'from' and 'to' age range")
            .hasValidationError("Enter the sixth form field")
            .hasValidationError("Enter the school type field")
            .hasValidationError("Enter the faith status field")
            .hasValidationError("Enter the school phase field");

        cy.executeAccessibilityTests();

        schoolDetailsPage
            .withSchoolNameExceedingMaxLength()
            .withFormsOfEntryExceedingLimit()
            .clickContinue();

        validationComponent
            .hasValidationError("Current free school name must be 100 characters or less")
            .hasValidationError("Forms of entry must be 100 characters or less")

        const updatedSchoolName = dataGenerator.generateSchoolName();

        Logger.log("Adding new values");
        schoolDetailsPage
            .titleIs("Edit School")
            .withSchoolName(updatedSchoolName)
            .withSchoolType("Mainstream")
            .withSchoolPhase("Secondary")
            .withAgeRange("11", "16")
            .withFormsOfEntry("3")
            .withGender("Mixed")
            .withNursery("Yes")
            .withSixthForm("No")
            .withAlternativeProvision("No")
            .withSpecialEducationNeeds("No")
            .withFaithStatus("Designation")
            .withFaithType("faith-type-Jewish")
            .clickContinue();

        summaryPage
            .schoolNameIs(updatedSchoolName)
            .inOrder()
            .summaryShows("Current free school name").HasValue(updatedSchoolName)
            .summaryShows("School type").HasValue("Mainstream")
            .summaryShows("School phase").HasValue("Secondary")
            .summaryShows("Age range").HasValue("11-16")
            .summaryShows("Forms of entry").HasValue("3")
            .summaryShows("Gender").HasValue("Mixed")
            .summaryShows("Nursery").HasValue("Yes")
            .summaryShows("Sixth form").HasValue("No")
            .summaryShows("Alternative provision (specialist resource provision)").HasValue("No")
            .summaryShows("Special educational needs (specialist resource provision)").HasValue("No")
            .summaryShows("Faith status").HasValue("Designation")
            .summaryShows("Faith type").HasValue("Jewish")
            .clickChange();

        Logger.log("Update the existing values");
        Logger.log("When special is selected alternative provision and special educational needs are not displayed");
        schoolDetailsPage
            .withSchoolType("Special")
            .withSchoolPhase("Primary")
            .withAgeRange("5", "11")
            .withFormsOfEntry("10")
            .withGender("BoysOnly")
            .withNursery("No")
            .withSixthForm("Yes")
            .withFaithStatus("Ethos")
            .withFaithType("faith-type-Christian")
            .hasNoAlternativeProvision()
            .hasNoSpecialEducationNeeds()
            .clickContinue();

        summaryPage
            .schoolNameIs(updatedSchoolName)
            .inOrder()
            .summaryShows("Current free school name").HasValue(updatedSchoolName)
            .summaryShows("School type").HasValue("Special")
            .summaryShows("School phase").HasValue("Primary")
            .summaryShows("Age range").HasValue("5-11")
            .summaryShows("Forms of entry").HasValue("10")
            .summaryShows("Gender").HasValue("Boys only")
            .summaryShows("Nursery").HasValue("No")
            .summaryShows("Sixth form").HasValue("Yes")
            .summaryShows("Faith status").HasValue("Ethos")
            .summaryShows("Faith type").HasValue("Christian")
            .summaryDoesNotShow("Alternative provision (specialist resource provision)")
            .summaryDoesNotShow("Special educational needs (specialist resource provision)")
            .clickChange();

        Logger.log("Checking faith type 'Other'");
        schoolDetailsPage
            .withFaithType("faith-type-Other")
            .clickContinue();

        validationComponent.hasValidationError("Other faith type is required");

        schoolDetailsPage
            .withFaithTypeOtherDescriptionExceedingMaxLength()
            .clickContinue();

        validationComponent.hasValidationError("Other faith type must be 100 characters or less");

        schoolDetailsPage
            .withFaithTypeOtherDescription("(This is $invalid)")
            .clickContinue();

        validationComponent.hasValidationError("Other faith type must only contain letters and spaces");

        cy.executeAccessibilityTests();

        schoolDetailsPage
            .withFaithTypeOtherDescription("This is my faith")
            .clickContinue();

        summaryPage
            .startFromRow(9)
            .summaryShows("Faith type").HasValue("Other - This is my faith");

        summaryPage.clickChange();

        schoolDetailsPage
            .withFaithType("faith-type-Christian")
            .faithTypeOtherDescriptionIsNotVisible()
            .clickContinue();

        summaryPage
            .startFromRow(9)
            .summaryShows("Faith type").HasValue("Christian");

        summaryPage.clickChange();

        schoolDetailsPage
            .withFaithStatus("None")
            .faithTypeSectionIsNotVisible()
            .clickContinue();

        summaryPage
            .startFromRow(9)
            .summaryShows("Faith type").HasValue("None");

        Logger.log("Should update the task status");
        summaryPage.clickConfirmAndContinue();

        taskListPage.isTaskStatusInProgress("School").selectSchoolFromTaskList();

        summaryPage
            .MarkAsComplete()
            .clickConfirmAndContinue();

        taskListPage.isTaskStatusIsCompleted("School");
    });

    it("Should validate the faith type field if a faith status is selected", () => {

        taskListPage.selectSchoolFromTaskList();

        summaryPage.clickChange();

        schoolDetailsPage
            .withSchoolType("Mainstream")
            .withSchoolPhase("Secondary")
            .withAgeRange("11", "16")
            .withFormsOfEntry("3")
            .withGender("Mixed")
            .withNursery("Yes")
            .withSixthForm("No")
            .withAlternativeProvision("No")
            .withSpecialEducationNeeds("No")
            .withFaithStatus("Designation")
            .clickContinue();

        validationComponent
            .hasValidationError("Faith type is required");

        Logger.log("Change faith status to none and back to designation to check validation against faith type none");

        schoolDetailsPage
            .withFaithStatus("None")
            .withFaithStatus("Designation")
            .clickContinue();

        validationComponent
            .hasValidationError("Select a different faith type, if faith status is designation.");
    });

    it("Should validate the class type field if a school type with specialist provisions is selected", () => {

        taskListPage.selectSchoolFromTaskList();

        summaryPage.clickChange();

        schoolDetailsPage
            .withSchoolType("Mainstream")
            .withSchoolPhase("Secondary")
            .withAgeRange("11", "16")
            .withFormsOfEntry("3")
            .withGender("Mixed")
            .withNursery("Yes")
            .withSixthForm("No")
            .withFaithStatus("Designation")
            .withFaithType("faith-type-Christian")
            .clickContinue();

        validationComponent
            .hasValidationError("Select yes if it will have alternative provision")
            .hasValidationError("Select yes if it will have special educational needs provision");
    });
});
