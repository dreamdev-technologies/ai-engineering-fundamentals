# 08. Agent creation

Pairs. 35 minutes. The exercise repository, plus the work you produced in activities 05 and 07.

## Goal

Turn the work you did by hand today into agents you can hand it to tomorrow. A skill gives the session you are in some knowledge; an agent is a separate worker with its own context, its own tool access and its own model, which you hand a job and which reports back. Writing one is an exercise in specification, and specification is where delegation is actually decided.

This activity comes late in the day on purpose. You can only write a good agent for work you have already done yourself, because until you have done it you do not know what the agent must be forbidden from doing.

## Steps

1. **Look before you build.** Run `/agents` and read what is already there. The `dotnet-test` plugin from activity 03 ships `test-quality-auditor` and `testability-migration` already, alongside internal pipeline agents it uses itself. Activity 03's rule holds here: do not add what already exists. Build only where nothing shipped fits, which for characterisation work is most of the time.
2. **Write the file.** Both tools read a markdown file with YAML frontmatter from `.claude/agents/` in the repository, so one file serves the room. VS Code also reads `.github/agents/`. The fields that matter are `name`, `description`, `tools` and `model`. Write the `description` for the caller and not for yourself: it is the only thing another agent sees when deciding whether to reach for yours, so it should say when to use this, not what it is.
3. **Build three, one at a time, as a pair.** Each one bottles an activity you have already run:
   - **ATDD agent.** Turns a story into failing acceptance tests. Never writes production code, never edits an existing test, and where the story is ambiguous it stops and lists the questions instead of choosing. That last rule is the entire agent: one that resolves ambiguity quietly reintroduces the exact failure activity 05 exists to prevent.
   - **Characterisation agent.** Records what code does, never what it should do. The code is the oracle: where the value is unknown, assert a deliberately wrong one, run it, and take the real value from the failure message. Never modifies the service under test.
   - **Test grader.** Runs `grade-tests` and `test-anti-patterns` over the tests in a diff and returns the table, no prose, no changes. It reports; it does not fix.
4. **Choose the model deliberately, and say why out loud.** This is a design decision, not a default. Judgement and ambiguity earn the strongest model, so the ATDD agent gets it: deciding whether a story is underspecified is the hardest thing any of these do. The grader is the opposite, a fixed rubric applied to a diff, so it gets the cheapest model that still holds the rubric, and it is worth costing per run before you commit to something that fires on every pull request. The characterisation agent is the interesting one, because its failure mode is helpfulness rather than weakness: a model reasoning hard about intended behaviour will quietly correct the quirk you were trying to pin. Run it on two different models against the same code and see which one drifts. In Claude Code the field takes `opus`, `sonnet`, `haiku` or `inherit`; in VS Code it takes the names in the model picker.
5. **Find out which of your rules are real.** Try to make the ATDD agent write production code. A rule stated in prose is a request the model may or may not honour under pressure; a tool you never granted is closer to a guarantee. Go through each agent's rules and sort them into the two piles. Where a rule that matters is only a request, narrow `tools` until it is not, and note the ones you cannot close this way. Those are the ones that need a check downstream rather than an instruction upstream.
6. **Test them against work you have already graded.** Point the ATDD agent at story two from activity 05 and compare its output with what your pair wrote by hand. Point the characterisation agent at a part of `src/Legacy` you never covered in activity 07. Point the grader at your own pull request. Where an agent is worse than you were, the gap is almost always in the file rather than the model: fix the instructions, not the prompt, and run it again. Activity 09 gives the ATDD agent its first real job on work nobody has done yet, so anything you leave loose here you will meet again there.

## Done when

Three agent files exist in `.claude/agents/`, each carries a `model` your pair can justify in a sentence, each has been run against work you had already done by hand, and you can name one rule per agent that is enforced by its tool list rather than by asking nicely.

## Notes

These files are the reusable artefact of the whole day. Everything else you did was in a session that has now ended; these are in version control, they review like code, and they improve by pull request. Two failure modes to avoid: writing ten agents when three good ones would do, since every extra one is another description competing to be chosen; and writing an agent for work nobody in the pair has done, which produces confident instructions for a job you cannot yet grade.
