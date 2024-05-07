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

        it("Should go through create a project wizard happy path up until Check your answers page", () => {
            const temporaryProjectId = dataGenerator.generateTemporaryId();
            const schoolName = dataGenerator.generateSchoolName();
            const TestTrn = "TR00111";

            cy.executeAccessibilityTests();

            homePage.createNewProjects();

            Logger.log("Check method required");
            cy.executeAccessibilityTests();

            createProjectPage
                .continue()
                .errorMessage("The method field is required.");

            cy.executeAccessibilityTests();

            createProjectPage
                .selectOption("Create one project")
                .continue();

            Logger.log("Check project id validation");
            createProjectPage
                .titleIs("What is the temporary project ID?")
                .continue()
                .errorMessage("The temporary project ID field is required")
                .enterProjectId("T-00008")
                .continue()
                .errorMessage("Temporary project ID must only include numbers and letters")
                .enterProjectId("T 00009")
                .continue()
                .errorMessage("Temporary project ID must not include spaces")
                .enterProjectId(dataGenerator.generateAlphaNumeric(26))
                .continue()
                .errorMessage("The temporary project ID must be 25 characters or less");

            cy.executeAccessibilityTests();

            Logger.log("Enter Valid project ID");
            createProjectPage
                .enterProjectId(temporaryProjectId)
                .continue();

            Logger.log("Check school name validation");
            createProjectPage
                .titleIs("What is the current free school name?")
                .continue()
                .errorMessage("Enter the current free school name")
                .enterSchoolName("Invalid#")
                .continue()
                .errorMessage("School name must not include special characters other than , ( ) '")
                .continue()
                .enterSchoolName(dataGenerator.generateAlphaNumeric(101))
                .continue()
                .errorMessage("The school name must be 100 characters or less")

            cy.executeAccessibilityTests();
            Logger.log("Enter school name");

            createProjectPage
                .enterSchoolName(schoolName)
                .continue();

            createProjectPage
                .titleIs("What is the region of the school?")
                .continue()
                .errorMessage("Select the region of the free school")

            cy.executeAccessibilityTests();

            Logger.log("Select East of England");
            createProjectPage
                .selectOption("East of England")
                .continue();

            Logger.log("Check local authority required");
            createProjectPage
                .titleIs("What is the local authority?")
                .continue()
                .errorMessage("Select the local authority of the free school");

            cy.executeAccessibilityTests();

            Logger.log("Select Local authority");
            createProjectPage
                .selectOption("Luton")
                .continue();

            Logger.log("Check trust validation");
            createProjectPage
                .titleIs("Search for a trust by TRN")
                .continue()
                .errorMessage("Enter the TRN")
                .enterTRN("POTATO")
                .continue()
                .errorMessage("The TRN must be in the format TRXXXXX")
                .enterTRN("TR00000000")
                .continue()
                .errorMessage("The TRN (trust reference number) must be 7 characters or less")
                .enterTRN("TR99999")
                .continue()
                .errorMessage("Trust ID not found")

            cy.executeAccessibilityTests();

            Logger.log("Enter valid trust");
            createProjectPage
                .enterTRN(TestTrn)
                .continue();

            Logger.log("Confirm trust validation");
            createProjectPage
                .titleIs("Confirm the trust")
                .continue()
                .errorMessage("Confirm that the trust displayed is correct");

            cy.executeAccessibilityTests();

            Logger.log("Selecting No returns to previous page");
            createProjectPage
                .selectOption("No")
                .continue()
                .titleIs("Search for a trust by TRN")
                .enterTRN(TestTrn)
                .continue();

            createProjectPage
                .titleIs("Confirm the trust")
                .back()
                .titleIs("Search for a trust by TRN")
                .enterTRN(TestTrn)
                .continue();

            createProjectPage
                .titleIs("Confirm the trust")
                .hasCorrectTrustName("Aurora Academies Trust")
                .hasCorrectTrustType("MAT (multi-academy trust)")
                .selectOption("Yes")
                .continue();

            Logger.log("Check school type validation");
            createProjectPage
                .titleIs("What is the school type?")
                .continue()
                .errorMessage("Select school type");

            cy.executeAccessibilityTests();

            Logger.log("Selecting Mainstream school type");
            createProjectPage
                .selectOption("Mainstream")
                .continue();
            
            
            Logger.log("Check class types validation");
            createProjectPage
                .titleIs("Will it have any of these provisions?")
                .continue()
                .errorMessage("Select yes if it will have a nursery")
                .errorMessage("Select yes if it will have a sixth form")
                .errorMessage("Select yes if it will have alternative provision")
                .errorMessage("Select yes if it will have special educational needs provision")

            cy.executeAccessibilityTests();

            Logger.log("Set class types");
            createProjectPage
                .setNurseryTo("Yes")
                .setSixthFormTo("Yes")
                .setAlternativeProvisionTo("Yes")
                .setSpecialEducationNeedsTo("No")
                .continue();

            Logger.log("Check school phase validation");
            createProjectPage
                .titleIs("What is the school phase?")
                .continue()
                .errorMessage("Select the school phase");

            cy.executeAccessibilityTests();

            Logger.log("Selecting Secondary school phase");
            createProjectPage
                .selectOption("Secondary")
                .continue();

            Logger.log("Check age range validation-limited");
            createProjectPage
                .titleIs("What is the age range?")
                .enterAgeRangeFrom("2")
                .enterAgeRangeTo("2")
                .continue()
                .errorMessage("'To' age range must be 5 or above")

            cy.executeAccessibilityTests();

            createProjectPage
                .enterAgeRangeFrom("2")
                .enterAgeRangeTo("7")
                .continue();

            Logger.log("Check capacity validation");
            createProjectPage
                .titleIs("What is the capacity?")
                .continue()
                .errorMessage("Enter the Nursery Capacity")
                .errorMessage("Enter the Reception - Year 6 Capacity")
                .errorMessage("Enter the Year 7 - Year 11 Capacity")
                .errorMessage("Enter the Year 12 - Year 14 Capacity")
                .enterNurseryCapacity("A")
                .enterReceptionToYear6("A")
                .enterYear7ToYear11("A")
                .enterYear12ToYear14("A")
                .continue()
                .errorMessage("Nursery capacity must be a number, like 30")
                .errorMessage("Reception to year 6 capacity must be a number, like 30")
                .errorMessage("Year 7 to year 11 capacity must be a number, like 30")
                .errorMessage("Year 12 to year 14 capacity must be a number, like 30")
                .enterNurseryCapacity("-1")
                .enterReceptionToYear6("-1")
                .enterYear7ToYear11("-1")
                .enterYear12ToYear14("-1")
                .continue()
                .errorMessage("Nursery capacity must be between 0 and 9999")
                .errorMessage("Reception to year 6 capacity must be between 0 and 9999")
                .errorMessage("Year 7 to year 11 capacity must be between 0 and 9999")
                .errorMessage("Year 12 to year 14 capacity must be between 0 and 9999")
                .continue();

            cy.executeAccessibilityTests();

            Logger.log("Enter valid capacity");
            createProjectPage
                .enterNurseryCapacity("200")
                .enterReceptionToYear6("0")
                .enterYear7ToYear11("400")
                .enterYear12ToYear14("150")
                .continue();

            Logger.log("Check faith status validation");
            createProjectPage
                .titleIs("What is the faith status?")
                .continue()
                .errorMessage("Select the faith status of the free school");

            cy.executeAccessibilityTests();

            Logger.log("Select Designation");
            createProjectPage
                .selectOption("Designation")
                .continue();

            Logger.log("Check faith type validation");
            createProjectPage
                .titleIs("What is the faith type?")
                .continue()
                .errorMessage("Select the faith type of the free school")
                .selectOption("Other")
                .continue()
                .errorMessage("Enter the other faith type")
                .continue()
                .errorMessage("Enter the other faith type")
                .enterOtherFaith(dataGenerator.generateAlpha(101))
                .continue()
                .errorMessage("Other faith type must be 100 characters or less")
                .enterOtherFaith("2")
                .continue()
                .errorMessage("Other faith type must only contain letters and spaces")

            cy.executeAccessibilityTests();

            Logger.log("Select Greek Orthodox");
            createProjectPage
                .selectOption("Greek Orthodox")
                .continue()

            Logger.log("Check provisional opening date validation");
            createProjectPage
                .titleIs("What is the provisional opening date? (optional)")
                .setProvisionalOpeningDate("1", "10", "2020")
                .continue()
                .errorMessage("Provisional opening date date must be in the future");

            cy.executeAccessibilityTests();

            Logger.log("Enter valid provisional opening date");
            createProjectPage
                .setProvisionalOpeningDate("1", "10", "2035")
                .continue();

            Logger.log("Check project lead validation");
            createProjectPage
                .titleIs("Who is the project lead?")
                .continue()
                .errorMessage("Please enter the name")
                .errorMessage("Please enter an email")
                .enterProjectLeadName("j")
                .enterProjectLeadEmail("test.person@education.gov.uk")
                .continue()
                .errorMessage("Enter the full name, for example John Smith")
                .enterProjectLeadName("joe bloggs")
                .enterProjectLeadEmail("test.person@edunation.gov.uk")
                .continue()
                .errorMessage("Enter an email address in the correct format. For example, firstname.surname@education.gov.uk")
                .enterProjectLeadEmail("@education.gov.uk")
                .continue()
                .errorMessage("Enter an email address in the correct format. For example, firstname.surname@education.gov.uk")

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
                .summaryShows("Trust").HasValue("Aurora Academies Trust").HasChangeLink()
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

            createProjectPage.clickCreateProject();

            Logger.log("Confirm created");
            createProjectPage.hasConfirmedProjectId(temporaryProjectId)
                .hasConfirmedEmailMessage("We have sent a notification email to test.person@education.gov.uk.")

            cy.executeAccessibilityTests();
        });
    });
