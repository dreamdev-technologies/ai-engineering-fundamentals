# 04. The LLM wiki

Pairs. 30 minutes. The exercise repository; the `wiki/` directory; `answers.md` in this folder for the before-and-after. How to talk to the tools is in [tools.md](../tools.md).

## Why this activity exists

An agent that knows nothing about your business guesses. Ask it what a mis-shipped consignment costs, or which system owns the price list, and it will answer confidently from the code in front of it and whatever the word "consignment" means on the internet. The usual fix is retrieval: put the documents somewhere searchable and let the agent pull fragments in when it needs them. That is what most "chat with your docs" tools do, and it has a flaw that Andrej Karpathy named in a gist in April 2026 that has since become the reference for this pattern: "the LLM is rediscovering knowledge from scratch on every question. There's no accumulation." Ask something that needs five documents pieced together and it pieces them together every time, and then forgets.

Karpathy's alternative is the LLM wiki: the agent "incrementally builds and maintains a persistent wiki, a structured, interlinked collection of markdown files", so that the next question starts from what the last one worked out. "The cross-references are already there. The contradictions have already been flagged." It has three layers. Raw sources, which are immutable: here, the code under `src/` and the stories. The wiki, "a directory of LLM-generated markdown files": here, `wiki/`. And the schema, "a document (e.g. CLAUDE.md for Claude Code or AGENTS.md for Codex) that tells the LLM how the wiki is structured": here, `wiki/README.md` and the pointer in `CLAUDE.md`. Three operations run it: ingest a source into the wiki, query the wiki and file good answers back as new pages, and lint it for contradictions, stale claims and orphan pages. The division of labour is the point: people "curate sources, direct the analysis, ask good questions, and think about what it all means"; the LLM does "all the grunt work: the summarizing, cross-referencing, filing, and bookkeeping" that made every human-maintained wiki rot.

This is also what Anthropic's guidance on context engineering recommends for agents generally: keep "lightweight identifiers (file paths, stored queries, web links)" and load the detail "just in time", rather than pre-loading everything, and put the tokens where they carry signal. The July 2026 write-up of Claude Code's own context says to keep the always-on file short and "spend most of the tokens on gotchas inside of the codebase"; the wiki is where those gotchas live in a form the agent can search before it reads. It is also the item on DORA's 2025 list from the opening talk that people find hardest to picture: "well-kept data the AI can reach". This is what that looks like for a codebase.

Two things follow that the house rules below enforce. The wiki is only as good as its worst confident sentence, so every fact points at the code it came from and stale values are never copied in. And the wiki must be readable by people, or nobody will maintain it, whoever does the grunt work.

## Goal

Give the agent business context it can read, and prove to yourselves that the quality of an agent's answer is set by the quality of the context you hand it: same question, before and after, with every improvement traceable to a line you wrote.

## House rules for every wiki file

These are what make the wiki work for both audiences, and step 5 checks them:

- **One topic per file, and small.** A file stays under roughly 150 lines. If it grows past that, split it; an oversized file crowds out everything else in the agent's context.
- **A searchable header.** Every file opens with YAML frontmatter: a one-line `description` and `tags`, using glossary terms. Agents find files by grep before they read them, so the words in the header are the words a search will use.
- **Navigable by humans.** `wiki/index.md` lists every page with a one-line summary; every page links to its neighbours. If a person cannot browse it, they will not maintain it, and an unmaintained wiki rots into confident lies.
- **Point at living values, never copy them.** Write "the duty rates live in `src/Calculator/DutyRates.cs`", not a table of rates that will silently go stale. Stale facts stated confidently are the most dangerous content a wiki can hold.

## Steps

Each pair takes one layer, assigned by the facilitator: `business-context.md`, `architecture.md`, `glossary.md` or `gotchas.md`.

1. **Before touching anything**, in a fresh session, ask the agent one domain question your layer should answer, and paste the question and the answer into `answers.md`. Pick one that fits your layer:
   ```
   What does a mis-shipped consignment cost this business, and where is that written down?
   Which systems hold a price list, and which one is the source of truth?
   Which behaviours of the legacy pricing are deliberate, and who depends on them?
   ```
2. Read your assigned wiki file against the house rules above and against the code in `src/`. Say out loud what is missing, what is wrong, and anything that breaks a rule.
3. Hydrate the layer. Ask the agent to draft, then check every fact yourselves before it goes in:
   ```
   Read wiki/<your file>.md and the code under src/. Draft the sections that are missing or marked unknown, and for every fact say which file it comes from. Do not edit anything yet; show me the draft.
   ```
   Keep what you can verify against the code, cut what you cannot, and then tell it to write the file. Add the frontmatter header if the file lacks one, and keep the file inside the size limit.
4. Update the navigation: ask the agent to add or fix your page's line in `wiki/index.md` and to link your page from the page it is most related to. Check that `CLAUDE.md` (and `.github/copilot-instructions.md` for Copilot) points agents at the wiki; it should already.
5. **Lint.** Type `Use the wiki-auditor agent to check wiki/<your file>.md` and read the table it returns: contradictions with other pages, claims with no source in the code, missing cross-references, oversized files. Fix what it finds; ignore what it invents.
6. Start a fresh session (`/clear` in Claude Code, New Chat in Copilot) and ask exactly the same question as step 1. Paste the answer under "After" in `answers.md` and read the two side by side.

## Done when

The before and after answers differ visibly, every improvement traces to a line your pair wrote, and your layer passes the house rules: header present, size inside the limit, indexed, cross-linked.

## Notes

The wiki is a version-controlled artefact like the code: wrong entries get fixed by pull request, not by memory. Good answers the agent gives you can be filed back in as new content, so the wiki compounds; if the "after" answer in step 6 is good, ask the agent to file it as a page and add it to the index, and append a line to `wiki/log.md` saying what was ingested. If your before answer was already good, your question was too easy; ask a harder one and rerun the comparison.

## Sources

- Andrej Karpathy, [LLM Wiki](https://gist.github.com/karpathy/442a6bf555914893e9891c11519de94f), gist, 4 April 2026. The pattern, the three layers, the three operations, the index and log pages, and the division of labour. Deliberately abstract: "it describes the idea, not a specific implementation".
- [Effective context engineering for AI agents](https://www.anthropic.com/engineering/effective-context-engineering-for-ai-agents), Anthropic Engineering, 29 September 2025. Just-in-time retrieval by lightweight identifiers, and the smallest set of high-signal tokens.
- Thariq Shihipar, [The new rules of context engineering for Claude 5 generation models](https://claude.com/blog/the-new-rules-of-context-engineering-for-claude-5-generation-models), Anthropic, 24 July 2026. Keep the always-on file short and spend the tokens on gotchas; progressive disclosure.
- DORA, [State of AI-assisted Software Development 2025](https://dora.dev/research/2025/dora-report/), Google. "Well-kept data the AI can reach" among the capabilities that decide whether AI pays off.
