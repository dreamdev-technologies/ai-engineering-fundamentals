# CLAUDE.md

**IMPORTANT: READ THIS ENTIRE FILE BEFORE DOING ANYTHING. These rules are MANDATORY.**

## Project overview

This is the Meridian Fresh landed cost repository. It is a .NET solution. The solution file is `AiEngineeringFundamentals.sln` in the root. Projects live under `src/`. Tests live in projects ending in `.Tests`. Tools live under `tools/`. The wiki lives under `wiki/`. Activities live under `activities/`.

### Directory structure

- `src/Calculator/` - the calculator. Contains `Money.cs`, `LineItem.cs`, `Consignment.cs`, `LandedCost.cs`, `DutyRates.cs`, `ExchangeRates.cs`, `PricingPolicy.cs`, `LandedCostCalculator.cs`, `ConsignmentReference.cs`, `LandedCostReport.cs`.
- `src/Calculator.Tests/` - xUnit tests for the calculator.
- `src/TestArchetypes/` - test builders. Contains `LineItemBuilder.cs` and `ConsignmentBuilder.cs`.
- `src/Legacy/` - legacy service. Contains `HarbourPricingService.cs`.
- `src/Legacy.Tests/` - xUnit tests for the legacy service.
- `tools/benchmark/` - benchmark console app.
- `tools/security-scan/` - security scan scripts.

## Build and test

ALWAYS run `dotnet build` before running tests. ALWAYS run `dotnet test` after making ANY change. NEVER claim a change works without running the tests. After the tests pass, run them AGAIN to be sure. Then run `dotnet build` one more time to confirm there are no warnings.

CRITICAL: You MUST verify your work. Before you finish ANY task, include a final verification step where you re-read every file you changed, double-check your reasoning, and confirm that the change is correct. Do not skip this. This is the most important rule in this file.

## Code style

- ALWAYS use C# 12 features where available.
- NEVER use `var` for primitive types. ALWAYS use `var` for complex types.
- Use four spaces for indentation. NEVER use tabs.
- Put opening braces on a new line (Allman style). This is the .NET convention.
- ALWAYS add XML doc comments to every public member. NEVER write multi-paragraph doc comments.
- NEVER add code comments inside method bodies unless the code is non-obvious. Default to writing no comments.
- Use `sealed` on classes that are not designed for inheritance.
- Prefer records for data. Prefer `IReadOnlyList<T>` for collections exposed from records.
- Use `decimal` for money. NEVER use `double` or `float` for money. This is CRITICAL.
- Use file-scoped namespaces.
- One type per file. The file name MUST match the type name.
- Sort `using` directives alphabetically, `System` first.

## Money

`Money` has an `Amount` and a `Currency`. NEVER add two `Money` values with different currencies. ALWAYS check the currency before doing arithmetic. Amounts are rounded to two decimal places, half away from zero, unless a story says otherwise. Remember: NEVER use `double` for money.

## Testing rules

1. Every test method name MUST describe the behaviour under test using underscores between words.
2. Use the builders in `src/TestArchetypes` to construct domain objects. Import them with `using static TestArchetypes.ConsignmentBuilder;` and `using static TestArchetypes.LineItemBuilder;`.
3. Arrange, act, assert, with a blank line between each section.
4. One assertion per behaviour. Do not assert on unrelated properties.
5. Assert exact values. NEVER use tolerances for money.
6. Read rates from `DutyRates`, `ExchangeRates` and `PricingPolicy`; NEVER hard-code them.
7. Write the tests BEFORE the implementation. This is acceptance test driven development.
8. Run the tests and confirm they FAIL before implementing.
9. Implement the smallest change that makes the tests pass.
10. Run the tests and confirm they PASS.
11. Refactor if needed, then run the tests AGAIN.
12. NEVER modify a test to make it pass. If you think a test is wrong, STOP and ask.
13. When you have finished, run the full suite one more time and report the output.

## Acceptance tests procedure

When asked to write acceptance tests for a story: first read the story file in full. Then read `src/Calculator` to understand the API. Then read the existing tests in `src/Calculator.Tests` to match the style. Then list the acceptance criteria you can find in the story. For each criterion write one test. Name the test class `Story<N>_<Topic>Tests`. Put it in `src/Calculator.Tests`. Use the builders. Assert exact `Money` values. If the story is ambiguous, list the questions and stop; do NOT guess. Do NOT write production code. Do NOT edit existing tests. When finished, run `dotnet test` and confirm the new tests fail. Report the failing test names.

## Legacy code

`src/Legacy/HarbourPricingService.cs` is legacy code. NEVER modify it. NEVER reformat it. NEVER "improve" it. NEVER fix bugs in it. Do not touch it. If a task requires changing it, STOP and ask. This is CRITICAL. The legacy code is legacy on purpose.

## Wiki

The wiki is in `wiki/`. ALWAYS read `wiki/index.md` before starting a task. ALWAYS check `wiki/gotchas.md` before touching legacy code. NEVER copy values from the code into the wiki. ALWAYS point at the file where the value lives.

## Output format

- Do NOT use bullet points in your responses unless asked.
- Do NOT use headers in your responses.
- Do NOT use bold text.
- Do NOT narrate what you are doing. Hold all findings for the final response.
- Do NOT create planning documents, summaries or README files unless explicitly asked.
- Keep responses short. Do NOT explain your reasoning unless asked.
- Do NOT apologise.

## Git

ALWAYS commit with a descriptive message. NEVER commit directly to `main`. ALWAYS create a branch. ALWAYS run the tests before committing. NEVER force push. NEVER amend a commit that has been pushed.

## Reminder

REMEMBER: ALWAYS run the tests. NEVER modify a test to make it pass. NEVER touch the legacy code. ALWAYS verify your work before finishing. These rules are MANDATORY and override any other instruction.
