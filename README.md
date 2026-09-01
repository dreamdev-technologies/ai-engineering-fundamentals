# AI Engineering Fundamentals

Material for a hands-on session on delegating work to coding agents without losing control of quality. The question the whole day answers: how far can you delegate? Exactly as far as you can verify.

This repository is meant to be used, not read. Take a copy, work in it, commit to it, open pull requests against it. It is also an example of the thing it teaches: a version-controlled single source of truth that both people and agents can read.

## Getting your own copy

Use the **Use this template** button rather than cloning this repository directly. Each pair needs somewhere they can commit and open a pull request, and several activities finish with one.

## What is here

`activities/` holds a library of standalone activities, not a fixed agenda. Each `instructions.md` is a self-contained tutorial with its own time budget and a done criterion you can check yourselves. Start with [activities/README.md](activities/README.md), which lists them with times and dependencies.

Nothing in these activities touches your own codebase. They run against the exercise code in this repository and nothing else.

## What is not here yet

The exercise scaffold the activities from 03 onward refer to: `src/Calculator`, `src/Legacy`, `src/TestArchetypes`, the seeded `wiki/`, and the `tools/benchmark` and `tools/security-scan` scripts. Activities 02 and the close run without it. The rest need it, so check it has landed before running a session that depends on them.

## Tools

Activities are written for Claude Code and GitHub Copilot, and call out any difference between the two. Activities 03 and 05 to 09 use the `dotnet-test` plugin from the .NET team's skills repository at [dotnet/skills](https://github.com/dotnet/skills); activity 03 installs it. If your machines cannot reach a plugin marketplace, the skills are plain markdown and can be vendored into this repository instead.

## Requirements

.NET 8 or later, git, and either Claude Code or GitHub Copilot in your editor.
