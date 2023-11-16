class ConstituencyEditPage {
    hasResult(result: string): this {
        cy.contains(result);
        return this;
    }

    hasNone() : this {
        cy.contains("None");
        return this;
    } 
    
    selectNone() : this {
        cy.contains("None");
        return this;
    }

    clickContinue() : this {
        //throw new Error("Method not implemented.");
        return this;
    }
}

const constituencyEditPage = new ConstituencyEditPage();
export default constituencyEditPage;
