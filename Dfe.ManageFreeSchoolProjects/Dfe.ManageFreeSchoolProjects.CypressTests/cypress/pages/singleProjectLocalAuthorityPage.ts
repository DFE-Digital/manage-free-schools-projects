import BasePage from "./BasePage";

class SingleProjectLocalAuthorityPage extends BasePage {
    public checkElementsVisible(): this {
        cy.contains("Back");

        cy.get("h1").contains("Creating a new free school project");
        cy.get("h1").contains("What is the local authority?");

        cy.getByTestId("Isles of Scilly").should("not.be.checked");
        cy.contains("Isles of Scilly");

        cy.getByTestId("Bath and North East Somerset").should("not.be.checked");
        cy.contains("Bath and North East Somerset");
        cy.getByTestId("Isles of Scilly").should("not.be.checked");
        cy.contains("Isles of Scilly");

        cy.getByTestId("Bath and North East Somerset").should("not.be.checked");
        cy.contains("Bath and North East Somerset");

        cy.getByTestId("Bristol").should("not.be.checked");
        cy.contains("Bristol");

        cy.getByTestId("North Somerset").should("not.be.checked");
        cy.contains("North Somerset");

        cy.getByTestId("South Gloucestershire").should("not.be.checked");
        cy.contains("South Gloucestershire");
        cy.getByTestId("Bristol").should("not.be.checked");
        cy.contains("Bristol");

        cy.getByTestId("North Somerset").should("not.be.checked");
        cy.contains("North Somerset");

        cy.getByTestId("South Gloucestershire").should("not.be.checked");
        cy.contains("South Gloucestershire");

        cy.getByTestId("Poole").should("not.be.checked");
        cy.contains("Poole");

        cy.getByTestId("Dorset").should("not.be.checked");
        cy.contains("Dorset");

        cy.getByTestId("Bournemouth, Christchurch and Poole").should("not.be.checked");
        cy.contains("Bournemouth, Christchurch and Poole");

        cy.getByTestId("Wiltshire").should("not.be.checked");
        cy.contains("Wiltshire");
        cy.getByTestId("Poole").should("not.be.checked");
        cy.contains("Poole");

        cy.getByTestId("Dorset").should("not.be.checked");
        cy.contains("Dorset");

        cy.getByTestId("Bournemouth, Christchurch and Poole").should("not.be.checked");
        cy.contains("Bournemouth, Christchurch and Poole");

        cy.getByTestId("Wiltshire").should("not.be.checked");
        cy.contains("Wiltshire");

        cy.getByTestId("Swindon").should("not.be.checked");
        cy.contains("Swindon");

        cy.getByTestId("Devon").should("not.be.checked");
        cy.contains("Devon");

        cy.getByTestId("Plymouth").should("not.be.checked");
        cy.contains("Plymouth");
        cy.getByTestId("Swindon").should("not.be.checked");
        cy.contains("Swindon");

        cy.getByTestId("Devon").should("not.be.checked");
        cy.contains("Devon");

        cy.getByTestId("Plymouth").should("not.be.checked");
        cy.contains("Plymouth");

        cy.getByTestId("Torbay").should("not.be.checked");
        cy.contains("Torbay");

        cy.getByTestId("Cornwall").should("not.be.checked");
        cy.contains("Cornwall");

        cy.getByTestId("Gloucestershire").should("not.be.checked");
        cy.contains("Gloucestershire");

        cy.getByTestId("Somerset").should("not.be.checked");
        cy.contains("Somerset");

        cy.getByTestId("continue");
        
        return this;
    }

    public selectContinue(): this {
        cy.getByTestId("continue").click();

        return this;
    }

    public verifyValidationMessage(): this {
        cy.getById("local-authority-error-link").contains("Select the local authority of the free school");
        cy.getById("local-authority-error").contains("Select the local authority of the free school");

        return this;
    }

    public selectIslesOfScilly(): this {
        cy.getByTestId("Isles of Scilly").click();
        cy.getByTestId("Isles of Scilly").should("be.checked");

        cy.getByTestId("Bath and North East Somerset").should("not.be.checked");
        cy.getByTestId("Bristol").should("not.be.checked");
        cy.getByTestId("North Somerset").should("not.be.checked");
        cy.getByTestId("South Gloucestershire").should("not.be.checked");
        cy.getByTestId("Poole").should("not.be.checked");
        cy.getByTestId("Dorset").should("not.be.checked");
        cy.getByTestId("Bournemouth, Christchurch and Poole").should("not.be.checked");
        cy.getByTestId("Wiltshire").should("not.be.checked");
        cy.getByTestId("Swindon").should("not.be.checked");
        cy.getByTestId("Devon").should("not.be.checked");
        cy.getByTestId("Plymouth").should("not.be.checked");
        cy.getByTestId("Torbay").should("not.be.checked");
        cy.getByTestId("Cornwall").should("not.be.checked");
        cy.getByTestId("Gloucestershire").should("not.be.checked");
        cy.getByTestId("Somerset").should("not.be.checked");


    
        return this;
    }

