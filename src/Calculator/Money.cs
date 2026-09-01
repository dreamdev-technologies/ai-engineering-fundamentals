namespace Calculator;

/// <summary>An amount in a named currency. Arithmetic across currencies is a mistake; keep them apart.</summary>
public readonly record struct Money(decimal Amount, string Currency)
{
    public static Money Of(decimal amount, string currency) => new(amount, currency);

    public override string ToString() => $"{Amount:0.00} {Currency}";
}
