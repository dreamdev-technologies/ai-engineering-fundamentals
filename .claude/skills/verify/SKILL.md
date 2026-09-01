---
name: verify
description: What to run before claiming any change in this repository is done, and what evidence to show. Use whenever you are about to say work is complete, tests pass, or a goal is met.
---

# Verify

Done means the checks ran and you can show their output. Not "should pass", not "I believe".

## Run, in this order

1. `dotnet build --nologo` from the repository root. Zero errors. Note any new warnings.
2. `dotnet test --nologo` from the repository root. Every project green. Quote the summary line for each test project.
3. If the task touched `src/Calculator`, run `tools/benchmark/run.ps1` (or `run.sh`). Report elapsed time against the budget in `tools/benchmark/budget.json`.
4. If the task touched dependencies or anything under `src/`, run `tools/security-scan/run.ps1` (or `run.sh`). Report PASS or FAIL and the findings.

## Report

- The exact commands you ran and their output, pasted, not paraphrased.
- Any check you did not run, and why.
- Any test you changed, with the reason. Changing a test to make it pass is not a fix; if you think a test is wrong, stop and say so.

## Never

- Claim green without output.
- Skip a check because it is slow.
- Relax a budget, suppress a finding or edit a scan configuration to get to PASS.
