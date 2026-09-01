using System.Text.Json;

namespace Calculator;

/// <summary>Serialises results for the benchmark and for anyone who wants to eyeball them.</summary>
public static class LandedCostReport
{
    private static readonly JsonSerializerOptions Options = new() { WriteIndented = true };

    public static string ToJson(LandedCost landedCost) => JsonSerializer.Serialize(landedCost, Options);
}
