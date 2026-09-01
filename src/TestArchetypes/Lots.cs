namespace TestArchetypes;

/// <summary>The commonest line in the business. Lot A in story 1.</summary>
public sealed class ACartonOfBananas : LotArchetype
{
    public override string Description => "One hundred 18kg cartons of Cavendish bananas at 12.50, 1800 kg. The commonest lot in the business.";

    public override LineItemBuilder Configure(LineItemBuilder b) => b
        .WithCommodityCode("08039010")
        .WithDescription("Bananas, Cavendish, 18kg carton")
        .WithQuantity(100)
        .WithUnitCost(12.50m)
        .WithWeightKg(1800m);
}

/// <summary>A citrus lot, whose duty rate depends on the season. Lot B in story 1.</summary>
public sealed class APalletOfWinterCitrus : LotArchetype
{
    public override string Description => "Forty cases of oranges at 9.80, 600 kg. Citrus carries a higher duty rate between November and April.";

    public override LineItemBuilder Configure(LineItemBuilder b) => b
        .WithCommodityCode("08051020")
        .WithDescription("Oranges, navel, 15kg case")
        .WithQuantity(40)
        .WithUnitCost(9.80m)
        .WithWeightKg(600m);
}

/// <summary>Packaging and service lines are booked as lots but weigh nothing, so they attract no freight.</summary>
public sealed class APackagingLine : LotArchetype
{
    public override string Description => "Two hundred banana boxes at 0.40, weight zero. A lot with no weight, of the kind story 2 has to handle.";

    public override LineItemBuilder Configure(LineItemBuilder b) => b
        .WithCommodityCode("48191000")
        .WithDescription("Cartons, corrugated, flat")
        .WithQuantity(200)
        .WithUnitCost(0.40m)
        .WithWeightKg(0m);
}

/// <summary>More cases than the legacy line record can hold. Logistics split these by hand; see the wiki gotchas.</summary>
public sealed class AnOversizedLot : LotArchetype
{
    public override string Description => "One thousand cartons of bananas on a single line. More than 999, which the legacy system cannot represent.";

    public override LineItemBuilder Configure(LineItemBuilder b) => new ACartonOfBananas().Configure(b)
        .WithQuantity(1000)
        .WithWeightKg(18000m);
}
