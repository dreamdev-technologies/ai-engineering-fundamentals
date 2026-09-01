# Story 2: freight that does not divide evenly

**As** finance
**I want** the freight shares on a consignment to add up to the freight exactly
**so that** the invoice reconciles to the cent against the carrier's bill.

## Rules

- Story 1 still holds: freight is shared in proportion to weight and each share is rounded to two decimal places.
- After rounding, the shares must add up to the consignment's freight, exactly. Freight of 100.00 across three lots of equal weight cannot be invoiced as three lots of 33.33.
- A lot can have a weight of zero (packaging and service lines are booked as lots). A zero-weight lot attracts no freight.
- A consignment with no lots has a landed cost of zero.

## Worked example

A consignment in EUR with freight of 100.00 and three lots of 1000 kg each, all bananas at 10.00 per case, 10 cases each. Product cost 100.00 per lot, duty 7.50 per lot. The three freight shares add up to 100.00.

## Notes

The commercial and logistics teams were asked what should happen in the cases this story does not spell out. They gave different answers. Decide as a pair, write the decision as a test, and note it in the pull request.
