# 07. Characterisation tests

Pairs. 40 minutes. The exercise repository: `src/Legacy`, `src/Legacy.Tests`, `wiki/gotchas.md`.

## Goal

Build a safety net under code nobody fully understands. Characterisation tests capture what the code does now, quirks included, so that later change becomes safe. This is the technique for any legacy migration.

`src/Legacy` is a single service written the way legacy code really is: mixed responsibilities, magic numbers, and a handful of surprising behaviours. It has no tests.

## Steps

1. Read the service first, both of you, without fixing anything. Resist the itch: for now you are a naturalist, not a surgeon. Note anything that looks odd.
2. Ask the agent to propose characterisation tests for the service in `src/Legacy.Tests`: tests that assert the behaviour the code currently has, not the behaviour anyone thinks it should have. Use `code-testing-agent` and read what comes back carefully. Point it at `HarbourPricingService` in `src/Legacy/HarbourPricingService.cs`. Asked for one method it answers directly; asked for the class it may run its full seven-stage pipeline (research, plan, implement, build, test, fix, lint) with its own build-and-test loop, which can take five to ten minutes, and in Copilot it may not chain its stages at all, in which case ask for the tests directly. Whichever it does, if it is still going at ten minutes, stop it and work with what it has produced. One trap for any test you keep: `HarbourPricingService.AuditLog` is static and shared, so assert on entries that contain your own consignment reference, never on counts or the last entry, or the suite goes flaky when xUnit runs classes in parallel. It is built to produce the tests a healthy codebase should have, so it will hand you a confident, well-organised suite that is quietly wrong for this job. That is useful rather than a problem; step 3 is where you repair it.
3. Review the proposals hard. The agent will tend to write tests for what the code plausibly should do; that is exactly wrong here. Use the classic move: where you are unsure what the code returns, write the assertion with a deliberately wrong expected value, run the test, and let the failure message tell you the real answer; that observed value becomes the assertion. The code is the oracle. Where a test fails, the correct fix is the assertion, not the code.
4. Hunt the quirks. There are three or four deliberate surprises in the service, of the kind `wiki/gotchas.md` describes. Do not browse for them at random. Run `crap-score` over `HarbourPricingService` (or `coverage-analysis`, asking for CRAP scores explicitly; it does not report them unless asked). CRAP ranks methods by complexity, weighted up the less your tests touch them: high complexity crossed with thin coverage is exactly where surprising behaviour survives unnoticed. With the tests from step 3 already in place, coverage will be high and the ranking is mostly complexity, which is still the right search order. Keep that output; activity 09 uses it. Make sure each quirk is pinned by a test that would fail if the quirk vanished.
5. For each pinned quirk, decide as a pair: load-bearing or bug? Would anything downstream break if this behaviour changed? Mark the decision with a comment on the test: KEEP with the reason, or BUG with the intended behaviour.
6. Finish green: the whole suite passing against the untouched service.

## Done when

The suite is green against unmodified legacy code, at least two quirks are pinned by tests, and every pinned quirk carries a KEEP or BUG decision with a reason.

## Notes

A characterisation test asserting the wrong "right" answer silently deletes a behaviour someone may depend on. That is why step 3 checks the code, not the intuition. Your KEEP or BUG comments and your CRAP scores are both input to activity 09, two activities later; that is exactly why you are writing the decisions down rather than trusting anyone to remember them.
