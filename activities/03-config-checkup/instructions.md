# 03. Config check-up

Pairs. 25 minutes. The exercise repository, cloned, plus `old-style-CLAUDE.md` in this directory.

## Why this activity exists

Context is the scarcest resource an agent has. Everything loaded at session start spends attention before you have asked a question, and the model's ability to use what is in the window degrades as the window fills: Anthropic's own engineering guidance calls this context rot and describes the job as finding "the smallest possible set of high-signal tokens" for the task. A Claude Code session starts by loading the system prompt, your CLAUDE.md files, the one-line descriptions of every skill, memory, and the names of every MCP tool; a few thousand tokens before you type, and every file the agent reads afterwards is another one to two thousand on top.

The second half of the why is newer than that. The instruction files most teams carry were written for 2024 and 2025 models, which needed to be told things twice, needed capital letters to notice a rule, and needed to be ordered to verify their work. The Claude 5 generation does not. In July 2026 the Claude Code team removed more than 80% of Claude Code's own system prompt for Opus 5 and Fable 5 with no measurable loss on coding evaluations, and wrote up what they learned as six shifts: rules to judgement ("write code that reads like the surrounding code" instead of a list of formatting bans), examples to interface design, upfront information to progressive disclosure, repetition to a single description, manual memory to auto-memory, and thin specs to rich references such as tests and rubrics. The advice for CLAUDE.md is to "keep your CLAUDE.md lightweight and briefly describe what your repo is for, but spend most of the tokens on gotchas inside of the codebase".

The consequence is that an instruction file tuned for the old models now does harm on the new ones. A rule stated in capitals is followed too literally. A rule stated twice is followed twice. An instruction to double-check or to run a verification step makes a model that already verifies do it again, at your cost: the Opus 5 prompting guide says to remove those lines outright because they "cause over-verification" and "add cost without improving results". Anti-formatting and anti-narration rules written to restrain older models push the new ones the wrong way. A 30-line procedure in CLAUDE.md is loaded into every session that does not need it; it belongs in a skill that loads on demand. Bloated instruction files do not make the agent better informed. They make it ignore your actual instructions, burn tokens, and exhaust the window faster.

So this activity is not "tidy the config". It is: know what your agent is carrying, know which of it was written for a model you no longer use, and measure what removing it buys.

## Steps

1. Open the exercise repository in your assigned tool.
2. **Measure the baseline.** Claude Code: run `/context` and write down the total and the breakdown (system prompt, CLAUDE.md, skills, MCP, memory). Copilot: open `.github/copilot-instructions.md` and any `.github/instructions/*.instructions.md` files, and list which extensions and MCP servers are active; there is no token count, so note the line counts instead.
3. **Audit the old-style file.** Read `old-style-CLAUDE.md` in this directory. It is a realistic instruction file of the kind teams wrote in 2025. Mark every line with one of these smells:
   - **Shouting.** ALWAYS, NEVER, CRITICAL, IMPORTANT on more than one line. When everything is emphasised, nothing is, and a Claude 5 model follows each one literally.
   - **Repetition.** The same rule stated twice, or stated here and again inside a skill.
   - **Verification orders.** "Double-check", "verify before responding", "run the tests again at the end". The model already does this; the line makes it do it twice.
   - **Restraints for a model you no longer use.** Bans on bullet points, on narration, on comments, on planning documents. Replace with judgement ("match the surrounding code") or delete.
   - **Procedures.** Anything longer than a few lines describing how to do a task. That is a skill, loaded when the task calls for it, not a tax on every session.
   - **Things the repository already shows.** Directory listings, file-by-file descriptions, standard language conventions, anything the agent can read for itself.
   Count what survives. Usually it is the purpose line and the gotchas, which is the point.
