Order Pricing System – Technical Assignment
Overview

This project is a .NET Core Web API developed as part of a technical assignment for KODEQA TECHNOLOGIES.

The system handles e-commerce order pricing by:

Calculating order subtotal

Applying tiered discounts based on quantity

Enforcing a minimum subtotal threshold for discounts

Applying country-specific tax rates

Returning a structured JSON pricing response

The solution follows clean structure principles, separation of concerns, and includes proper validation and error handling.

How to Run

Ensure you have the .NET SDK installed.

Open a terminal/command prompt in the root folder (OrderPricingSystem).

Run the application using: dotnet run

Once started, the Swagger UI will be available at:
https://localhost:{port}/swagger

(The port number may vary depending on your environment.)

API Endpoint

GET /api/pricing/calculate

Query Parameters

productId (string) – Product identifier (e.g., PROD-001)

quantity (int) – Number of units

country (string) – Country code (e.g., MK, DE, USA)

Example Request

GET /api/pricing/calculate?productId=PROD-001&quantity=55&country=MK

Pricing Rules Implemented
Subtotal Calculation

Subtotal = Unit Price × Quantity

Discount Rules

Discount is applied only if subtotal ≥ 500 EUR.

Quantity tiers:

10–49 units → 5%

50–99 units → 10%

100+ units → 15%

Tax Calculation

Tax is calculated on the amount after the discount is applied.

Country-specific tax rates:

MK → 18%

DE → 20%

USA → 10%

Calculated Test Results

Product: PROD-001
Unit Price: 12.00 EUR

Case 1 – 55 units, MK
Subtotal: 660.00
Discount: 66.00 (10%)
Tax: 106.92 (18%)
Final Price: 700.92

Case 2 – 100 units, DE
Subtotal: 1,200.00
Discount: 180.00 (15%)
Tax: 204.00 (20%)
Final Price: 1,224.00

Case 3 – 25 units, USA
Subtotal: 300.00
Discount: 0.00 (0%)
Tax: 30.00 (10%)
Final Price: 330.00

Explanation for Case 3:
No discount is applied because the subtotal (300.00 EUR) is below the required 500.00 EUR threshold.

Bugs Fixed & Logic Improvements

Subtotal Threshold Rule – Ensured discounts are applied only when subtotal ≥ 500 EUR.

Tiered Discount Logic – Corrected discount tiers to prevent overwriting percentage values.

Tax Calculation Base – Tax is calculated on the discounted amount.

Product Data Retrieval – Implemented product loading from products.json.

Response Construction – Structured JSON response according to specification.

Validation & Robustness – Added validation for missing product, invalid quantity, and unsupported country.

JSON Case-Sensitivity Fix – Configured JSON deserialization to be case-insensitive.

Project Structure

Controllers → API endpoints

Services → Business logic

Models → Request & response models

Data → products.json

Program.cs → Dependency Injection configuration 
