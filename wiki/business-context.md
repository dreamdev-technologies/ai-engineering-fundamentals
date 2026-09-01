---
description: What Meridian Fresh does, who uses its systems, and what a mis-shipped consignment, a pricing error or a missed customs window costs.
tags: [business, grower, consignment, price list, landed cost, customs]
---

# Business context

Meridian Fresh imports fresh produce and sells it to retailers and wholesalers across Europe. Bananas, pineapples, avocados, citrus and berries, bought from growers in Central and South America, Southern Africa and Spain, shipped by sea in refrigerated containers, ripened and packed at three European sites, and delivered to customer distribution centres. Around 900 consignments a year; a typical consignment is worth between 40,000 and 120,000 euro at landed cost.

Related: [glossary](glossary.md) for the terms, [architecture](architecture.md) for the systems.

## Who uses the systems

- **Commercial team** (12 people). Set price lists twice a year, negotiate grower terms, quote customers. They live in the pricing screens and in spreadsheets that reconcile against them.
- **Logistics** (8). Book shipments, track consignments, split and merge lots, chase containers through customs. They are the people who know the gotchas.
- **Customs and compliance** (3). Commodity codes, duty, the customs window, the paperwork that has to be right before a container lands.
- **Finance** (6). Landed cost, margin, month-end reconciliation, and the auditors.
- **Growers** (about 60 suppliers). See their own consignments and payments through a portal.
- **Customers** (about 200 accounts). Receive quotes and invoices; the biggest ten integrate by file.

## What wrong costs

Produce is perishable, so a mistake is not a delay, it is a loss.

- **A mis-shipped consignment.** A container to the wrong site or the wrong customer is a day or two of ripening lost plus the cost of moving it. Worst case, a write-off: a 40-foot container of bananas is roughly 20 tonnes.
- **A pricing error.** Retail contracts are priced from landed cost plus margin. A landed cost out by a few percent on a seasonal line is a contract quarter sold at a loss, and the customer will not accept a correction after the fact.
- **A missed customs window.** A container that misses its customs slot waits, and pays demurrage while it waits; fruit continues to ripen in the box. Two days is normally survivable; a week usually is not.
- **A duty mistake.** Under-declared duty is a penalty and an audit; over-declared duty is money nobody claims back.

## Where the money is decided

Landed cost. Everything commercial hangs off it: quotes, margin, grower performance, which origins to buy from next season. It is currently calculated by the legacy core system (Harbour) and re-calculated in spreadsheets by people who do not trust it. Replacing that calculation with something the business can read and test is the first slice of the migration, which is why the exercises in this repository are built around it.

## What the business asks for most

In rough order: faster quotes; landed cost that finance and commercial agree on; fewer surprises at customs; a grower portal that shows the same numbers as the invoice.
