# 03. Config check-up

Pairs. 25 minutes. Two laptops: one with the exercise repository open, one with a repository your pair works in every day. `old-style-CLAUDE.md` in this directory.

## Before you start: which tool, and how to work as a pair

**Which tool.** "Copilot" here means GitHub Copilot in VS Code, with the Copilot Chat panel open (`Ctrl+Alt+I`, or the chat icon in the top bar). "Claude Code" means the `claude` command in a terminal, or the Claude Code panel in VS Code if the extension is installed. On your own repository, use the tool you already use on that code at work. For most people in the room that is Copilot. Use Claude Code on your own repository only if it has been approved for that code; if you are not sure, it has not, and Claude Code stays on the exercise repository.

**How to pair.** One person has the keyboard (the driver) and does exactly what the steps say. The other (the navigator) reads the step aloud, watches, and writes the results into the record sheet below. Swap roles at step 4.

**The record sheet.** Before you begin, create a file `notes.md` in this directory (in the exercise repository, not your own) and paste this into it. You fill it in as you go, and it is what you show at the end.

```
Pair:
Own repository:                    Tool used on it:

Step 1  Old-style file: lines total ___  lines kept ___
Step 2  Instruction files found in own repo (path : lines):

Step 3  Session baseline (Claude Code /context total tokens, or Copilot: extensions + MCP servers + instruction lines):

Step 4  Lines cut ___   Lines kept ___   Procedures moved to skills ___
Step 6  Other things loaded, and what would break without each:

Step 7  Question asked:            Answer good enough? (y/n) and why:
Step 8  Skills after install ___   Agents ___   /context total after install ___
One number for what the change saves per prompt:
```

## Why this activity exists

Context is the scarcest resource an agent has. Everything loaded at session start spends attention before you have asked a question, and the model's ability to use what is in the window degrades as the window fills: Anthropic's own engineering guidance calls this context rot and describes the job as finding "the smallest possible set of high-signal tokens" for the task. A Claude Code session starts by loading the system prompt, your CLAUDE.md files, the one-line descriptions of every skill, memory, and the names of every MCP tool; a few thousand tokens before you type, and every file the agent reads afterwards is another one to two thousand on top. Copilot loads its instruction files the same way, on every request.

The second half of the why is newer than that. The instruction files most teams carry were written for 2024 and 2025 models, which needed to be told things twice, needed capital letters to notice a rule, and needed to be ordered to verify their work. The Claude 5 generation does not. In July 2026 the Claude Code team removed more than 80% of Claude Code's own system prompt for Opus 5 and Fable 5 with no measurable loss on coding evaluations, and wrote up what they learned as six shifts: rules to judgement ("write code that reads like the surrounding code" instead of a list of formatting bans), examples to interface design, upfront information to progressive disclosure, repetition to a single description, manual memory to auto-memory, and thin specs to rich references such as tests and rubrics. The advice for CLAUDE.md is to "keep your CLAUDE.md lightweight and briefly describe what your repo is for, but spend most of the tokens on gotchas inside of the codebase".

The consequence is that an instruction file tuned for the old models now does harm on the new ones. A rule stated in capitals is followed too literally. A rule stated twice is followed twice. An instruction to double-check or to run a verification step makes a model that already verifies do it again, at your cost: the Opus 5 prompting guide says to remove those lines outright because they "cause over-verification" and "add cost without improving results". Anti-formatting and anti-narration rules written to restrain older models push the new ones the wrong way. A 30-line procedure in an instructions file is loaded into every session that does not need it; it belongs in a skill that loads on demand. Bloated instruction files do not make the agent better informed. They make it ignore your actual instructions, burn tokens, and exhaust the window faster.

So this activity is not "tidy the config". It is: find out what your agent is carrying on your own repository, work out which of it was written for a model you no longer use, and leave with the change ready to make.

## The smells

Every line of an instructions file gets one of these tags, or `[KEEP]`:

