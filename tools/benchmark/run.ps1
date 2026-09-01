# Runs the calculator benchmark against tools/benchmark/budget.json. Exit code 0 = within budget.
dotnet run --project "$PSScriptRoot/Benchmark.csproj" -c Release --nologo
exit $LASTEXITCODE
