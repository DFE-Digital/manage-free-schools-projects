import { v4 } from "uuid";
import { ProjectDetails } from "./domain";
import { EnvUsername } from "cypress/constants/cypressConstants";

export class RequestBuilder {
    public static createProjectDetails(): ProjectDetails {
        const result: ProjectDetails = {
            ProjectId: v4().substring(0, 24),
            ApplicationNumber: v4().substring(0, 9),
            ApplicationWave: v4(),
            CreatedBy: Cypress.env(EnvUsername),
            SchoolName: v4(),
        };

        return result;
    }
}