- `[SHOUT]` **Shouting.** ALWAYS, NEVER, CRITICAL, IMPORTANT on more than one line. When everything is emphasised, nothing is, and a Claude 5 model follows each one literally.
- `[REPEAT]` **Repetition.** The same rule stated twice, or stated here and again inside a skill or a second instructions file.
- `[VERIFY]` **Verification orders.** "Double-check", "verify before responding", "run the tests again at the end". The model already does this; the line makes it do it twice.
- `[OLD]` **Restraints for a model you no longer use.** Bans on bullet points, on narration, on comments, on planning documents. Replace with judgement ("match the surrounding code") or delete.
- `[PROC]` **Procedures.** Anything longer than a few lines describing how to do a task. That is a skill, loaded when the task calls for it, not a tax on every session.
- `[SHOWN]` **Things the repository already shows.** Directory listings, file-by-file descriptions, standard language conventions, anything the agent can read for itself.

What usually survives is the purpose line, the gotchas, and pointers to where things live.

## Steps

### Step 1. Calibrate on the old-style file (5 minutes, exercise repository)

1. In VS Code, open `activities/03-config-checkup/old-style-CLAUDE.md`. Save a copy next to it as `old-style-CLAUDE.audit.md` (File, Save As) so the original stays clean.
2. Work down the copy one line at a time. The navigator reads the line aloud; the driver types a tag at the start of it: one of the six smell tags above, or `[KEEP]`. Blank lines and headings get no tag. Do not discuss any line for more than fifteen seconds; if you disagree, tag it with both and move on.
3. When you reach the end, count the keeps. In the VS Code search box (`Ctrl+F`) search for `[KEEP]`; the count appears on the right of the box. Write the total number of tagged lines and the number of keeps into the record sheet.
4. Open the exercise repository's real `CLAUDE.md` beside it (drag the tab to the right to split the editor). That file is roughly what your `[KEEP]` lines say. That is the end state you are aiming for on your own repository.

Five minutes, then stop, whether or not you reached the end of the file.

### Step 2. Find the instruction files in your own repository (3 minutes, own repository)