    public selectBathAndNorthEastSomerset(): this {
        cy.getByTestId("Bath and North East Somerset").click();
        cy.getByTestId("Bath and North East Somerset").should("be.checked");

        cy.getByTestId("Isles of Scilly").should("not.be.checked");
        cy.getByTestId("Bristol").should("not.be.checked");
        cy.getByTestId("North Somerset").should("not.be.checked");
        cy.getByTestId("South Gloucestershire").should("not.be.checked");
        cy.getByTestId("Poole").should("not.be.checked");
        cy.getByTestId("Dorset").should("not.be.checked");
        cy.getByTestId("Bournemouth, Christchurch and Poole").should("not.be.checked");
        cy.getByTestId("Wiltshire").should("not.be.checked");
        cy.getByTestId("Swindon").should("not.be.checked");
        cy.getByTestId("Devon").should("not.be.checked");
        cy.getByTestId("Plymouth").should("not.be.checked");
        cy.getByTestId("Torbay").should("not.be.checked");
        cy.getByTestId("Cornwall").should("not.be.checked");
        cy.getByTestId("Gloucestershire").should("not.be.checked");
        cy.getByTestId("Somerset").should("not.be.checked");

    
        return this;
    }

    public selectBristol(): this {
        cy.getByTestId("Bristol").click();
        cy.getByTestId("Bristol").should("be.checked");

        cy.getByTestId("Isles of Scilly").should("not.be.checked");
        cy.getByTestId("Bath and North East Somerset").should("not.be.checked");
        cy.getByTestId("North Somerset").should("not.be.checked");
        cy.getByTestId("South Gloucestershire").should("not.be.checked");
        cy.getByTestId("Poole").should("not.be.checked");
        cy.getByTestId("Dorset").should("not.be.checked");
        cy.getByTestId("Bournemouth, Christchurch and Poole").should("not.be.checked");
        cy.getByTestId("Wiltshire").should("not.be.checked");
        cy.getByTestId("Swindon").should("not.be.checked");
        cy.getByTestId("Devon").should("not.be.checked");
        cy.getByTestId("Plymouth").should("not.be.checked");
        cy.getByTestId("Torbay").should("not.be.checked");
        cy.getByTestId("Cornwall").should("not.be.checked");
        cy.getByTestId("Gloucestershire").should("not.be.checked");
        cy.getByTestId("Somerset").should("not.be.checked");

    
    
        return this;
    }

    public selectNorthSomerset(): this {
        cy.getByTestId("North Somerset").click();
        cy.getByTestId("North Somerset").should("be.checked");

        cy.getByTestId("Isles of Scilly").should("not.be.checked");
        cy.getByTestId("Bath and North East Somerset").should("not.be.checked");
        cy.getByTestId("Bristol").should("not.be.checked");
        cy.getByTestId("South Gloucestershire").should("not.be.checked");
        cy.getByTestId("Poole").should("not.be.checked");
        cy.getByTestId("Dorset").should("not.be.checked");
        cy.getByTestId("Bournemouth, Christchurch and Poole").should("not.be.checked");
        cy.getByTestId("Wiltshire").should("not.be.checked");
        cy.getByTestId("Swindon").should("not.be.checked");
        cy.getByTestId("Devon").should("not.be.checked");
        cy.getByTestId("Plymouth").should("not.be.checked");
        cy.getByTestId("Torbay").should("not.be.checked");
        cy.getByTestId("Cornwall").should("not.be.checked");
        cy.getByTestId("Gloucestershire").should("not.be.checked");
        cy.getByTestId("Somerset").should("not.be.checked");
    
        return this;
    }

    public selectSouthGloucestershire(): this {
        cy.getByTestId("South Gloucestershire").click();
        cy.getByTestId("South Gloucestershire").should("be.checked");

        cy.getByTestId("Isles of Scilly").should("not.be.checked");
        cy.getByTestId("Bath and North East Somerset").should("not.be.checked");
        cy.getByTestId("Bristol").should("not.be.checked");
        cy.getByTestId("North Somerset").should("not.be.checked");
        cy.getByTestId("Poole").should("not.be.checked");
        cy.getByTestId("Dorset").should("not.be.checked");
        cy.getByTestId("Bournemouth, Christchurch and Poole").should("not.be.checked");
        cy.getByTestId("Wiltshire").should("not.be.checked");
        cy.getByTestId("Swindon").should("not.be.checked");
        cy.getByTestId("Devon").should("not.be.checked");
        cy.getByTestId("Plymouth").should("not.be.checked");
        cy.getByTestId("Torbay").should("not.be.checked");
        cy.getByTestId("Cornwall").should("not.be.checked");
        cy.getByTestId("Gloucestershire").should("not.be.checked");
        cy.getByTestId("Somerset").should("not.be.checked");
    
    
        return this;
    }

