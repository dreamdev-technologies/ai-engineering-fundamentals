# 08. Agent creation

On your own. 35 minutes. The exercise repository, plus the work you produced in activities 05 and 07. How to talk to the tools and use an agent is in [tools.md](../tools.md).

## Why this activity exists

Everything you have done today lived in a session that is now gone. The criteria you agreed in 05, the way you pinned quirks in 07, the checks you ran before believing an agent: all of it was in the conversation, and the next conversation starts from nothing. This activity moves it into files that are in version control, review like code, and run the same way next Monday for whoever is at the keyboard.

Three of the mechanisms for that exist, and they do different jobs. Activity 03 met two of them: an instructions file, which loads every session and should hold only what applies everywhere, and skills, which load on demand and hold procedures. The third is the agent, and the Claude Code documentation states the difference in one sentence: subagents "run in their own context with their own set of allowed tools". Their own context, so the research an agent does to write characterisation tests does not fill your session and it does not inherit your session's accumulated mistakes. Their own tools, which is the part that matters most here. The same documentation says of instructions that they are "advisory", and of the mechanisms that are not: "hooks are deterministic and guarantee the action happens". An agent's tool list is the same kind of guarantee. An ATDD agent told in prose never to write production code may or may not comply under pressure; an ATDD agent that has no tool to edit files under `src/Calculator` cannot. That is why step 5 exists, and it is the single most useful thing to take from the day: sort every rule into the pile that is a request and the pile that is a guarantee, and move what matters into the second pile.

The other design decision an agent forces on you is the model, and Anthropic's own guidance for the Claude 5 generation is that this is a real choice with real costs. Judgement earns the strongest model; a fixed rubric applied to a diff does not, and something that fires on every pull request should be priced before it is committed. The characterisation agent is the interesting case, because its failure mode is helpfulness: a stronger model reasoning harder about what the code should do is more likely, not less, to correct the quirk you were trying to pin. Anthropic's March 2026 harness study found the same thing from the other side: agents grading their own work "confidently praising" it, with the fix being to separate the worker from the judge. The test grader you build here is that separate judge, and it is cheap on purpose.

The reason this activity comes after 05 and 07 rather than before is in the notes below, and it is not procedural. You cannot specify what an agent must be forbidden from doing for a job you have not done yourself.

## Goal

Turn the work you did by hand today into agents you can hand it to tomorrow. A skill gives the session you are in some knowledge; an agent is a separate worker with its own context, its own tool access and its own model, which you hand a job and which reports back. Writing one is an exercise in specification, and specification is where delegation is actually decided.

This activity comes late in the day on purpose. You can only write a good agent for work you have already done yourself, because until you have done it you do not know what the agent must be forbidden from doing.

## Steps

1. **Look before you build.** Run `/agents` and read what is already there. The `dotnet-test` plugin from activity 03 ships `test-quality-auditor` and `testability-migration` already, alongside internal pipeline agents it uses itself. Activity 03's rule holds here: do not add what already exists. Build only where nothing shipped fits, which for characterisation work is most of the time.
2. **Write the file.** Both tools read a markdown file with YAML frontmatter, but from different places: Claude Code reads `.claude/agents/*.md`, VS Code reads `.github/agents/*.agent.md`, and the `tools` list in each uses that tool's own names. `wiki-auditor` exists in both folders as the same agent written for each. Start from it: `Copy .claude/agents/wiki-auditor.md to .claude/agents/atdd.md` (or the `.github/agents/` equivalent with the `.agent.md` suffix), then open the copy and change it. If you use both tools, keep both files identical. The fields that matter are `name`, `description`, `tools` and `model`. Write the `description` for the caller and not for yourself: it is the only thing another agent sees when deciding whether to reach for yours, so it should say when to use this, not what it is.
3. **Build three, one at a time.** Each one bottles an activity you have already run:
   - **ATDD agent.** Turns a story into failing acceptance tests. Never writes production code, never edits an existing test, and where the story is ambiguous it stops and lists the questions instead of choosing. That last rule is the entire agent: one that resolves ambiguity quietly reintroduces the exact failure activity 05 exists to prevent.
   - **Characterisation agent.** Records what code does, never what it should do. The code is the oracle: where the value is unknown, assert a deliberately wrong one, run it, and take the real value from the failure message. Never modifies the service under test.
   - **Test grader.** Runs `grade-tests` and `test-anti-patterns` over the tests in a diff and returns the table, no prose, no changes. It reports; it does not fix.
