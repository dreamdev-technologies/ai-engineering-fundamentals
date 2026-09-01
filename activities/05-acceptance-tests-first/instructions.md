# 05. Acceptance tests first

On your own. 45 minutes. The exercise repository: `src/Calculator`, `src/Calculator.Tests`, and the three stories in this directory (`story-1.md`, `story-2.md`, `story-3.md`). How to talk to the tools and use a skill is in [tools.md](../tools.md).

## Why this activity exists

The opening talk made the argument that you can hand an agent as much work as you can verify. This activity is where that stops being a slogan. An agent given a story and told to build it will build something plausible, report success, and be wrong in ways that only show up later, because the only check on its work was its own reading of the story. The Claude Code documentation is blunt about it: "Claude stops when the work looks done. Without a check it can run, 'looks done' is the only signal available, and you become the verification loop: every mistake waits for you to notice it." Give it "something that produces a pass or fail, and the loop closes on its own."

The evidence that this matters is older than the tools. DORA's research has, since the first Accelerate report, listed test automation and continuous testing among the capabilities that predict delivery performance, and their 2024 survey found that AI adoption reduced delivery stability, 7.2% for every 25% increase in use. The 2025 report's explanation is that AI amplifies what an organisation already is; the seven capabilities it names as deciding whether AI pays off include working in small batches and strong version control, and the reason those matter is that they are what makes a change checkable. The METR trial from the opening talk found experienced developers 19% slower with AI tools; they accepted fewer than half of the tool's suggestions and spent the time reviewing and correcting the rest. A test the agent has to pass moves that review from after the fact to before the work starts.

The same lesson now comes from the people who build the tools. Anthropic's July 2026 write-up of Claude Code's own context lists six shifts for the Claude 5 generation, and the last is from "simple specs" to "rich references": beyond markdown plans, to "test suites, and rubrics for verification". A separate post the same month describes turning the checks a developer runs by hand into skills so that "Claude closes its own feedback loops". The direction is the same in both: the useful part of a specification is the part that can fail.

That is also how to place this against spec-driven development, which some of you will have seen as GitHub's Spec Kit (September 2025): specify, plan, break into tasks, implement, with the spec as "the source of truth your tools and AI agents use to generate, test, and validate code". The specify and plan steps are worth keeping; writing the story down and asking what it leaves open is exactly step 1 here. But a spec is prose, and the agent that implements it is also the thing that decides whether it has met it. Nothing enforces a paragraph. An acceptance test is the sentence of the spec that has been made executable: the agent cannot reinterpret it, cannot skip it, and cannot claim success while it is red. This is not new either; Gojko Adzic called it specification by example in 2011, and the practice of writing the acceptance tests before the code, acceptance test driven development, is older still. What has changed is that the thing on the other side of the contract now works at machine speed, so the contract has to be checkable at machine speed too.

Two consequences shape the steps below. You decide the acceptance criteria and the agent types the tests, because the decisions are where the story's gaps show up and typing fixtures is not; the danger is an agent that resolves those gaps silently on your behalf, so the agent is told to list the questions and stop, and you answer them before a line of test code exists. And the moment an agent edits a test to make it pass is the most valuable moment of the activity: it is unverified delegation doing exactly what it always does, in front of you.

## Goal

Run the loop that makes delegation safe: write the acceptance tests before any code exists, then let the agent implement against them. The tests are the contract; the agent is the contractor.

The three stories rise in ambiguity. That is deliberate: story one is precise, story three leaves things unsaid.

## Steps

1. Read story one yourself. Then agree the criteria with the agent before any test exists. In a fresh session, type:
   ```
   Read activities/05-acceptance-tests-first/story-1.md and the acceptance-tests skill. List the acceptance criteria you can find in the story, one line each, and separately list every question the story leaves open, with the two readings you can see for each. Do not write any tests or code yet.
   ```
   Read both lists. Add any criterion it missed. For each open question, decide the answer yourself and tell it: `Question 2: the first reading. Question 3: the second, because ...`. Story one should produce few questions; stories two and three will produce more, and that is the point of them.
