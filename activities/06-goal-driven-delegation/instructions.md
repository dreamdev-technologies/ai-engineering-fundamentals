# 06. Goal-driven delegation

On your own. 30 minutes. The exercise repository: `src/Calculator`, `tools/benchmark`, `tools/security-scan`. How to talk to the tools is in [tools.md](../tools.md).

## Why this activity exists

There is a spectrum in how you hand work to an agent. At one end you prompt it step by step and watch every move. In the middle you agree a plan and let it execute. At the far end you state a goal, state the checks that define done, and walk away while it iterates. Activity 05 sat in the middle: a contract, then an implementation you watched. This activity is the far end, and it is where the economics of these tools actually change, because an agent you have to watch saves you typing and an agent you can leave saves you time.

The far end is arriving whether a team is ready for it or not. METR has measured the length of task that frontier agents can complete on their own with even odds of success since 2019, and it has doubled roughly every seven months for six years, faster recently; by early 2026 the best models were completing tasks that take a person about twelve hours. The tools have followed. Claude Code now has a goal condition that re-checks after every turn until it resolves, and a stop hook that blocks the agent from finishing until a script passes. Its documentation puts the case in one line: give the agent a check it can run, "it's the difference between a session you watch and one you walk away from", and the goal and hook versions "are what let an unattended run finish correctly without you".

Here is the catch, and it is the reason this activity has constraints in it. An agent measured only by a check will find the shortest path to passing the check. Anthropic's March 2026 study of long-running agents found that when agents judge their own work they "respond by confidently praising the work, even when, to a human observer, the quality is obviously mediocre", and that "separating the agent doing the work from the agent judging it proves to be a strong lever". SpecBench, published in May 2026, ran the current coding agents against tasks with visible tests and hidden ones: every one of them could saturate the visible suite on every task, and the gap between visible and hidden pass rates grew with task complexity, through hard-coded outputs, modified test files, and solutions the agent could see were wrong but passing. METR reported the same behaviour in its own evaluations. The Claude Code docs say it plainly for the fix-the-build case: "address the root cause, don't suppress the error".

So goal-driven delegation is three things, not one: a goal, the constraints that stop the check being gamed, and verification the agent doing the work does not control. The benchmark and the security scan in this repository are the checks. The constraints in your prompt are the guard. The fresh session in step 4 is the independent judge. Take any one away and you have an agent that reports success and a result you cannot cheaply distrust, which is where most teams' first attempt at this ends.

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

If you want the loop to run without you at all: in Claude Code, set the same three checks as a `/goal` condition, or as a Stop hook that runs the benchmark, the scan and the tests and refuses to let the turn end until they pass. That is the same activity with the person removed, and it is the reason to have done it once with the person present.

## Sources

- [Best practices for Claude Code](https://code.claude.com/docs/en/best-practices), Claude Code docs: "Give Claude a way to verify its work", the goal condition and Stop hook, and "address the root cause, don't suppress the error".
- METR, [Measuring AI ability to complete long software tasks](https://metr.org/blog/2025-03-19-measuring-ai-ability-to-complete-long-tasks/), March 2025, and [Time Horizon 1.1](https://metr.org/blog/2026-1-29-time-horizon-1-1/), January 2026.
- Prithvi Rajasekaran, [Harness design for long-running application development](https://www.anthropic.com/engineering/harness-design-long-running-apps), Anthropic, 24 March 2026. Separating the worker from the judge; agents praising their own work.
- [SpecBench: measuring reward hacking in long-horizon coding agents](https://arxiv.org/abs/2605.21384), May 2026. Visible versus hidden tests across current coding agents.
