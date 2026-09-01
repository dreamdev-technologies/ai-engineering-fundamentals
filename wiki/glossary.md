---
description: Domain terms used in the code, the stories and the test archetypes: grower, consignment, lot, price list, order, shipment, landed cost, duty, commodity code, margin.
tags: [glossary, grower, consignment, lot, price list, order, shipment, landed cost, duty, margin]
---

# Glossary

Terms as the business uses them. Where the code uses a different name, it is given in brackets.

- **Grower.** A supplier of produce. Some have negotiated terms that change the price; see [gotchas](gotchas.md).
- **Consignment.** A shipment of produce from one origin, arriving on one date, with one freight charge. Made up of lines (lots). The unit that gets priced and invoiced. (`Consignment` in `src/Calculator`; `HarbourConsignment` in `src/Legacy`.)
- **Lot** (line, line item). One product on a consignment: a commodity code, a quantity in cases, a unit cost from the grower, a gross weight. (`LineItem`; `HarbourLine`.)
- **Commodity code.** The customs classification of a product. The first four digits are the heading and select the duty rate. Bananas are heading 0803, pineapples and avocados 0804, citrus 0805, berries 0810. Rates live in `src/Calculator/DutyRates.cs`.
- **Duty.** Import tax on a lot, calculated as a percentage of product cost (ad valorem). Some headings have a seasonal rate.
- **Freight.** The cost of getting the consignment to the door. One figure per consignment, shared out across lots by weight.
- **Landed cost.** What a lot actually cost to get here: product cost plus its share of freight plus duty. The number the whole commercial side hangs off. (`LandedCost`, `LandedCostLine`.)
- **Price list.** The prices in force for a period, issued twice a year by the commercial team. Used when a lot arrives without an agreed unit cost. Harbour holds them; see [gotchas](gotchas.md) for the boundary-day behaviour.
- **Order.** A customer's request for produce. Not modelled in this repository.
- **Shipment.** The physical movement: container, vessel, carrier, dates. Owned by the consignment service; the exercises only see its arrival date.
- **Customs window.** The slot in which a container must clear customs. Missing it costs demurrage and ripening time.
- **Margin.** What is added to landed cost to get a customer price. The standard rate lives in `src/Calculator/PricingPolicy.cs`. Whether "margin" means a markup on cost or a share of the selling price is a question the commercial and finance teams answer differently.
- **Quote.** A customer price for a consignment, in the customer's currency. (`CustomerQuote`.)
- **Exchange rate.** Conversion between the consignment currency and another. Rates and the dates they apply from live in `src/Calculator/ExchangeRates.cs`.
- **Archetype.** A well-known lot or consignment, named the way the business would say it (a carton of bananas, the story one consignment, an oversized lot), that tests are built from so that a test overrides only the value it is about. The names, and the one-sentence descriptions, live in `src/TestArchetypes`; see its README for why. The well-known values the business talks about (growers with negotiated terms, the last day of a price list, the rate change) are named in `WellKnown` there.