2. Now have it write the tests from the agreed criteria:
   ```
   Write the acceptance tests for those criteria and decisions in src/Calculator.Tests, one test per criterion, following the acceptance-tests skill. Do not write or change anything under src/Calculator. Then run the tests and show me that every one of them fails.
   ```
   Review each test with one filter: if the implementation were subtly wrong, would this fail? Ask for the case that catches it. Check that every decision you made in step 1 is a test, not a comment. Then grade the contract before anyone builds against it: `/grade-tests src/Calculator.Tests/<the test file>.cs` for a letter grade per test method, then `/assertion-quality src/Calculator.Tests/<the test file>.cs` for whether the assertions pin behaviour or merely confirm that nothing threw. Act on what they surface. This is the contract; the time goes into getting it right, not into typing it.
3. Hand over. Type:
   ```
   Make the tests in src/Calculator.Tests/<your test file>.cs pass by implementing LandedCostCalculator in src/Calculator. Do not modify any test. When you are done, run the tests and show me the output.
   ```
   Let it work.
4. When it claims success, demand evidence, not assertion: `Show me the commands you ran and their full output.` Then check for yourselves: `Run the full test suite and show me the output`, and `Show me the diff`, and read both. Green is necessary, not sufficient; check the implementation is not hard-coded to your examples. Now that an implementation exists, type `/test-gap-analysis src/Calculator.Tests/<your test file>.cs`. It reasons about mutations rather than running a mutation testing tool, so treat its output as a structured review: it names changes to the code it believes your suite would not notice. Each one you agree with is a hole in your contract that the agent was free to walk through.
5. Start a fresh session (`/clear` in Claude Code, New Chat in Copilot) before each story so failed approaches from the last story do not pollute the next. Repeat for story two, then story three. When a story is ambiguous, do not guess silently and do not let the agent guess: decide, then encode the decision as a test. The test is where a decision lives; the prompt is not.
6. Finish by asking: `Commit the tests and implementation, push the branch, and open a pull request titled "Activity 05".` If it cannot open the pull request from your machine, open it on GitHub yourselves.

## Done when

Completed stories are green with tests that were written before the code, you can point at one test that encodes a decision the story left open, and any mutation that survived step 4 was one you consciously accepted rather than one you never saw.

## Notes

Do not reach for the test generation skills (`code-testing-agent` and its pipeline) in this activity. They generate tests from code, and here there is no code: the tests come from the story and from your decisions, which is why step 1 makes the agent list the questions and stop rather than answer them. If the agent modifies a test to make it pass, that is the most valuable moment of the exercise: it did what unverified delegation always does. Revert the test, tighten the prompt, and go again. Two stories done well beats three done loosely.

If your team uses a spec-driven workflow, keep it and add this to it: the specify and plan steps produce the story; the acceptance tests are the part of the spec you hand to the agent as a contract; the implement step runs against them. A spec the agent can only read is advice. A test it has to pass is a requirement.

## Sources

- [Best practices for Claude Code](https://code.claude.com/docs/en/best-practices), Claude Code docs, "Give Claude a way to verify its work" and "the trust-then-verify gap".
- DORA, [Test automation](https://dora.dev/capabilities/test-automation/) in the DORA capabilities catalogue, which lists continuous testing alongside it; [Accelerate State of DevOps 2024](https://dora.dev/research/2024/dora-report/) for the stability finding; [State of AI-assisted Software Development 2025](https://dora.dev/research/2025/dora-report/) and the [AI Capabilities Model](https://dora.dev/ai/capabilities-model/report/) for the seven capabilities.
- METR, [Measuring the impact of early-2025 AI on experienced open-source developer productivity](https://metr.org/blog/2025-07-10-early-2025-ai-experienced-os-dev-study/), July 2025.
- Thariq Shihipar, [The new rules of context engineering for Claude 5 generation models](https://claude.com/blog/the-new-rules-of-context-engineering-for-claude-5-generation-models), Anthropic, 24 July 2026. Shift six: simple specs to rich references, test suites and rubrics.
- Delba de Oliveira, [Building verification loops in Claude Code with skills](https://claude.com/blog/building-verification-loops-in-claude-code-with-skills), Anthropic, 22 July 2026.
- Den Delimarsky, [Spec-driven development with AI: get started with a new open source toolkit](https://github.blog/ai-and-ml/generative-ai/spec-driven-development-with-ai-get-started-with-a-new-open-source-toolkit/), GitHub, September 2025; [github/spec-kit](https://github.com/github/spec-kit).
- Gojko Adzic, *Specification by Example*, Manning, 2011.
