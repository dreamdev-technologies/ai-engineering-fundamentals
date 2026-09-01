using TestArchetypes;
using Xunit;
using static TestArchetypes.ConsignmentBuilder;
using static TestArchetypes.LineItemBuilder;

namespace Calculator.Tests;

/// <summary>
/// The same test three ways. Read them top to bottom and ask which one a product owner could read,
/// which one breaks when a field is added to LineItem, and which one an agent can write in one line.
/// </summary>
public class ArchetypeExampleTests
{
    // Without builders or archetypes: every test knows the shape of every type. Add a field to LineItem
    // and every test like this one breaks. Nothing here says what the consignment is in business terms.
    [Fact]
    public void Story_one_consignment_weighs_2400_kg_without_archetypes()
    {
        var consignment = new Consignment(
            "MF-2026-0001",
            "CR",
            new DateOnly(2026, 3, 15),
            new Money(950m, "EUR"),
            new List<LineItem>
            {
                new("08039010", "Bananas, Cavendish, 18kg carton", 100, new Money(12.50m, "EUR"), 1800m),
                new("08051020", "Oranges, navel, 15kg case", 40, new Money(9.80m, "EUR"), 600m),
            });

        Assert.Equal(2400m, consignment.Lines.Sum(l => l.WeightKg));
    }

    // With builders: the shape is hidden and the defaults are sensible, but the test still lists values
    // that are not what it is about, and two tests that mean "the story one consignment" can drift apart.
    [Fact]
    public void Story_one_consignment_weighs_2400_kg_with_builders()
    {
        Consignment consignment = AConsignment()
            .WithFreight(950m)
            .WithLines(
                ALineItem().WithQuantity(100).WithUnitCost(12.50m).WithWeightKg(1800m),
                ALineItem().WithCommodityCode("08051020").WithQuantity(40).WithUnitCost(9.80m).WithWeightKg(600m));

        Assert.Equal(2400m, consignment.Lines.Sum(l => l.WeightKg));
    }

    // With archetypes: the test says what the consignment is, in the words the story uses. The values live
    // in one place. A test that is about one thing overrides only that thing.
    [Fact]
    public void Story_one_consignment_weighs_2400_kg_with_archetypes()
    {
        Consignment consignment = For<TheStoryOneConsignment>();

        Assert.Equal(2400m, consignment.Lines.Sum(l => l.WeightKg));
    }

    [Fact]
    public void An_archetype_is_a_starting_point_and_a_test_overrides_only_what_it_is_about()
    {
        LineItem lot = LineItemBuilder.For<ACartonOfBananas>().WithQuantity(7);

        Assert.Equal(7, lot.Quantity);
        Assert.Equal(12.50m, lot.UnitCost.Amount);
    }

    [Fact]
    public void Every_archetype_describes_itself_in_business_language()
    {
        var descriptions = new LotArchetype[] { new ACartonOfBananas(), new APalletOfWinterCitrus(), new APackagingLine(), new AnOversizedLot() }
            .Select(a => a.Description)
            .Concat(new ConsignmentArchetype[] { new TheStoryOneConsignment(), new AConsignmentWhoseFreightDoesNotDivide(), new AnEmptyConsignment(), new AConsignmentAcrossTheRateChange() }
            .Select(a => a.Description));

        Assert.All(descriptions, d => Assert.False(string.IsNullOrWhiteSpace(d)));
    }
}
