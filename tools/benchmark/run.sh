#!/usr/bin/env bash
# Runs the calculator benchmark against tools/benchmark/budget.json. Exit code 0 = within budget.
dir="$(cd "$(dirname "$0")" && pwd)"
dotnet run --project "$dir/Benchmark.csproj" -c Release --nologo
