import { ProjectDetailsRequest } from "cypress/api/domain";
import projectApi from "cypress/api/projectApi";
import { RequestBuilder } from "cypress/api/requestBuilder";
import { Logger } from "cypress/common/logger";
import projectOverviewPage from "cypress/pages/projectOverviewPage";
import contactsSummaryPage from "cypress/pages/contacts/contactsSummaryPage";
import dataGenerator from "cypress/fixtures/dataGenerator";
import editContactPage from "../pages/contacts/editContactPage";
import editProjectManagedByPage from "../pages/contacts/editProjectManagedByPage";

describe("Testing that we can add contacts", () => {
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

    describe("Adding Contacts", () => {
        it("Should be able to add contacts", () => {

            cy.executeAccessibilityTests();

            projectOverviewPage
                .hasSchoolChairOfGovernors("Empty")
                .hasProjectManagedBy("Empty");
            

            Logger.log("Changing contacts")
            projectOverviewPage.changeContacts();

            cy.executeAccessibilityTests();

            Logger.log("When there are no contacts, it should display empty for all fields");
            contactsSummaryPage
                .hasTitle("Contacts")
                .hasSchoolName(project.schoolName)
                .hasProjectManagedByName("")
                .hasProjectManagedByEmail("")
                .hasSchoolChairName("")
                .hasSchoolChairEmail("");

            Logger.log("Edit Contact");
            contactsSummaryPage.goToEditSchoolChair();

            cy.executeAccessibilityTests();

            Logger.log("Check edit contact validation");
            editContactPage
                .hasTitle("Edit School chair")
                .hasSchoolName(project.schoolName)
                .withContactName("$da")
                .clickContinue()
                .errorForContactName("Contact name must not include special characters other than , ( )")
                .withContactName("4da")
                .clickContinue()
                .errorForContactName("The contact name cannot contain numbers")
                .withContactName(dataGenerator.generateAlphaNumeric(101))
                .clickContinue()
                .errorForContactName("The contact name must be 100 characters or less")
                .withContactEmail("da")
                .clickContinue()
                .errorForContactEmail("Enter an email address in the correct format")
                .withContactEmail(dataGenerator.generateAlphaNumeric(101))
                .clickContinue()
                .errorForContactEmail("The contact email must be 100 characters or less")
                .withContactName("Contact")
                .withContactEmail("school@chair.com")
                .clickContinue();
            
            cy.executeAccessibilityTests();

            Logger.log("Check user is back to contact summary page");
            contactsSummaryPage
                .hasTitle("Contacts")
                .hasSchoolName(project.schoolName)
                .hasProjectManagedByName("")
                .hasProjectManagedByEmail("")
                .hasSchoolChairName("Contact")
                .hasSchoolChairEmail("school@chair.com");

            Logger.log("Edit project managed by");
            contactsSummaryPage.goToEditProjectManagedBy();

            cy.executeAccessibilityTests();

            Logger.log("Check edit project managed by validation");
            editProjectManagedByPage
                .hasTitle("Edit Project managed by")
                .hasSchoolName(project.schoolName)
                .withProjectManagedByName("$da")
                .clickContinue()
                .errorForProjectManagedByName("Project managed by name must not include special characters other than , ( )")
                .withProjectManagedByName("4da")
                .clickContinue()
                .errorForProjectManagedByName("The project managed by name cannot contain numbers")
                .withProjectManagedByName(dataGenerator.generateAlphaNumeric(101))
                .clickContinue()
                .errorForProjectManagedByName("The project managed by name must be 100 characters or less")
                .withProjectManagedByEmail("da")
                .clickContinue()
                .errorForProjectManagedByEmail("Enter an email address in the correct format")
                .withProjectManagedByEmail(dataGenerator.generateAlphaNumeric(101))
                .clickContinue()
                .errorForProjectManagedByEmail("The project managed by email must be 100 characters or less")
                .withProjectManagedByName("Project Managed By")
                .withProjectManagedByEmail("trust@chair.com")
                .clickContinue();
            
            cy.executeAccessibilityTests();

            Logger.log("Check user is back to contact summary page");
            contactsSummaryPage
                .hasTitle("Contacts")
                .hasSchoolName(project.schoolName)
                .hasProjectManagedByName("Project Managed By")
                .hasProjectManagedByEmail("trust@chair.com")
                .hasSchoolChairName("Contact")
                .hasSchoolChairEmail("school@chair.com")
                .goToProjectsOverviewPage();
            
            cy.executeAccessibilityTests();

            Logger.log("Check user is back to projects overview page");
            projectOverviewPage
                .hasSchoolChairOfGovernors("Contact")
                .hasProjectManagedBy("Project Managed By");

            Logger.log("Check existing contacts can be resaved as null strings/empty on ProjectManagedBy Page");
            projectOverviewPage.changeContacts();

            cy.executeAccessibilityTests();

            contactsSummaryPage.goToEditSchoolChair();
            editContactPage
                .hasTitle("Edit School chair")
                .hasSchoolName(project.schoolName)
                .withNullContactName()
                .withNullContactEmail()
                .clickContinue();
            
            cy.executeAccessibilityTests();
            
            contactsSummaryPage
                .hasTitle("Contacts")
                .hasSchoolName(project.schoolName)
                .hasProjectManagedByName("Project Managed By")
                .hasProjectManagedByEmail("trust@chair.com")
                .hasSchoolChairName("")
                .hasSchoolChairEmail("");
            
            contactsSummaryPage.goToEditProjectManagedBy();

            cy.executeAccessibilityTests();

            editProjectManagedByPage
                .hasTitle("Edit project managed by")
                .hasSchoolName(project.schoolName)
                .withNullProjectManagedByName()
                .withNullProjectManagedByEmail()
                .clickContinue();
            
            cy.executeAccessibilityTests();
            
            contactsSummaryPage
                .hasTitle("Contacts")
                .hasSchoolName(project.schoolName)
                .hasProjectManagedByName("")
                .hasProjectManagedByEmail("")
                .hasSchoolChairName("")
                .hasSchoolChairEmail("");


        })
    })

})