# AI Engineering Fundamentals

Material for a hands-on session on delegating work to coding agents without losing control of quality. The question the whole day answers: how far can you delegate? Exactly as far as you can verify.

This repository is meant to be used, not read. Take a copy, work in it, commit to it, open pull requests against it. It is also an example of the thing it teaches: a version-controlled single source of truth that both people and agents can read.

## Getting your own copy

Use the **Use this template** button rather than cloning this repository directly. Each pair needs somewhere they can commit and open a pull request, and several activities finish with one. If you cannot see the button, you do not have access to the template yet; ask the facilitator.

## What is here

| Path | What it is |
|---|---|
| `activities/` | A library of standalone activities, not a fixed agenda. Start with [activities/README.md](activities/README.md) |
| `wiki/` | Written context about the fictional business and its systems. Start at [wiki/index.md](wiki/index.md) |
| `src/Calculator` | The new landed cost calculator: a domain model and an empty calculator. Activities 05 and 06 fill it in |
| `src/Calculator.Tests` | Where the acceptance tests go. Wired up, nearly empty |
| `src/TestArchetypes` | Builders that produce realistic default domain objects for tests |
| `src/Legacy` | A slice of the legacy pricing system, left the way it was found. Activities 07 and 09 |
| `src/Legacy.Tests` | Where the characterisation tests go. Empty on purpose |
| `tools/benchmark`, `tools/security-scan` | The checks activity 06 hands to an agent |
| `CLAUDE.md`, `.github/copilot-instructions.md` | Agent instructions, deliberately short. Activity 03 reads them |
| `.claude/skills/`, `.claude/agents/`, `.github/agents/` | Two skills and one worked example agent. Activity 08 adds more |
| `vendor/dotnet-skills` | An offline copy of the .NET testing plugin, for machines that cannot reach the marketplace |

The activities run against the code in this repository and change nothing in any other codebase. The one exception is activity 03, which reads the instruction files of a repository you already work in, using the tool already approved for it, and leaves you a diff to review. The business here, Meridian Fresh, is fictional.

## Running the code

```
dotnet build
dotnet test
tools/benchmark/run.ps1
tools/security-scan/run.ps1
```

Use the `.sh` versions on macOS or Linux. From a clean clone, build and tests pass. The benchmark and the security scan fail, on purpose: the calculator is not implemented yet and the scaffold carries two planted security findings. Activities 05 and 06 fix that. Every build also prints NU1903 warnings about a vulnerable package; that is one of the two findings, not a problem with your machine.

## Tools

Activities are written for Claude Code and GitHub Copilot in VS Code, and call out any difference between the two. Activities 05 and 07 to 09 use the `dotnet-test` plugin from the .NET team's skills repository, [dotnet/skills](https://github.com/dotnet/skills); activity 03 installs it. If the marketplace is blocked on your machine, `vendor/dotnet-skills/README.md` explains the offline install.

## Requirements

.NET SDK 8 or later (the projects target .NET 8 and roll forward to whatever runtime you have), git, access to nuget.org for package restore, and Claude Code or GitHub Copilot in your editor.
