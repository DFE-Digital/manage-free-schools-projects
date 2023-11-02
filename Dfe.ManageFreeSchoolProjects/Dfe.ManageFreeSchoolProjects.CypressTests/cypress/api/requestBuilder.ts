import { v4 } from "uuid";
import { ProjectDetailsRequest } from "./domain";
import { EnvUsername } from "cypress/constants/cypressConstants";

export class RequestBuilder {
    public static createProjectDetails(): ProjectDetailsRequest {
        const result: ProjectDetailsRequest = {
            projectId: v4().substring(0, 8),
            applicationNumber: v4().substring(0, 9),
            applicationWave: v4(),
            createdBy: Cypress.env(EnvUsername),
            schoolName: "(" + v4().substring(0, 4) + ")Test School",
            region: v4(),
            localAuthority: v4(),
        };

        return result;
    }
}