4. **Measure the difference.** Claude Code: copy `old-style-CLAUDE.md` over the repository's `CLAUDE.md`, start a fresh session, run `/context`, and note the total. Restore the original (`git checkout CLAUDE.md`), start fresh again, and note it. That difference is paid on every prompt of every session by everyone on the team. Then run `/doctor` on the real `CLAUDE.md` and see what it proposes to cut; it should find little. Copilot: compare the line counts and reason about what each session is carrying.
5. **Apply the same test to the real file.** Read the repository's `CLAUDE.md` line by line with the standard question: would removing this line cause the agent to make mistakes? If not, it is a cut. Then check the split: CLAUDE.md loads every session, so it holds only what applies broadly; the two skills in `.claude/skills/` hold what is needed occasionally. Confirm the repository follows that split, and say in one sentence why the gotchas are where they are.
6. **Install a skill set and watch the split in action.** Add the .NET team's testing skills, which activities 05 to 09 use. Claude Code: `/plugin marketplace add dotnet/skills`, then `/plugin install dotnet-test@dotnet-agent-skills`, then `/skills` and `/agents` to see what arrived. Copilot: set `chat.plugins.enabled` to true and add `dotnet/skills` to `chat.plugins.marketplaces` in `settings.json`, then use `/plugins` in Copilot Chat. Around two dozen skills and ten agents have just arrived; count them rather than taking that number on trust. Check your token count again before guessing what that cost. It is close to nothing, because only each skill's name and description loads at rest; the body arrives when something invokes it. That is progressive disclosure, measured rather than asserted.
7. **List everything else that is loaded**: MCP servers, plugins, skills, memory files, hooks. For each item, answer as a pair: what would break if this were removed? Remove or disable anything you could not answer for. In Claude Code, check the token count once more and note what the pruning bought you.
8. Ask the agent one question about the repository, for example: what does this repository do, and how do I run its tests? Judge the answer against what you now know is loaded.

## Done when

Your pair can name every item loaded into the session and say why it is there, anything you could not justify is gone, you have two token counts (or line counts) showing what the old-style file cost, and you can state the always-on versus on-demand split in one sentence with a number behind it.

## Notes

If the plugin marketplace is blocked on your machines, use the copy vendored under `vendor/dotnet-skills`. It is itself a local marketplace, so the same two commands work with a path: `/plugin marketplace add ./vendor/dotnet-skills`, then `/plugin install dotnet-test@dotnet-agent-skills-local`. For Copilot, `vendor/dotnet-skills/README.md` gives the copy step. Either way they are markdown files under version control like anything else, which is the point.

Three habits to carry out of this activity. Clear the session between unrelated tasks rather than letting context accumulate; after two failed corrections on the same thing, clear and write a better first prompt. Treat the instructions file like code: owned, reviewed, pruned, and tested by whether behaviour actually changes when a line goes. And when you upgrade the model, re-read the file: what was a necessary restraint for the last model is dead weight, or worse, for the next.

## Sources

- Thariq Shihipar, [The new rules of context engineering for Claude 5 generation models](https://claude.com/blog/the-new-rules-of-context-engineering-for-claude-5-generation-models), Anthropic, 24 July 2026. The 80% figure and the six shifts.
- Michael Segner, [Steering Claude Code: when to use CLAUDE.md, skills, hooks, and subagents](https://claude.com/blog/steering-claude-code-skills-hooks-rules-subagents-and-more), Anthropic, 18 June 2026. "Every line costs tokens whether relevant or not"; procedures belong in skills.
- [Effective context engineering for AI agents](https://www.anthropic.com/engineering/effective-context-engineering-for-ai-agents), Anthropic Engineering, 29 September 2025. Context rot, the smallest set of high-signal tokens, system prompts at the right altitude.
- [Best practices for Claude Code](https://code.claude.com/docs/en/best-practices) and [Explore the context window](https://code.claude.com/docs/en/context-window), Claude Code docs. What loads at startup and what each read costs; "bloated CLAUDE.md files cause Claude to ignore your actual instructions"; `/doctor`.
- [Prompting Claude Opus 5](https://platform.claude.com/docs/en/build-with-claude/prompt-engineering/prompting-claude-opus-5) and [Prompting Claude Fable 5.1](https://platform.claude.com/docs/en/build-with-claude/prompt-engineering/prompting-claude-fable-5-1), Claude Platform docs. Over-verification, self-correction, and the anti-formatting and anti-narration lines to remove.
