namespace TestArchetypes;

/// <summary>
/// A well-known lot, named in the language the business uses, so that a test, a story, a wiki page
/// and an agent all mean the same thing by it. Consumed by <see cref="LineItemBuilder.For{T}"/>.
/// </summary>
public abstract class LotArchetype
{
    /// <summary>One sentence a business person would recognise.</summary>
    public abstract string Description { get; }

    /// <summary>Sets the builder up as this lot. Tests override only what they are about.</summary>
    public abstract LineItemBuilder Configure(LineItemBuilder builder);
}

/// <summary>A well-known consignment. Consumed by <see cref="ConsignmentBuilder.For{T}"/>.</summary>
public abstract class ConsignmentArchetype
{
    public abstract string Description { get; }

    public abstract ConsignmentBuilder Configure(ConsignmentBuilder builder);
}
