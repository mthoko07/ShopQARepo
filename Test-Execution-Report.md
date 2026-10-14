# ShopQA — Test Execution Report

**Project:** ShopQA E-Commerce Application  
**Report Version:** 1.0  
**Last Updated:** 14 October 2026

## 1. Purpose

This report records the execution of ShopQA tests, including actual outcomes, defects, and outstanding testing activities.

## 2. Test Environment

- Operating System: Windows
- IDE: Visual Studio
- Framework: ASP.NET Core / .NET
- Database: SQLite
- API documentation: Swagger/OpenAPI
- Unit Testing: xUnit
- CI: GitHub Actions
- UI Automation: Playwright planned

## 3. Execution Summary

| Test Area | Current Result | Notes |
|---|---|---|
| Product validation unit tests | Previously reported as passing | Confirm with a fresh test run |
| API manual testing | Partially tested | Confirm response codes and persistence |
| API integration automation | Pending | Add automated HTTP tests |
| Frontend product display | In progress | Verify product data and error handling |
| Playwright UI automation | Not run | Framework not yet verified |
| Cart and checkout | Not run | Features pending implementation |
| GitHub Actions | Previously reported as successful | Confirm latest workflow run |

## 4. Previously Observed Results

These are results reported during development. They should be confirmed before being treated as final execution evidence.

| Area | Observation | Follow-up |
|---|---|---|
| Product validation | xUnit validation tests passed | Run the complete test suite again |
| API creation | HTTP 201 was observed for a valid create request | Confirm the created record persists |
| API retrieval | HTTP 200 was observed for successful retrieval | Verify response body and product data |
| Invalid API request | HTTP 400 was observed | Confirm validation response details |
| Product deletion | HTTP 200 was observed, followed by HTTP 404 on retrieval | Investigate whether DELETE should return HTTP 204 |
| CI pipeline | A GitHub Actions workflow completed successfully | Verify the latest run and test results |

## 5. Defect and Investigation Log

### DEF-001 — Product DELETE Response

**Status:** Investigate

**Observation:** The DELETE request returned HTTP 200, while a subsequent GET for the same product returned HTTP 404.

**Expected result:** If the endpoint is intended to return `NoContent()`, DELETE should return HTTP 204.

**Impact:** The product appears to have been deleted, but the response status does not match the intended API contract.

**Next actions:**

1. Inspect the DELETE controller method.
2. Confirm the return statement.
3. Restart the API.
4. Delete an existing test product.
5. Record the actual DELETE response.
6. Confirm that the deleted product can no longer be retrieved.

**Resolution:** Leave open until the expected behaviour is verified.

## 6. Outstanding Test Activities

- [ ] Run all xUnit tests and record the result.
- [ ] Verify all product CRUD endpoints.
- [ ] Confirm validation for missing and invalid fields.
- [ ] Verify database persistence after create and update operations.
- [ ] Investigate the DELETE response.
- [ ] Expand automated API integration tests.
- [ ] Verify the frontend product display.
- [ ] Implement Playwright browser tests.
- [ ] Test cart, checkout, and order workflows after implementation.
- [ ] Confirm the latest GitHub Actions build and test results.

## 7. Evidence and Traceability

Store supporting evidence in the repository when appropriate:

- Test output and CI run links.
- Screenshots of application behaviour.
- Swagger request and response examples.
- Playwright screenshots and traces.
- Defect records and retest results.

Suggested folder structure:

`docs/evidence/`

Avoid storing passwords, access tokens, personal information, or other secrets in screenshots and logs.

## 8. Report Maintenance

Update this report after each meaningful test run.

For every executed test, record the date, actual result, status, and related defect ID. Never mark a test as passed based only on its expected result; verify the actual outcome.

The report should reflect the current state of ShopQA rather than planned functionality.