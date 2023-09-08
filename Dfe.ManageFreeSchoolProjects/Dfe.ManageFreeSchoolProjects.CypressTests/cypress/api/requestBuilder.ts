import { v4 } from "uuid";
import { ProjectDetails } from "./domain";
import { EnvUsername } from "cypress/constants/cypressConstants";

export class RequestBuilder {
    public static createProjectDetails(): ProjectDetails {
        const result: ProjectDetails = {
            projectId: v4().substring(0, 24),
            applicationNumber: v4().substring(0, 9),
            applicationWave: v4(),
            createdBy: Cypress.env(EnvUsername),
            schoolName: v4(),
            region: v4(),
        };

        return result;
    }
}
