import { defineConfig } from "cypress";

export default defineConfig({
  reporter: 'mochawesome',
  reporterOptions: {
    reportDir: 'cypress/reports',
    overwrite: true,
    html: true,
    json: false
  },
  e2e: {
    setupNodeEvents(on, config) {
      // implement node event listeners here
    },
  },
});