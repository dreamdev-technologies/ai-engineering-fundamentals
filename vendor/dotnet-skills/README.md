# Vendored copy of dotnet/skills (dotnet-test plugin)

An offline copy of the `dotnet-test` plugin from [dotnet/skills](https://github.com/dotnet/skills), taken from commit `34950f875e1db782ab97417bfb6e44d1c4a9acf9`, MIT licensed (see LICENSE). Use it only if the plugin marketplace is blocked on your machine; otherwise install from GitHub as activity 03 describes, because that copy is kept current and this one is not.

## Simplest: no plugin at all

`/plugin` is not available in every place Claude Code runs. The skills are plain markdown, so the reliable route is to ask the agent:

```
Copy every folder under vendor/dotnet-skills/plugins/dotnet-test/skills into .claude/skills, and every file under vendor/dotnet-skills/plugins/dotnet-test/agents into .github/agents. Do not change their contents.
```

Both tools read `.claude/skills/`. Start a fresh session afterwards.

## Claude Code, where `/plugin` works

This folder is itself a plugin marketplace, so the install is the same two commands with a local path:

```
/plugin marketplace add ./vendor/dotnet-skills
/plugin install dotnet-test@dotnet-agent-skills-local
```

## GitHub Copilot in VS Code

Copilot reads skills from `.claude/skills/` and `.github/skills/` in the repository. Copy the skills across with:

```
powershell -ExecutionPolicy Bypass -File vendor/dotnet-skills/activate-skills.ps1
```

That puts a copy of every skill under `.claude/skills/`, which both tools read, and the agents under `.github/agents/`. Delete them again afterwards if you want activity 03's before-and-after measurement to mean something.
