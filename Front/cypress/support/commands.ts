Cypress.Commands.add('login', () => {
    cy.fixture('environment').then((environment) => {
        cy.visit(environment.url);

        cy.get('[data-cy="input-email"]')
            .should('exist')
            .and('be.visible')
            .type(environment.email);

        cy.get('[data-cy="input-pass"]')
            .should('exist')
            .and('be.visible')
            .type(environment.password);

        cy.get('[data-cy="btn-submit"]')
            .should('exist')
            .and('be.visible')
            .click();
    });
});