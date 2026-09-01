using System.Diagnostics;
using System.Text.Json;
using Calculator;
using TestArchetypes;

// Times LandedCostCalculator over a generated input and reports pass or fail against budget.json.
// Exit code 0 on pass, 1 on fail or if the calculator throws.

var budgetPath = Path.Combine(AppContext.BaseDirectory, "budget.json");
var budget = JsonSerializer.Deserialize<Budget>(File.ReadAllText(budgetPath), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
             ?? throw new InvalidOperationException("budget.json could not be read");

var consignments = Generate(budget);
var calculator = new LandedCostCalculator();

Console.WriteLine($"benchmark: {budget.LineItems} line items across {consignments.Count} consignments, budget {budget.MaxMilliseconds} ms");

decimal checksum = 0;
Stopwatch stopwatch;
try
{
    // Warm up on the first consignment so JIT time is not charged to the run.
    calculator.Calculate(consignments[0]);

    stopwatch = Stopwatch.StartNew();
    foreach (var consignment in consignments)
    {
        checksum += calculator.Calculate(consignment).Total.Amount;
    }
    stopwatch.Stop();
}
catch (Exception ex)
{
    Console.WriteLine($"FAIL: calculator threw {ex.GetType().Name}: {ex.Message}");
    return 1;
}

var elapsed = stopwatch.ElapsedMilliseconds;
var pass = elapsed <= budget.MaxMilliseconds;
Console.WriteLine($"elapsed: {elapsed} ms   checksum: {checksum:0.00}");
Console.WriteLine(pass ? $"PASS: within budget ({budget.MaxMilliseconds} ms)" : $"FAIL: over budget by {elapsed - budget.MaxMilliseconds} ms");
return pass ? 0 : 1;

#pragma warning disable CA5394 // deterministic test data, not security
static List<Consignment> Generate(Budget budget)
{
    var random = new Random(20260901);
    string[] codes = ["08039010", "08043000", "08051020", "08101000", "07020000"];
    string[] origins = ["CR", "EC", "CO", "ZA", "ES"];
    var count = Math.Max(1, budget.LineItems / budget.LinesPerConsignment);
    var result = new List<Consignment>(count);

    for (var i = 0; i < count; i++)
    {
        var lines = new List<LineItem>(budget.LinesPerConsignment);
        for (var j = 0; j < budget.LinesPerConsignment; j++)
        {
            lines.Add(LineItemBuilder.ALineItem()
                .WithCommodityCode(codes[random.Next(codes.Length)])
                .WithQuantity(random.Next(1, 400))
                .WithUnitCost(Math.Round((decimal)(random.NextDouble() * 30 + 5), 2))
                .WithWeightKg(random.Next(50, 5000)));
        }

        result.Add(ConsignmentBuilder.AConsignment()
            .WithReference($"BM-{i:00000}")
            .From(origins[random.Next(origins.Length)])
            .ArrivingOn(new DateOnly(2026, 1, 1).AddDays(random.Next(0, 364)))
            .WithFreight(random.Next(200, 5000))
            .WithLines(lines.ToArray()));
    }

    return result;
}

sealed record Budget(int LineItems, int LinesPerConsignment, int MaxMilliseconds);
