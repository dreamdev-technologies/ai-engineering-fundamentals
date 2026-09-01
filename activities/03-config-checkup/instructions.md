# 03. Config check-up

Pairs. 20 minutes. The exercise repository, cloned.

## Goal

Know exactly what your agent is carrying before you trust it with anything. Context is the scarcest resource an agent has: everything loaded at session start spends attention before you have asked a question, and performance degrades as the window fills. Bloated instruction files do not make the agent better informed; they make it ignore your actual instructions.

## Steps

1. Open the exercise repository in your assigned tool.
2. **Claude Code:** run `/context`. Read the breakdown: system prompt, CLAUDE.md, skills, MCP servers, memory. Write down the total token count.
   **Copilot:** open `.github/copilot-instructions.md` and any `.github/instructions/*.instructions.md` files (these scope rules to paths via `applyTo`), and list which extensions and MCP servers are active in your editor.
3. Read the repository's `CLAUDE.md` top to bottom. It is deliberately short. Apply the standard test to each line: **would removing this line cause the agent to make mistakes?** If not, it is a cut. Also check the opposite failure: is anything here something the agent could discover by reading the repository? That is a cut too.
4. Check what is always-on against what should be on-demand. CLAUDE.md loads every session, so it should hold only what applies broadly; knowledge needed occasionally (how to write acceptance tests, how to verify work) belongs in skills, which load only when relevant. Confirm this repository follows that split.
5. **Install a skill set and watch the split in action.** Add the .NET team's testing skills, which activities 05 to 09 use. **Claude Code:** `/plugin marketplace add dotnet/skills`, then `/plugin install dotnet-test@dotnet-agent-skills`, then `/skills` to list what arrived. **Copilot:** plugin support is preview, so set `chat.plugins.enabled` to true and add `dotnet/skills` to `chat.plugins.marketplaces` in `settings.json`, then use `/plugins` in Copilot Chat. Around two dozen skills and ten agents have just arrived; count them in the `/skills` and `/agents` output rather than taking that number on trust. Check your token count again before guessing what that cost. It is close to nothing, because only each skill's name and description loads at rest; the body arrives when something invokes it. That is step 4's on-demand half, measured rather than asserted.
6. List everything else that is loaded: MCP servers, plugins, skills, memory files, hooks. For each item, answer as a pair: what would break if this were removed? Remove or disable anything you could not answer for. In Claude Code, check the token count once more and note what the pruning bought you.
7. Ask the agent one question about the repository, for example: what does this repository do, and how do I run its tests? Judge the answer against what you now know is loaded.

## Done when

Your pair can name every item loaded into the session and say why it is there, anything you could not justify is gone, and you can state the always-on versus on-demand split in one sentence with a token count behind it.

## Notes

If the plugin marketplace is blocked on your machines, use the copy vendored under `vendor/dotnet-skills`. It is itself a local marketplace, so the same two commands work with a path: `/plugin marketplace add ./vendor/dotnet-skills`, then `/plugin install dotnet-test@dotnet-agent-skills-local`. For Copilot, `vendor/dotnet-skills/README.md` gives the copy step. Either way they are markdown files under version control like anything else, which is the point. The same hygiene applies to both tools; only the file names differ. Two habits to carry out of this exercise: clear the session between unrelated tasks rather than letting context accumulate, and treat the instructions file like code, pruned regularly, tested by whether behaviour actually changes.
