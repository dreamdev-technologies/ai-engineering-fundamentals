using Legacy;
using Xunit;

namespace Legacy.Tests;

/// <summary>Proves the wiring works. Activity 07 adds the characterisation tests alongside this file.</summary>
public class ScaffoldTests
{
    [Fact]
    public void The_legacy_service_can_be_constructed()
    {
        Assert.NotNull(new HarbourPricingService());
    }
}
