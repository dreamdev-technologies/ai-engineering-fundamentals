# benchmark

Generates the number of line items in `budget.json`, runs `LandedCostCalculator.Calculate` over all of them and reports the elapsed time against `maxMilliseconds`. Exit code 0 means within budget; anything else is a fail, including the calculator throwing.

Run it with `tools/benchmark/run.ps1` (Windows) or `tools/benchmark/run.sh`. The budget is deliberately tight enough that an implementation which does something silly per line (re-parsing rate tables, allocating heavily, quadratic work over the lines) will miss it. Activity 06 hands a failing run to an agent.
