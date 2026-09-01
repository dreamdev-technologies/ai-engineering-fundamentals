# Test archetypes

Two layers. **Builders** hide the shape of a domain type and supply sensible defaults. **Archetypes** are the well-known instances the business talks about, named in its language, that the builders are set up from: `LineItemBuilder.For<ACartonOfBananas>()`, `ConsignmentBuilder.For<TheStoryOneConsignment>()`. A test overrides only the one value it is about.

`src/Calculator.Tests/ArchetypeExampleTests.cs` shows the same test three ways: raw constructors, builders, archetypes. Read it first.

## Why

1. **A shared language.** "The story one consignment" and "a pallet of winter citrus" mean the same thing to the product owner, the analyst, the developer, the wiki and the agent. A test that reads `For<AnOversizedLot>()` says what it is about; a test that reads `new LineItem("08039010", "...", 1000, ...)` does not. Every archetype carries a one-sentence `Description` in business terms for exactly this reason.
2. **The fragile test problem.** When a field is added to `LineItem`, every test that calls its constructor breaks. With builders, one builder changes. With archetypes, the well-known values also live in one place, so "the story one consignment now has three lots" is a one-line change rather than a hunt through hundreds of tests that each spell it out.
3. **Tokens.** An agent writing or reading tests spends context on every setup block. `For<TheStoryOneConsignment>()` is one line where the raw version is twelve, and the agent does not have to rediscover what the values mean each time. Multiplied over a suite, that is the difference between an agent that can hold the tests in view and one that cannot.

## What is here

| | |
|---|---|
| `LineItemBuilder`, `ConsignmentBuilder` | Builders with `A...()` entry points, `With...` methods that chain, `Build()`, and an implicit conversion so a builder can be passed where the object is expected. |
| `LotArchetype`, `ConsignmentArchetype` | The base classes. An archetype has a `Description` and a `Configure(builder)`. |
| `Lots.cs` | `ACartonOfBananas`, `APalletOfWinterCitrus`, `APackagingLine`, `AnOversizedLot`. |
| `Consignments.cs` | `TheStoryOneConsignment`, `AConsignmentWhoseFreightDoesNotDivide`, `AnEmptyConsignment`, `AConsignmentAcrossTheRateChange`. |
| `WellKnown` | Named constants for currencies, origins, growers with negotiated terms, and the dates that matter (the last day of a price list, the rate change, the citrus season). |

## Adding one

Name it the way the business would say it, as a noun phrase: `AnAvocadoLotFromSouthAfrica`, not `TestLot3`. Give it a `Description` a non-developer would recognise. Build it from an existing archetype where one is close (`AnOversizedLot` is `ACartonOfBananas` with a different quantity). Put it in `Lots.cs` or `Consignments.cs`, and add a line to `wiki/glossary.md` under Archetype if it introduces a term. Agents writing tests are told, in the `acceptance-tests` skill, to look here first and add here rather than spell values out.

## Extending to the legacy service

`src/Legacy` has its own types (`HarbourConsignment`, `HarbourLine`). The same pattern applies: a `HarbourConsignmentBuilder` with `For<T>()`, and archetypes for the cases the gotchas describe, such as a consignment arriving on `WellKnown.Dates.LastDayOfTheFirstHalfPriceList` or a first line from `WellKnown.Growers.FincaVerde`. That is a good stretch task in activity 07 for anyone who finishes early.
