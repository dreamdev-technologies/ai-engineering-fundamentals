using Calculator;

namespace TestArchetypes;

/// <summary>A single-line banana consignment from Costa Rica, arriving in March, unless told otherwise.</summary>
public sealed class ConsignmentBuilder
{
    private string _reference = "MF-2026-0001";
    private string _origin = "CR";
    private DateOnly _arrival = new(2026, 3, 15);
    private decimal _freight = 950m;
    private string _currency = "EUR";
    private readonly List<LineItem> _lines = [];
    private bool _noLines;

    public static ConsignmentBuilder AConsignment() => new();

    /// <summary>A builder set up as a well-known consignment, for example <c>ConsignmentBuilder.For&lt;TheStoryOneConsignment&gt;()</c>.</summary>
    public static ConsignmentBuilder For<T>() where T : ConsignmentArchetype, new() => new T().Configure(new ConsignmentBuilder());

    public ConsignmentBuilder WithReference(string reference) { _reference = reference; return this; }
    public ConsignmentBuilder From(string originCountry) { _origin = originCountry; return this; }
    public ConsignmentBuilder ArrivingOn(DateOnly date) { _arrival = date; return this; }
    public ConsignmentBuilder ArrivingOn(int year, int month, int day) => ArrivingOn(new DateOnly(year, month, day));
    public ConsignmentBuilder WithFreight(decimal amount) { _freight = amount; return this; }
    public ConsignmentBuilder InCurrency(string currency) { _currency = currency; return this; }

    /// <summary>Replaces the default line with the given ones.</summary>
    public ConsignmentBuilder WithLines(params LineItem[] lines) { _lines.Clear(); _lines.AddRange(lines); return this; }

    public ConsignmentBuilder WithLine(LineItem line) { _lines.Add(line); return this; }

    /// <summary>A consignment with no lots at all, rather than the default single line.</summary>
    public ConsignmentBuilder WithNoLines() { _lines.Clear(); _noLines = true; return this; }

    public Consignment Build()
    {
        List<LineItem> lines = _noLines
            ? []
            : _lines.Count > 0
                ? _lines.ToList()
                : [LineItemBuilder.ALineItem().InCurrency(_currency).Build()];
        return new Consignment(_reference, _origin, _arrival, Money.Of(_freight, _currency), lines);
    }

    public static implicit operator Consignment(ConsignmentBuilder builder) => builder.Build();
}
