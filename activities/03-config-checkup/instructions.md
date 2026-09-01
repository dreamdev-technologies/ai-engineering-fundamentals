# 03. Config check-up

Pairs. 25 minutes. A repository your pair works in every day, opened in the tool you already use on it. The exercise repository is the fallback, and the place to take a token measurement. `old-style-CLAUDE.md` in this directory.

This is the one activity that opens your own code, and it changes nothing there: it reads your instruction files and what your session loads, and the only output is a diff you review yourselves. Use the tool that is already approved for that repository. If Claude Code is not cleared for your codebase, run this in Copilot on your repo, and use Claude Code only on the exercise repository.

## Why this activity exists

Context is the scarcest resource an agent has. Everything loaded at session start spends attention before you have asked a question, and the model's ability to use what is in the window degrades as the window fills: Anthropic's own engineering guidance calls this context rot and describes the job as finding "the smallest possible set of high-signal tokens" for the task. A Claude Code session starts by loading the system prompt, your CLAUDE.md files, the one-line descriptions of every skill, memory, and the names of every MCP tool; a few thousand tokens before you type, and every file the agent reads afterwards is another one to two thousand on top. Copilot loads its instruction files the same way, on every request.

The second half of the why is newer than that. The instruction files most teams carry were written for 2024 and 2025 models, which needed to be told things twice, needed capital letters to notice a rule, and needed to be ordered to verify their work. The Claude 5 generation does not. In July 2026 the Claude Code team removed more than 80% of Claude Code's own system prompt for Opus 5 and Fable 5 with no measurable loss on coding evaluations, and wrote up what they learned as six shifts: rules to judgement ("write code that reads like the surrounding code" instead of a list of formatting bans), examples to interface design, upfront information to progressive disclosure, repetition to a single description, manual memory to auto-memory, and thin specs to rich references such as tests and rubrics. The advice for CLAUDE.md is to "keep your CLAUDE.md lightweight and briefly describe what your repo is for, but spend most of the tokens on gotchas inside of the codebase".

The consequence is that an instruction file tuned for the old models now does harm on the new ones. A rule stated in capitals is followed too literally. A rule stated twice is followed twice. An instruction to double-check or to run a verification step makes a model that already verifies do it again, at your cost: the Opus 5 prompting guide says to remove those lines outright because they "cause over-verification" and "add cost without improving results". Anti-formatting and anti-narration rules written to restrain older models push the new ones the wrong way. A 30-line procedure in an instructions file is loaded into every session that does not need it; it belongs in a skill that loads on demand. Bloated instruction files do not make the agent better informed. They make it ignore your actual instructions, burn tokens, and exhaust the window faster.

So this activity is not "tidy the config". It is: find out what your agent is carrying on your own repository, work out which of it was written for a model you no longer use, and leave with the change ready to make.

## The smells

Mark every line of an instructions file with one of these, or with "keep":

- **Shouting.** ALWAYS, NEVER, CRITICAL, IMPORTANT on more than one line. When everything is emphasised, nothing is, and a Claude 5 model follows each one literally.
- **Repetition.** The same rule stated twice, or stated here and again inside a skill or a second instructions file.
- **Verification orders.** "Double-check", "verify before responding", "run the tests again at the end". The model already does this; the line makes it do it twice.
- **Restraints for a model you no longer use.** Bans on bullet points, on narration, on comments, on planning documents. Replace with judgement ("match the surrounding code") or delete.
- **Procedures.** Anything longer than a few lines describing how to do a task. That is a skill, loaded when the task calls for it, not a tax on every session.
- **Things the repository already shows.** Directory listings, file-by-file descriptions, standard language conventions, anything the agent can read for itself.

What usually survives is the purpose line, the gotchas, and pointers to where things live.

## Steps

