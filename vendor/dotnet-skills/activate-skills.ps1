# Copies the vendored dotnet-test skills and agents into the locations both Claude Code and VS Code read.
$root = (Resolve-Path "$PSScriptRoot/../..").Path
$src = Join-Path $PSScriptRoot "plugins/dotnet-test"
New-Item -ItemType Directory -Force (Join-Path $root ".claude/skills") | Out-Null
New-Item -ItemType Directory -Force (Join-Path $root ".github/agents") | Out-Null
Get-ChildItem (Join-Path $src "skills") -Directory | ForEach-Object {
    Copy-Item $_.FullName (Join-Path $root ".claude/skills" $_.Name) -Recurse -Force
}
Get-ChildItem (Join-Path $src "agents") -File | ForEach-Object {
    Copy-Item $_.FullName (Join-Path $root ".github/agents" $_.Name) -Force
}
Write-Host "Copied $((Get-ChildItem (Join-Path $src 'skills') -Directory).Count) skills to .claude/skills and $((Get-ChildItem (Join-Path $src 'agents') -File).Count) agents to .github/agents"
