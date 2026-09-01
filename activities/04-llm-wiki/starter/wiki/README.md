---
description: What the wiki is for, how agents load it, and the house rules for adding to it.
tags: [wiki, conventions]
---

# The wiki

Written context about the business and the system, kept in the repository so that people and agents read the same thing. An agent with no context guesses; an agent with this wiki can be wrong in ways you can trace.

## Three layers

1. **Raw sources.** The code, and anything dropped into `wiki/raw/`: notes, documents, transcripts. Nobody edits these to make the wiki right; the wiki points at them.
2. **The wiki.** The pages here. Agents help draft and cross-reference them; a person owns what is true.
3. **The schema.** This file plus `CLAUDE.md` and `.github/copilot-instructions.md`, which tell agents the wiki exists and how it is organised.

## How agents load it

`CLAUDE.md` and the Copilot instructions point at `wiki/index.md`. Agents find pages by searching the `description` and `tags` in each page's frontmatter before they read the body, so the words in the header are the words a search will use. Nothing in the wiki loads into a session by default; a page costs context only when it is read.

## House rules

- **One topic per file, under 150 lines.** Split anything bigger; an oversized page crowds out everything else in the agent's context.
- **A searchable header.** YAML frontmatter with a one-line `description` and `tags`: the page type (business, architecture, gotchas, glossary) plus the [glossary](glossary.md) terms the page covers.
- **Navigable by people.** Every page is listed in [index.md](index.md) with a one-line summary and links to its neighbours. If a person cannot browse it, they will not maintain it.
- **Point at living values, never copy them.** Write "duty rates live in `src/Calculator/DutyRates.cs`", not a table of rates that will go stale. A stale fact stated confidently is the most dangerous thing a wiki can hold.
- **Fix by pull request.** A wrong page is corrected the way wrong code is: a change, a review, a merge.
- **Every fact names its source.** A person and date, or a document. A fact with no source is a guess.

## Adding a page

Create the file with frontmatter, keep it inside the size limit, add its line to `index.md`, link it from the page it is most related to, append a line to `log.md`, then run the `wiki-auditor` agent (`.claude/agents/`) over it before opening the pull request. Those are the three operations of the LLM wiki pattern: ingest, query with good answers filed back, and lint.
