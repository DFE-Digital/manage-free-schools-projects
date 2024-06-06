import { ProjectDetailsRequest } from "cypress/api/domain";
import projectApi from "cypress/api/projectApi";
import { RequestBuilder } from "cypress/api/requestBuilder";
import { Logger } from "cypress/common/logger";
import dataGenerator from "cypress/fixtures/dataGenerator";
import contactsPage from "cypress/pages/contacts/contactsPage";
import editProjectAssignedToPage from "../pages/contacts/editProjectAssignedToPage";
import editTeamLeadPage from "../pages/contacts/editTeamLeadPage";
import editGrade6Page from "../pages/contacts/editGrade6Page";
import editProjectManagerPage from "cypress/pages/contacts/editProjectManagerPage";
import editProjectDirectorPage from "cypress/pages/contacts/editProjectDirectorPage";
import editOfstedContactPage from "cypress/pages/contacts/editOfstedContactPage";
import editTrustContactPage from "cypress/pages/contacts/editTrustContactPage";

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
                cy.visit(`/projects/${project.projectId}/contacts`);
            });
    });

    describe("Editing Contacts", () => {
        it("Should be able to edit Project assigned to", () => {

            cy.executeAccessibilityTests();

            contactsPage
                .hasProjectAssignedToName(project.projectAssignedToName)
                .hasProjectAssignedToEmail(project.projectAssignedToEmail)            
            Logger.log("Edit Project assigned to")
            contactsPage.goToEditProjectAssignedTo();

            cy.executeAccessibilityTests();

            Logger.log("Check edit contact validation");
            editProjectAssignedToPage
                .hasProjectAssignedToTitle("Edit Project assigned to")
                .clickContinue()
                .withProjectAssignedToName("$da")
                .clickContinue()
                .errorForProjectAssignedToName("Project assigned to name must not include special characters other than , ( )")
                .withProjectAssignedToName("da")
                .clickContinue()
                .errorForProjectAssignedToName("Enter the full name, for example John Smith")
                .withProjectAssignedToName("da 1")
                .clickContinue()
                .errorForProjectAssignedToName("The project assigned to name cannot contain numbers")
                .withProjectAssignedToName(dataGenerator.generateAlphaNumeric(101))
                .clickContinue()
                .errorForProjectAssignedToName("The project assigned to name must be 100 characters or less")
                .withNullProjectAssignedToName()
                .clickContinue()
                .errorForProjectAssignedToName("Please enter the name")
                .withProjectAssignedToName("Test Person")
                .withProjectAssignedToEmail("firstname.surname@educaion.gov.uk")
                .clickContinue()
                .errorForProjectAssignedToEmail("Enter an email address in the correct format. For example, firstname.surname@education.gov.uk")
                .withProjectAssignedToEmail(dataGenerator.generateAlphaNumeric(101))
                .clickContinue()
                .errorForProjectAssignedToEmail("The project assigned to email must be 100 characters or less")
                .withNullProjectAssignedToEmail()
                .clickContinue()
                .errorForProjectAssignedToEmail("Please enter an email")
                .withProjectAssignedToEmail("test.person@education.gov.uk")
                .clickContinue();

            contactsPage
                .hasProjectAssignedToName("Test Person")
                .hasProjectAssignedToEmail("test.person@education.gov.uk")

            });

        it("Should be able to edit Team lead", () => {

                cy.executeAccessibilityTests();
    
                contactsPage
                    .isEmpty("team-lead-name")
                    .isEmpty("team-lead-email")
                Logger.log("Edit Team lead")
                contactsPage.goToEditTeamLead();
    
                cy.executeAccessibilityTests();
    
                Logger.log("Check edit contact validation");
                editTeamLeadPage
                    .hasTeamLeadTitle("Edit Team lead")
                    .clickContinue()
                    .withTeamLeadName("$da")
                    .clickContinue()
                    .errorForTeamLeadName("Team lead name must not include special characters other than , ( )")
                    .withTeamLeadName("da")
                    .clickContinue()
                    .errorForTeamLeadName("Enter the full name, for example John Smith")
                    .withTeamLeadName("da 1")
                    .clickContinue()
                    .errorForTeamLeadName("The team lead name cannot contain numbers")
                    .withTeamLeadName(dataGenerator.generateAlphaNumeric(101))
                    .clickContinue()
                    .errorForTeamLeadName("The team lead name must be 100 characters or less")
                    .withTeamLeadName("Test Person")
                    .withTeamLeadEmail("firstname.surname@educaion.gov.uk")
                    .clickContinue()
                    .errorForTeamLeadEmail("Enter an email address in the correct format. For example, firstname.surname@education.gov.uk")
                    .withTeamLeadEmail(dataGenerator.generateAlphaNumeric(101))
                    .clickContinue()
                    .errorForTeamLeadEmail("The team lead email must be 100 characters or less")
                    .withTeamLeadEmail("test.person@education.gov.uk")
                    .clickContinue();
    
                contactsPage
                    .hasTeamLeadName("Test Person")
                    .hasTeamLeadEmail("test.person@education.gov.uk")
    
                });

                it("Should be able to edit Grade 6", () => {

                    cy.executeAccessibilityTests();
        
                    contactsPage
                        .isEmpty("grade-6-name")
                        .isEmpty("grade-6-email")
                    Logger.log("Edit Grade 6")
                    contactsPage.goToEditGrade6();
        
                    cy.executeAccessibilityTests();
        
                    Logger.log("Check edit contact validation");
                    editGrade6Page
                        .hasGrade6Title("Edit Grade 6")
                        .clickContinue()
                        .withGrade6Name("$da")
                        .clickContinue()
                        .errorForGrade6Name("Grade 6 name must not include special characters other than , ( )")
                        .withGrade6Name("da")
                        .clickContinue()
                        .errorForGrade6Name("Enter the full name, for example John Smith")
                        .withGrade6Name("da 1")
                        .clickContinue()
                        .errorForGrade6Name("The grade 6 name cannot contain numbers")
                        .withGrade6Name(dataGenerator.generateAlphaNumeric(101))
                        .clickContinue()
                        .errorForGrade6Name("The grade 6 name must be 100 characters or less")
                        .withGrade6Name("Test Person")
                        .withGrade6Email("firstname.surname@educaion.gov.uk")
                        .clickContinue()
                        .errorForGrade6Email("Enter an email address in the correct format. For example, firstname.surname@education.gov.uk")
                        .withGrade6Email(dataGenerator.generateAlphaNumeric(101))
                        .clickContinue()
                        .errorForGrade6Email("The grade 6 email must be 100 characters or less")
                        .withGrade6Email("test.person@education.gov.uk")
                        .clickContinue();
        
                    contactsPage
                        .hasGrade6Name("Test Person")
                        .hasGrade6Email("test.person@education.gov.uk")
        
                });

                it("Should be able to edit Project manager", () => {

                    cy.executeAccessibilityTests();
        
                    contactsPage
                        .isEmpty("project-manager-name")
                        .isEmpty("project-manager-email")
                    Logger.log("Edit Project manager")
                    contactsPage.goToEditProjectManager();
        
                    cy.executeAccessibilityTests();
        
                    Logger.log("Check edit contact validation");
                    editProjectManagerPage
                        .hasProjectManagerTitle("Edit Project manager")
                        .clickContinue()
                        .withProjectManagerName("$da")
                        .clickContinue()
                        .errorForProjectManagerName("Project manager name must not include special characters other than , ( )")
                        .withProjectManagerName("da")
                        .clickContinue()
                        .errorForProjectManagerName("Enter the full name, for example John Smith")
                        .withProjectManagerName("da 1")
                        .clickContinue()
                        .errorForProjectManagerName("The project manager name cannot contain numbers")
                        .withProjectManagerName(dataGenerator.generateAlphaNumeric(101))
                        .clickContinue()
                        .errorForProjectManagerName("The project manager name must be 100 characters or less")
                        .withProjectManagerName("Test Person")
                        .withProjectManagerEmail("firstname.surname")
                        .clickContinue()
                        .errorForProjectManagerEmail("Enter an email address in the correct format")
                        .withProjectManagerEmail(dataGenerator.generateAlphaNumeric(101))
                        .clickContinue()
                        .errorForProjectManagerEmail("The project manager email must be 100 characters or less")
                        .withProjectManagerEmail("test.person@gmail.com")
                        .clickContinue();
        
                    contactsPage
                        .hasProjectManagerName("Test Person")
                        .hasProjectManagerEmail("test.person@gmail.com")
        
                });

                it("Should be able to edit Project director", () => {

                    cy.executeAccessibilityTests();
        
                    contactsPage
                        .isEmpty("project-director-name")
                        .isEmpty("project-director-email")
                    Logger.log("Edit Project director")
                    contactsPage.goToEditProjectDirector();
        
                    cy.executeAccessibilityTests();
        
                    Logger.log("Check edit contact validation");
                    editProjectDirectorPage
                        .hasProjectDirectorTitle("Edit Project director")
                        .clickContinue()
                        .withProjectDirectorName("$da")
                        .clickContinue()
                        .errorForProjectDirectorName("Project director name must not include special characters other than , ( )")
                        .withProjectDirectorName("da")
                        .clickContinue()
                        .errorForProjectDirectorName("Enter the full name, for example John Smith")
                        .withProjectDirectorName("da 1")
                        .clickContinue()
                        .errorForProjectDirectorName("The project director name cannot contain numbers")
                        .withProjectDirectorName(dataGenerator.generateAlphaNumeric(101))
                        .clickContinue()
                        .errorForProjectDirectorName("The project director name must be 100 characters or less")
                        .withProjectDirectorName("Test Person")
                        .withProjectDirectorEmail("firstname.surname")
                        .clickContinue()
                        .errorForProjectDirectorEmail("Enter an email address in the correct format")
                        .withProjectDirectorEmail(dataGenerator.generateAlphaNumeric(101))
                        .clickContinue()
                        .errorForProjectDirectorEmail("The project director email must be 100 characters or less")
                        .withProjectDirectorEmail("test.person@gmail.com")
                        .clickContinue();
        
                    contactsPage
                        .hasProjectDirectorName("Test Person")
                        .hasProjectDirectorEmail("test.person@gmail.com")
        
                });

                it("Should be able to edit Ofsted contact", () => {

                    cy.executeAccessibilityTests();
        
                    contactsPage
                        .isEmpty("ofsted-contact-name")
                        .isEmpty("ofsted-contact-email")
                    Logger.log("Edit Ofsted contact")
                    contactsPage.goToEditOfstedContact();
        
                    cy.executeAccessibilityTests();
        
                    Logger.log("Check edit contact validation");
                    editOfstedContactPage
                        .hasOfstedContactTitle("Edit Ofsted contact")
                        .clickContinue()
                        .withOfstedContactName("$da")
                        .clickContinue()
                        .errorForOfstedContactName("Ofsted contact name must not include special characters other than , ( )")
                        .withOfstedContactName("da")
                        .clickContinue()
                        .errorForOfstedContactName("Enter the full name, for example John Smith")
                        .withOfstedContactName("da 1")
                        .clickContinue()
                        .errorForOfstedContactName("The ofsted contact name cannot contain numbers")
                        .withOfstedContactName(dataGenerator.generateAlphaNumeric(101))
                        .clickContinue()
                        .errorForOfstedContactName("The ofsted contact name must be 100 characters or less")
                        .withOfstedContactName("Test Person")
                        .withOfstedContactEmail("firstname.surname")
                        .clickContinue()
                        .errorForOfstedContactEmail("Enter an email address in the correct format")
                        .withOfstedContactEmail(dataGenerator.generateAlphaNumeric(101))
                        .clickContinue()
                        .errorForOfstedContactEmail("The ofsted contact email must be 100 characters or less")
                        .withOfstedContactEmail("test.person@gmail.com")
                        .withOfstedContactPhoneNumber("0")
                        .clickContinue()
                        .errorForOfstedContactPhoneNumber("Phone number must be between 5 numbers and 15 numbers")
                        .withOfstedContactPhoneNumber("(0++  --0)")
                        .clickContinue()
                        .errorForOfstedContactPhoneNumber("Phone number must be between 5 numbers and 15 numbers")
                        .withOfstedContactPhoneNumber("55555555555555555555")
                        .clickContinue()
                        .errorForOfstedContactPhoneNumber("Phone number must be between 5 numbers and 15 numbers")
                        .withOfstedContactPhoneNumber("07123456e")
                        .clickContinue()
                        .errorForOfstedContactPhoneNumber("Phone number must only include numbers and ( ) - +")
                        .withOfstedContactPhoneNumber("(0) 7123456-89")
                        .withOfstedContactRole("^^^")
                        .clickContinue()
                        .errorForOfstedContactRole("Ofsted contact role must not include special characters other than , ( ) '")
                        .withOfstedContactRole(dataGenerator.generateAlphaNumeric(101))
                        .clickContinue()
                        .errorForOfstedContactRole("The ofsted contact role must be 100 characters or less")
                        .withOfstedContactRole("Tester")
                        .clickContinue();

                    contactsPage
                        .hasOfstedContactName("Test Person")
                        .hasOfstedContactEmail("test.person@gmail.com")
                        .hasOfstedContactPhoneNumber("(0) 7123456-89")
                        .hasOfstedContactRole("Tester");
        
                });

                it("Should be able to edit Trust contact", () => {

                    cy.executeAccessibilityTests();
        
                    contactsPage
                        .isEmpty("trust-contact-name")
                        .isEmpty("trust-contact-email")
                    Logger.log("Edit Trust contact")
                    contactsPage.goToEditTrustContact();
        
                    cy.executeAccessibilityTests();
        
                    Logger.log("Check edit contact validation");
                    editTrustContactPage
                        .hasTrustContactTitle("Edit Trust contact")
                        .clickContinue()
                        .withTrustContactName("$da")
                        .clickContinue()
                        .errorForTrustContactName("Trust contact name must not include special characters other than , ( )")
                        .withTrustContactName("da")
                        .clickContinue()
                        .errorForTrustContactName("Enter the full name, for example John Smith")
                        .withTrustContactName("da 1")
                        .clickContinue()
                        .errorForTrustContactName("The trust contact name cannot contain numbers")
                        .withTrustContactName(dataGenerator.generateAlphaNumeric(101))
                        .clickContinue()
                        .errorForTrustContactName("The trust contact name must be 100 characters or less")
                        .withTrustContactName("Test Person")
                        .withTrustContactEmail("firstname.surname")
                        .clickContinue()
                        .errorForTrustContactEmail("Enter an email address in the correct format")
                        .withTrustContactEmail(dataGenerator.generateAlphaNumeric(101))
                        .clickContinue()
                        .errorForTrustContactEmail("The trust contact email must be 100 characters or less")
                        .withTrustContactEmail("test.person@gmail.com")
                        .withTrustContactPhoneNumber("0")
                        .clickContinue()
                        .errorForTrustContactPhoneNumber("Phone number must be between 5 numbers and 15 numbers")
                        .withTrustContactPhoneNumber("(0++  --0)")
                        .clickContinue()
                        .errorForTrustContactPhoneNumber("Phone number must be between 5 numbers and 15 numbers")
                        .withTrustContactPhoneNumber("55555555555555555555")
                        .clickContinue()
                        .errorForTrustContactPhoneNumber("Phone number must be between 5 numbers and 15 numbers")
                        .withTrustContactPhoneNumber("07123456e")
                        .clickContinue()
                        .errorForTrustContactPhoneNumber("Phone number must only include numbers and ( ) - +")
                        .withTrustContactPhoneNumber("(0) 7123456-89")
                        .withTrustContactRole("^^^")
                        .clickContinue()
                        .errorForTrustContactRole("Trust contact role must not include special characters other than , ( ) '")
                        .withTrustContactRole(dataGenerator.generateAlphaNumeric(101))
                        .clickContinue()
                        .errorForTrustContactRole("The trust contact role must be 100 characters or less")
                        .withTrustContactRole("Tester")
                        .clickContinue();

                    contactsPage
                        .hasTrustContactName("Test Person")
                        .hasTrustContactEmail("test.person@gmail.com")
                        .hasTrustContactPhoneNumber("(0) 7123456-89")
                        .hasTrustContactRole("Tester");
        
                });
               
            
            // cy.executeAccessibilityTests();

            // Logger.log("Check user is back to contact summary page");
            // contactsPage
            //     .hasTitle("Contacts")
            //     .hasSchoolName(project.schoolName)
            //     .hasProjectManagedByName("")
            //     .hasProjectManagedByEmail("")
            //     .hasSchoolChairName("Contact")
            //     .hasSchoolChairEmail("school@chair.com");

            // Logger.log("Edit project managed by");
            // contactsPage.goToEditProjectManagedBy();

            // cy.executeAccessibilityTests();

            // Logger.log("Check edit project managed by validation");
            // editProjectManagedByPage
            //     .hasTitle("Edit Project managed by")
            //     .hasSchoolName(project.schoolName)
            //     .withProjectManagedByName("$da")
            //     .clickContinue()
            //     .errorForProjectManagedByName("Project managed by name must not include special characters other than , ( )")
            //     .withProjectManagedByName("4da")
            //     .clickContinue()
            //     .errorForProjectManagedByName("The project managed by name cannot contain numbers")
            //     .withProjectManagedByName(dataGenerator.generateAlphaNumeric(101))
            //     .clickContinue()
            //     .errorForProjectManagedByName("The project managed by name must be 100 characters or less")
            //     .withProjectManagedByEmail("da")
            //     .clickContinue()
            //     .errorForProjectManagedByEmail("Enter an email address in the correct format. For example, firstname.surname@education.gov.uk")
            //     .withProjectManagedByEmail(dataGenerator.generateAlphaNumeric(101))
            //     .clickContinue()
            //     .errorForProjectManagedByEmail("The project managed by email must be 100 characters or less")
            //     .withProjectManagedByName("Project Managed By")
            //     .withProjectManagedByEmail("project.manager@education.gov.uk")
            //     .clickContinue();
            
            // cy.executeAccessibilityTests();

            // Logger.log("Check user is back to contact summary page");
            // contactsPage
            //     .hasTitle("Contacts")
            //     .hasSchoolName(project.schoolName)
            //     .hasProjectManagedByName("Project Managed By")
            //     .hasProjectManagedByEmail("project.manager@education.gov.uk")
            //     .hasSchoolChairName("Contact")
            //     .hasSchoolChairEmail("school@chair.com")
            //     .goToProjectsOverviewPage();
            
            // cy.executeAccessibilityTests();

            // Logger.log("Check user is back to projects overview page");
            // projectOverviewPage
            //     .hasSchoolChairOfGovernors("Contact")
            //     .hasProjectManagedBy("Project Managed By");

            // Logger.log("Check existing contacts can be resaved as null strings/empty on ProjectManagedBy Page");
            // projectOverviewPage.changeContacts();

            // cy.executeAccessibilityTests();

            // contactsPage.goToEditSchoolChair();
            // editContactPage
            //     .hasTitle("Edit School chair")
            //     .hasSchoolName(project.schoolName)
            //     .withNullContactName()
            //     .withNullContactEmail()
            //     .clickContinue();
            
            // cy.executeAccessibilityTests();
            
            // contactsPage
            //     .hasTitle("Contacts")
            //     .hasSchoolName(project.schoolName)
            //     .hasProjectManagedByName("Project Managed By")
            //     .hasProjectManagedByEmail("project.manager@education.gov.uk")
            //     .hasSchoolChairName("")
            //     .hasSchoolChairEmail("");
            
            // contactsPage.goToEditProjectManagedBy();

            // cy.executeAccessibilityTests();

            // editProjectManagedByPage
            //     .hasTitle("Edit Project managed by")
            //     .hasSchoolName(project.schoolName)
            //     .withNullProjectManagedByName()
            //     .withNullProjectManagedByEmail()
            //     .clickContinue();
            
            // cy.executeAccessibilityTests();
            
            // contactsPage
            //     .hasTitle("Contacts")
            //     .hasSchoolName(project.schoolName)
            //     .hasProjectManagedByName("")
            //     .hasProjectManagedByEmail("")
            //     .hasSchoolChairName("")
            //     .hasSchoolChairEmail("");


    })
})