4. **Choose the model deliberately, and say why out loud.** This is a design decision, not a default. Judgement and ambiguity earn the strongest model, so the ATDD agent gets it: deciding whether a story is underspecified is the hardest thing any of these do. The grader is the opposite, a fixed rubric applied to a diff, so it gets the cheapest model that still holds the rubric, and it is worth costing per run before you commit to something that fires on every pull request. The characterisation agent is the interesting one, because its failure mode is helpfulness rather than weakness: a model reasoning hard about intended behaviour will quietly correct the quirk you were trying to pin. Run it on two different models against the same code and see which one drifts. In Claude Code the field takes `opus`, `sonnet`, `haiku` or `inherit`; in VS Code it takes the names in the model picker.
5. **Find out which of your rules are real.** Try to make the ATDD agent write production code: `Use the atdd agent to implement story 2 in src/Calculator`. A rule stated in prose is a request the model may or may not honour under pressure; a tool you never granted is closer to a guarantee. Go through each agent's rules and sort them into the two piles. Where a rule that matters is only a request, narrow `tools` until it is not, and note the ones you cannot close this way. Those are the ones that need a check downstream rather than an instruction upstream.
6. **Test them against work you have already graded.** In a fresh session each time:
   ```
   Use the atdd agent to write acceptance tests for activities/05-acceptance-tests-first/story-2.md into a new file, without touching the existing tests.
   Use the characterisation agent on the Render method in src/Legacy/HarbourPricingService.cs.
   Use the test-grader agent on the tests changed in the current branch.
   ```
   Compare the first with what you wrote by hand in activity 05, the second with what you learned in activity 07, and the third with your own opinion of your tests. Where an agent is worse than you were, the gap is almost always in the file rather than the model: fix the instructions, not the prompt, and run it again. Activity 09 gives the ATDD agent its first real job on work nobody has done yet, so anything you leave loose here you will meet again there.

## Done when

Three agent files exist in `.claude/agents/` or `.github/agents/`, each carries a `model` you can justify in a sentence, each has been run against work you had already done by hand, and you can name one rule per agent that is enforced by its tool list rather than by asking nicely.

## Notes

These files are the reusable artefact of the whole day. Everything else you did was in a session that has now ended; these are in version control, they review like code, and they improve by pull request. Two failure modes to avoid: writing ten agents when three good ones would do, since every extra one is another description competing to be chosen; and writing an agent for work you have not done yourself, which produces confident instructions for a job you cannot yet grade.

## Sources

- [Best practices for Claude Code](https://code.claude.com/docs/en/best-practices) and [Subagents](https://code.claude.com/docs/en/sub-agents), Claude Code docs. Own context, own tools; instructions are advisory, hooks are deterministic.
- Michael Segner, [Steering Claude Code: when to use CLAUDE.md, skills, hooks, and subagents](https://claude.com/blog/steering-claude-code-skills-hooks-rules-subagents-and-more), Anthropic, 18 June 2026. Which mechanism for which job.
- [Prompting Claude Opus 5](https://platform.claude.com/docs/en/build-with-claude/prompt-engineering/prompting-claude-opus-5), Claude Platform docs. Effort and model choice as a cost control; delegation to subagents.
- Prithvi Rajasekaran, [Harness design for long-running application development](https://www.anthropic.com/engineering/harness-design-long-running-apps), Anthropic, 24 March 2026. Separating the worker from the judge.
