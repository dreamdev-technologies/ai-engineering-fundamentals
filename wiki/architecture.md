---
description: The systems at Meridian Fresh: the legacy core (Harbour), the .NET services around it, the low-code front ends and the integrations. Known gaps are marked.
tags: [architecture, legacy, services, integration, price list, consignment]
---

# Architecture

This page is known to be incomplete. Gaps are marked **unknown**; fill them from the code and from the people who know, and remove the marker.

Related: [business context](business-context.md), [gotchas](gotchas.md) for the behaviours that live in Harbour.

## The legacy core: Harbour

A single application, in production since 2009, ported from an earlier VB system in 2014. It owns orders, consignments, lots, price lists, pricing and invoicing, in one database. Most business rules live in it, and most of them are not written down anywhere else.

In this repository, `src/Legacy` is a slice of Harbour's pricing, kept the way it was found. `src/Legacy.Tests` is where its behaviour gets pinned before anything moves.

## Services around the core

.NET services, each with its own repository and pipeline, that take work away from Harbour one slice at a time.

| Service | Owns | Talks to |
|---|---|---|
| Consignment service | Shipment bookings, lot splits and merges | Harbour (writes back), carrier integration |
| Pricing API | The new landed cost calculation. In this repository, `src/Calculator` | **unknown**: whether it reads price lists from Harbour or from its own store |
| Customs integration | Document generation and the broker feed | Customs broker, Harbour for commodity codes |
| Grower portal API | What growers see | Harbour (read only) |

**unknown**: which service is the source of truth for exchange rates. Harbour has its own table; the Pricing API has `src/Calculator/ExchangeRates.cs`; finance has a spreadsheet.

## Front ends

Low-code portals for the grower and commercial teams, built on a low-code platform, calling the services above and, for anything not yet extracted, Harbour directly. Finance uses Harbour's own screens.

## Integrations

- Customs broker: daily file exchange plus a status feed.
- Carriers: booking and tracking, per carrier, mostly file based.
- Finance system: invoices out, payments in, nightly.
- Top ten customers: order and invoice files, formats **unknown** here.

## Delivery

Work is tracked in a work item system organised as epics, features and stories. Builds and deployments run in pipelines per repository. The services deploy independently; Harbour deploys quarterly, with a freeze around month end.

## Test support in this repository

`src/TestArchetypes` holds the builders and the named archetypes that tests are built from: the lots and consignments the business talks about, each with a one-sentence description, plus `WellKnown` names for the currencies, origins, growers and dates that matter. Add an archetype there whenever a story or a gotcha names a case none of the existing ones covers, and add its name to the [glossary](glossary.md).
