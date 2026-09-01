namespace TestArchetypes;

/// <summary>The worked example in story 1: bananas and citrus from Costa Rica, freight 950 EUR, landed cost 2710.84.</summary>
public sealed class TheStoryOneConsignment : ConsignmentArchetype
{
    public override string Description => "A carton of bananas and a pallet of winter citrus from Costa Rica, arriving 15 March 2026, freight 950.00 EUR. Story 1 works this one through to the cent.";

    public override ConsignmentBuilder Configure(ConsignmentBuilder b) => b
        .WithReference("MF-2026-0001")
        .From(WellKnown.Origins.CostaRica)
        .ArrivingOn(2026, 3, 15)
        .InCurrency(WellKnown.Currencies.Euro)
        .WithFreight(950m)
        .WithLines(LineItemBuilder.For<ACartonOfBananas>(), LineItemBuilder.For<APalletOfWinterCitrus>());
}

/// <summary>Three equal lots and freight that will not divide by three. Story 2's reconciliation case.</summary>
public sealed class AConsignmentWhoseFreightDoesNotDivide : ConsignmentArchetype
{
    public override string Description => "Three lots of ten cartons of bananas at 10.00, 1000 kg each, freight 100.00 EUR. The shares round to 33.33 and a cent goes missing.";

    public override ConsignmentBuilder Configure(ConsignmentBuilder b)
    {
        LineItemBuilder Lot() => LineItemBuilder.For<ACartonOfBananas>().WithQuantity(10).WithUnitCost(10.00m).WithWeightKg(1000m);
        return b
            .WithReference("MF-2026-0002")
            .From(WellKnown.Origins.CostaRica)
            .ArrivingOn(2026, 3, 15)
            .WithFreight(100m)
            .WithLines(Lot(), Lot(), Lot());
    }
}

/// <summary>Booked, but with no lots on it yet.</summary>
public sealed class AnEmptyConsignment : ConsignmentArchetype
{
    public override string Description => "A consignment with a reference and freight booked but no lots. What is its landed cost?";

    public override ConsignmentBuilder Configure(ConsignmentBuilder b) => b
        .WithReference("MF-2026-0003")
        .WithFreight(250m)
        .WithNoLines();
}

/// <summary>Arrives in June and is quoted in July, across the change of exchange rates. Story 3's date case.</summary>
public sealed class AConsignmentAcrossTheRateChange : ConsignmentArchetype
{
    public override string Description => "Story 1's two lots arriving on 25 June 2026, six days before the exchange rates change on 1 July. Which day's rate applies is the question.";

    public override ConsignmentBuilder Configure(ConsignmentBuilder b) => new TheStoryOneConsignment().Configure(b)
        .WithReference("MF-2026-0004")
        .ArrivingOn(WellKnown.Dates.SixDaysBeforeTheRateChange);
}
