# 05. Acceptance tests first

Pairs. 45 minutes. The exercise repository: `src/Calculator`, `src/Calculator.Tests`, and the three stories in this directory (`story-1.md`, `story-2.md`, `story-3.md`). How to talk to the tools and use a skill is in [tools.md](../tools.md).

## Goal

Run the loop that makes delegation safe: write the acceptance tests before any code exists, then let the agent implement against them. The tests are the contract; the agent is the contractor.

The three stories rise in ambiguity. That is deliberate: story one is precise, story three leaves things unsaid.

## Steps

1. Read story one. Together, write its acceptance tests in `src/Calculator.Tests` before any implementation. Use the builders in `src/TestArchetypes` and the repository's `acceptance-tests` skill for style. Do not let the agent write these; drafting them is where you find the questions.
2. Review your tests together with one filter: if the implementation were subtly wrong, would these fail? Add the case that catches it. Then grade the contract before anyone builds against it. Type `/grade-tests src/Calculator.Tests/<your test file>.cs` for a letter grade per test method, then `/assertion-quality src/Calculator.Tests/<your test file>.cs` for whether your assertions pin behaviour or merely confirm that nothing threw. (In words, if you prefer: `Use the grade-tests skill on ...`.) Act on what they surface. Neither skill writes tests for you, and that is deliberate: you are grading your own contract, not outsourcing it.
3. Hand over. Type:
   ```
   Make the tests in src/Calculator.Tests/<your test file>.cs pass by implementing LandedCostCalculator in src/Calculator. Do not modify any test. When you are done, run the tests and show me the output.
   ```
   Let it work.
4. When it claims success, demand evidence, not assertion: `Show me the commands you ran and their full output.` Then check for yourselves: `Run the full test suite and show me the output`, and `Show me the diff`, and read both. Green is necessary, not sufficient; check the implementation is not hard-coded to your examples. Now that an implementation exists, type `/test-gap-analysis src/Calculator.Tests/<your test file>.cs`. It reasons about mutations rather than running a mutation testing tool, so treat its output as a structured review: it names changes to the code it believes your suite would not notice. Each one you agree with is a hole in your contract that the agent was free to walk through.
5. Start a fresh session (`/clear` in Claude Code, New Chat in Copilot) before each story so failed approaches from the last story do not pollute the next. Repeat for story two, then story three. When a story is ambiguous, do not guess silently and do not let the agent guess: decide between you, then encode the decision as a test. The test is where a decision lives; the prompt is not.
6. Finish by asking: `Commit the tests and implementation, push the branch, and open a pull request titled "Activity 05".` If it cannot open the pull request from your machine, open it on GitHub yourselves.

## Done when

Completed stories are green with tests that were written before the code, your pair can point at one test that encodes a decision the story left open, and any mutation that survived step 4 was one you consciously accepted rather than one you never saw.

## Notes

Do not reach for the test generation skills in this activity. Writing the tests yourself is where the questions in the stories surface, and an agent that drafts them first answers those questions silently on your behalf. If the agent modifies a test to make it pass, that is the most valuable moment of the exercise: it did what unverified delegation always does. Revert the test, tighten the prompt, and go again. Two stories done well beats three done loosely.
