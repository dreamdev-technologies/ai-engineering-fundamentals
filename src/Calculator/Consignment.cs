namespace Calculator;

/// <summary>A shipment of lots from one origin, arriving on one date, with one freight charge to share out.</summary>
public sealed record Consignment(
    string Reference,
    string OriginCountry,
    DateOnly ArrivalDate,
    Money Freight,
    IReadOnlyList<LineItem> Lines);
