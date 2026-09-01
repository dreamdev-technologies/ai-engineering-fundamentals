# 06. Goal-driven delegation

Pairs. 30 minutes. The exercise repository: `src/Calculator`, `tools/benchmark`, `tools/security-scan`.

## Goal

Move one level up the delegation spectrum: hand the agent a goal and a set of checks, not a list of steps, and watch it iterate against the checks. This only works because activity 05 left verification in place.

## Steps

1. Baseline first. Run `tools/benchmark` and `tools/security-scan` and record both results. The benchmark times the calculator over a 10,000-line input against a budget; the scan fails on findings. At least one of them should be failing at the start.
2. Write the goal prompt as a pair. It needs three parts: the goal (both tools passing, all existing tests green), the constraints (do not modify the tests, the benchmark budget or the scan configuration), and the verification commands the agent should run itself.
3. Hand it over and watch. Do not steer unless it is genuinely stuck. Keep notes on what it tries: the detours are the interesting part. Watch specifically for the check being gamed rather than met: caching that dodges the benchmark's work, a finding suppressed instead of fixed, a constraint quietly relaxed. An agent optimising a metric will find the shortest path to it, which is exactly why the constraints in step 2 exist.
4. When it declares the goal met, demand the evidence: which commands it ran and their output. Then verify yourselves: run the benchmark, the scan and the full test suite from a clean state. The agent doing the work should never be the only one grading it.
5. Debrief for five minutes: what did it try that you would not have? Where did it go wrong, and which check caught that? What would this run have produced if the benchmark and scan did not exist?

## Done when

Benchmark within budget, scan clean, tests green, verified by your own runs, and your notes name at least one thing the agent tried that surprised you.

## Notes

The last debrief question is the whole exercise. Without executable checks, a goal-driven agent produces confident work you cannot cheaply distrust. The checks are what let you look away while it iterates, which is what delegation means.
