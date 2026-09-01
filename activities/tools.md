# Working with the tools

Read once. Every activity assumes this page and does not repeat it.

## The two tools

- **Copilot** means GitHub Copilot in VS Code. Open the chat panel with `Ctrl+Alt+I`. In the picker at the bottom of the panel choose **Agent**, so it can read files and run commands, not just answer.
- **Claude Code** means the `claude` command. In VS Code open a terminal (Terminal menu, New Terminal) in the repository folder and type `claude`. Type `/exit` to leave.

## Ask in plain English

You never need to know the command. Say what you want:

```
Run the tests and show me the output.
Run tools/benchmark/run.ps1 and show me the output.
Create a branch called activity-05 and switch to it.
Commit everything with the message "Story 1 tests".
Show me the diff.
```

On macOS or Linux say `.sh` instead of `.ps1`; the agent will work it out anyway.

## Start clean

Between stories, between activities, and after two failed corrections on the same thing: Claude Code `/clear`, Copilot the **+** (New Chat) button. A long session full of failed attempts makes the next attempt worse.

## Use a skill

A skill is an instructions file the tool loads only when asked. Two ways to use one, and both work:

- Type `/` and start typing its name, then pick it from the list, with the file it should look at: `/grade-tests src/Calculator.Tests/Story1Tests.cs`. In Claude Code, skills from a plugin may appear as `/dotnet-test:grade-tests`.
- Or name it in words: `Use the grade-tests skill on src/Calculator.Tests/Story1Tests.cs`.

Claude Code lists them with `/skills`. Copilot lists them when you type `/`.

## Use an agent

An agent is a separate worker with its own instructions and its own tool access. Name it in words: `Use the wiki-auditor agent to check wiki/glossary.md`. Claude Code lists them with `/agents`; Copilot shows them in the agent picker at the top of the chat panel.

## See what the session is carrying

Claude Code: `/context` for the token breakdown, then `/skills`, `/agents`, `/mcp`, `/plugin`, `/hooks` and `/memory` for the lists. Copilot has no token count: the Extensions view (`Ctrl+Shift+X`, type `@installed`) and the Command Palette entry `MCP: List Servers` show what is attached.

## When it says it is done

Ask: `Show me the commands you ran and their output.` Then have it run the check again in a fresh session, or run it yourself, and read the raw output. The agent that did the work is never the only one grading it.

## Working through it

Each activity is written to be done on your own, at your own laptop, in your own copy of the repository. When the facilitator walks one person through an activity on the projector, follow along on your own copy rather than watching. Ask the moment you are stuck; nobody is expected to know these tools yet.
