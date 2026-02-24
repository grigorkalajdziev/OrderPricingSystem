Order Pricing System - Technical Assignment

Overview

A .NET Core Web API built for KODEQA TECHNOLOGIES to handle e-commerce order pricing. 
This system calculates subtotals, tiered discounts based on unit quantity and price thresholds, and country-specific tax rates.

How to Run

1. Ensure you have the .NET SDK installed.
2. Open your terminal/command prompt in the root folder (OrderPricingSystem).
3. Run the application using the command:dotnet run
4. The application will start. You can access the Swagger UI to test the endpoint at the URL provided in the terminal (usually http://localhost:5238/swagger or similar).   

Calculated Test Results

As per the assignment requirements, here are the calculated results for Product PROD-001 (Price: 12.00 EUR):

Test Case,Inputs,Subtotal,Discount,Tax,Final Price
Case 1,"55 units, MK",660.00,66.00 (10%),106.92 (18%),700.92
Case 2,"100 units, DE","1,200.00",180.00 (15%),204.00 (20%),"1,224.00"
Case 3,"25 units, USA",300.00,0.00 (0%),30.00 (10%),330.00

Reason for Case 3: No discount is applied because the subtotal (300.00 EUR) is below the required 500.00 EUR threshold.

Bugs Fixed & Logic Implemented

Subtotal Threshold Rule: Fixed the logic to ensure discounts are only applied if the subtotal is >= 500 EUR.
Tiered Discount Logic: Corrected the quantity-based tiers to properly apply 5% (10–49 units), 10% (50–99 units), and 15% (100+ units) without overwriting the values.
Tax Calculation Base: Fixed the tax formula to ensure it is calculated on the amount AFTER the discount is deducted.
Product Data Retrieval: Fully implemented the GetProduct() method to read and deserialize product information from the products.json file.
Response Construction: Built the BuildResponse() method to match the exact JSON structure required by the specification.
Validation & Robustness: Added error handling for missing products, invalid quantities, and unsupported countries to prevent system crashes.
JSON Case-Sensitivity: Resolved the "Value cannot be null" error by configuring the JSON serializer to be case-insensitive when reading product data.

