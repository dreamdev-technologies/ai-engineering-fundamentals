# 04. The LLM wiki

Pairs. 30 minutes. The exercise repository; the `wiki/` directory; `answers.md` in this folder for the before-and-after. How to talk to the tools is in [tools.md](../tools.md).

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

The wiki is a version-controlled artefact like the code: wrong entries get fixed by pull request, not by memory. Good answers the agent gives you can be filed back in as new content, so the wiki compounds. If your before answer was already good, your question was too easy; ask a harder one and rerun the comparison.
