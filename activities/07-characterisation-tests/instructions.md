# 07. Characterisation tests

On your own. 40 minutes. The exercise repository: `src/Legacy`, `src/Legacy.Tests`, `wiki/gotchas.md`. How to talk to the tools and use a skill is in [tools.md](../tools.md).

## Why this activity exists

Everything so far has been on new code, where you write the contract first and the agent builds to it. Most of the work in a team mid-migration is not like that. It is old code, with rules nobody wrote down, that has to keep running while it is replaced. Michael Feathers gave the working definition twenty years ago: "legacy code is simply code without tests", and the reason that definition matters more now than it did then is that an agent will change code without tests just as confidently as code with them, and nothing will tell you what it changed.

Two things make agents unusually dangerous on this kind of code. The first is that they are built to write the tests a healthy codebase should have: hand a test-generation tool a method that rounds sterling by truncation and it will write a test that asserts proper rounding, watch it fail, and offer to fix the code. The dry run of this activity did exactly that. On legacy code the surprising behaviour is often the behaviour someone downstream depends on, and a test that asserts the "right" answer deletes it silently. The second is that agents tidy. GitClear's study of 211 million changed lines, from the opening talk, found refactoring collapsing and duplication rising as AI use grew; on a system whose quirks are load-bearing, an agent that reorganises as it goes is a liability.

Feathers's answer is the characterisation test: a test that "characterizes the actual behavior of a piece of code", written by running the code and recording what it does, not what anyone thinks it should do. The technique is deliberately dumb. Assert a value you know is wrong, run the test, and let the failure message tell you the truth; that observed value becomes the assertion. The code is the oracle. Done over the behaviours that matter, it produces a net: a suite that is green against the code as it stands and goes red the moment any behaviour changes, intended or not. That net is what makes the next two activities possible, and it is what makes an agent safe to point at a legacy system at all.

There is one more step that the tests alone do not give you, and it is the one people skip. For every quirk you pin, someone has to decide whether it is load-bearing or a bug, and write the decision down. A characterisation suite that pins everything and decides nothing turns into a fossil that blocks every change. The KEEP and BUG comments in step 5 are that decision, and activity 09 acts on them.

## Goal

Build a safety net under code nobody fully understands. Characterisation tests capture what the code does now, quirks included, so that later change becomes safe. This is the technique for any legacy migration.

`src/Legacy` is a single service written the way legacy code really is: mixed responsibilities, magic numbers, and a handful of surprising behaviours. It has no tests.

## Steps

1. Read the service first, without fixing anything. Resist the itch: for now you are a naturalist, not a surgeon. Note anything that looks odd.
2. Ask the agent to propose characterisation tests. Type:
   ```
   Use the code-testing-agent skill to write characterisation tests in src/Legacy.Tests for PriceConsignment in src/Legacy/HarbourPricingService.cs. Assert what the code does today, not what it should do. Do not modify anything under src/Legacy.
   ```
   Asked for one method it answers directly; asked for the whole class it may run its full seven-stage pipeline (research, plan, implement, build, test, fix, lint), which can take five to ten minutes, and in Copilot it may not chain its stages at all, in which case just ask for the tests in words. Whichever it does, if it is still going at ten minutes, stop it and work with what it has produced. Read what comes back carefully: it is built to produce the tests a healthy codebase should have, so it will hand you a confident, well-organised suite that is quietly wrong for this job. That is useful rather than a problem; step 3 is where you repair it. One trap for any test you keep: `HarbourPricingService.AuditLog` is static and shared, so assert on entries that contain your own consignment reference, never on counts or the last entry, or the suite goes flaky when xUnit runs classes in parallel.
3. Review the proposals hard. The agent will tend to write tests for what the code plausibly should do; that is exactly wrong here. Use the classic move: where you are unsure what the code returns, write the assertion with a deliberately wrong expected value, ask `Run the Legacy.Tests project and show me the failures in full`, and let the failure message tell you the real answer; that observed value becomes the assertion. The code is the oracle. Where a test fails, the correct fix is the assertion, not the code.
4. Hunt the quirks. There are three or four deliberate surprises in the service, of the kind `wiki/gotchas.md` describes. Do not browse for them at random. Type `/crap-score src/Legacy/HarbourPricingService.cs` (or in words, `Use the crap-score skill on ...`). CRAP ranks methods by complexity, weighted up the less your tests touch them: high complexity crossed with thin coverage is exactly where surprising behaviour survives unnoticed. With the tests from step 3 already in place, coverage will be high and the ranking is mostly complexity, which is still the right search order. Keep that output; activity 09 uses it. Make sure each quirk is pinned by a test that would fail if the quirk vanished.
5. For each pinned quirk, decide: load-bearing or bug? Would anything downstream break if this behaviour changed? Mark the decision with a comment on the test: KEEP with the reason, or BUG with the intended behaviour.
6. Finish green: `Run the full test suite and show me the output`, everything passing against the untouched service. Then `Commit the tests with the message "Activity 07 characterisation tests"`.

## Done when

The suite is green against unmodified legacy code, at least two quirks are pinned by tests, and every pinned quirk carries a KEEP or BUG decision with a reason.

## Notes

A characterisation test asserting the wrong "right" answer silently deletes a behaviour someone may depend on. That is why step 3 checks the code, not the intuition. Your KEEP or BUG comments and your CRAP scores are both input to activity 09, two activities later; that is exactly why you are writing the decisions down rather than trusting anyone to remember them.

## Sources

- Michael Feathers, *Working Effectively with Legacy Code*, Prentice Hall, 2004. The definition of legacy code, characterisation tests, and seams.
- GitClear, [AI Copilot Code Quality](https://www.gitclear.com/ai_assistant_code_quality_2025_research), 2025. Refactoring falling and duplication rising with AI use, from the opening talk.
- Alberto Savoia, [CRAP: Change Risk Anti-Patterns](https://www.artima.com/weblogs/viewpost.jsp?thread=210575), 2007. The metric step 4 uses: complexity weighted by how little of the method your tests touch.
