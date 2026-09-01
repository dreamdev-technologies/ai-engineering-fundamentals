# 04. The LLM wiki: start one for your own system

Pairs. 30 minutes. A repository your pair works in every day, in the tool already approved for it; the `starter` folder in this directory; `answers.md` in this directory for the before-and-after. How to talk to the tools is in [tools.md](../tools.md).

You will be putting knowledge about your own system into your own repository, so the rule from activity 03 holds: use the tool already approved for that code, and only feed it material you are allowed to. Nothing here changes any code.

## Why this activity exists

An agent that knows nothing about your business guesses. Ask it what a mis-shipped consignment costs, or which system owns the price list, and it will answer confidently from the code in front of it and whatever the word "consignment" means on the internet. The usual fix is retrieval: put the documents somewhere searchable and let the agent pull fragments in when it needs them. That is what most "chat with your docs" tools do, and it has a flaw that Andrej Karpathy named in a gist in April 2026 that has since become the reference for this pattern: "the LLM is rediscovering knowledge from scratch on every question. There's no accumulation." Ask something that needs five documents pieced together and it pieces them together every time, and then forgets.

Karpathy's alternative is the LLM wiki: the agent "incrementally builds and maintains a persistent wiki, a structured, interlinked collection of markdown files", so that the next question starts from what the last one worked out. "The cross-references are already there. The contradictions have already been flagged." It has three layers. Raw sources, which are immutable: documents, transcripts, notes, code. The wiki, "a directory of LLM-generated markdown files". And the schema, "a document (e.g. CLAUDE.md for Claude Code or AGENTS.md for Codex) that tells the LLM how the wiki is structured". Three operations run it: ingest a source into the wiki, query the wiki and file good answers back as new pages, and lint it for contradictions, stale claims and orphan pages. The division of labour is the point: people "curate sources, direct the analysis, ask good questions, and think about what it all means"; the LLM does "all the grunt work: the summarizing, cross-referencing, filing, and bookkeeping" that made every human-maintained wiki rot.

This is also what Anthropic's guidance on context engineering recommends for agents generally: keep "lightweight identifiers (file paths, stored queries, web links)" and load the detail "just in time", rather than pre-loading everything, and put the tokens where they carry signal. The July 2026 write-up of Claude Code's own context says to keep the always-on file short and "spend most of the tokens on gotchas inside of the codebase"; the wiki is where those gotchas live in a form the agent can search before it reads. It is also the item on DORA's 2025 list from the opening talk that people find hardest to picture: "well-kept data the AI can reach". This is what that looks like for a codebase.

Two things follow that the house rules enforce. The wiki is only as good as its worst confident sentence, so every fact names its source and stale values are never copied in. And the wiki must be readable by people, or nobody will maintain it, whoever does the grunt work.

## Goal

Start the wiki for your own system, from nothing, in half an hour: the schema, three ingests from real sources, a lint, and one question answered badly before and well after. You leave with it on a branch.

## What is in the starter folder

`starter/` is a wiki with nothing in it yet, plus the two pieces of automation:

- `wiki/README.md`, the schema: what the wiki is for, how agents load it, the house rules. `wiki/index.md` and `wiki/log.md`, empty. `wiki/raw/`, where source material goes and is never edited.
- `.claude/skills/wiki-ingest/SKILL.md`, a skill that takes whatever you give it (dictated notes, a pasted document, a transcript, a file in `raw/`), extracts the facts, files each one on the right page with its source attached, updates the index and the log, and reports what it could not place. It never invents a fact and never copies a value that lives in code.
- `.claude/agents/wiki-auditor.md` and `.github/agents/wiki-auditor.agent.md`, the lint: checks pages against the house rules and reports, with no power to edit.

## Steps

### Step 1. Ask the question first (3 minutes)

In the tool approved for your repository, in a fresh session, ask one question about your own system that the code cannot answer. Something a new joiner asked in their first week, or something that went wrong because nobody knew it. Paste the question and the answer into `answers.md` under "Before". Expect a confident guess; that is the point.

### Step 2. Install the schema (3 minutes)