    public selectPoole(): this {
        cy.getByTestId("Poole").click();
        cy.getByTestId("Poole").should("be.checked");

        cy.getByTestId("Isles of Scilly").should("not.be.checked");
        cy.getByTestId("Bath and North East Somerset").should("not.be.checked");
        cy.getByTestId("Bristol").should("not.be.checked");
        cy.getByTestId("North Somerset").should("not.be.checked");
        cy.getByTestId("South Gloucestershire").should("not.be.checked");
        cy.getByTestId("Dorset").should("not.be.checked");
        cy.getByTestId("Bournemouth, Christchurch and Poole").should("not.be.checked");
        cy.getByTestId("Wiltshire").should("not.be.checked");
        cy.getByTestId("Swindon").should("not.be.checked");
        cy.getByTestId("Devon").should("not.be.checked");
        cy.getByTestId("Plymouth").should("not.be.checked");
        cy.getByTestId("Torbay").should("not.be.checked");
        cy.getByTestId("Cornwall").should("not.be.checked");
        cy.getByTestId("Gloucestershire").should("not.be.checked");
        cy.getByTestId("Somerset").should("not.be.checked");
    
    
        return this;
    }

    public selectDorset(): this {
        cy.getByTestId("Dorset").click();
        cy.getByTestId("Dorset").should("be.checked");

        cy.getByTestId("Isles of Scilly").should("not.be.checked");
        cy.getByTestId("Bath and North East Somerset").should("not.be.checked");
        cy.getByTestId("Bristol").should("not.be.checked");
        cy.getByTestId("North Somerset").should("not.be.checked");
        cy.getByTestId("South Gloucestershire").should("not.be.checked");
        cy.getByTestId("Poole").should("not.be.checked");
        cy.getByTestId("Bournemouth, Christchurch and Poole").should("not.be.checked");
        cy.getByTestId("Wiltshire").should("not.be.checked");
        cy.getByTestId("Swindon").should("not.be.checked");
        cy.getByTestId("Devon").should("not.be.checked");
        cy.getByTestId("Plymouth").should("not.be.checked");
        cy.getByTestId("Torbay").should("not.be.checked");
        cy.getByTestId("Cornwall").should("not.be.checked");
        cy.getByTestId("Gloucestershire").should("not.be.checked");
        cy.getByTestId("Somerset").should("not.be.checked");
    
    
        return this;
    }

    public selectBournemouthChristchurchAndPoole(): this {
        cy.getByTestId("Bournemouth, Christchurch and Poole").click();
        cy.getByTestId("Bournemouth, Christchurch and Poole").should("be.checked");

        cy.getByTestId("Isles of Scilly").should("not.be.checked");
        cy.getByTestId("Bath and North East Somerset").should("not.be.checked");
        cy.getByTestId("Bristol").should("not.be.checked");
        cy.getByTestId("North Somerset").should("not.be.checked");
        cy.getByTestId("South Gloucestershire").should("not.be.checked");
        cy.getByTestId("Poole").should("not.be.checked");
        cy.getByTestId("Dorset").should("not.be.checked");
        cy.getByTestId("Wiltshire").should("not.be.checked");
        cy.getByTestId("Swindon").should("not.be.checked");
        cy.getByTestId("Devon").should("not.be.checked");
        cy.getByTestId("Plymouth").should("not.be.checked");
        cy.getByTestId("Torbay").should("not.be.checked");
        cy.getByTestId("Cornwall").should("not.be.checked");
        cy.getByTestId("Gloucestershire").should("not.be.checked");
        cy.getByTestId("Somerset").should("not.be.checked");
    
    
        return this;
    }

