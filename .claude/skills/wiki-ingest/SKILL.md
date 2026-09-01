---
name: wiki-ingest
description: Turn raw material (dictated notes, a pasted document, a call or meeting transcript, a file under wiki/raw/) into wiki pages that follow the house rules in wiki/README.md, with every fact attributed to its source, then update the index and the log. Use whenever someone gives you source material and says to add it to the wiki.
---

# Wiki ingest

You are given raw material and asked to put what it says into the wiki. The material is the source; the wiki is your output; the person is the editor. You do the filing. They decide what is true.

## Input

One of: text pasted into the conversation (spoken notes typed or dictated, a document, a transcript), or a path under `wiki/raw/`. If it was pasted and is more than a screen long, save it first as `wiki/raw/<yyyy-mm-dd>-<short-name>.md` so the source is kept; raw files are never edited afterwards.

Ask for two things if they are not stated: who or what the source is (a person and role, a document title, a meeting and date) and the date. Put both on every fact that comes from it.

## Steps

1. Read `wiki/README.md` for the house rules and `wiki/index.md` for what already exists. Never create a page on a topic that already has one; merge into it.
2. Read the material once and list the facts in it, one line each. A fact is a claim about the business, the system, a term, a rule, a cost, a person's role, or a way things are done. Opinions, pleasantries and digressions are not facts.
3. For each fact decide where it lives: `business-context.md` (what the business does, who uses what, what a mistake costs), `glossary.md` (a term and what it means here), `architecture.md` (systems, who owns what, integrations), `gotchas.md` (non-obvious behaviour someone depends on), or a new single-topic page if none fits. Point at living values instead of copying them: if the material quotes a rate, a threshold or a date that lives in code or config, write where it lives, not the number.
4. Write or merge the pages. Keep every page under 150 lines; if a merge would exceed that, split by topic and say so. Every page has frontmatter with a one-line `description` and `tags`. Every fact carries its source in brackets: `(Fernando, logistics lead, 2 Sept 2026)` or `(onboarding-notes.md, 2025)`. Where the material contradicts an existing page, keep both statements, mark them `CONFLICT`, and raise it in your report.
5. Cross-link: the new or changed page links to its neighbours, and at least one existing page links to it.
6. Update `wiki/index.md` with a one-line summary per page, and append one line to `wiki/log.md`: date, source, pages changed.

## Report

End with three short lists: pages created or changed; facts you could not place or were unsure of, with the sentence they came from; questions the editor should answer. Nothing else.

## Never

- Invent a fact to fill a gap. A gap is reported, not filled.
- State something more confidently than the source did. "Probably" in the source is "probably" in the wiki.
- Copy a value that lives in code. Point at the file.
- Edit anything under `wiki/raw/`.
