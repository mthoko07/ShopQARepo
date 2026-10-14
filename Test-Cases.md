# ShopQA — Test Cases

**Version:** 1.0  
**Last Updated:** 14 October 2026

## 1. Purpose

This document records test scenarios, preconditions, execution steps, and expected results for ShopQA.

## 2. Test Case Status

- **Not Run:** Test has not been executed.
- **Pass:** Actual result matches expected result.
- **Fail:** Actual result differs from expected result.
- **Blocked:** Test cannot run because of a dependency or defect.

Record actual results and execution dates when running each test.

## 3. Product Validation Unit Tests

| Test ID | Scenario | Test Data | Expected Result | Status |
|---|---|---|---|---|
| UNIT-PROD-001 | Valid product name | Name with 2–100 characters | Validation passes | Pass — previously reported |
| UNIT-PROD-002 | Missing product name | Empty name | Validation fails | Pass — previously reported |
| UNIT-PROD-003 | Product name too short | One character | Validation fails | Not Run |
| UNIT-PROD-004 | Product name too long | More than 100 characters | Validation fails | Not Run |
| UNIT-PROD-005 | Valid price | Price greater than 0 and within limit | Validation passes | Pass — previously reported |
| UNIT-PROD-006 | Zero price | Price = 0 | Validation fails | Pass — previously reported |
| UNIT-PROD-007 | Negative price | Price below 0 | Validation fails | Not Run |
| UNIT-PROD-008 | Price above maximum | Price = 1,000,000.01 | Validation fails | Not Run |
| UNIT-PROD-009 | Valid stock | Stock = 0 | Validation passes | Pass — previously reported |
| UNIT-PROD-010 | Negative stock | Stock = -1 | Validation fails | Pass — previously reported |
| UNIT-PROD-011 | Stock above maximum | Stock = 100,001 | Validation fails | Not Run |
| UNIT-PROD-012 | Description too long | More than 500 characters | Validation fails | Not Run |

*Note: Previously reported results should be confirmed against the test code and latest execution report before final portfolio publication.*

## 4. Product API Test Cases

**Precondition:** API is running and Swagger UI is accessible.

| Test ID | Scenario | Steps | Expected Result | Status |
|---|---|---|---|---|
| API-PROD-001 | Retrieve all products | Send GET to `/api/Products` | HTTP 200 and product list returned | Not Run |
| API-PROD-002 | Retrieve existing product | Send GET to `/api/Products/{id}` using a valid ID | HTTP 200 and matching product returned | Not Run |
| API-PROD-003 | Retrieve missing product | Send GET using a nonexistent ID | HTTP 404 | Not Run |
| API-PROD-004 | Create valid product | Send POST with valid product data | HTTP 201 and created product or location returned | Not Run |
| API-PROD-005 | Create invalid product | Send POST with invalid price or missing required name | HTTP 400 or the documented validation response | Not Run |
| API-PROD-006 | Update existing product | Send PUT with a valid ID and valid updated data | Success response and changes persisted | Not Run |
| API-PROD-007 | Update missing product | Send PUT using a nonexistent ID | HTTP 404 | Not Run |
| API-PROD-008 | Delete existing product | Send DELETE using an existing ID | HTTP 204 if the controller uses `NoContent()` | Investigate |
| API-PROD-009 | Confirm deleted product | Send GET for the deleted ID | HTTP 404 if deletion is permanent | Not Run |
| API-PROD-010 | Create product with invalid category | Send POST using a nonexistent category ID | Rejection if category existence is enforced | Not Run |

Use the actual routes and response contract defined in the current application. If the API uses a different route, update the test case accordingly.

## 5. Frontend Test Cases

**Precondition:** Frontend is running and connected to the API.

| Test ID | Scenario | Steps | Expected Result | Status |
|---|---|---|---|---|
| UI-PROD-001 | Open storefront | Navigate to the storefront URL | Page loads successfully | Not Run |
| UI-PROD-002 | Display products | Open the product page | Products returned by the API are displayed | In Progress |
| UI-PROD-003 | Handle empty catalogue | Open catalogue when no products exist | A clear empty-state message appears | Not Run |
| UI-PROD-004 | Handle API failure | Make the API unavailable and open the product page | User receives a clear error message | Not Run |
| UI-PROD-005 | Display product details | Inspect a displayed product | Name, price, and other available details are correct | Not Run |

## 6. Planned Shopping Workflow Tests

Execute these tests after the relevant features are implemented.

| Test ID | Scenario | Expected Result | Status |
|---|---|---|---|
| CART-001 | Add an available product to cart | Product and quantity appear in cart | Not Run |
| CART-002 | Set quantity to zero | Invalid quantity is rejected | Not Run |
| CART-003 | Set quantity above ten | Invalid quantity is rejected | Not Run |
| CART-004 | Remove cart item | Item is removed and totals update | Not Run |
| CHECKOUT-001 | Checkout with valid cart | Order is created successfully | Not Run |
| CHECKOUT-002 | Checkout with insufficient stock | Checkout is rejected and stock remains consistent | Not Run |
| CHECKOUT-003 | Check shipping below threshold | R60 shipping is applied when subtotal is below R500 | Not Run |
| CHECKOUT-004 | Check free shipping threshold | Shipping is free at a subtotal of R500 or more | Not Run |
| ORDER-001 | View order history | Customer sees their orders | Not Run |
| ORDER-002 | Cancel an eligible pending order | Order is cancelled according to business rules | Not Run |

## 7. Test Execution Record

For each test, record:

- Test ID.
- Execution date.
- Actual result.
- Pass, Fail, or Blocked status.
- Defect ID, if applicable.
- Screenshot, log, or other evidence.

Keep the execution results in `Test-Execution-Report.md` so that planned test cases and completed test runs remain separate.