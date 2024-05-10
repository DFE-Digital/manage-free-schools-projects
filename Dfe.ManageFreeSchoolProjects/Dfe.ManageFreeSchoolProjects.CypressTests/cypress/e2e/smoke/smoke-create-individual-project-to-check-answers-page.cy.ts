import { Logger } from "cypress/common/logger";
import { ProjectRecordCreator } from "cypress/constants/cypressConstants";
import dataGenerator from "cypress/fixtures/dataGenerator";
import createProjectPage from "cypress/pages/createProject/createProjectPage";
import homePage from "cypress/pages/homePage";
import summaryPage from "cypress/pages/task-summary-base";

    describe("Smoke Test Create an individual project - (Up Until Check your answers Page)", () => {
        beforeEach(() => {
            cy.login({ role: ProjectRecordCreator });
            cy.visit('/');
        });

        it("Should Smoke Test go through create a project wizard happy path up until Check your answers page", { tags: ['smoke'] }, () => {
            const temporaryProjectId = dataGenerator.generateTemporaryId();
            const schoolName = dataGenerator.generateSchoolName();
            const TestTrn = "TR00111";

            cy.executeAccessibilityTests();

            homePage.createNewProjects();

            cy.executeAccessibilityTests();

            createProjectPage
                .selectOption("Create one project")
                .continue();

            cy.executeAccessibilityTests();

            Logger.log("Enter Valid project ID");
            createProjectPage
                .enterProjectId(temporaryProjectId)
                .continue();

            cy.executeAccessibilityTests();

            Logger.log("Enter school name");

            createProjectPage
                .enterSchoolName(schoolName)
                .continue();

            createProjectPage
                .titleIs("What is the region of the school?")

            cy.executeAccessibilityTests();

            Logger.log("Select East of England");
            createProjectPage
                .selectOption("East of England")
                .continue();

            createProjectPage
                .titleIs("What is the local authority?")

            cy.executeAccessibilityTests();

            Logger.log("Select Local authority");
            createProjectPage
                .selectOption("Luton")
                .continue();

            cy.executeAccessibilityTests();

            Logger.log("Enter valid trust");
            createProjectPage
                .enterTRN(TestTrn)
                .continue();

            cy.executeAccessibilityTests();

            createProjectPage
                .titleIs("Confirm the trust")
                .hasCorrectTrustName("The James Web School")
                .hasCorrectTrustType("SAT (single academy trust)")
                .selectOption("Yes")
                .continue();

            cy.executeAccessibilityTests();

            Logger.log("Selecting Mainstream school type");
            createProjectPage
                .selectOption("Mainstream")
                .continue();

            cy.executeAccessibilityTests();

            Logger.log("Set class types");
            createProjectPage
                .setNurseryTo("Yes")
                .setSixthFormTo("Yes")
                .setAlternativeProvisionTo("Yes")
                .setSpecialEducationNeedsTo("No")
                .continue();

            cy.executeAccessibilityTests();

            Logger.log("Selecting Secondary school phase");
            createProjectPage
                .selectOption("Secondary")
                .continue();

            cy.executeAccessibilityTests();

            createProjectPage
                .enterAgeRangeFrom("2")
                .enterAgeRangeTo("7")
                .continue();

            cy.executeAccessibilityTests();

            Logger.log("Enter valid capacity");
            createProjectPage
                .enterNurseryCapacity("200")
                .enterReceptionToYear6("0")
                .enterYear7ToYear11("400")
                .enterYear12ToYear14("150")
                .continue();

            cy.executeAccessibilityTests();

            Logger.log("Select Designation");
            createProjectPage
                .selectOption("Designation")
                .continue();

            cy.executeAccessibilityTests();

            Logger.log("Select Greek Orthodox");
            createProjectPage
                .selectOption("Greek Orthodox")
                .continue()

            cy.executeAccessibilityTests();

            Logger.log("Enter valid provisional opening date");
            createProjectPage
                .setProvisionalOpeningDate("1", "10", "2035")
                .continue();

            cy.executeAccessibilityTests();

            Logger.log("Set email");
            createProjectPage
                .enterProjectLeadName("joe bloggs")
                .enterProjectLeadEmail("test.person@education.gov.uk")
                .continue();

            Logger.log("Check answers");
            summaryPage.inOrder()
                .summaryShows("Temporary Project ID").HasValue(temporaryProjectId).HasChangeLink()
                .summaryShows("Current free school name").HasValue(schoolName).HasChangeLink()
                .summaryShows("Region").HasValue("East of England").HasChangeLink()
                .summaryShows("Local authority").HasValue("Luton").HasChangeLink()
                .summaryShows("Trust").HasValue("The James Web School").HasChangeLink()
                .summaryShows("School type").HasValue("Mainstream").HasChangeLink()
                .summaryShows("Nursery").HasValue("Yes").HasChangeLink()
                .summaryShows("Sixth form").HasValue("Yes").HasChangeLink()
                .summaryShows("Alternative provision (specialist resource provision)").HasValue("Yes").HasChangeLink()
                .summaryShows("Special educational needs (specialist resource provision)").HasValue("No").HasChangeLink()
                .summaryShows("School phase").HasValue("Secondary").HasChangeLink()
                .summaryShows("Age range").HasValue("2-7").HasChangeLink()
                .summaryShows("Nursery capacity").HasValue("200").HasChangeLink()
                .summaryShows("Reception to year 6 capacity").HasValue("0").HasChangeLink()
                .summaryShows("Year 7 to year 11 capacity").HasValue("400").HasChangeLink()
                .summaryShows("Year 12 to year 14 capacity").HasValue("150").HasChangeLink()
                .summaryShows("Faith status").HasValue("Designation").HasChangeLink()
                .summaryShows("Faith type").HasValue("Greek Orthodox").HasChangeLink()
                .summaryShows("Provisional opening date agreed with trust").HasValue("1 October 2035").HasChangeLink()
                .summaryShows("Project lead name").HasValue("joe bloggs").HasChangeLink()
                .summaryShows("Project lead email").HasValue("test.person@education.gov.uk").HasChangeLink();

            cy.executeAccessibilityTests();
            
        });
    });
