using Xunit;
namespace OMSSample.Tests.Unit_Tests;

public class Tests
{
    [Fact]
    public void OrderAmount_SetValidValue_ShouldSetOrderAmout()
    {
        var sample = new OMSSample();
        sample.orderAmount = 10;
        
        Assert.Equal(10u, sample.orderAmount);
    }

    [Fact]
    public void OrderAmount_SetInvalidValue_ShouldThrowArgumentException()
    {
        var sample = new OMSSample();

        Assert.Throws<ArgumentException>(() => sample.orderAmount = 0);
    }

    [Fact]
    public void Price_SetAndGet_ShouldWorkCorrectly()
    {
        var sample = new OMSSample();
        decimal expectedPrice = 10.5m;

        sample.price = expectedPrice;
        
        Assert.Equal(expectedPrice, sample.price);
    }

    [Fact]
    public void OrderSymbol_SetAndGet_ShouldWorkCorrectly()
    {
        var sample = new OMSSample();
        string expectedSymbol = "Teste";

        sample.orderSymbol = expectedSymbol;
        
        Assert.Equal(expectedSymbol, sample.orderSymbol);
    }
}