import { EnvApi } from "cypress/constants/cypressConstants";

export class ApiClient {
    public post<TRequest extends object, TResponse extends object>(
        endpoint: string,
        request: TRequest,
    ): Cypress.Chainable<TResponse> {
        return cy
            .request<TResponse>({
                method: "POST",
                url: Cypress.env(EnvApi) + endpoint,
                headers: this.getHeaders,
                body: request,
            })
            .then((response) => {
                return response.body;
            });
    }

    protected getHeaders(): object {
        const result = {
            "Content-type": "application/json",
        };
        return result;
    }
}
