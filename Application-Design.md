# ShopQA — Application Design Document

**Project:** ShopQA E-Commerce Application  
**Version:** 1.0  
**Last Updated:** 14 October 2026  
**Purpose:** Software development and test automation portfolio

## 1. Project Overview

ShopQA is an e-commerce application built with C# and ASP.NET Core. It demonstrates application development, database management, REST API design, software testing, UI automation, and continuous integration.

The intended customer journey includes browsing products, managing a shopping cart, checking out, and viewing orders. Administrators will manage products and categories.

Payments are simulated. No real payment provider is integrated.

## 2. Technology Stack

- C# and ASP.NET Core Web API
- Entity Framework Core and LINQ
- SQLite for the current database implementation
- SQL Server as a planned database target
- REST API and Swagger/OpenAPI
- xUnit for unit testing
- ASP.NET Core frontend
- Playwright for planned browser automation
- Git and GitHub
- GitHub Actions for CI
- Docker and Docker Compose as planned deployment tools

Authentication, JWT, BCrypt, and additional architecture components will be documented as implemented when they are verified.

## 3. Architecture

The current application contains an ASP.NET Core Web API and a separate test project.

Current components include:

- **ShopQA:** API controllers, models, database context, and application configuration.
- **ShopQA.Tests:** Unit tests for product validation.
- **Frontend:** Storefront development is in progress.
- **GitHub Actions:** Automated build and test workflow.

The planned architecture separates responsibilities into:

- **ShopQA.Core:** Entities, DTOs, interfaces, and business rules.
- **ShopQA.Data:** Database context, configurations, migrations, and seed data.
- **ShopQA.Api:** API controllers, authentication, validation, and Swagger.
- **ShopQA.Web:** Frontend pages for customer and administrator workflows.

The layered architecture will be introduced as development progresses.

## 4. User Roles

### Customer — Planned

- Register and log in.
- Browse and search products.
- Add, update, and remove cart items.
- Complete checkout.
- View order history.
- Cancel eligible orders.

### Administrator — Planned

- Create and update products.
- Deactivate products.
- Manage categories.
- View and filter orders.

## 5. Business Rules

The following are intended business rules for the completed application:

- Currency: South African Rand (ZAR).
- VAT: 15%.
- Prices are stored excluding VAT.
- Shipping costs R60 when the subtotal is below R500.
- Shipping is free when the subtotal is R500 or more.
- Cart quantities range from 1 to 10 per product.
- Checkout must validate stock before confirming an order.
- Orders should retain product-name and unit-price snapshots.
- Products referenced by order history should be deactivated rather than permanently deleted where necessary.
- Passwords must be securely hashed and never returned in API responses.

These rules must be implemented and tested before being considered complete.

## 6. Database Design

The planned database entities are:

- User
- Category
- Product
- Cart
- CartItem
- Order
- OrderItem

The current implementation includes product and category models, an Entity Framework Core database context, and SQLite.

Additional entities and relationships will be added as the application develops.

## 7. Testing Strategy

### Unit Testing
Use xUnit to verify product validation and business rules.

### API Testing
Test HTTP methods, response status codes, request validation, database persistence, and error handling.

### UI Automation
Use Playwright to test customer workflows through the actual frontend.

### Manual Testing
Use Swagger UI and the browser to perform exploratory testing and verify application behaviour.

### Continuous Integration
Use GitHub Actions to build the solution and run automated tests.

### Test Evidence
Maintain test cases, execution results, defect records, and screenshots or logs where appropriate.

## 8. Deployment Strategy

The target deployment will use Docker Compose to run the application components and database. Configuration values and secrets must be managed securely.

Docker deployment remains planned until implemented and verified.

## 9. Current Implementation Status

Implemented or previously verified:

- ASP.NET Core Web API
- Entity Framework Core with SQLite
- Product and category models
- Product CRUD endpoints
- Swagger/OpenAPI
- Product validation using data annotations
- xUnit product validation tests
- GitHub Actions workflow

Frontend product display has been started and should be tracked separately until its required workflows are complete.

Further work includes:

- Complete frontend and customer workflows
- API integration tests
- Authentication and authorization
- Cart and checkout functionality
- Order management
- Playwright UI automation
- Expanded test documentation
- SQL Server and Docker deployment

## 10. Future Improvements

- Complete the frontend and API integration.
- Implement and test the full customer shopping journey.
- Add authentication and role-based authorization.
- Expand automated API and UI test coverage.
- Add detailed test execution reports and defect tracking.
- Complete containerized deployment.
- Maintain reliable CI checks for every code change.
