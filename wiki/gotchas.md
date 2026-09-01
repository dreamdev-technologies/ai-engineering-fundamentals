---
description: Non-obvious behaviours of Harbour's pricing that downstream teams may depend on. Read before changing or replacing anything in src/Legacy.
tags: [gotchas, legacy, price list, landed cost, freight, grower, rounding]
---

# Gotchas

Behaviours of the legacy pricing (Harbour, `src/Legacy`) that are surprising, undocumented in the code, and possibly load-bearing. None of them is marked as a bug or a feature here; that decision is made deliberately, with a test, when the behaviour is replaced. See [architecture](architecture.md) for where Harbour sits.

## Rounding is not one rule

Finance agreed rounding with the auditors per currency, in different years, and the agreements differ. Euro amounts round one way, dollar amounts another, and anything that is not euro or dollar was never agreed at all and is simply cut off at the cent. Line totals in Harbour differ by a cent from a spreadsheet depending on the currency, and finance has reconciliation notes explaining which is "right" for each. Nobody has written down which is intended.

## The last day of a price list

The commercial calendar says a price list is valid to and including its last day. Harbour prices a consignment arriving on that last day off the next list. Logistics know this and occasionally rely on it: a container held a day at the port picks up the new season's price. Commercial regard it as a fault. Both teams have customers who would notice if it changed.

## Order matters

Freight is shared across lots by weight, and the odd cents from rounding land on whichever lot is last. Re-ordering the lots on a consignment changes the landed cost of individual lots, though never the total. Some customers are invoiced per lot.

Harbour also assumes one grower per consignment, which was true when it was written. The grower on the first lot decides whether a negotiated discount applies to the whole consignment, whoever grew the rest of it.

## Quantities above 999

Harbour's line record has a three-digit quantity field. A lot of more than 999 cases is priced wrongly, and no error is raised. Logistics split large lots into several lines to avoid it, which is why nobody has seen it happen in years and why the workaround will break the moment someone stops doing it.
