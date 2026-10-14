# ShopQA — Software Test Plan

**Project:** ShopQA E-Commerce Application  
**Version:** 1.0  
**Date:** 14 October 2026

## 1. Purpose

This test plan defines the scope, approach, environment, and completion criteria for testing ShopQA. The objective is to demonstrate practical software testing and test automation skills.

## 2. Test Objectives

- Verify that the application behaves according to its requirements.
- Validate product data and business rules.
- Verify API responses, status codes, and error handling.
- Test the frontend from a user's perspective.
- Identify, record, and retest defects.
- Automate repeatable tests and execute them through GitHub Actions.

## 3. Scope of Testing

### In Scope

- Product creation, retrieval, updating, and deletion.
- Product field validation and boundary values.
- Category references and product data persistence.
- API response codes and invalid requests.
- Frontend product display and navigation.
- Shopping cart and checkout when implemented.
- Order management when implemented.
- Regression testing after code changes.
- Automated builds and tests in GitHub Actions.

### Out of Scope for the Current Stage

- Real payment processing.
- Production performance and load testing.
- Production security certification.
- Features that have not yet been implemented.

## 4. Test Levels and Types

### Unit Testing
Use xUnit to verify product validation rules, including required fields, price limits, and stock limits.

### API Integration Testing
Verify endpoints through HTTP requests and confirm responses, validation, and database behaviour. These tests will be added or expanded as needed.

### UI and End-to-End Testing
Use Playwright to automate workflows through the frontend, including viewing products and, when implemented, adding items to the cart and completing checkout.

### Manual Testing
Use Swagger UI and the browser to explore functionality and verify expected behaviour.

### Regression Testing
Rerun relevant automated tests after changes to confirm that existing functionality still works.

### Continuous Integration
Use GitHub Actions to build the solution and execute automated tests.

## 5. Test Environment

- Operating system: Windows
- IDE: Visual Studio
- Language and framework: C# and ASP.NET Core
- Database: SQLite in the current implementation
- API documentation: Swagger/OpenAPI
- Unit testing: xUnit
- Source control: Git and GitHub
- CI: GitHub Actions
- UI automation: Playwright, planned

## 6. Entry Criteria

Testing can begin when:

- The relevant code has been saved and builds successfully.
- Required database configuration is available.
- The application or test project can be started.
- The test cases and expected results are defined.

## 7. Exit Criteria

A feature can be considered tested when:

- Its planned test cases have been executed.
- Actual results have been recorded.
- Critical defects have been resolved or explicitly documented.
- Relevant regression tests pass.
- Test evidence is retained where useful.

Passing tests does not by itself prove that the application is defect-free.

## 8. Defect Management

For each defect, record:

- Defect ID and title.
- Environment and application version.
- Steps to reproduce.
- Expected result.
- Actual result.
- Severity and priority.
- Screenshot, log, or other evidence.
- Status and retest result.

## 9. Risks

- Frontend changes may affect existing API behaviour.
- Database changes may cause migration or persistence issues.
- Invalid requests may not always return the expected status codes.
- Tests may pass locally but fail in CI due to environment differences.
- Planned features cannot be fully tested until implemented.

## 10. Test Deliverables

- Application Design Document.
- Test Plan.
- Test Cases.
- Project Tracker.
- Test Execution Report.
- Automated test source code.
- GitHub Actions execution results.
- Defect records and supporting evidence.

## 11. Current Test Status

- Product validation unit tests: previously reported as passing.
- GitHub Actions workflow: previously reported as successful.
- Manual product API testing: performed; verify and record final expected status codes.
- API integration tests: to be added or expanded.
- Playwright UI automation: planned.
- Cart, checkout, and order tests: pending implementation of those features.

Update this plan as ShopQA develops and the test strategy changes.