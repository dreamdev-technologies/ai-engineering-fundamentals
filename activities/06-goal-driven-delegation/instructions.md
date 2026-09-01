# 06. Goal-driven delegation

On your own. 30 minutes. The exercise repository: `src/Calculator`, `tools/benchmark`, `tools/security-scan`. How to talk to the tools is in [tools.md](../tools.md).

## Goal

Move one level up the delegation spectrum: hand the agent a goal and a set of checks, not a list of steps, and watch it iterate against the checks. This only works because activity 05 left verification in place.

## Steps

1. Baseline first. Type `Run tools/benchmark/run.ps1 and tools/security-scan/run.ps1 and show me both outputs in full` (`.sh` on macOS or Linux) and write down the two results. The benchmark times the calculator over a 10,000-line input against a budget; the scan fails on findings. At least one of them should be failing at the start.
2. Write the goal prompt. It needs three parts: the goal, the constraints, and the checks the agent must run itself. Start from this and change what you disagree with:
   ```
   Goal: tools/benchmark and tools/security-scan both pass, and the full test suite is green.
   Constraints: do not modify any test, tools/benchmark/budget.json, or anything under tools/security-scan. Fix findings; do not suppress them.
   Before you tell me you are done, run all three checks yourself and show me the output.
   ```
3. Hand it over and watch. Do not steer unless it is genuinely stuck. Keep notes on what it tries: the detours are the interesting part. Watch specifically for the check being gamed rather than met: caching that dodges the benchmark's work, a finding suppressed instead of fixed, a constraint quietly relaxed. An agent optimising a metric will find the shortest path to it, which is exactly why the constraints in step 2 exist.
4. When it declares the goal met, demand the evidence: `Show me the commands you ran and their full output.` Then start a fresh session, so the agent that did the work is not the one grading it, and type the same request as step 1 plus `Run the full test suite`. Read the raw output yourselves.
5. Debrief for five minutes: what did it try that you would not have? Where did it go wrong, and which check caught that? What would this run have produced if the benchmark and scan did not exist?

## Done when

Benchmark within budget, scan clean, tests green, verified by your own runs, and your notes name at least one thing the agent tried that surprised you.

## Notes

The last debrief question is the whole exercise. Without executable checks, a goal-driven agent produces confident work you cannot cheaply distrust. The checks are what let you look away while it iterates, which is what delegation means.