    public selectWiltshire(): this {
        cy.getByTestId("Wiltshire").click();
        cy.getByTestId("Wiltshire").should("be.checked");

        cy.getByTestId("Isles of Scilly").should("not.be.checked");
        cy.getByTestId("Bath and North East Somerset").should("not.be.checked");
        cy.getByTestId("Bristol").should("not.be.checked");
        cy.getByTestId("North Somerset").should("not.be.checked");
        cy.getByTestId("South Gloucestershire").should("not.be.checked");
        cy.getByTestId("Poole").should("not.be.checked");
        cy.getByTestId("Dorset").should("not.be.checked");
        cy.getByTestId("Bournemouth, Christchurch and Poole").should("not.be.checked");
        cy.getByTestId("Swindon").should("not.be.checked");
        cy.getByTestId("Devon").should("not.be.checked");
        cy.getByTestId("Plymouth").should("not.be.checked");
        cy.getByTestId("Torbay").should("not.be.checked");
        cy.getByTestId("Cornwall").should("not.be.checked");
        cy.getByTestId("Gloucestershire").should("not.be.checked");
        cy.getByTestId("Somerset").should("not.be.checked");
    
    
        return this;
    }

    public selectSwindon(): this {
        cy.getByTestId("Swindon").click();
        cy.getByTestId("Swindon").should("be.checked");

        cy.getByTestId("Isles of Scilly").should("not.be.checked");
        cy.getByTestId("Bath and North East Somerset").should("not.be.checked");
        cy.getByTestId("Bristol").should("not.be.checked");
        cy.getByTestId("North Somerset").should("not.be.checked");
        cy.getByTestId("South Gloucestershire").should("not.be.checked");
        cy.getByTestId("Poole").should("not.be.checked");
        cy.getByTestId("Dorset").should("not.be.checked");
        cy.getByTestId("Bournemouth, Christchurch and Poole").should("not.be.checked");
        cy.getByTestId("Wiltshire").should("not.be.checked");
        cy.getByTestId("Devon").should("not.be.checked");
        cy.getByTestId("Plymouth").should("not.be.checked");
        cy.getByTestId("Torbay").should("not.be.checked");
        cy.getByTestId("Cornwall").should("not.be.checked");
        cy.getByTestId("Gloucestershire").should("not.be.checked");
        cy.getByTestId("Somerset").should("not.be.checked");
    
    
        return this;
    }

    public selectDevon(): this {
        cy.getByTestId("Devon").click();
        cy.getByTestId("Devon").should("be.checked");

        cy.getByTestId("Isles of Scilly").should("not.be.checked");
        cy.getByTestId("Bath and North East Somerset").should("not.be.checked");
        cy.getByTestId("Bristol").should("not.be.checked");
        cy.getByTestId("North Somerset").should("not.be.checked");
        cy.getByTestId("South Gloucestershire").should("not.be.checked");
        cy.getByTestId("Poole").should("not.be.checked");
        cy.getByTestId("Dorset").should("not.be.checked");
        cy.getByTestId("Bournemouth, Christchurch and Poole").should("not.be.checked");
        cy.getByTestId("Wiltshire").should("not.be.checked");
        cy.getByTestId("Swindon").should("not.be.checked");
        cy.getByTestId("Plymouth").should("not.be.checked");
        cy.getByTestId("Torbay").should("not.be.checked");
        cy.getByTestId("Cornwall").should("not.be.checked");
        cy.getByTestId("Gloucestershire").should("not.be.checked");
        cy.getByTestId("Somerset").should("not.be.checked");
    
    
        return this;
    }


    public selectPlymouth(): this {
        cy.getByTestId("Plymouth").click();
        cy.getByTestId("Plymouth").should("be.checked");

        cy.getByTestId("Isles of Scilly").should("not.be.checked");
        cy.getByTestId("Bath and North East Somerset").should("not.be.checked");
        cy.getByTestId("Bristol").should("not.be.checked");
        cy.getByTestId("North Somerset").should("not.be.checked");
        cy.getByTestId("South Gloucestershire").should("not.be.checked");
        cy.getByTestId("Poole").should("not.be.checked");
        cy.getByTestId("Dorset").should("not.be.checked");
        cy.getByTestId("Bournemouth, Christchurch and Poole").should("not.be.checked");
        cy.getByTestId("Wiltshire").should("not.be.checked");
        cy.getByTestId("Swindon").should("not.be.checked");
        cy.getByTestId("Devon").should("not.be.checked");
        cy.getByTestId("Torbay").should("not.be.checked");
        cy.getByTestId("Cornwall").should("not.be.checked");
        cy.getByTestId("Gloucestershire").should("not.be.checked");
        cy.getByTestId("Somerset").should("not.be.checked");
    
    
        return this;
    }