1. **Calibrate, five minutes.** Read `old-style-CLAUDE.md` in this directory. It is a realistic instruction file of the kind teams wrote in 2025, and every smell above is in it. Mark it up as a pair, count what survives, then compare with the exercise repository's real `CLAUDE.md`. That is what the end state looks like. Do not spend longer than five minutes here.
2. **Open your own repository** in the tool already approved for it. Find every instructions file: for Copilot, `.github/copilot-instructions.md`, `.github/instructions/*.instructions.md` (scoped by `applyTo`), `AGENTS.md`; for Claude Code, `CLAUDE.md` at the root and in subdirectories, `.claude/rules/`, and your personal `~/.claude/CLAUDE.md`. Note their line counts. If there is no instructions file at all, go to step 5.
3. **Measure what the session carries.** Claude Code: run `/context` and write down the total and the breakdown (system prompt, CLAUDE.md, skills, MCP, memory). Copilot: there is no token count; list the extensions and MCP servers active in your editor and the total lines of instructions loaded, and use the exercise repository in Claude Code if you want a number to attach to it.
4. **Audit your own files** with the smells above, line by line, as a pair. For each line ask the standard question: would removing this cause the agent to make mistakes? If not, it is a cut. Then check the split: what loads every session should hold only what applies broadly; anything needed occasionally belongs in a skill or a path-scoped instructions file. Write the cuts as an actual edit in a branch, not a list. Where you moved a procedure out, create the skill file it moves to. Claude Code users can run `/doctor` on the result and see what it still proposes.
5. **If there was no instructions file**, write one, now, and keep it under 30 lines: one line on what the repository is for, the gotchas an agent cannot discover by reading the code (the non-obvious build step, the directory that must not be touched, the convention that differs from the language default), and pointers to where the values live. Nothing the repository already shows.
6. **List everything else that is loaded**: MCP servers, plugins, extensions, memory files, hooks. For each item, answer as a pair: what would break if this were removed? Note anything you could not answer for; those are the next things to switch off.
7. **Ask the agent one question** about your repository that a new joiner would ask, before and after the change if you have time. Judge the answer against what you now know is loaded.
8. **Install the testing skills** you will need for the later activities, in the exercise repository. Claude Code: `/plugin marketplace add dotnet/skills`, then `/plugin install dotnet-test@dotnet-agent-skills`, then `/skills` and `/agents` to see what arrived. Copilot: set `chat.plugins.enabled` to true and add `dotnet/skills` to `chat.plugins.marketplaces` in `settings.json`, then use `/plugins` in Copilot Chat. Around two dozen skills and ten agents have just arrived; count them rather than taking that number on trust. In Claude Code, check `/context` again: the cost is close to nothing, because only each skill's name and description loads at rest and the body arrives when something invokes it. That is progressive disclosure, measured rather than asserted, and it is where the procedures you cut in step 4 should live.

## Done when

Your pair has a branch in your own repository with the instructions file pruned or written, can say in one sentence what each surviving line prevents, can name everything else the session loads and why, and has one number (tokens or lines) for what the change saves on every prompt.

## Fallback

If nobody in the pair has a repository they can open, or the tool is not cleared for it, run steps 2 to 7 against the exercise repository with `old-style-CLAUDE.md` copied over its `CLAUDE.md`: measure with `/context`, prune it back, measure again, restore with `git checkout CLAUDE.md`. The lesson is the same; only the diff is less useful on Monday.

## Notes

If the plugin marketplace is blocked on your machines, use the copy vendored under `vendor/dotnet-skills`. It is itself a local marketplace, so the same two commands work with a path: `/plugin marketplace add ./vendor/dotnet-skills`, then `/plugin install dotnet-test@dotnet-agent-skills-local`. For Copilot, `vendor/dotnet-skills/README.md` gives the copy step.

Three habits to carry out of this activity. Clear the session between unrelated tasks rather than letting context accumulate; after two failed corrections on the same thing, clear and write a better first prompt. Treat the instructions file like code: owned, reviewed, pruned, and tested by whether behaviour actually changes when a line goes. And when you upgrade the model, re-read the file: what was a necessary restraint for the last model is dead weight, or worse, for the next.

## Sources

- Thariq Shihipar, [The new rules of context engineering for Claude 5 generation models](https://claude.com/blog/the-new-rules-of-context-engineering-for-claude-5-generation-models), Anthropic, 24 July 2026. The 80% figure and the six shifts.
- Michael Segner, [Steering Claude Code: when to use CLAUDE.md, skills, hooks, and subagents](https://claude.com/blog/steering-claude-code-skills-hooks-rules-subagents-and-more), Anthropic, 18 June 2026. "Every line costs tokens whether relevant or not"; procedures belong in skills.
- [Effective context engineering for AI agents](https://www.anthropic.com/engineering/effective-context-engineering-for-ai-agents), Anthropic Engineering, 29 September 2025. Context rot, the smallest set of high-signal tokens, system prompts at the right altitude.
- [Best practices for Claude Code](https://code.claude.com/docs/en/best-practices) and [Explore the context window](https://code.claude.com/docs/en/context-window), Claude Code docs. What loads at startup and what each read costs; "bloated CLAUDE.md files cause Claude to ignore your actual instructions"; `/doctor`.
- [Prompting Claude Opus 5](https://platform.claude.com/docs/en/build-with-claude/prompt-engineering/prompting-claude-opus-5) and [Prompting Claude Fable 5.1](https://platform.claude.com/docs/en/build-with-claude/prompt-engineering/prompting-claude-fable-5-1), Claude Platform docs. Over-verification, self-correction, and the anti-formatting and anti-narration lines to remove.
