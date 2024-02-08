using Microsoft.AspNetCore.Mvc;
using OMSSample.Models;
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

    [Fact]
    public void AddToDb_Should_Add_Element()
    {
        var database = new Dictionary<string, List<int>>();
        var model = new Model<int>(database);
        var key = "key";
        var list = new List<int> { 1, 2, 3 };
        
        model.AddToDb(key, list);
        Assert.True(database.ContainsKey(key));
        Assert.Equal(list, database[key]);
    }

    [Fact]
    public void Delete_Should_Remove_Element_With_Two_Paramaters()
    {
        var key = "key";
        var list = new List<int> { 1, 2, 3 };
        var database = new Dictionary<string, List<int>> { { key, list } };
        var model = new Model<int>(database);
        
        model.Delete(key, list);
        
        Assert.False(database.ContainsKey(key));
        Assert.False(database.ContainsValue(list));
    }

    [Fact]
    public void Delete_Should_Remove_Element_with_Only_Key()
    {
        var key = "key";
        var list = new List<int> { 1, 2, 3 };
        var database = new Dictionary<string, List<int>> { { key, list } };
        var model = new Model<int>(database);

        
        model.Delete(key);

        
        Assert.False(database.ContainsKey(key));
    }
}