1. Copy the contents of `starter/` (the `wiki` folder, the `.claude` folder and the `.github` folder) into the root of your own repository. Drag them in the file explorer, or if both repositories are on the same machine tell the agent: `Copy everything inside <path to the exercise repository>/activities/04-llm-wiki/starter into the root of this repository, merging with any folders that already exist.`
2. Tell the agent: `Create a git branch called llm-wiki and switch to it. Then add one line to <your instructions file: CLAUDE.md or .github/copilot-instructions.md> saying: read wiki/index.md before starting work on anything that depends on how the business or the system works.`
3. Start a fresh session so the skill and the agent are picked up. Type `/` and check `wiki-ingest` is offered.

### Step 3. Ingest, three times (15 minutes, five each)

Each ingest is the same shape: give the agent raw material and one line saying who it is from and when, then `Use the wiki-ingest skill on this.` Read its report; answer its questions; check the pages it wrote. You decide what is true; it does the filing.

**First, dictation.** One of you talks for two minutes about what the business does, who uses the system, and what it costs when something goes wrong. The other types it as spoken, or use voice typing (on Windows, `Win+H` in the chat box), and do not tidy it. Then:

```
This is <name>, <role>, speaking on <date>, describing the business from memory. Use the wiki-ingest skill on it.
```

**Second, a document or a transcript.** One real piece of material you are allowed to use with this tool: a page of onboarding notes, an architecture note, a runbook, the transcript or minutes of a recent call. Save it into `wiki/raw/` (or paste it if it is short). Then:

```
wiki/raw/<file> is <what it is, from whom, dated when>. Use the wiki-ingest skill on it.
```

**Third, the gotchas.** Each of you names two things about the system that nobody has written down: the rule that is not in the code, the thing that breaks if you do it the obvious way, the value that must never be hard-coded. Type them as a list, then:

```
These are gotchas from <names>, <date>, from experience, not from a document. Use the wiki-ingest skill on them.
```

After each ingest, open one page it changed and check three things: every fact has a source, no value was copied that lives in code, and it did not state anything more confidently than you did.

### Step 4. Lint (3 minutes)

Type `Use the wiki-auditor agent to check every page under wiki/` and read the table. Fix what it finds by telling the agent what to change; ignore anything it invents.

### Step 5. Ask the question again (3 minutes)

Start a fresh session and ask exactly the same question as step 1. Paste the answer under "After" in `answers.md`. Then, if the answer is good, file it: `File that answer as a wiki page, add it to the index, and log it.` That is the query operation, and it is how the wiki compounds.

### Step 6. Leave it on a branch (3 minutes)

`Commit everything on the llm-wiki branch with the message "Start the LLM wiki".` On Monday, open the pull request and have the person who knows the most review the pages before they merge; the wiki is only as good as its worst confident sentence.

## Done when

`answers.md` shows the same question answered before and after, the after answer is grounded in pages you can point at, every fact on those pages names a source, the auditor is clean or you can say why not, and the branch exists in your own repository.

## Fallback

If nobody in the pair has a repository they can open, or the tool is not cleared for it, or you have no material you are allowed to use: run the same steps on the exercise repository. Its wiki already exists, so skip step 2, take the architecture page (it has three gaps marked unknown) and ingest what `src/` tells you about them. The exercise repository already has the `wiki-ingest` skill and the auditor.

## Notes

The wiki is a version-controlled artefact like the code: wrong entries get fixed by pull request, not by memory. The three operations you ran, ingest, query with the answer filed back, and lint, are the whole maintenance loop; a standing rule of "one ingest per week, one lint per month" keeps it alive, and the log shows whether it is being kept.

## Sources

- Andrej Karpathy, [LLM Wiki](https://gist.github.com/karpathy/442a6bf555914893e9891c11519de94f), gist, 4 April 2026. The pattern, the three layers, the three operations, the index and log pages, and the division of labour. Deliberately abstract: "it describes the idea, not a specific implementation".
- [Effective context engineering for AI agents](https://www.anthropic.com/engineering/effective-context-engineering-for-ai-agents), Anthropic Engineering, 29 September 2025. Just-in-time retrieval by lightweight identifiers, and the smallest set of high-signal tokens.
- Thariq Shihipar, [The new rules of context engineering for Claude 5 generation models](https://claude.com/blog/the-new-rules-of-context-engineering-for-claude-5-generation-models), Anthropic, 24 July 2026. Keep the always-on file short and spend the tokens on gotchas; progressive disclosure.
- DORA, [State of AI-assisted Software Development 2025](https://dora.dev/research/2025/dora-report/), Google. "Well-kept data the AI can reach" among the capabilities that decide whether AI pays off.
