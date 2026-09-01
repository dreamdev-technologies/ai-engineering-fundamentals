using Calculator;

namespace TestArchetypes;

/// <summary>A pallet of bananas unless told otherwise.</summary>
public sealed class LineItemBuilder
{
    private string _commodityCode = "08039010";
    private string _description = "Bananas, Cavendish, 18kg carton";
    private int _quantity = 100;
    private decimal _unitCost = 12.50m;
    private string _currency = "EUR";
    private decimal _weightKg = 1800m;

    public static LineItemBuilder ALineItem() => new();

    /// <summary>A builder set up as a well-known lot, for example <c>LineItemBuilder.For&lt;ACartonOfBananas&gt;()</c>.</summary>
    public static LineItemBuilder For<T>() where T : LotArchetype, new() => new T().Configure(new LineItemBuilder());

    public LineItemBuilder WithCommodityCode(string code) { _commodityCode = code; return this; }
    public LineItemBuilder WithDescription(string description) { _description = description; return this; }
    public LineItemBuilder WithQuantity(int quantity) { _quantity = quantity; return this; }
    public LineItemBuilder WithUnitCost(decimal amount) { _unitCost = amount; return this; }
    public LineItemBuilder InCurrency(string currency) { _currency = currency; return this; }
    public LineItemBuilder WithWeightKg(decimal weightKg) { _weightKg = weightKg; return this; }

    public LineItem Build() => new(_commodityCode, _description, _quantity, Money.Of(_unitCost, _currency), _weightKg);

    public static implicit operator LineItem(LineItemBuilder builder) => builder.Build();
}
