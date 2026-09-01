namespace Calculator;

/// <summary>One lot on a consignment: what it is, how much of it, what it cost and what it weighs.</summary>
/// <param name="CommodityCode">Customs commodity code. The first four digits select the duty rate.</param>
/// <param name="Description">Human-readable description of the produce.</param>
/// <param name="Quantity">Number of cases.</param>
/// <param name="UnitCost">Cost per case from the grower, in the consignment currency.</param>
/// <param name="WeightKg">Gross weight of the whole lot, used to share out freight.</param>
public sealed record LineItem(
    string CommodityCode,
    string Description,
    int Quantity,
    Money UnitCost,
    decimal WeightKg);
