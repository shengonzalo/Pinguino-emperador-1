export const LoginTest = () => {
    describe('Log into the application', () => {
        beforeEach('Username and password', () => {
            cy.login();
        });
    
        it('Verify successful login', () => {
            cy.url().should('include', '/home');
        });
    });
}