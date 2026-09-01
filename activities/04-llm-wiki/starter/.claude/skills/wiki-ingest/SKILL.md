---
name: wiki-ingest
description: Turn raw material (dictated notes, a pasted document, a call or meeting transcript, a file under wiki/raw/) into wiki pages that follow the house rules in wiki/README.md, with every fact attributed to its source, then update the index and the log. Use whenever someone gives you source material and says to add it to the wiki.
---

# Wiki ingest

You are given raw material and asked to put what it says into the wiki. The material is the source; the wiki is your output; the person is the editor. You do the filing. They decide what is true.

## Input

One of: text pasted into the conversation (spoken notes typed or dictated, a document, a transcript), or a path under `wiki/raw/`. Always save pasted or dictated material to `wiki/raw/<yyyy-mm-dd>-<short-name>.md` before filing it, however short, so the verbatim source is kept. Raw files are never edited afterwards.

Ask for two things if they are not stated: who or what the source is, and the date. Attribute every fact in a fixed form: `(Name, role, D Mon YYYY)` for a person, `(document title, owner, year or date)` for a document.

## Steps

1. Read `wiki/README.md` for the house rules and `wiki/index.md` for what already exists. If `business-context.md`, `glossary.md`, `architecture.md` or `gotchas.md` does not exist yet, create it with frontmatter and a heading. Never create a second page on a topic that already has one; merge into the existing page.
2. Read the material once and list the facts in it, one line each. A fact is a claim about the business, the system, a term, a rule, a cost, a person's role, or a way things are done. Opinions, pleasantries and digressions are not facts.
3. For each fact decide the one page it lives on: `business-context.md` (what the business does, who uses what, what a mistake costs), `glossary.md` (a term and what it means here), `architecture.md` (systems, who owns what, integrations), `gotchas.md` (non-obvious behaviour someone depends on), or a new single-topic page if none fits. A fact appears in full on one page only; other pages that need it link to that section rather than restating it.
   - Living values: if the material quotes a rate, a threshold or a date and you can find the file or config it lives in, write where it lives, not the number. If you cannot find where it lives, keep the value, attribute it to the source, and list it in the report under facts to verify. Times, thresholds and dates that exist only in a document are attributed to that document.
   - Glossary: only write a definition the source actually gave. If the source uses a term without defining it, write "used by (source) without a definition" and add the term to the questions list. Never define a term from general knowledge and attribute it to the source.
4. Write or merge the pages. Keep every page under 150 lines; if a merge would exceed that, split by topic and report the split. Every page has frontmatter with a one-line `description` and `tags` that are, or will be after this ingest, glossary terms. Every fact carries its source in the fixed form. Where the material contradicts, or may contradict, an existing statement, put both statements next to each other on the page that owns the topic, prefix the pair with `CONFLICT:`, and report it; do not repeat the marker on other pages, link to it.
5. Cross-link: the new or changed page links to its neighbours, and at least one existing page links to it. On the first ingest the index row satisfies this.
6. Update `wiki/index.md` with a one-line summary per page, and append one line to `wiki/log.md`: date, source, pages changed. Do these in the same pass as the page edits, not as separate steps.
7. Run the `wiki-auditor` agent over the pages you changed and add its findings count to the log line.

## Report

End with four short lists: pages created or changed; facts to verify (values you could not point at, and any CONFLICT), with the sentence each came from; terms used without a definition; questions the editor should answer. Nothing else.

## Never

- Invent a fact or a definition to fill a gap. A gap is reported, not filled.
- State something more confidently than the source did. "Probably" in the source is "probably" in the wiki. Editorial advice ("confirm with compliance before relying on this") is not a fact; leave it out.
- Copy a value that lives in code or config. Point at the file.
- Edit anything under `wiki/raw/`.
