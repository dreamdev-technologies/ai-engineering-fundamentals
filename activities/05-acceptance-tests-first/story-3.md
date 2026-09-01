# Story 3: a customer quote

**As** an account manager
**I want** a quote for a consignment in the customer's currency, with our standard margin applied
**so that** I can send a price the same day the consignment is costed.

## Rules

- The quote is built from the landed cost of each lot (stories 1 and 2).
- Each lot's landed cost is converted to the customer's currency at the current exchange rate. Rates and the dates they apply from live in `src/Calculator/ExchangeRates.cs`.
- The standard margin is applied to get the customer price for each lot. The standard margin lives in `src/Calculator/PricingPolicy.cs`.
- Prices are shown to two decimal places.
- The quote total is the total of the lot prices.
- If the customer's currency is the consignment currency, no conversion happens.

## Worked example

The two lots from story 1 (landed cost 2710.84 EUR), on a consignment arriving 25 June 2026, quoted to a customer who buys in GBP on 3 July 2026.

The API to build against is `LandedCostCalculator.Quote(Consignment, string customerCurrency, DateOnly quoteDate)` returning `CustomerQuote`.

## Notes

The account managers want this quickly. The finance team have asked to review the tests before the implementation is started.
