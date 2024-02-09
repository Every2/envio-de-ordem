using Microsoft.AspNetCore.Mvc;
using OMSSample.Controllers;
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
        string? expectedSymbol = "Teste";

        sample.orderSymbol = expectedSymbol;
        
        Assert.Equal(expectedSymbol, sample.orderSymbol);
    }

    [Fact]
    public void AddToDb_Should_Add_Element()
    {
        var model = new Model();
        var key = "key";
        var sampleList = new List<OMSSample>
        {
            new OMSSample { orderAmount = 1, orderSymbol = "ABC", price = 10.5m },
            new OMSSample { orderAmount = 2, orderSymbol = "DEF", price = 20.7m }
        };

        
        model.AddToDb(key, sampleList);
        
        Assert.True(model.GetFromDb(key) != null);
        Assert.Equal(sampleList, model.GetFromDb(key));
    }
    
    [Fact]
    public void Delete_Should_Remove_Element()
    {
        var model = new Model();
        var key = "key";
        var sampleList = new List<OMSSample>
        {
            new OMSSample { orderAmount = 1, orderSymbol = "ABC", price = 10.5m },
            new OMSSample { orderAmount = 2, orderSymbol = "DEF", price = 20.7m }
        };
        var database = new Dictionary<string, List<OMSSample>> { { key, sampleList } };

        model.Delete(key);
        Assert.False(model.ContainsKey(key));
    }

    [Fact]
    public void Post_ReturnsStatusCode200()
    {
        var controller = new OMSSampleController();
        var result = controller.SendNewOrder("teste", 1, 10.5m);
        var actionResult = Assert.IsType<ActionResult<OMSSample>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.Equal(200, okResult.StatusCode);
    }
}