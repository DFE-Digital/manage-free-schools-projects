import { v4 } from "uuid";
import { CreateProjectRiskRequest, ProjectDetailsRequest } from "./domain";
import { EnvUsername } from "cypress/constants/cypressConstants";
import dataGenerator from "cypress/fixtures/dataGenerator";

export class RequestBuilder {
    public static createProjectDetails(): ProjectDetailsRequest {
        const result: ProjectDetailsRequest = {
            projectId: dataGenerator.generateTemporaryId(25),
            applicationNumber: v4().substring(0, 9),
            applicationWave: "FS - Presumption",
            createdBy: Cypress.env(EnvUsername),
            schoolName: dataGenerator.generateSchoolName(),
            TRN: 'TR00111',
            projectAssignedToName: "Test Person",
            projectAssignedToEmail: "test.person.education.gov.uk"
        };

        return result;
    }

    public static CreateProjectRiskRequest(): CreateProjectRiskRequest {
        const result: CreateProjectRiskRequest = {
            overall: {
                riskRating: 1,
                summary: "This is my risk summary"
            },
        };

        return result;
    }
}