1. Open your own repository in VS Code (File, Open Folder).
2. Open a terminal inside VS Code (`` Ctrl+` ``). Run the command for your shell. It lists every instructions file the tools read, with its line count.

   PowerShell:
   ```
   Get-ChildItem -Recurse -Force -File -Include CLAUDE.md,AGENTS.md,copilot-instructions.md,*.instructions.md | Where-Object { $_.FullName -notmatch '\\node_modules\\|\\bin\\|\\obj\\' } | ForEach-Object { "{0,6}  {1}" -f (Get-Content $_.FullName | Measure-Object -Line).Lines, $_.FullName }
   ```
   Bash (macOS, Linux, Git Bash):
   ```
   find . \( -name CLAUDE.md -o -name AGENTS.md -o -name copilot-instructions.md -o -name '*.instructions.md' \) -not -path '*/node_modules/*' -not -path '*/bin/*' -not -path '*/obj/*' | xargs wc -l
   ```
3. Also check the two places the command does not reach: a `.claude/rules/` folder in the repository root (open it in the file explorer; each `.md` file in it is an instructions file), and your personal file at `C:\Users\<you>\.claude\CLAUDE.md` (or `~/.claude/CLAUDE.md`), which loads into every Claude Code session on every project. Open each and note its line count from the status bar at the bottom right of VS Code, which shows `Ln x` with the cursor on the last line.
4. Write every path and line count into the record sheet. If the list is empty, skip to step 5.

### Step 3. Measure what a session on your repository carries (3 minutes, own repository)

**Claude Code** (only if approved for this repository): in the terminal, in the repository folder, run `claude`. When the prompt appears, type `/context` and press Enter. It prints a breakdown: system prompt, CLAUDE.md, skills, MCP tools, memory, with a token count for each and a total. Write the total and the CLAUDE.md line into the record sheet. Type `/exit` to leave.

**Copilot:** there is no token count. Instead record three things. Extensions: open the Extensions view (`Ctrl+Shift+X`), type `@installed` in its search box, and count what is listed. MCP servers: open the Command Palette (`Ctrl+Shift+P`), type `MCP: List Servers`, press Enter, and count what is listed. Instruction lines: add up the line counts from step 2. Write all three into the record sheet. If you want a token number to attach to it, do the Claude Code version of this step on the exercise repository instead and note that it is a different repository.

### Step 4. Audit your own instruction files and make the change (8 minutes, own repository)

Swap driver and navigator.

1. In the terminal, create a branch so nothing touches your main line: `git switch -c config-checkup`.
2. Open the largest instructions file from step 2. Tag every line exactly as in step 1: the navigator reads, the driver types the tag at the start of the line. Fifteen seconds a line at most.
3. Now make the edit, in the same file: delete every line tagged `[SHOUT]`, `[REPEAT]`, `[VERIFY]`, `[OLD]` or `[SHOWN]`, then remove the `[KEEP]` tags from what remains. For a `[SHOUT]` line whose rule you want to keep, keep the rule and drop the capitals; if one rule genuinely matters more than the rest, that one line may keep its emphasis.
4. For each block tagged `[PROC]`, move it out. Create a folder and a file: for Claude Code `.claude/skills/<short-name>/SKILL.md`, for Copilot `.github/skills/<short-name>/SKILL.md`. Paste the procedure into it under this header, and put one line back in the instructions file that names the skill and when to use it:
   ```
   ---
   name: <short-name>
   description: <one sentence saying when to use this>
   ---
   ```
5. Save. Run `git diff --stat` in the terminal and write the lines removed and lines kept into the record sheet. Claude Code users: run `claude`, type `/doctor`, press Enter, and read what it still proposes to cut; act on anything you agree with.

Do not open, edit or ask about any other file in the repository. The scope is the instruction files.

### Step 5. If there was no instructions file, write one (8 minutes, own repository)

1. `git switch -c config-checkup`.
2. Create the file: for Copilot `.github/copilot-instructions.md`, for Claude Code `CLAUDE.md` in the repository root. (If both tools are used on the repository, create both with the same content.)
3. Write it in three parts, under 30 lines in total. First, one line saying what the repository is for. Second, the gotchas: the things an agent cannot discover by reading the code, such as the build step that is not obvious, the folder that must not be edited, the convention that differs from the language default, the command that must not be run locally. Third, pointers: where the values live that the agent must never copy. Nothing the repository already shows; if it is visible in the folder tree or the code, it is not a gotcha.
4. Save, and go to step 6.

### Step 6. List everything else the session loads (2 minutes)

**Claude Code:** in a session on the exercise repository, type each of these and note what comes back: `/mcp` (servers), `/plugin` (installed plugins), `/skills`, `/agents`, `/hooks`, `/memory`. **Copilot:** the extensions and MCP servers from step 3, plus any `.github/prompts/` and `.github/skills/` folders in the repository.

For each item, answer as a pair in one line: what would break if this were removed? Write the list and the answers into the record sheet. Anything you could not answer for is the next thing to switch off; you do not have to switch it off now.

### Step 7. Ask the agent one question (2 minutes, own repository)

In the tool approved for your repository, start a fresh session (`claude` in the terminal, or a new chat in Copilot with the `+` button) and type a question a new joiner would ask, for example:

```
What does this repository do, and how do I build and run its tests?
```

Read the answer against what you now know is loaded. Did it use your instructions file, or did it read the code? Was anything in the answer wrong because of a line you have just cut, or a line you kept? Write one line in the record sheet.

### Step 8. Install the testing skills for the later activities (2 minutes, exercise repository)

**Claude Code:** in a session on the exercise repository, type `/plugin marketplace add dotnet/skills` and press Enter, then `/plugin install dotnet-test@dotnet-agent-skills`, then `/skills` and `/agents` to see what arrived. Count them; around two dozen skills and ten agents. Then `/context` again and write the new total into the record sheet. It will have moved very little, because only each skill's name and description loads at rest and the body arrives when something invokes it. That is progressive disclosure, measured, and it is where the procedures you moved out in step 4 now live.

**Copilot:** open Settings (`Ctrl+,`), search `chat.plugins.enabled` and tick it, then search `chat.plugins.marketplaces` and add `dotnet/skills` to the list. In Copilot Chat type `/plugins` and install `dotnet-test`. If the marketplace is blocked, see the notes.

Last line of the record sheet: one number for what your change saves on every prompt. Claude Code: the difference in `/context` totals between the old and new instructions file (start a fresh session to measure the new one). Copilot: the lines removed from step 4.

## Done when

The record sheet is filled in, your own repository has a `config-checkup` branch with the instructions file pruned or written, your pair can say in one sentence what each surviving line prevents, and you have one number for what the change saves on every prompt.

## Fallback

If nobody in the pair has a repository they can open on a laptop in the room, or the tool is not cleared for it, do steps 2 to 7 on the exercise repository instead. First copy the planted file over the real one so there is something to cut: in the exercise repository terminal, `Copy-Item activities/03-config-checkup/old-style-CLAUDE.md CLAUDE.md` (PowerShell) or `cp activities/03-config-checkup/old-style-CLAUDE.md CLAUDE.md` (Bash). Measure with `/context`, prune it back, measure again, then restore the original with `git checkout CLAUDE.md`. The lesson is the same; only the diff is less useful on Monday.

## Notes

If the plugin marketplace is blocked on your machines, use the copy vendored under `vendor/dotnet-skills`. It is itself a local marketplace, so the same two commands work with a path: `/plugin marketplace add ./vendor/dotnet-skills`, then `/plugin install dotnet-test@dotnet-agent-skills-local`. For Copilot, `vendor/dotnet-skills/README.md` gives the copy step.

Three habits to carry out of this activity. Clear the session between unrelated tasks (`/clear` in Claude Code, a new chat in Copilot) rather than letting context accumulate; after two failed corrections on the same thing, clear and write a better first prompt. Treat the instructions file like code: owned, reviewed, pruned, and tested by whether behaviour actually changes when a line goes. And when you upgrade the model, re-read the file: what was a necessary restraint for the last model is dead weight, or worse, for the next.

## Sources

- Thariq Shihipar, [The new rules of context engineering for Claude 5 generation models](https://claude.com/blog/the-new-rules-of-context-engineering-for-claude-5-generation-models), Anthropic, 24 July 2026. The 80% figure and the six shifts.
- Michael Segner, [Steering Claude Code: when to use CLAUDE.md, skills, hooks, and subagents](https://claude.com/blog/steering-claude-code-skills-hooks-rules-subagents-and-more), Anthropic, 18 June 2026. "Every line costs tokens whether relevant or not"; procedures belong in skills.
- [Effective context engineering for AI agents](https://www.anthropic.com/engineering/effective-context-engineering-for-ai-agents), Anthropic Engineering, 29 September 2025. Context rot, the smallest set of high-signal tokens, system prompts at the right altitude.
- [Best practices for Claude Code](https://code.claude.com/docs/en/best-practices) and [Explore the context window](https://code.claude.com/docs/en/context-window), Claude Code docs. What loads at startup and what each read costs; "bloated CLAUDE.md files cause Claude to ignore your actual instructions"; `/doctor`.
- [Prompting Claude Opus 5](https://platform.claude.com/docs/en/build-with-claude/prompt-engineering/prompting-claude-opus-5) and [Prompting Claude Fable 5.1](https://platform.claude.com/docs/en/build-with-claude/prompt-engineering/prompting-claude-fable-5-1), Claude Platform docs. Over-verification, self-correction, and the anti-formatting and anti-narration lines to remove.
