# Story 1: landed cost per lot

**As** the commercial team
**I want** the landed cost of each lot on a consignment
**so that** I can see what each product actually cost to the door, in the consignment currency.

## Rules

- Product cost of a lot is quantity multiplied by unit cost.
- Duty on a lot is product cost multiplied by the duty rate for the lot's commodity code (the rate lookup already exists in `src/Calculator/DutyRates.cs`).
- The consignment's freight is shared across lots in proportion to weight: a lot's freight share is freight multiplied by the lot's weight divided by the total weight of all lots.
- Product cost, freight share and duty are each rounded to two decimal places, half away from zero, before they are added.
- A lot's landed cost is product cost plus freight share plus duty.
- The consignment's landed cost is the sum of the lots' landed costs.
- Everything is in the consignment currency. No conversion in this story.

## Worked example

A consignment in EUR with freight of 950.00 and two lots:

| Lot | Commodity code | Quantity | Unit cost | Weight |
|---|---|---|---|---|
| A | 08039010 (bananas) | 100 | 12.50 | 1800 kg |
| B | 08051020 (citrus) | 40 | 9.80 | 600 kg |

| | Product cost | Freight share | Duty | Landed cost |
|---|---|---|---|---|
| A | 1250.00 | 712.50 | 93.75 | 2056.25 |
| B | 392.00 | 237.50 | 25.09 | 654.59 |
| Total | | | | 2710.84 |

The API to build against is `LandedCostCalculator.Calculate(Consignment)` returning `LandedCost`. Both exist in `src/Calculator`; the method throws until you implement it.
