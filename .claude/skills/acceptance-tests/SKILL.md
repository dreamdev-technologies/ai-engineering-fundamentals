---
name: acceptance-tests
description: How to turn a user story into acceptance tests in this repository's style, written before any implementation. Use when asked to write tests for a story, story file or feature request against src/Calculator.
---

# Acceptance tests

Acceptance tests are the contract an implementation is built against. They are written first, against the public API, from the story alone.

## Where and how

- xUnit, in `src/Calculator.Tests`. One class per story, file named `Story<N>_<Topic>Tests.cs`.
- Build inputs from the archetypes in `src/TestArchetypes`: `ConsignmentBuilder.For<TheStoryOneConsignment>()`, `LineItemBuilder.For<APalletOfWinterCitrus>()`, then `.With...` for the one value the test is about. Read `src/TestArchetypes/README.md` and `Lots.cs`, `Consignments.cs` and `WellKnown.cs` first; use an existing archetype where one is close, and when a story needs a case none of them covers, add a named archetype there rather than spelling the values out in the test. Never construct domain objects directly in a test.
- Test names are sentences about behaviour, underscores for spaces: `Freight_is_shared_by_weight`, `Shares_add_up_to_the_freight_exactly`. No `Test1`, no method names.
- Arrange, act, assert, in that order, separated by a blank line. One behaviour per test.
- Assert exact values. Money is compared as `Money.Of(712.50m, "EUR")`, never with a tolerance. Where the story gives a worked example, use its numbers; add at least one case the example does not cover.
- Rates and policy values are read from `DutyRates`, `ExchangeRates` and `PricingPolicy` in the test, never copied in as literals, so the test is about the rule and not about today's number.

## When the story is ambiguous

Do not choose. List the questions the story leaves open, one line each, with the two readings you can see, and stop. A test that silently picks a reading turns a guess into a contract.

## Never

- Write or change production code under `src/Calculator`. That is the implementer's job, and it comes after the tests exist.
- Edit an existing test.
- Write a test for new behaviour that already passes. Every acceptance test must fail against the code as it stands when you write it: the empty scaffold for the first story, the previous story's implementation after that. A test that passes straight away is either pinning behaviour that already exists (fine, but say so in its name) or is not testing the new rule.