    public selectTorbay(): this {
        cy.getByTestId("Torbay").click();
        cy.getByTestId("Torbay").should("be.checked");

        cy.getByTestId("Isles of Scilly").should("not.be.checked");
        cy.getByTestId("Bath and North East Somerset").should("not.be.checked");
        cy.getByTestId("Bristol").should("not.be.checked");
        cy.getByTestId("North Somerset").should("not.be.checked");
        cy.getByTestId("South Gloucestershire").should("not.be.checked");
        cy.getByTestId("Poole").should("not.be.checked");
        cy.getByTestId("Dorset").should("not.be.checked");
        cy.getByTestId("Bournemouth, Christchurch and Poole").should("not.be.checked");
        cy.getByTestId("Wiltshire").should("not.be.checked");
        cy.getByTestId("Swindon").should("not.be.checked");
        cy.getByTestId("Devon").should("not.be.checked");
        cy.getByTestId("Plymouth").should("not.be.checked");
        cy.getByTestId("Cornwall").should("not.be.checked");
        cy.getByTestId("Gloucestershire").should("not.be.checked");
        cy.getByTestId("Somerset").should("not.be.checked");
    
    
        return this;
    }

    public selectCornwall(): this {
        cy.getByTestId("Cornwall").click();
        cy.getByTestId("Cornwall").should("be.checked");

        cy.getByTestId("Isles of Scilly").should("not.be.checked");
        cy.getByTestId("Bath and North East Somerset").should("not.be.checked");
        cy.getByTestId("Bristol").should("not.be.checked");
        cy.getByTestId("North Somerset").should("not.be.checked");
        cy.getByTestId("South Gloucestershire").should("not.be.checked");
        cy.getByTestId("Poole").should("not.be.checked");
        cy.getByTestId("Dorset").should("not.be.checked");
        cy.getByTestId("Bournemouth, Christchurch and Poole").should("not.be.checked");
        cy.getByTestId("Wiltshire").should("not.be.checked");
        cy.getByTestId("Swindon").should("not.be.checked");
        cy.getByTestId("Devon").should("not.be.checked");
        cy.getByTestId("Plymouth").should("not.be.checked");
        cy.getByTestId("Torbay").should("not.be.checked");
        cy.getByTestId("Gloucestershire").should("not.be.checked");
        cy.getByTestId("Somerset").should("not.be.checked");
    
    
        return this;
    }

    public selectGloucestershire(): this {
        cy.getByTestId("Gloucestershire").click();
        cy.getByTestId("Gloucestershire").should("be.checked");

        cy.getByTestId("Isles of Scilly").should("not.be.checked");
        cy.getByTestId("Bath and North East Somerset").should("not.be.checked");
        cy.getByTestId("Bristol").should("not.be.checked");
        cy.getByTestId("North Somerset").should("not.be.checked");
        cy.getByTestId("South Gloucestershire").should("not.be.checked");
        cy.getByTestId("Poole").should("not.be.checked");
        cy.getByTestId("Dorset").should("not.be.checked");
        cy.getByTestId("Bournemouth, Christchurch and Poole").should("not.be.checked");
        cy.getByTestId("Wiltshire").should("not.be.checked");
        cy.getByTestId("Swindon").should("not.be.checked");
        cy.getByTestId("Devon").should("not.be.checked");
        cy.getByTestId("Plymouth").should("not.be.checked");
        cy.getByTestId("Torbay").should("not.be.checked");
        cy.getByTestId("Cornwall").should("not.be.checked");
        cy.getByTestId("Somerset").should("not.be.checked");
    
    
        return this;
    }

    public selectSomerset(): this {
        cy.getByTestId("Somerset").click();
        cy.getByTestId("Somerset").should("be.checked");

        cy.getByTestId("Isles of Scilly").should("not.be.checked");
        cy.getByTestId("Bath and North East Somerset").should("not.be.checked");
        cy.getByTestId("Bristol").should("not.be.checked");
        cy.getByTestId("North Somerset").should("not.be.checked");
        cy.getByTestId("South Gloucestershire").should("not.be.checked");
        cy.getByTestId("Poole").should("not.be.checked");
        cy.getByTestId("Dorset").should("not.be.checked");
        cy.getByTestId("Bournemouth, Christchurch and Poole").should("not.be.checked");
        cy.getByTestId("Wiltshire").should("not.be.checked");
        cy.getByTestId("Swindon").should("not.be.checked");
        cy.getByTestId("Devon").should("not.be.checked");
        cy.getByTestId("Plymouth").should("not.be.checked");
        cy.getByTestId("Torbay").should("not.be.checked");
        cy.getByTestId("Cornwall").should("not.be.checked");
        cy.getByTestId("Gloucestershire").should("not.be.checked");
    
    
        return this;
    }


}

const singleProjectLocalAuthorityPage = new SingleProjectLocalAuthorityPage();

export default singleProjectLocalAuthorityPage;