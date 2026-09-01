# 04. The LLM wiki

Pairs. 30 minutes. The exercise repository; the `wiki/` directory.

## Goal

Give the agent business context it can read, and prove to yourselves that the quality of an agent's answer is set by the quality of the context you hand it.

The wiki follows the LLM wiki pattern: three layers. Raw sources (code, documents) that nobody edits by hand for this purpose; the wiki itself, markdown the agent helps maintain; and the schema (`wiki/README.md` plus `CLAUDE.md`) that defines the conventions. The agent does the grunt work of drafting and cross-referencing; you own what is true.

## House rules for every wiki file

These are what make the wiki work for both audiences, and step 5 checks them:

- **One topic per file, and small.** A file stays under roughly 150 lines. If it grows past that, split it; an oversized file crowds out everything else in the agent's context.
- **A searchable header.** Every file opens with YAML frontmatter: a one-line `description` and `tags`, using glossary terms. Agents find files by grep before they read them, so the words in the header are the words a search will use.
- **Navigable by humans.** `wiki/index.md` lists every page with a one-line summary; every page links to its neighbours. If a person cannot browse it, they will not maintain it, and an unmaintained wiki rots into confident lies.
- **Point at living values, never copy them.** Write "the duty rates live in `src/Calculator/DutyRates.cs`", not a table of rates that will silently go stale. Stale facts stated confidently are the most dangerous content a wiki can hold.

## Steps

Each pair takes one layer, assigned by the facilitator: `business-context.md`, `architecture.md`, `glossary.md` or `gotchas.md`.

1. **Before touching anything**, ask the agent one domain question your layer should answer. Examples: What does a mis-shipped consignment cost this business? Which services touch a price list? Which behaviours in the legacy service are deliberate? Save the answer to a scratch file.
2. Read your assigned wiki file against the house rules above and against the code in `src/`. List what is missing, what is wrong, and anything that breaks a rule.
3. Hydrate the layer. Let the agent draft; verify every fact against the code yourselves before it goes in. Add the frontmatter header if the file lacks one, and keep the file inside the size limit.
4. Update the navigation: add or fix your page's line in `wiki/index.md`, cross-link related pages, and make sure `CLAUDE.md` (and `.github/copilot-instructions.md` for Copilot) points agents at the wiki.
5. **Lint.** Ask the agent to audit your layer against the house rules: contradictions with other pages, claims with no source in the code, missing cross-references, oversized files. Fix what it finds; ignore what it invents.
6. Start a fresh agent session and ask the same question from step 1. Put the two answers side by side.

## Done when

The before and after answers differ visibly, every improvement traces to a line your pair wrote, and your layer passes the house rules: header present, size inside the limit, indexed, cross-linked.

## Notes

The wiki is a version-controlled artefact like the code: wrong entries get fixed by pull request, not by memory. Good answers the agent gives you can be filed back in as new content, so the wiki compounds. If your before answer was already good, your question was too easy; ask a harder one and rerun the comparison.
