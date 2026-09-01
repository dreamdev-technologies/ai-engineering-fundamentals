# Test archetypes

An archetype is a builder that produces a valid, realistic default object. A test overrides only the one or two values it is about, so the test reads as a statement about behaviour rather than a wall of setup, and a change to the domain model is fixed in one builder rather than in every test.

Conventions used here:

- A static entry point named for the thing: `ALineItem()`, `AConsignment()`.
- `With...` methods that return the builder, so calls chain.
- `Build()` at the end, plus an implicit conversion so a builder can be passed where the object is expected.
- Defaults are a plausible real case, not zeros and empty strings. The defaults are the archetype.

`LineItemBuilder` and `ConsignmentBuilder` are the worked examples. Add a builder here whenever a test needs a domain object the existing ones cannot produce, and document it in `wiki/architecture.md`.
